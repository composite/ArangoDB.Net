/*
* DISCLAIMER
*
* Copyright 2016 ArangoDB GmbH, Cologne, Germany
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
* Copyright holder is ArangoDB GmbH, Cologne, Germany
*/

namespace ArangoDB.Internal.VelocyStream
{
    using global::ArangoDB.Velocypack;

    /// <author>Mark - mark at arangodb.com</author>
	public abstract class Connection
    {
        private static readonly org.slf4j.Logger LOGGER = org.slf4j.LoggerFactory.getLogger
            (typeof(Connection
        ));

        private static readonly byte[] PROTOCOL_HEADER = Sharpen.Runtime.getBytesForString
            ("VST/1.0\r\n\r\n");

        private readonly string host;

        private readonly int port;

        private readonly int timeout;

        private readonly bool useSsl;

        private readonly javax.net.ssl.SSLContext sslContext;

        private java.net.Socket socket;

        private java.io.OutputStream outputStream;

        private java.io.InputStream inputStream;

        protected internal Connection(string host, int port, int timeout, bool useSsl, javax.net.ssl.SSLContext
             sslContext)
            : base()
        {
            this.host = host;
            this.port = port;
            this.timeout = timeout;
            this.useSsl = useSsl;
            this.sslContext = sslContext;
        }

        public virtual bool isOpen()
        {
            lock (this)
            {
                return this.socket != null && this.socket.isConnected() && !this.socket.isClosed();
            }
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void open()
        {
            lock (this)
            {
                if (this.isOpen())
                {
                    return;
                }
                if (this.useSsl != null && this.useSsl)
                {
                    if (this.sslContext != null)
                    {
                        this.socket = this.sslContext.getSocketFactory().createSocket();
                    }
                    else
                    {
                        this.socket = javax.net.ssl.SSLSocketFactory.getDefault().createSocket();
                    }
                }
                else
                {
                    this.socket = javax.net.SocketFactory.getDefault().createSocket();
                }
                string host = this.host != null ? this.host : ArangoDBConstants
                    .DEFAULT_HOST;
                int port = this.port != null ? this.port : ArangoDBConstants
                    .DEFAULT_PORT;
                if (LOGGER.isDebugEnabled())
                {
                    LOGGER.debug(string.format("Open connection to addr=%s,port=%s", host, port));
                }
                this.socket.connect(new java.net.InetSocketAddress(host, port), this.timeout != null ? this.timeout
                     : ArangoDBConstants.DEFAULT_TIMEOUT);
                this.socket.setKeepAlive(true);
                this.socket.setTcpNoDelay(true);
                if (LOGGER.isDebugEnabled())
                {
                    LOGGER.debug(string.format("Connected to %s", this.socket));
                }
                this.outputStream = new java.io.BufferedOutputStream(this.socket.getOutputStream());
                this.inputStream = this.socket.getInputStream();
                if (this.useSsl != null && this.useSsl)
                {
                    if (LOGGER.isDebugEnabled())
                    {
                        LOGGER.debug(string.format("Start Handshake on %s", this.socket));
                    }
                    ((javax.net.ssl.SSLSocket)this.socket).startHandshake();
                }
                this.sendProtocolHeader();
            }
        }

        public virtual void close()
        {
            lock (this)
            {
                if (LOGGER.isDebugEnabled())
                {
                    LOGGER.debug(string.format("Close connection %s", this.socket));
                }
                if (this.socket != null)
                {
                    try
                    {
                        this.socket.close();
                    }
                    catch (System.IO.IOException e)
                    {
                        throw new ArangoDBException(e);
                    }
                }
            }
        }

        /// <exception cref="System.IO.IOException"/>
        private void sendProtocolHeader()
        {
            if (LOGGER.isDebugEnabled())
            {
                LOGGER.debug(string.format("Send velocystream protocol header to %s", this.socket));
            }
            this.outputStream.write(PROTOCOL_HEADER);
            this.outputStream.flush();
        }

        protected internal virtual void writeIntern(Message
             message, System.Collections.Generic.ICollection<Chunk
            > chunks)
        {
            lock (this)
            {
                foreach (Chunk chunk in chunks)
                {
                    try
                    {
                        if (LOGGER.isDebugEnabled())
                        {
                            LOGGER.debug(string.format("Send chunk %s:%s from message %s", chunk.getChunk(),
                                chunk.isFirstChunk() ? 1 : 0, chunk.getMessageId()));
                        }
                        this.writeChunkHead(chunk);
                        int contentOffset = chunk.getContentOffset();
                        int contentLength = chunk.getContentLength();
                        VPackSlice head = message.getHead();
                        int headLength = head.getByteSize();
                        int written = 0;
                        if (contentOffset < headLength)
                        {
                            written = System.Math.min(contentLength, headLength - contentOffset);
                            this.outputStream.write(head.getBuffer(), contentOffset, written);
                        }
                        if (written < contentLength)
                        {
                            VPackSlice body = message.getBody();
                            this.outputStream.write(body.getBuffer(), contentOffset + written - headLength, contentLength
                                 - written);
                        }
                        this.outputStream.flush();
                    }
                    catch (System.IO.IOException e)
                    {
                        throw new System.Exception(e);
                    }
                }
            }
        }

        /// <exception cref="System.IO.IOException"/>
        private void writeChunkHead(Chunk chunk)
        {
            long messageLength = chunk.getMessageLength();
            int headLength = messageLength > -1L ? ArangoDBConstants.CHUNK_MAX_HEADER_SIZE
                 : ArangoDBConstants.CHUNK_MIN_HEADER_SIZE;
            int length = chunk.getContentLength() + headLength;
            java.nio.ByteBuffer buffer = java.nio.ByteBuffer.allocate(headLength).order(java.nio.ByteOrder
                .LITTLE_ENDIAN);
            buffer.putInt(length);
            buffer.putInt(chunk.getChunkX());
            buffer.putLong(chunk.getMessageId());
            if (messageLength > -1L)
            {
                buffer.putLong(messageLength);
            }
            this.outputStream.write((byte[])buffer.array());
        }

        /// <exception cref="System.IO.IOException"/>
        protected internal virtual Chunk readChunk()
        {
            java.nio.ByteBuffer chunkHeadBuffer = this.readBytes(ArangoDBConstants
                .CHUNK_MIN_HEADER_SIZE);
            int length = chunkHeadBuffer.getInt();
            int chunkX = chunkHeadBuffer.getInt();
            long messageId = chunkHeadBuffer.getLong();
            long messageLength;
            int contentLength;
            if ((1 == (chunkX & unchecked((int)0x1))) && (chunkX >> 1 > 1))
            {
                messageLength = this.readBytes(ArangoDBConstants.LONG_BYTES).getLong
                    ();
                contentLength = length - ArangoDBConstants.CHUNK_MAX_HEADER_SIZE;
            }
            else
            {
                messageLength = -1L;
                contentLength = length - ArangoDBConstants.CHUNK_MIN_HEADER_SIZE;
            }
            Chunk chunk = new Chunk
                (messageId, chunkX, messageLength, 0, contentLength);
            if (LOGGER.isDebugEnabled())
            {
                LOGGER.debug(string.format("Received chunk %s:%s from message %s", chunk.getChunk
                    (), chunk.isFirstChunk() ? 1 : 0, chunk.getMessageId()));
            }
            return chunk;
        }

        /// <exception cref="System.IO.IOException"/>
        private java.nio.ByteBuffer readBytes(int len)
        {
            byte[] buf = new byte[len];
            this.readBytesIntoBuffer(buf, 0, len);
            return java.nio.ByteBuffer.wrap(buf).order(java.nio.ByteOrder.LITTLE_ENDIAN);
        }

        /// <exception cref="System.IO.IOException"/>
        protected internal virtual void readBytesIntoBuffer(byte[] buf, int off, int len)
        {
            for (int readed = 0; readed < len;)
            {
                int read = this.inputStream.read(buf, off + readed, len - readed);
                if (read == -1)
                {
                    throw new System.IO.IOException("Reached the end of the stream.");
                }
                else
                {
                    readed += read;
                }
            }
        }
    }
}