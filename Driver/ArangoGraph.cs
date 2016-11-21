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
    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class ArangoGraph : InternalArangoGraph<ArangoExecutorSync
        , Response, ConnectionSync
        >
    {
        protected internal ArangoGraph(ArangoDatabase db, string name)
            : base(db.executor(), db.name(), name)
        {
        }

        protected internal virtual ArangoExecutorSync executor()
        {
            return executor;
        }

        /// <summary>Delete an existing graph</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#drop-a-graph">API Documentation</a>
        /// 	</seealso>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void drop()
        {
            executor.execute(this.dropRequest(), typeof(java.lang.Void
            ));
        }

        /// <summary>Get a graph from the graph module</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#get-a-graph">API Documentation</a>
        /// 	</seealso>
        /// <returns>the definition content of this graph</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual GraphEntity getInfo()
        {
            return executor.execute(this.getInfoRequest(), this.getInfoResponseDeserializer());
        }

        /// <summary>Lists all vertex collections used in this graph</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#list-vertex-collections">API
        /// *      Documentation</a></seealso>
        /// <returns>all vertex collections within this graph</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<string> getVertexCollections
            ()
        {
            return executor.execute(this.getVertexCollectionsRequest(), this.getVertexCollectionsResponseDeserializer
                ());
        }

        /// <summary>Adds a vertex collection to the set of collections of the graph.</summary>
        /// <remarks>
        /// Adds a vertex collection to the set of collections of the graph. If the collection does not exist, it will be
        /// created.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#add-vertex-collection">API
        /// *      Documentation</a></seealso>
        /// <param name="name">The name of the collection</param>
        /// <returns>information about the graph</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual GraphEntity addVertexCollection(string name)
        {
            return executor.execute(this.addVertexCollectionRequest(name), this.addVertexCollectionResponseDeserializer
                ());
        }

        /// <summary>Returns a handler of the vertex collection by the given name</summary>
        /// <param name="name">Name of the vertex collection</param>
        /// <returns>collection handler</returns>
        public virtual ArangoVertexCollection vertexCollection(string name)
        {
            return new ArangoVertexCollection(this, name);
        }

        /// <summary>Returns a handler of the edge collection by the given name</summary>
        /// <param name="name">Name of the edge collection</param>
        /// <returns>collection handler</returns>
        public virtual ArangoEdgeCollection edgeCollection(string name)
        {
            return new ArangoEdgeCollection(this, name);
        }

        /// <summary>Lists all edge collections used in this graph</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#list-edge-definitions">API
        /// *      Documentation</a></seealso>
        /// <returns>all edge collections within this graph</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual System.Collections.Generic.ICollection<string> getEdgeDefinitions(
            )
        {
            return executor.execute(this.getEdgeDefinitionsRequest(), this.getEdgeDefinitionsDeserializer
                ());
        }

        /// <summary>Add a new edge definition to the graph</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#add-edge-definition">API
        /// *      Documentation</a></seealso>
        /// <param name="definition"/>
        /// <returns>information about the graph</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual GraphEntity addEdgeDefinition(EdgeDefinition
             definition)
        {
            return executor.execute(this.addEdgeDefinitionRequest(definition), this.addEdgeDefinitionResponseDeserializer
                ());
        }

        /// <summary>Change one specific edge definition.</summary>
        /// <remarks>
        /// Change one specific edge definition. This will modify all occurrences of this definition in all graphs known to
        /// your database
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#replace-an-edge-definition">API
        /// *      Documentation</a></seealso>
        /// <param name="definition">The edge definition</param>
        /// <returns>information about the graph</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual GraphEntity replaceEdgeDefinition(EdgeDefinition
             definition)
        {
            return executor.execute(this.replaceEdgeDefinitionRequest(definition), this.replaceEdgeDefinitionResponseDeserializer
                ());
        }

        /// <summary>Remove one edge definition from the graph.</summary>
        /// <remarks>
        /// Remove one edge definition from the graph. This will only remove the edge collection, the vertex collections
        /// remain untouched and can still be used in your queries
        /// </remarks>
        /// <seealso><a href=
        /// *      "https://docs.arangodb.com/current/HTTP/Gharial/Management.html#remove-an-edge-definition-from-the-graph">API
        /// *      Documentation</a></seealso>
        /// <param name="definitionName">The name of the edge collection used in the definition
        /// 	</param>
        /// <returns>information about the graph</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual GraphEntity removeEdgeDefinition(string definitionName
            )
        {
            return executor.execute(this.removeEdgeDefinitionRequest(definitionName), this.removeEdgeDefinitionResponseDeserializer
                ());
        }
    }
}