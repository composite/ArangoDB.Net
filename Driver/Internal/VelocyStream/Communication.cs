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
    using global::ArangoDB.Entity;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public abstract class Communication<R, C>
        where C : Connection
    {
        private const int ERROR_STATUS = 300;

        private static readonly org.slf4j.Logger LOGGER = org.slf4j.LoggerFactory.getLogger
            (typeof(com.arangodb.@internal.velocystream.Communication
        ));

        protected internal static readonly java.util.concurrent.atomic.AtomicLong mId = new
            java.util.concurrent.atomic.AtomicLong(0L);

        protected internal readonly VPack vpack;

        protected internal readonly C connection;

        protected internal readonly CollectionCache collectionCache;

        protected internal readonly string user;

        protected internal readonly string password;

        protected internal readonly int chunksize;

        protected internal Communication(string host, int port, int timeout, string user,
            string password, bool useSsl, javax.net.ssl.SSLContext sslContext, VPack
             vpack, CollectionCache collectionCache, int chunksize, C
             connection)
        {
            this.user = user;
            this.password = password;
            this.vpack = vpack;
            this.collectionCache = collectionCache;
            this.connection = connection;
            this.chunksize = chunksize != null ? chunksize : ArangoDBConstants
                .CHUNK_DEFAULT_CONTENT_SIZE;
        }

        protected internal virtual void connect(Connection
             connection)
        {
            if (!connection.isOpen())
            {
                try
                {
                    connection.open();
                    if (this.user != null)
                    {
                        this.authenticate();
                    }
                }
                catch (System.IO.IOException e)
                {
                    LOGGER.error(e.Message, e);
                    throw new ArangoDBException(e);
                }
            }
        }

        protected internal abstract void authenticate();

        public virtual void disconnect()
        {
            this.disconnect(this.connection);
        }

        public virtual void disconnect(Connection connection
            )
        {
            connection.close();
        }

        /// <exception cref="ArangoDBException"/>
        public abstract R execute(Request request);

        /// <exception cref="ArangoDBException"/>
        protected internal virtual void checkError(Response response
            )
        {
            try
            {
                if (response.getResponseCode() >= ERROR_STATUS)
                {
                    if (response.getBody() != null)
                    {
                        throw new ArangoDBException(this.createErrorMessage(response));
                    }
                    else
                    {
                        throw new ArangoDBException(string.format("Response Code: %s", response
                            .getResponseCode()));
                    }
                }
            }
            catch (VPackParserException e)
            {
                throw new ArangoDBException(e);
            }
        }

        /// <exception cref="VPackParserException"/>
        private string createErrorMessage(Response response)
        {
            string errorMessage;
            ErrorEntity errorEntity = this.vpack.deserialize(response.getBody(
                ), typeof(ErrorEntity));
            errorMessage = string.format("Response: %s, Error: %s - %s", errorEntity.getCode(
                ), errorEntity.getErrorNum(), errorEntity.getErrorMessage());
            return errorMessage;
        }

        /// <exception cref="VPackParserException"/>
        protected internal virtual Response createResponse(Message
             messsage)
        {
            Response response = this.vpack.deserialize(messsage.getHead(
                ), typeof(Response));
            if (messsage.getBody() != null)
            {
                response.setBody(messsage.getBody());
            }
            return response;
        }

        /// <exception cref="VPackParserException"/>
        protected internal virtual Message createMessage
            (Request request)
        {
            long id = mId.incrementAndGet();
            return new Message(id, this.vpack.serialize(request
                ), request.getBody());
        }

        /// <exception cref="System.IO.IOException"/>
        protected internal virtual System.Collections.Generic.ICollection<Chunk
            > buildChunks(Message message)
        {
            System.Collections.Generic.ICollection<Chunk>
                 chunks = new System.Collections.Generic.List<Chunk
                >();
            VPackSlice head = message.getHead();
            int size = head.getByteSize();
            VPackSlice body = message.getBody();
            if (body != null)
            {
                size += body.getByteSize();
            }
            int n = size / this.chunksize;
            int numberOfChunks = size % this.chunksize != 0 ? n + 1 : n;
            int off = 0;
            for (int i = 0; size > 0; i++)
            {
                int len = System.Math.min(this.chunksize, size);
                long messageLength = i == 0 && numberOfChunks > 1 ? size : -1L;
                Chunk chunk = new Chunk
                    (message.getId(), i, numberOfChunks, messageLength, off, len);
                size -= len;
                off += len;
                chunks.add(chunk);
            }
            return chunks;
        }
    }
}