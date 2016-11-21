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

namespace ArangoDB.Internal
{
    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class ArangoExecutorSync : ArangoExecutor<Response
        , ConnectionSync>
    {
        public ArangoExecutorSync(Communication<Response
            , ConnectionSync> communication, VPack
             vpacker, VPack vpackerNull, VPackParser
             vpackParser, DocumentCache documentCache, CollectionCache
             collectionCache)
            : base(communication, vpacker, vpackerNull, vpackParser, documentCache, collectionCache
                )
        {
        }

        /// <exception cref="ArangoDBException"/>
        public virtual T execute<T>(Request request, global::System.Type
             type)
        {
            return execute(request, new _ResponseDeserializer_47(this, type));
        }

        private sealed class _ResponseDeserializer_47 : ArangoExecutor<,>.IResponseDeserializer
            <T>
        {
            public _ResponseDeserializer_47(ArangoExecutorSync _enclosing, global::System.Type
                 type)
            {
                this._enclosing = _enclosing;
                this.type = type;
            }

            /// <exception cref="VPackException"/>
            public T Deserialize(Response response)
            {
                return this._enclosing.CreateResult(this.type, response);
            }

            private readonly ArangoExecutorSync _enclosing;

            private readonly global::System.Type type;
        }

        /// <exception cref="ArangoDBException"/>
        public virtual T execute<T>(Request request, com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <T> responseDeserializer)
        {
            try
            {
                Response response = this.communication().execute(request);
                return responseDeserializer.deserialize(response);
            }
            catch (VPackException e)
            {
                throw new ArangoDBException(e);
            }
        }
    }
}