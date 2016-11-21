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
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class CommunicationSync : Communication
        <Response, ConnectionSync
        >
    {
        private static readonly org.slf4j.Logger LOGGER = org.slf4j.LoggerFactory.getLogger
            (typeof(CommunicationSync
        ));

        public class Builder
        {
            private string host;

            private int port;

            private int timeout;

            private string user;

            private string password;

            private bool useSsl;

            private javax.net.ssl.SSLContext sslContext;

            private int chunksize;

            public Builder()
                : base()
            {
            }

            public virtual CommunicationSync.Builder host
                (string host)
            {
                this.host = host;
                return this;
            }

            public virtual CommunicationSync.Builder port
                (int port)
            {
                this.port = port;
                return this;
            }

            public virtual CommunicationSync.Builder timeout
                (int timeout)
            {
                this.timeout = timeout;
                return this;
            }

            public virtual CommunicationSync.Builder user
                (string user)
            {
                this.user = user;
                return this;
            }

            public virtual CommunicationSync.Builder password
                (string password)
            {
                this.password = password;
                return this;
            }

            public virtual CommunicationSync.Builder useSsl
                (bool useSsl)
            {
                this.useSsl = useSsl;
                return this;
            }

            public virtual CommunicationSync.Builder sslContext
                (javax.net.ssl.SSLContext sslContext)
            {
                this.sslContext = sslContext;
                return this;
            }

            public virtual CommunicationSync.Builder chunksize
                (int chunksize)
            {
                this.chunksize = chunksize;
                return this;
            }

            public virtual Communication<Response
                , ConnectionSync> build(VPack
                 vpack, CollectionCache collectionCache)
            {
                return new CommunicationSync(host, port, timeout
                    , user, password, useSsl, sslContext, vpack, collectionCache, chunksize);
            }
        }

        protected internal CommunicationSync(string host, int port, int timeout, string user
            , string password, bool useSsl, javax.net.ssl.SSLContext sslContext, VPack
             vpack, CollectionCache collectionCache, int chunksize)
            : base(host, port, timeout, user, password, useSsl, sslContext, vpack, collectionCache
                , chunksize, new ConnectionSync.Builder().host
                (host).port(port).timeout(timeout).useSsl(useSsl).sslContext(sslContext).build()
                )
        {
        }

        /// <exception cref="ArangoDBException"/>
        public override Response execute(Request
             request)
        {
            this.connect(this.connection);
            try
            {
                Message requestMessage = this.createMessage(request
                    );
                Message responseMessage = this.send(requestMessage
                    );
                this.collectionCache.setDb(request.getDatabase());
                Response response = this.createResponse(responseMessage);
                this.checkError(response);
                return response;
            }
            catch (VPackParserException e)
            {
                throw new ArangoDBException(e);
            }
            catch (System.IO.IOException e)
            {
                throw new ArangoDBException(e);
            }
        }

        /// <exception cref="System.IO.IOException"/>
        private Message send(Message
             message)
        {
            if (LOGGER.isDebugEnabled())
            {
                LOGGER.debug(string.format("Send Message (id=%s, head=%s, body=%s)", message.getId
                    (), message.getHead(), message.getBody() != null ? message.getBody() : "{}"));
            }
            return this.connection.write(message, this.buildChunks(message));
        }

        protected internal override void authenticate()
        {
            Response response = this.execute(new AuthenticationRequest
                (this.user, this.password != null ? this.password : string.Empty, ArangoDBConstants
                .ENCRYPTION_PLAIN));
            this.checkError(response);
        }
    }
}