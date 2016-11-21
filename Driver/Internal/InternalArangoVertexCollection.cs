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
	public class InternalArangoVertexCollection<E, R, C> : ArangoExecuteable
        <E, R, C>
        where E : ArangoExecutor<R, C>
        where C : Connection
    {
        private readonly string db;

        private readonly string graph;

        private readonly string name;

        public InternalArangoVertexCollection(E executor, string db, string graph, string
             name)
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

        protected internal virtual Request dropRequest()
        {
            return new Request(this.db, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_GHARIAL
                , this.graph, ArangoDBConstants.VERTEX, name));
        }

        protected internal virtual Request insertVertexRequest<
            T>(T value, VertexCreateOptions options)
        {
            Request request = new Request
                (this.db, RequestType.POST, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, this.graph, ArangoDBConstants.VERTEX, name)
                );
            VertexCreateOptions @params = options != null ? options : new
                                                                          VertexCreateOptions();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.setBody(this.executor.Serialize(value));
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <VertexEntity> insertVertexResponseDeserializer<T>(T value)
        {
            return new _ResponseDeserializer_84(this, value);
        }

        private sealed class _ResponseDeserializer_84 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <VertexEntity>
        {
            public _ResponseDeserializer_84(InternalArangoVertexCollection<E, R, C> _enclosing
                , T value)
            {
                this._enclosing = _enclosing;
                this.value = value;
            }

            /// <exception cref="VPackException"/>
            public VertexEntity deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody().get(ArangoDBConstants
                    .VERTEX);
                VertexEntity doc = this._enclosing.executor.Deserialize(body,
                    typeof(VertexEntity));
                System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type, string
                    > values = new System.Collections.Generic.Dictionary<com.arangodb.entity.DocumentFieldAttribute.Type
                    , string>();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.ID] = doc.getId();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.KEY] = doc.getKey();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.REV] = doc.getRev();
                this._enclosing.executor.documentCache().setValues(this.value, values);
                return doc;
            }

            private readonly InternalArangoVertexCollection<E, R, C> _enclosing;

            private readonly T value;
        }

        protected internal virtual Request getVertexRequest(string
             key, DocumentReadOptions options)
        {
            Request request = new Request
                (this.db, RequestType.GET, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, this.graph, ArangoDBConstants.VERTEX, this.createDocumentHandle
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
            <T> getVertexResponseDeserializer<T>()
        {
            System.Type type = typeof(T);
            return new _ResponseDeserializer_109(this, type);
        }

        private sealed class _ResponseDeserializer_109 : ArangoExecutor<,>.IResponseDeserializer
            <T>
        {
            public _ResponseDeserializer_109(InternalArangoVertexCollection<E, R, C> _enclosing
                , java.lang.Class type)
            {
                this._enclosing = _enclosing;
                this.type = type;
            }

            /// <exception cref="VPackException"/>
            public T Deserialize(Response response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .VERTEX), this.type);
            }

            private readonly InternalArangoVertexCollection<E, R, C> _enclosing;

            private readonly java.lang.Class type;
        }

        protected internal virtual Request replaceVertexRequest
            <T>(string key, T value, VertexReplaceOptions options)
        {
            Request request = new Request
                (this.db, RequestType.PUT, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, this.graph, ArangoDBConstants.VERTEX, this.createDocumentHandle
                (key)));
            VertexReplaceOptions @params = options != null ? options : new
                                                                           VertexReplaceOptions();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            request.setBody(this.executor.Serialize(value));
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <VertexUpdateEntity> replaceVertexResponseDeserializer<T>(T
            value)
        {
            return new _ResponseDeserializer_128(this, value);
        }

        private sealed class _ResponseDeserializer_128 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <VertexUpdateEntity>
        {
            public _ResponseDeserializer_128(InternalArangoVertexCollection<E, R, C> _enclosing
                , T value)
            {
                this._enclosing = _enclosing;
                this.value = value;
            }

            /// <exception cref="VPackException"/>
            public VertexUpdateEntity deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody().get(ArangoDBConstants
                    .VERTEX);
                VertexUpdateEntity doc = this._enclosing.executor.Deserialize
                    (body, typeof(VertexUpdateEntity
                ));
                System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type, string
                    > values = new System.Collections.Generic.Dictionary<com.arangodb.entity.DocumentFieldAttribute.Type
                    , string>();
                values[com.arangodb.entity.DocumentFieldAttribute.Type.REV] = doc.getRev();
                this._enclosing.executor.documentCache().setValues(this.value, values);
                return doc;
            }

            private readonly InternalArangoVertexCollection<E, R, C> _enclosing;

            private readonly T value;
        }

        protected internal virtual Request updateVertexRequest<
            T>(string key, T value, VertexUpdateOptions options)
        {
            Request request;
            request = new Request(this.db, RequestType
                .PATCH, this.executor.createPath(ArangoDBConstants.PATH_API_GHARIAL
                , this.graph, ArangoDBConstants.VERTEX, this.createDocumentHandle(key
                )));
            VertexUpdateOptions @params = options != null ? options : new
                                                                          VertexUpdateOptions();
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
            <VertexUpdateEntity> updateVertexResponseDeserializer<T>(T value
            )
        {
            return new _ResponseDeserializer_154(this);
        }

        private sealed class _ResponseDeserializer_154 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <VertexUpdateEntity>
        {
            public _ResponseDeserializer_154(InternalArangoVertexCollection<E, R, C> _enclosing
                )
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public VertexUpdateEntity deserialize(Response
                 response)
            {
                VPackSlice body = response.getBody().get(ArangoDBConstants
                    .VERTEX);
                return this._enclosing.executor.Deserialize(body, 
                    typeof(VertexUpdateEntity));
            }

            private readonly InternalArangoVertexCollection<E, R, C> _enclosing;
        }

        protected internal virtual Request deleteVertexRequest(
            string key, VertexDeleteOptions options)
        {
            Request request = new Request
                (this.db, RequestType.DELETE, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, this.graph, ArangoDBConstants.VERTEX, this.createDocumentHandle
                (key)));
            VertexDeleteOptions @params = options != null ? options : new
                                                                          VertexDeleteOptions();
            request.putQueryParam(ArangoDBConstants.WAIT_FOR_SYNC, @params
                .getWaitForSync());
            request.putHeaderParam(ArangoDBConstants.IF_MATCH, @params
                .getIfMatch());
            return request;
        }
    }
}