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
	public class InternalArangoGraph<E, R, C> : ArangoExecuteable
        <E, R, C>
        where E : ArangoExecutor<R, C>
        where C : Connection
    {
        private readonly string db;

        private readonly string name;

        public InternalArangoGraph(E executor, string db, string name)
            : base(executor)
        {
            this.db = db;
            this.name = name;
        }

        public virtual string db()
        {
            return db;
        }

        public virtual string name()
        {
            return name;
        }

        protected internal virtual Request dropRequest()
        {
            return new Request(db, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_GHARIAL
                , name));
        }

        protected internal virtual Request getInfoRequest()
        {
            return new Request(db, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_GHARIAL
                , name));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity> getInfoResponseDeserializer()
        {
            return this.addVertexCollectionResponseDeserializer();
        }

        protected internal virtual Request getVertexCollectionsRequest
            ()
        {
            return new Request(db, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_GHARIAL
                , name, ArangoDBConstants.VERTEX));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<string>> getVertexCollectionsResponseDeserializer
            ()
        {
            return new _ResponseDeserializer_79(this);
        }

        private sealed class _ResponseDeserializer_79 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<string>>
        {
            public _ResponseDeserializer_79(InternalArangoGraph<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public System.Collections.Generic.ICollection<string> deserialize(Response
                 response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .COLLECTIONS), new _Type_83().getType());
            }

            private sealed class _Type_83 : Type<System.Collections.Generic.ICollection
                <string>>
            {
                public _Type_83()
                {
                }
            }

            private readonly InternalArangoGraph<E, R, C> _enclosing;
        }

        protected internal virtual Request addVertexCollectionRequest
            (string name)
        {
            Request request = new Request
                (db, RequestType.POST, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, name(), ArangoDBConstants.VERTEX));
            request.setBody(this.executor.Serialize(OptionsBuilder.build(new VertexCollectionCreateOptions
                (), name)));
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity> addVertexCollectionResponseDeserializer()
        {
            return this.addEdgeDefinitionResponseDeserializer();
        }

        protected internal virtual Request getEdgeDefinitionsRequest
            ()
        {
            return new Request(db, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_GHARIAL
                , name, ArangoDBConstants.EDGE));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<string>> getEdgeDefinitionsDeserializer(
            )
        {
            return new _ResponseDeserializer_106(this);
        }

        private sealed class _ResponseDeserializer_106 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<string>>
        {
            public _ResponseDeserializer_106(InternalArangoGraph<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public System.Collections.Generic.ICollection<string> deserialize(Response
                 response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .COLLECTIONS), new _Type_110().getType());
            }

            private sealed class _Type_110 : Type<System.Collections.Generic.ICollection
                <string>>
            {
                public _Type_110()
                {
                }
            }

            private readonly InternalArangoGraph<E, R, C> _enclosing;
        }

        protected internal virtual Request addEdgeDefinitionRequest
            (EdgeDefinition definition)
        {
            Request request = new Request
                (db, RequestType.POST, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, name, ArangoDBConstants.EDGE));
            request.setBody(this.executor.Serialize(definition));
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity> addEdgeDefinitionResponseDeserializer()
        {
            return new _ResponseDeserializer_124(this);
        }

        private sealed class _ResponseDeserializer_124 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity>
        {
            public _ResponseDeserializer_124(InternalArangoGraph<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public GraphEntity deserialize(Response
                 response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .GRAPH), typeof(GraphEntity
                    ));
            }

            private readonly InternalArangoGraph<E, R, C> _enclosing;
        }

        protected internal virtual Request replaceEdgeDefinitionRequest
            (EdgeDefinition definition)
        {
            Request request = new Request
                (db, RequestType.PUT, this.executor.createPath(ArangoDBConstants
                .PATH_API_GHARIAL, name, ArangoDBConstants.EDGE, definition
                .getCollection()));
            request.setBody(this.executor.Serialize(definition));
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity> replaceEdgeDefinitionResponseDeserializer()
        {
            return new _ResponseDeserializer_140(this);
        }

        private sealed class _ResponseDeserializer_140 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity>
        {
            public _ResponseDeserializer_140(InternalArangoGraph<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public GraphEntity deserialize(Response
                 response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .GRAPH), typeof(GraphEntity
                    ));
            }

            private readonly InternalArangoGraph<E, R, C> _enclosing;
        }

        protected internal virtual Request removeEdgeDefinitionRequest
            (string definitionName)
        {
            return new Request(db, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_GHARIAL
                , name, ArangoDBConstants.EDGE, definitionName));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity> removeEdgeDefinitionResponseDeserializer()
        {
            return new _ResponseDeserializer_154(this);
        }

        private sealed class _ResponseDeserializer_154 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity>
        {
            public _ResponseDeserializer_154(InternalArangoGraph<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public GraphEntity deserialize(Response
                 response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .GRAPH), typeof(GraphEntity
                    ));
            }

            private readonly InternalArangoGraph<E, R, C> _enclosing;
        }
    }
}