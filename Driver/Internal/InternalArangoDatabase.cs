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
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocystream;
    using System.Collections.Generic;

    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Velocypack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
	public class InternalArangoDatabase<E, R, C> : ArangoExecuteable
        <E, R, C>
        where E : ArangoExecutor<R, C>
        where C : Connection
    {
        private readonly string name;

        public InternalArangoDatabase(E executor, string name)
            : base(executor)
        {
            this.name = name;
        }

        public virtual string name()
        {
            return name;
        }

        protected internal virtual Request createCollectionRequest
            (string name, CollectionCreateOptions options)
        {
            return new Request(name(), RequestType
                .POST, ArangoDBConstants.PATH_API_COLLECTION).setBody(this.executor
                .Serialize(OptionsBuilder.build(options != null ? options : new
                CollectionCreateOptions(), name)));
        }

        protected internal virtual Request getCollectionsRequest
            (CollectionsReadOptions options)
        {
            Request request;
            request = new Request(this.name(), RequestType
                .GET, ArangoDBConstants.PATH_API_COLLECTION);
            CollectionsReadOptions @params = options != null ? options :
                                                 new CollectionsReadOptions();
            request.putQueryParam(ArangoDBConstants.EXCLUDE_SYSTEM, @params
                .getExcludeSystem());
            return request;
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<CollectionEntity>> getCollectionsResponseDeserializer
            ()
        {
            return new _ResponseDeserializer_90(this);
        }

        private sealed class _ResponseDeserializer_90 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<CollectionEntity>>
        {
            public _ResponseDeserializer_90(InternalArangoDatabase<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public System.Collections.Generic.ICollection<CollectionEntity
                > deserialize(Response response)
            {
                VPackSlice result = response.getBody().get(ArangoDBConstants
                    .RESULT);
                return this._enclosing.executor.Deserialize(result, new _Type_94().getType());
            }

            private sealed class _Type_94 : Type<ICollection<CollectionEntity>>
            {
                public _Type_94()
                {
                }
            }

            private readonly InternalArangoDatabase<E, R, C> _enclosing;
        }

        protected internal virtual Request getIndexRequest(string
             id)
        {
            return new Request(name, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_INDEX
                , id));
        }

        protected internal virtual Request deleteIndexRequest(string
             id)
        {
            return new Request(name, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_INDEX
                , id));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <string> deleteIndexResponseDeserializer()
        {
            return new _ResponseDeserializer_109();
        }

        private sealed class _ResponseDeserializer_109 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <string>
        {
            public _ResponseDeserializer_109()
            {
            }

            /// <exception cref="VPackException"/>
            public string deserialize(Response response)
            {
                return response.getBody().get(ArangoDBConstants.ID).getAsString
                    ();
            }
        }

        protected internal virtual Request dropRequest()
        {
            return new Request(ArangoDBConstants
                .SYSTEM, RequestType.DELETE, this.executor.createPath(ArangoDBConstants
                .PATH_API_DATABASE, name));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <bool> createDropResponseDeserializer()
        {
            return new _ResponseDeserializer_123();
        }

        private sealed class _ResponseDeserializer_123 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <bool>
        {
            public _ResponseDeserializer_123()
            {
            }

            /// <exception cref="VPackException"/>
            public bool deserialize(Response response)
            {
                return response.getBody().get(ArangoDBConstants.RESULT).getAsBoolean
                    ();
            }
        }

        protected internal virtual Request grantAccessRequest(string
             user)
        {
            return new Request(ArangoDBConstants
                .SYSTEM, RequestType.PUT, this.executor.createPath(ArangoDBConstants
                .PATH_API_USER, user, ArangoDBConstants.DATABASE, name)).
                setBody(this.executor.Serialize(OptionsBuilder.build(new UserAccessOptions
                (), ArangoDBConstants.RW)));
        }

        protected internal virtual Request revokeAccessRequest(
            string user)
        {
            return new Request(ArangoDBConstants
                .SYSTEM, RequestType.PUT, this.executor.createPath(ArangoDBConstants
                .PATH_API_USER, user, ArangoDBConstants.DATABASE, name)).
                setBody(this.executor.Serialize(OptionsBuilder.build(new UserAccessOptions
                (), ArangoDBConstants.NONE)));
        }

        protected internal virtual Request queryRequest(string
            query, System.Collections.Generic.IDictionary<string, object> bindVars, AqlQueryOptions
             options)
        {
            return new Request(name, RequestType
                .POST, ArangoDBConstants.PATH_API_CURSOR).setBody(this.executor
                .Serialize(OptionsBuilder.build(options != null ? options : new
                AqlQueryOptions(), query, bindVars)));
        }

        protected internal virtual Request queryNextRequest(string
             id)
        {
            return new Request(name, RequestType
                .PUT, this.executor.createPath(ArangoDBConstants.PATH_API_CURSOR
                , id));
        }

        protected internal virtual Request queryCloseRequest(string
             id)
        {
            return new Request(name, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_CURSOR
                , id));
        }

        protected internal virtual Request explainQueryRequest(
            string query, System.Collections.Generic.IDictionary<string, object> bindVars, AqlQueryExplainOptions
             options)
        {
            return new Request(name, RequestType
                .POST, ArangoDBConstants.PATH_API_EXPLAIN).setBody(this.executor
                .Serialize(OptionsBuilder.build(options != null ? options : new
                AqlQueryExplainOptions(), query, bindVars)));
        }

        protected internal virtual Request parseQueryRequest(string
             query)
        {
            return new Request(name, RequestType
                .POST, ArangoDBConstants.PATH_API_QUERY).setBody(this.executor
                .Serialize(OptionsBuilder.build(new AqlQueryParseOptions
                (), query)));
        }

        protected internal virtual Request clearQueryCacheRequest
            ()
        {
            return new Request(name, RequestType
                .DELETE, ArangoDBConstants.PATH_API_QUERY_CACHE);
        }

        protected internal virtual Request getQueryCachePropertiesRequest
            ()
        {
            return new Request(name, RequestType
                .GET, ArangoDBConstants.PATH_API_QUERY_CACHE_PROPERTIES);
        }

        protected internal virtual Request setQueryCachePropertiesRequest
            (QueryCachePropertiesEntity properties)
        {
            return new Request(name, RequestType
                .PUT, ArangoDBConstants.PATH_API_QUERY_CACHE_PROPERTIES).
                setBody(this.executor.Serialize(properties));
        }

        protected internal virtual Request getQueryTrackingPropertiesRequest
            ()
        {
            return new Request(name, RequestType
                .GET, ArangoDBConstants.PATH_API_QUERY_PROPERTIES);
        }

        protected internal virtual Request setQueryTrackingPropertiesRequest
            (QueryTrackingPropertiesEntity properties)
        {
            return new Request(name, RequestType
                .PUT, ArangoDBConstants.PATH_API_QUERY_PROPERTIES).setBody
                (this.executor.Serialize(properties));
        }

        protected internal virtual Request getCurrentlyRunningQueriesRequest
            ()
        {
            return new Request(name, RequestType
                .GET, ArangoDBConstants.PATH_API_QUERY_CURRENT);
        }

        protected internal virtual Request getSlowQueriesRequest
            ()
        {
            return new Request(name, RequestType
                .GET, ArangoDBConstants.PATH_API_QUERY_SLOW);
        }

        protected internal virtual Request clearSlowQueriesRequest
            ()
        {
            return new Request(name, RequestType
                .DELETE, ArangoDBConstants.PATH_API_QUERY_SLOW);
        }

        protected internal virtual Request killQueryRequest(string
             id)
        {
            return new Request(name, RequestType
                .DELETE, this.executor.createPath(ArangoDBConstants.PATH_API_QUERY
                , id));
        }

        protected internal virtual Request createAqlFunctionRequest
            (string name, string code, AqlFunctionCreateOptions options)
        {
            return new Request(name(), RequestType
                .POST, ArangoDBConstants.PATH_API_AQLFUNCTION).setBody(this.executor
                .Serialize(OptionsBuilder.build(options != null ? options : new
                AqlFunctionCreateOptions(), name, code)));
        }

        protected internal virtual Request deleteAqlFunctionRequest
            (string name, AqlFunctionDeleteOptions options)
        {
            Request request = new Request
                (name(), RequestType.DELETE, this.executor.createPath(ArangoDBConstants
                .PATH_API_AQLFUNCTION, name));
            AqlFunctionDeleteOptions @params = options != null ? options :
                new AqlFunctionDeleteOptions();
            request.putQueryParam(ArangoDBConstants.GROUP, @params.getGroup
                ());
            return request;
        }

        protected internal virtual Request getAqlFunctionsRequest
            (AqlFunctionGetOptions options)
        {
            Request request = new Request
                (this.name(), RequestType.GET, ArangoDBConstants
                .PATH_API_AQLFUNCTION);
            AqlFunctionGetOptions @params = options != null ? options : new
                AqlFunctionGetOptions();
            request.putQueryParam(ArangoDBConstants.NAMESPACE, @params
                .getNamespace());
            return request;
        }

        protected internal virtual Request createGraphRequest(string
             name, System.Collections.Generic.ICollection<EdgeDefinition
            > edgeDefinitions, GraphCreateOptions options)
        {
            return new Request(name(), RequestType
                .POST, ArangoDBConstants.PATH_API_GHARIAL).setBody(this.executor
                .Serialize(OptionsBuilder.build(options != null ? options : new
                GraphCreateOptions(), name, edgeDefinitions)));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity> createGraphResponseDeserializer()
        {
            return new _ResponseDeserializer_242(this);
        }

        private sealed class _ResponseDeserializer_242 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <GraphEntity>
        {
            public _ResponseDeserializer_242(InternalArangoDatabase<E, R, C> _enclosing)
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

            private readonly InternalArangoDatabase<E, R, C> _enclosing;
        }

        protected internal virtual Request getGraphsRequest()
        {
            return new Request(name, RequestType
                .GET, ArangoDBConstants.PATH_API_GHARIAL);
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<GraphEntity>> getGraphsResponseDeserializer
            ()
        {
            return new _ResponseDeserializer_255(this);
        }

        private sealed class _ResponseDeserializer_255 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <System.Collections.Generic.ICollection<GraphEntity>>
        {
            public _ResponseDeserializer_255(InternalArangoDatabase<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public System.Collections.Generic.ICollection<GraphEntity> deserialize
                (Response response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .GRAPHS), new _Type_259().getType());
            }

            private sealed class _Type_259 : Type<ICollection<GraphEntity>>
            {
                public _Type_259()
                {
                }
            }

            private readonly InternalArangoDatabase<E, R, C> _enclosing;
        }

        protected internal virtual Request transactionRequest(string
             action, TransactionOptions options)
        {
            return new Request(name, RequestType
                .POST, ArangoDBConstants.PATH_API_TRANSACTION).setBody(this.executor
                .Serialize(OptionsBuilder.build(options != null ? options : new
                TransactionOptions(), action)));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <T> transactionResponseDeserializer<T>()
        {
            System.Type type = typeof(T);
            return new _ResponseDeserializer_271(this, type);
        }

        private sealed class _ResponseDeserializer_271 : ArangoExecutor<,>.IResponseDeserializer
            <T>
        {
            public _ResponseDeserializer_271(InternalArangoDatabase<E, R, C> _enclosing, java.lang.Class
                 type)
            {
                this._enclosing = _enclosing;
                this.type = type;
            }

            /// <exception cref="VPackException"/>
            public T Deserialize(Response response)
            {
                VPackSlice body = response.getBody();
                if (body != null)
                {
                    VPackSlice result = body.get(ArangoDBConstants
                        .RESULT);
                    if (!result.isNone())
                    {
                        return this._enclosing.executor.Deserialize(result, this.type);
                    }
                }
                return null;
            }

            private readonly InternalArangoDatabase<E, R, C> _enclosing;

            private readonly java.lang.Class type;
        }

        protected internal virtual Request getInfoRequest()
        {
            return new Request(name, RequestType
                .GET, this.executor.createPath(ArangoDBConstants.PATH_API_DATABASE
                , ArangoDBConstants.CURRENT));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <DatabaseEntity> getInfoResponseDeserializer()
        {
            return new _ResponseDeserializer_292(this);
        }

        private sealed class _ResponseDeserializer_292 : com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <DatabaseEntity>
        {
            public _ResponseDeserializer_292(InternalArangoDatabase<E, R, C> _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackException"/>
            public DatabaseEntity deserialize(Response
                 response)
            {
                return this._enclosing.executor.Deserialize(response.getBody().get(ArangoDBConstants
                    .RESULT), typeof(DatabaseEntity
                ));
            }

            private readonly InternalArangoDatabase<E, R, C> _enclosing;
        }

        protected internal virtual Request executeTraversalRequest
            (TraversalOptions options)
        {
            return new Request(name, RequestType
                .POST, ArangoDBConstants.PATH_API_TRAVERSAL).setBody(this.executor
                .Serialize(options != null ? options : new TransactionOptions
                ()));
        }

        protected internal virtual com.arangodb.@internal.ArangoExecutor.ResponseDeserializer
            <TraversalEntity<V, E>> executeTraversalResponseDeserializer
            <E, V>()
        {
            System.Type vertexClass = typeof(V);
            System.Type edgeClass = typeof(E);
            return new _ResponseDeserializer_309(this, vertexClass, edgeClass);
        }

        private sealed class _ResponseDeserializer_309 : ArangoExecutor<,>.IResponseDeserializer
            <TraversalEntity<V, E>>
        {
            public _ResponseDeserializer_309(InternalArangoDatabase<E, R, C> _enclosing, java.lang.Class
                 vertexClass, java.lang.Class edgeClass)
            {
                this._enclosing = _enclosing;
                this.vertexClass = vertexClass;
                this.edgeClass = edgeClass;
            }

            /// <exception cref="VPackException"/>
            public TraversalEntity<V, E> Deserialize(Response
                 response)
            {
                TraversalEntity<V, E> result = new TraversalEntity
                    <V, E>();
                VPackSlice visited = response.getBody().get(ArangoDBConstants
                    .RESULT).get(ArangoDBConstants.VISITED);
                result.setVertices(this._enclosing.deserializeVertices(this.vertexClass, visited));
                System.Collections.Generic.ICollection<PathEntity<V, E>> paths
                     = new System.Collections.Generic.List<PathEntity<V, E>>();
                for (System.Collections.Generic.IEnumerator<VPackSlice> iterator
                     = visited.get("paths").arrayIterator(); iterator.MoveNext();)
                {
                    PathEntity<V, E> path = new PathEntity<V,
                        E>();
                    VPackSlice next = iterator.Current;
                    path.setEdges(this._enclosing.deserializeEdges(this.edgeClass, next));
                    path.setVertices(this._enclosing.deserializeVertices(this.vertexClass, next));
                    paths.add(path);
                }
                result.setPaths(paths);
                return result;
            }

            private readonly InternalArangoDatabase<E, R, C> _enclosing;

            private readonly java.lang.Class vertexClass;

            private readonly java.lang.Class edgeClass;
        }

        /// <exception cref="VPackException"/>
        protected internal virtual System.Collections.Generic.ICollection<V> deserializeVertices
            <V>(VPackSlice vpack)
        {
            System.Type vertexClass = typeof(V);
            System.Collections.Generic.ICollection<V> vertices = new System.Collections.Generic.List
                <V>();
            for (System.Collections.Generic.IEnumerator<VPackSlice> iterator
                 = vpack.get(ArangoDBConstants.VERTICES).arrayIterator();
                iterator.MoveNext();)
            {
                vertices.add((V)this.executor.Deserialize(iterator.Current, vertexClass));
            }
            return vertices;
        }

        /// <exception cref="VPackException"/>
        protected internal virtual System.Collections.Generic.ICollection<E> deserializeEdges
            <E>(VPackSlice next)
        {
            System.Type edgeClass = typeof(E);
            System.Collections.Generic.ICollection<E> edges = new System.Collections.Generic.List
                <E>();
            for (System.Collections.Generic.IEnumerator<VPackSlice> iteratorEdge
                 = next.get(ArangoDBConstants.EDGES).arrayIterator(); iteratorEdge
                .MoveNext();)
            {
                edges.add((E)this.executor.Deserialize(iteratorEdge.Current, edgeClass));
            }
            return edges;
        }

        protected internal virtual Request reloadRoutingRequest
            ()
        {
            return new Request(name, RequestType
                .POST, ArangoDBConstants.PATH_API_ADMIN_ROUTING_RELOAD);
        }
    }
}