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
    using global::ArangoDB.Util;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocystream;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <author>Mark - mark at arangodb.com</author>
	public abstract class ArangoExecutor<R, C>
        where C : Connection
    {
        public interface IResponseDeserializer<T>
        {
            /// <exception cref="VPackException"/>
            T Deserialize(Response response);
        }

        private const string REGEX_DOCUMENT_KEY = "[^/]+";

        private const string REGEX_DOCUMENT_ID = "[^/]+/[^/]+";

        protected internal ArangoExecutor(Communication
            <R, C> communication, VPack vpacker, VPack
             vpackerNull, VPackParser vpackParser, DocumentCache
             documentCache, CollectionCache collectionCache)
            : base()
        {
            this.Communication = communication;
            this.DocumentCache = documentCache;
            this.CollectionCache = collectionCache;
            Util = new ArangoUtil(vpacker, vpackerNull, vpackParser);
        }

        public virtual Communication<R, C> Communication { get; }

        public virtual DocumentCache DocumentCache { get; }

        protected internal virtual CollectionCache CollectionCache { get; }

        protected internal virtual ArangoUtil Util { get; }

        protected internal virtual string CreatePath(params string[] @params)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < @params.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append("/");
                }
                sb.Append(@params[i]);
            }
            return sb.ToString();
        }

        /// <exception cref="ArangoDBException"/>
        public virtual void ValidateDocumentKey(string key)
        {
            this.ValidateName("document key", REGEX_DOCUMENT_KEY, key);
        }

        /// <exception cref="ArangoDBException"/>
        public virtual void ValidateDocumentId(string id)
        {
            this.ValidateName("document id", REGEX_DOCUMENT_ID, id);
        }

        /// <exception cref="ArangoDBException"/>
        protected internal virtual void ValidateName(string type, string regex, string name)
        {
            if (!Regex.IsMatch(name, regex))
            {
                throw new ArangoDBException(string.Format("{0} {1} is not valid.", type, name));
            }
        }

        protected internal virtual T CreateResult<T>(Response response)
        {
            return response.getBody() != null ? this.Deserialize<T>(response.getBody()) : default(T);
        }

        /// <exception cref="ArangoDBException"/>
        protected internal virtual T Deserialize<T>(VPackSlice vpack)
        {
            return Util.Deserialize<T>(vpack);
        }

        /// <exception cref="ArangoDBException"/>
        protected internal virtual VPackSlice Serialize(object entity
            )
        {
            return Util.Serialize(entity);
        }

        /// <exception cref="ArangoDBException"/>
        protected internal virtual VPackSlice Serialize(object entity, bool serializeNullValues)
        {
            return Util.Serialize(entity, serializeNullValues);
        }

        /// <exception cref="ArangoDBException"/>
        protected internal virtual VPackSlice Serialize(object entity, global::System.Type type)
        {
            return Util.Serialize(entity, type);
        }

        /// <exception cref="ArangoDBException"/>
        protected internal virtual VPackSlice Serialize(object entity, global::System.Type type, bool serializeNullValues)
        {
            return Util.Serialize(entity, type, serializeNullValues);
        }

        /// <exception cref="ArangoDBException"/>
        protected internal virtual VPackSlice Serialize(object entity, global::System.Type type, System.Collections.Generic.IDictionary<string, object> additionalFields)
        {
            return Util.Serialize(entity, type, additionalFields);
        }
    }
}