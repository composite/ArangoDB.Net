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

namespace ArangoDB
{
    using global::ArangoDB.Entity;
    using global::ArangoDB.Internal;
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocystream;
    using System.Collections.Generic;

    using global::ArangoDB.Internal.VelocyStream;

    /// <author>Mark - mark at arangodb.com</author>
	public class ArangoDatabase : InternalArangoDatabase<ArangoExecutorSync
        , Response, ConnectionSync
        >
    {
        protected internal ArangoDatabase(ArangoDB arangoDB, string name)
            : base(arangoDB.executor(), name)
        {
        }

        protected internal ArangoDatabase(Communication
            <Response, ConnectionSync
            > communication, VPack vpacker, VPack
             vpackerNull, VPackParser vpackParser, DocumentCache
             documentCache, CollectionCache collectionCache, string name
            )
            : base(new ArangoExecutorSync(communication, vpacker, vpackerNull
                , vpackParser, documentCache, collectionCache), name)
        {
        }

        protected internal virtual ArangoExecutorSync executor()
        {
            return executor;
        }

        /// <summary>Returns a handler of the collection by the given name</summary>
        /// <param name="name">Name of the collection</param>
        /// <returns>collection handler</returns>
        public virtual ArangoCollection collection(string name)
        {
            return new ArangoCollection(this, name);
        }

        /// <summary>Creates a collection</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Creating.html#create-collection">API
        /// *      Documentation</a></seealso>
        /// <param name="name">The name of the collection</param>
        /// <returns>information about the collection</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionEntity createCollection(string name)
        {
            return executor.execute(this.createCollectionRequest(name, new CollectionCreateOptions
                ()), typeof(CollectionEntity
            ));
        }

        /// <summary>Creates a collection</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Creating.html#create-collection">API
        /// *      Documentation</a></seealso>
        /// <param name="name">The name of the collection</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the collection</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual CollectionEntity createCollection(string name,
            CollectionCreateOptions options)
        {
            return executor.execute(this.createCollectionRequest(name, options), 
                typeof(CollectionEntity));
        }

        /// <summary>Returns all collections</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Getting.html#reads-all-collections">API
        /// *      Documentation</a></seealso>
        /// <returns>list of information about all collections</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<CollectionEntity
            > getCollections()
        {
            return executor.execute(this.getCollectionsRequest(new CollectionsReadOptions
                ()), this.getCollectionsResponseDeserializer());
        }

        /// <summary>Returns all collections</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Getting.html#reads-all-collections">API
        /// *      Documentation</a></seealso>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>list of information about all collections</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<CollectionEntity
            > getCollections(CollectionsReadOptions options)
        {
            return executor.execute(this.getCollectionsRequest(options), this.getCollectionsResponseDeserializer
                ());
        }

        /// <summary>Returns an index</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/WorkingWith.html#read-index">API Documentation</a>
        /// 	</seealso>
        /// <param name="id">The index-handle</param>
        /// <returns>information about the index</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual IndexEntity getIndex(string id)
        {
            return executor.execute(this.getIndexRequest(id), typeof(
                IndexEntity));
        }

        /// <summary>Deletes an index</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Indexes/WorkingWith.html#delete-index">API Documentation</a>
        /// 	</seealso>
        /// <param name="id">The index-handle</param>
        /// <returns>the id of the index</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual string deleteIndex(string id)
        {
            return executor.execute(this.deleteIndexRequest(id), this.deleteIndexResponseDeserializer()
                );
        }

        /// <summary>Drop an existing database</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Database/DatabaseManagement.html#drop-database">API
        /// *      Documentation</a></seealso>
        /// <returns>true if the database was dropped successfully</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual bool drop()
        {
            return executor.execute(this.dropRequest(), this.createDropResponseDeserializer());
        }

        /// <summary>Grants access to the database dbname for user user.</summary>
        /// <remarks>
        /// Grants access to the database dbname for user user. You need permission to the _system database in order to
        /// execute this call.
        /// </remarks>
        /// <seealso><a href= "https://docs.arangodb.com/current/HTTP/UserManagement/index.html#grant-or-revoke-database-access">
        /// *      API Documentation</a></seealso>
        /// <param name="user">The name of the user</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void grantAccess(string user)
        {
            executor.execute(this.grantAccessRequest(user), typeof(
                java.lang.Void));
        }

        /// <summary>Revokes access to the database dbname for user user.</summary>
        /// <remarks>
        /// Revokes access to the database dbname for user user. You need permission to the _system database in order to
        /// execute this call.
        /// </remarks>
        /// <seealso><a href= "https://docs.arangodb.com/current/HTTP/UserManagement/index.html#grant-or-revoke-database-access">
        /// *      API Documentation</a></seealso>
        /// <param name="user">The name of the user</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void revokeAccess(string user)
        {
            executor.execute(this.revokeAccessRequest(user), typeof(
                java.lang.Void));
        }

        /// <summary>Create a cursor and return the first results</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlQueryCursor/AccessingCursors.html#create-cursor">API
        /// *      Documentation</a></seealso>
        /// <param name="query">contains the query string to be executed</param>
        /// <param name="bindVars">key/value pairs representing the bind parameters</param>
        /// <param name="options">Additional options, can be null</param>
        /// <param name="type">The type of the result (POJO class, VPackSlice, String for Json, or Collection/List/Map)
        /// 	</param>
        /// <returns>cursor of the results</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual ArangoCursor<T> query<T>(string query, System.Collections.Generic.IDictionary
            <string, object> bindVars, AqlQueryOptions options)
        {
            System.Type type = typeof(T);
            Request request = this.queryRequest(query, bindVars, options
                );
            CursorEntity result = executor.execute(request, 
                typeof(CursorEntity));
            return new ArangoCursor<T>(this, new _ArangoCursorExecute_242(this),
                type, result);
        }

        private sealed class _ArangoCursorExecute_242 : ArangoCursorExecute
        {
            public _ArangoCursorExecute_242(ArangoDatabase _enclosing)
            {
                this._enclosing = _enclosing;
            }

            public CursorEntity next(string id)
            {
                return this._enclosing._enclosing.executor.execute(this._enclosing.queryNextRequest
                    (id), typeof(CursorEntity));
            }

            public void close(string id)
            {
                this._enclosing._enclosing.executor.execute(this._enclosing.queryCloseRequest(id)
                    , typeof(java.lang.Void));
            }

            private readonly ArangoDatabase _enclosing;
        }

        /// <summary>Explain an AQL query and return information about it</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#explain-an-aql-query">API
        /// *      Documentation</a></seealso>
        /// <param name="query">the query which you want explained</param>
        /// <param name="bindVars">key/value pairs representing the bind parameters</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the query</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual AqlExecutionExplainEntity explainQuery(string
            query, System.Collections.Generic.IDictionary<string, object> bindVars, AqlQueryExplainOptions
             options)
        {
            return executor.execute(this.explainQueryRequest(query, bindVars, options), 
                typeof(AqlExecutionExplainEntity));
        }

        /// <summary>Parse an AQL query and return information about it This method is for query validation only.
        /// 	</summary>
        /// <remarks>
        /// Parse an AQL query and return information about it This method is for query validation only. To actually query
        /// the database, see
        /// <see cref="query{T}(string, System.Collections.Generic.IDictionary{K, V}, com.arangodb.model.AqlQueryOptions, java.lang.Class{T})
        /// 	"/>
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#parse-an-aql-query">API
        /// *      Documentation</a></seealso>
        /// <param name="query">the query which you want parse</param>
        /// <returns>imformation about the query</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual AqlParseEntity parseQuery(string query)
        {
            return executor.execute(this.parseQueryRequest(query), 
                typeof(AqlParseEntity));
        }

        /// <summary>Clears the AQL query cache</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlQueryCache/index.html#clears-any-results-in-the-aql-query-cache">API
        /// *      Documentation</a></seealso>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void clearQueryCache()
        {
            executor.execute(this.clearQueryCacheRequest(), typeof(
                java.lang.Void));
        }

        /// <summary>Returns the global configuration for the AQL query cache</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlQueryCache/index.html#returns-the-global-properties-for-the-aql-query-cache">API
        /// *      Documentation</a></seealso>
        /// <returns>configuration for the AQL query cache</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual QueryCachePropertiesEntity getQueryCacheProperties
            ()
        {
            return executor.execute(this.getQueryCachePropertiesRequest(), 
                typeof(QueryCachePropertiesEntity));
        }

        /// <summary>Changes the configuration for the AQL query cache.</summary>
        /// <remarks>
        /// Changes the configuration for the AQL query cache. Note: changing the properties may invalidate all results in
        /// the cache.
        /// </remarks>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlQueryCache/index.html#globally-adjusts-the-aql-query-result-cache-properties">API
        /// *      Documentation</a></seealso>
        /// <param name="properties">properties to be set</param>
        /// <returns>current set of properties</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual QueryCachePropertiesEntity setQueryCacheProperties
            (QueryCachePropertiesEntity properties)
        {
            return executor.execute(this.setQueryCachePropertiesRequest(properties), 
                typeof(QueryCachePropertiesEntity));
        }

        /// <summary>Returns the configuration for the AQL query tracking</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#returns-the-properties-for-the-aql-query-tracking">API
        /// *      Documentation</a></seealso>
        /// <returns>configuration for the AQL query tracking</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual QueryTrackingPropertiesEntity getQueryTrackingProperties
            ()
        {
            return executor.execute(this.getQueryTrackingPropertiesRequest(), 
                typeof(QueryTrackingPropertiesEntity));
        }

        /// <summary>Changes the configuration for the AQL query tracking</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#changes-the-properties-for-the-aql-query-tracking">API
        /// *      Documentation</a></seealso>
        /// <param name="properties">properties to be set</param>
        /// <returns>current set of properties</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual QueryTrackingPropertiesEntity setQueryTrackingProperties
            (QueryTrackingPropertiesEntity properties)
        {
            return executor.execute(this.setQueryTrackingPropertiesRequest(properties), 
                typeof(QueryTrackingPropertiesEntity));
        }

        /// <summary>Returns a list of currently running AQL queries</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#returns-the-currently-running-aql-queries">API
        /// *      Documentation</a></seealso>
        /// <returns>a list of currently running AQL queries</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<QueryEntity
            > getCurrentlyRunningQueries()
        {
            return executor.execute(this.getCurrentlyRunningQueriesRequest(), new _Type_372().getType
                ());
        }

        private sealed class _Type_372 : Type<ICollection<QueryEntity>>
        {
            public _Type_372()
            {
            }
        }

        /// <summary>Returns a list of slow running AQL queries</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#returns-the-list-of-slow-aql-queries">API
        /// *      Documentation</a></seealso>
        /// <returns>a list of slow running AQL queries</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<QueryEntity
            > getSlowQueries()
        {
            return executor.execute(this.getSlowQueriesRequest(), new _Type_386().getType());
        }

        private sealed class _Type_386 : Type<ICollection<QueryEntity>>
        {
            public _Type_386()
            {
            }
        }

        /// <summary>Clears the list of slow AQL queries</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#clears-the-list-of-slow-aql-queries">API
        /// *      Documentation</a></seealso>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void clearSlowQueries()
        {
            executor.execute(this.clearSlowQueriesRequest(), typeof(
                java.lang.Void));
        }

        /// <summary>Kills a running query.</summary>
        /// <remarks>Kills a running query. The query will be terminated at the next cancelation point.
        /// 	</remarks>
        /// <seealso><a href= "https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#kills-a-running-aql-query">API
        /// *      Documentation</a></seealso>
        /// <param name="id">The id of the query</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void killQuery(string id)
        {
            executor.execute(this.killQueryRequest(id), typeof(java.lang.Void
            ));
        }

        /// <summary>Create a new AQL user function</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlUserFunctions/index.html#create-aql-user-function">API
        /// *      Documentation</a></seealso>
        /// <param name="name">the fully qualified name of the user functions</param>
        /// <param name="code">a string representation of the function body</param>
        /// <param name="options">Additional options, can be null</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void createAqlFunction(string name, string code, AqlFunctionCreateOptions
             options)
        {
            executor.execute(this.createAqlFunctionRequest(name, code, options), 
                typeof(java.lang.Void));
        }

        /// <summary>Remove an existing AQL user function</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlUserFunctions/index.html#remove-existing-aql-user-function">API
        /// *      Documentation</a></seealso>
        /// <param name="name">the name of the AQL user function</param>
        /// <param name="options">Additional options, can be null</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void deleteAqlFunction(string name, AqlFunctionDeleteOptions
             options)
        {
            executor.execute(this.deleteAqlFunctionRequest(name, options), 
                typeof(java.lang.Void));
        }

        /// <summary>Gets all reqistered AQL user functions</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AqlUserFunctions/index.html#return-registered-aql-user-functions">API
        /// *      Documentation</a></seealso>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>all reqistered AQL user functions</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<AqlFunctionEntity
            > getAqlFunctions(AqlFunctionGetOptions options)
        {
            return executor.execute(this.getAqlFunctionsRequest(options), new _Type_461().getType(
                ));
        }

        private sealed class _Type_461 : Type<ICollection<AqlFunctionEntity>>
        {
            public _Type_461()
            {
            }
        }

        /// <summary>Returns a handler of the graph by the given name</summary>
        /// <param name="name">Name of the graph</param>
        /// <returns>graph handler</returns>
        public virtual ArangoGraph graph(string name)
        {
            return new ArangoGraph(this, name);
        }

        /// <summary>Create a new graph in the graph module.</summary>
        /// <remarks>
        /// Create a new graph in the graph module. The creation of a graph requires the name of the graph and a definition
        /// of its edges.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#create-a-graph">API
        /// *      Documentation</a></seealso>
        /// <param name="name">Name of the graph</param>
        /// <param name="edgeDefinitions">An array of definitions for the edge</param>
        /// <returns>information about the graph</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual GraphEntity createGraph(string name, System.Collections.Generic.ICollection
            <EdgeDefinition> edgeDefinitions)
        {
            return executor.execute(this.createGraphRequest(name, edgeDefinitions, new GraphCreateOptions
                ()), this.createGraphResponseDeserializer());
        }

        /// <summary>Create a new graph in the graph module.</summary>
        /// <remarks>
        /// Create a new graph in the graph module. The creation of a graph requires the name of the graph and a definition
        /// of its edges.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#create-a-graph">API
        /// *      Documentation</a></seealso>
        /// <param name="name">Name of the graph</param>
        /// <param name="edgeDefinitions">An array of definitions for the edge</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the graph</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual GraphEntity createGraph(string name, System.Collections.Generic.ICollection
            <EdgeDefinition> edgeDefinitions, GraphCreateOptions
             options)
        {
            return executor.execute(this.createGraphRequest(name, edgeDefinitions, options), this.createGraphResponseDeserializer
                ());
        }

        /// <summary>Lists all graphs known to the graph module</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#list-all-graphs">API
        /// *      Documentation</a></seealso>
        /// <returns>graphs stored in this database</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<GraphEntity
            > getGraphs()
        {
            return executor.execute(this.getGraphsRequest(), this.getGraphsResponseDeserializer());
        }

        /// <summary>Execute a server-side transaction</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Transaction/index.html#execute-transaction">API
        /// *      Documentation</a></seealso>
        /// <param name="action">the actual transaction operations to be executed, in the form of stringified JavaScript code
        /// 	</param>
        /// <param name="type">The type of the result (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>the result of the transaction if it succeeded</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual T transaction<T>(string action, TransactionOptions
             options)
        {
            System.Type type = typeof(T);
            return executor.execute(this.transactionRequest(action, options), transactionResponseDeserializer
                (type));
        }

        /// <summary>Retrieves information about the current database</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Database/DatabaseManagement.html#information-of-the-database">API
        /// *      Documentation</a></seealso>
        /// <returns>information about the current database</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual DatabaseEntity getInfo()
        {
            return executor.execute(this.getInfoRequest(), this.getInfoResponseDeserializer());
        }

        /// <summary>Execute a server-side traversal</summary>
        /// <seealso><a href= "https://docs.arangodb.com/current/HTTP/Traversal/index.html#executes-a-traversal">API
        /// *      Documentation</a></seealso>
        /// <param name="vertexClass">The type of the vertex documents (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="edgeClass">The type of the edge documents (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options</param>
        /// <returns>Result of the executed traversal</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual TraversalEntity<V, E> executeTraversal<V, E>(TraversalOptions
             options)
        {
            System.Type vertexClass = typeof(V);
            System.Type edgeClass = typeof(E);
            Request request = this.executeTraversalRequest(options);
            return executor.execute(request, executeTraversalResponseDeserializer(vertexClass
                , edgeClass));
        }

        /// <summary>Reads a single document</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#read-document">API
        /// *      Documentation</a></seealso>
        /// <param name="id">The id of the document</param>
        /// <param name="type">The type of the document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>the document identified by the id</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual T getDocument<T>(string id)
        {
            System.Type type = typeof(T);
            executor.validateDocumentId(id);
            string[] split = id.split("/");
            return this.collection(split[0]).getDocument(split[1], type);
        }

        /// <summary>Reads a single document</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#read-document">API
        /// *      Documentation</a></seealso>
        /// <param name="id">The id of the document</param>
        /// <param name="type">The type of the document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>the document identified by the id</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual T getDocument<T>(string id, DocumentReadOptions
             options)
        {
            System.Type type = typeof(T);
            executor.validateDocumentId(id);
            string[] split = id.split("/");
            return this.collection(split[0]).getDocument(split[1], type, options);
        }

        /// <summary>Reload the routing table.</summary>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/AdministrationAndMonitoring/index.html#reloads-the-routing-information">API
        /// *      Documentation</a></seealso>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void reloadRouting()
        {
            executor.execute(this.reloadRoutingRequest(), typeof(java.lang.Void
            ));
        }
    }
}