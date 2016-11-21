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
    /// <author>Mark - mark at arangodb.com</author>
    public class ConnectionSync : Connection
    {
        private static readonly org.slf4j.Logger LOGGER = org.slf4j.LoggerFactory.getLogger
            (typeof(ConnectionSync
        ));

        public class Builder
        {
            private string host;

            private int port;

            private int timeout;

            private bool useSsl;

            private javax.net.ssl.SSLContext sslContext;

            public Builder()
                : base()
            {
            }

            public virtual ConnectionSync.Builder host(string
                 host)
            {
                this.host = host;
                return this;
            }

            public virtual ConnectionSync.Builder port(int
                 port)
            {
                this.port = port;
                return this;
            }

            public virtual ConnectionSync.Builder timeout
                (int timeout)
            {
                this.timeout = timeout;
                return this;
            }

            public virtual ConnectionSync.Builder useSsl(
                bool useSsl)
            {
                this.useSsl = useSsl;
                return this;
            }

            public virtual ConnectionSync.Builder sslContext
                (javax.net.ssl.SSLContext sslContext)
            {
                this.sslContext = sslContext;
                return this;
            }

            public virtual ConnectionSync build()
            {
                return new ConnectionSync(host, port, timeout
                    , useSsl, sslContext);
            }
        }

        private ConnectionSync(string host, int port, int timeout, bool useSsl, javax.net.ssl.SSLContext
             sslContext)
            : base(host, port, timeout, useSsl, sslContext)
        {
        }

        /// <exception cref="ArangoDBException"/>
        public virtual Message write(Message
             message, System.Collections.Generic.ICollection<Chunk
            > chunks)
        {
            lock (this)
            {
                base.writeIntern(message, chunks);
                byte[] chunkBuffer = null;
                int off = 0;
                while (chunkBuffer == null || off < chunkBuffer.Length)
                {
                    if (!this.isOpen())
                    {
                        this.close();
                        throw new ArangoDBException(new System.IO.IOException("The socket is closed."
                            ));
                    }
                    try
                    {
                        Chunk chunk = this.readChunk();
                        int contentLength = chunk.getContentLength();
                        if (chunkBuffer == null)
                        {
                            if (!chunk.isFirstChunk())
                            {
                                throw new ArangoDBException("Wrong Chunk recieved! Expected first Chunk."
                                    );
                            }
                            int length = (int)(chunk.getMessageLength() > 0 ? chunk.getMessageLength() : contentLength
                                );
                            chunkBuffer = new byte[length];
                        }
                        this.readBytesIntoBuffer(chunkBuffer, off, contentLength);
                        off += contentLength;
                    }
                    catch (System.Exception e)
                    {
                        this.close();
                        throw new ArangoDBException(e);
                    }
                }
                Message responseMessage = new Message
                    (message.getId(), chunkBuffer);
                if (LOGGER.isDebugEnabled())
                {
                    LOGGER.debug(string.format("Received Message (id=%s, head=%s, body=%s)", responseMessage
                        .getId(), responseMessage.getHead(), responseMessage.getBody() != null ? responseMessage
                        .getBody() : "{}"));
                }
                return responseMessage;
            }
        }
    }
}