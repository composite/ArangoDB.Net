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
    using global::ArangoDB.Entity;
    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class InternalArangoEdgeCollection<E, R, C> : ArangoExecuteable
        <E, R, C>
        where E : ArangoExecutor<R, C>
        where C : Connection
    {
        private readonly string db;

        private readonly string graph;

        private readonly string name;

        public InternalArangoEdgeCollection(E executor, string db, string graph, string name
            )
            : base(executor)
        {
            this.db = db;
            this.graph = graph;
            this.name = name;
        }

        public virtual string name()
        {
            return name;
        }

        protected internal virtual string createDocumentHandle(string key)
        {
            this.executor.validateDocumentKey(key);
            return this.executor.createPath(name, key);
        }

        protected internal virtual Request insertEdgeRequest<T>
            (T value, EdgeCreateOptions options)
        {
            Request request = new Request
                (this.db, RequestType.POST, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, this.graph, ArangoDBConstants.EDGE, name));
            EdgeCreateOptions @params = options != null ? options : new EdgeCreateOptions
                                                                        ();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.setBody(this.executor.Serialize(value));
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <EdgeEntity> insertEdgeResponseDeserializer<T>(T value)
        {
            return new _ResponseDeserializer_79(this, value);
        }

        private sealed class _ResponseDeserializer_79 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <EdgeEntity>
        {
            public _ResponseDeserializer_79(InternalArangoEdgeCollection<E, R, C> _enclosing,
                T value)
            {
                this._enclosing = _enclosing;
                this.value = value;
            }

            /// <exception cref="VPackException"/>
            public EdgeEntity deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody().get(ArangoDBConstants
                    .EDGE);
                EdgeEntity doc = this._enclosing.executor.Deserialize(body, 
                    typeof(EdgeEntity));
                System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type, string
                    > values = new System.Collections.Generic.Dictionary<com.arangodb.entity.DocumentFieldAttribute.Type
                    , string>();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.ID] = doc.getId();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.KEY] = doc.getKey();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.REV] = doc.getRev();
                this._enclosing.executor.documentCache().setValues(this.value, values);
                return doc;
            }

            private readonly InternalArangoEdgeCollection<E, R, C> _enclosing;

            private readonly T value;
        }

        protected internal virtual Request getEdgeRequest(string
             key, DocumentReadOptions options)
        {
            Request request = new Request
                (this.db, RequestType.GET, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, this.graph, ArangoDBConstants.EDGE, this.createDocumentHandle
                (key)));
            DocumentReadOptions @params = options != null ? options : new
                                                                          DocumentReadOptions();
            request.putHeaderParam(ArangoDBConstants.IF_NONE_MATCH, @params
                .getIfNoneMatch());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <T> getEdgeResponseDeserializer<T>()
        {
            System.Type type = typeof(T);
            return new _ResponseDeserializer_104(this, type);
        }

        private sealed class _ResponseDeserializer_104 : ArangoExecutor<,>.IResponseDeserializer
            <T>
        {
            public _ResponseDeserializer_104(InternalArangoEdgeCollection<E, R, C> _enclosing
                , java.lang.Class type)
            {
                this._enclosing = _enclosing;
                this.type = type;
            }

            /// <exception cref="VPackException"/>
            public T Deserialize(Response response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .EDGE), this.type);
            }

            private readonly InternalArangoEdgeCollection<E, R, C> _enclosing;

            private readonly java.lang.Class type;
        }

        protected internal virtual Request replaceEdgeRequest<T
            >(string key, T value, EdgeReplaceOptions options)
        {
            Request request = new Request
                (this.db, RequestType.PUT, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, this.graph, ArangoDBConstants.EDGE, this.createDocumentHandle
                (key)));
            EdgeReplaceOptions @params = options != null ? options : new
                                                                         EdgeReplaceOptions();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            request.setBody(this.executor.Serialize(value));
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <EdgeUpdateEntity> replaceEdgeResponseDeserializer<T>(T value
            )
        {
            return new _ResponseDeserializer_123(this, value);
        }

        private sealed class _ResponseDeserializer_123 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <EdgeUpdateEntity>
        {
            public _ResponseDeserializer_123(InternalArangoEdgeCollection<E, R, C> _enclosing
                , T value)
            {
                this._enclosing = _enclosing;
                this.value = value;
            }

            /// <exception cref="VPackException"/>
            public EdgeUpdateEntity deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody().get(ArangoDBConstants
                    .EDGE);
                EdgeUpdateEntity doc = this._enclosing.executor.Deserialize(body
                    , typeof(EdgeUpdateEntity));
                System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type, string
                    > values = new System.Collections.Generic.Dictionary<com.arangodb.entity.DocumentFieldAttribute.Type
                    , string>();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.REV] = doc.getRev();
                this._enclosing.executor.documentCache().setValues(this.value, values);
                return doc;
            }

            private readonly InternalArangoEdgeCollection<E, R, C> _enclosing;

            private readonly T value;
        }

        protected internal virtual Request updateEdgeRequest<T>
            (string key, T value, EdgeUpdateOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .PATCH, this.executor.createPath(ArangoDBConstants.PATH_API_GHARIAL
                , this.graph, ArangoDBConstants.EDGE, this.createDocumentHandle(key
                )));
            EdgeUpdateOptions @params = options != null ? options : new EdgeUpdateOptions
                                                                        ();
            request.putQueryParam(ArangoDBConstants.KEEP_NULL, @params
                .getKeepNull());
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            request.setBody(this.executor.Serialize(value, true));
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <EdgeUpdateEntity> updateEdgeResponseDeserializer<T>(T value
            )
        {
            return new _ResponseDeserializer_149(this);
        }

        private sealed class _ResponseDeserializer_149 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <EdgeUpdateEntity>
        {
            public _ResponseDeserializer_149(InternalArangoEdgeCollection<E, R, C> _enclosing
                )
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public EdgeUpdateEntity deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody().get(ArangoDBConstants
                    .EDGE);
                return this._enclosing.executor.Deserialize(body, 
                    typeof(EdgeUpdateEntity));
            }

            private readonly InternalArangoEdgeCollection<E, R, C> _enclosing;
        }

        protected internal virtual Request deleteEdgeRequest(string
             key, EdgeDeleteOptions options)
        {
            Request request = new Request
                (this.db, RequestType.DELETE, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, this.graph, ArangoDBConstants.EDGE, this.createDocumentHandle
                (key)));
            EdgeDeleteOptions @params = options != null ? options : new EdgeDeleteOptions
                                                                        ();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            return request;
        }
    }
}