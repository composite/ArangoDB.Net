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
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class ArangoVertexCollection : InternalArangoVertexCollection
        <ArangoExecutorSync, Response,
        ConnectionSync>
    {
        protected internal ArangoVertexCollection(ArangoGraph graph, string
            name)
            : base(graph.executor(), graph.db(), graph.name(), name)
        {
        }

        /// <summary>
        /// Removes a vertex collection from the graph and optionally deletes the collection, if it is not used in any other
        /// graph
        /// </summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#remove-vertex-collection">API
        /// *      Documentation</a></seealso>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void drop()
        {
            this.executor.execute(this.dropRequest(), typeof(java.lang.Void
            ));
        }

        /// <summary>Creates a new vertex in the collection</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#create-a-vertex">API Documentation</a>
        /// 	</seealso>
        /// <param name="value">A representation of a single vertex (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>information about the vertex</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual VertexEntity insertVertex<T>(T value)
        {
            return this.executor.execute(this.insertVertexRequest(value, new VertexCreateOptions
                ()), this.insertVertexResponseDeserializer(value));
        }

        /// <summary>Creates a new vertex in the collection</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#create-a-vertex">API Documentation</a>
        /// 	</seealso>
        /// <param name="value">A representation of a single vertex (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the vertex</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual VertexEntity insertVertex<T>(T value, VertexCreateOptions
             options)
        {
            return this.executor.execute(this.insertVertexRequest(value, options), this.insertVertexResponseDeserializer
                (value));
        }

        /// <summary>Fetches an existing vertex</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#get-a-vertex">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the vertex</param>
        /// <param name="type">The type of the vertex-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>the vertex identified by the key</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual T getVertex<T>(string key)
        {
            System.Type type = typeof(T);
            return this.executor.execute(this.getVertexRequest(key, new DocumentReadOptions
                ()), getVertexResponseDeserializer(type));
        }

        /// <summary>Fetches an existing vertex</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#get-a-vertex">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the vertex</param>
        /// <param name="type">The type of the vertex-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>the vertex identified by the key</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual T getVertex<T>(string key, DocumentReadOptions
            options)
        {
            System.Type type = typeof(T);
            return this.executor.execute(this.getVertexRequest(key, options), getVertexResponseDeserializer
                (type));
        }

        /// <summary>
        /// Replaces the vertex with key with the one in the body, provided there is such a vertex and no precondition is
        /// violated
        /// </summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#replace-a-vertex">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the vertex</param>
        /// <param name="type">The type of the vertex-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>information about the vertex</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual VertexUpdateEntity replaceVertex<T>(string key
            , T value)
        {
            return this.executor.execute(this.replaceVertexRequest(key, value, new VertexReplaceOptions
                ()), this.replaceVertexResponseDeserializer(value));
        }

        /// <summary>
        /// Replaces the vertex with key with the one in the body, provided there is such a vertex and no precondition is
        /// violated
        /// </summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#replace-a-vertex">API
        /// *      Documentation</a></seealso>
        /// <param name="key">The key of the vertex</param>
        /// <param name="type">The type of the vertex-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the vertex</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual VertexUpdateEntity replaceVertex<T>(string key
            , T value, VertexReplaceOptions options)
        {
            return this.executor.execute(this.replaceVertexRequest(key, value, options), this.replaceVertexResponseDeserializer
                (value));
        }

        /// <summary>Partially updates the vertex identified by document-key.</summary>
        /// <remarks>
        /// Partially updates the vertex identified by document-key. The value must contain a document with the attributes to
        /// patch (the patch document). All attributes from the patch document will be added to the existing document if they
        /// do not yet exist, and overwritten in the existing document if they do exist there.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#modify-a-vertex">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the vertex</param>
        /// <param name="type">The type of the vertex-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>information about the vertex</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual VertexUpdateEntity updateVertex<T>(string key,
            T value)
        {
            return this.executor.execute(this.updateVertexRequest(key, value, new VertexUpdateOptions
                ()), this.updateVertexResponseDeserializer(value));
        }

        /// <summary>Partially updates the vertex identified by document-key.</summary>
        /// <remarks>
        /// Partially updates the vertex identified by document-key. The value must contain a document with the attributes to
        /// patch (the patch document). All attributes from the patch document will be added to the existing document if they
        /// do not yet exist, and overwritten in the existing document if they do exist there.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#modify-a-vertex">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the vertex</param>
        /// <param name="type">The type of the vertex-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the vertex</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual VertexUpdateEntity updateVertex<T>(string key,
            T value, VertexUpdateOptions options)
        {
            return this.executor.execute(this.updateVertexRequest(key, value, options), this.updateVertexResponseDeserializer
                (value));
        }

        /// <summary>Removes a vertex</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#remove-a-vertex">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the vertex</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void deleteVertex(string key)
        {
            this.executor.execute(this.deleteVertexRequest(key, new VertexDeleteOptions
                ()), typeof(java.lang.Void));
        }

        /// <summary>Removes a vertex</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Vertices.html#remove-a-vertex">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the vertex</param>
        /// <param name="options">Additional options, can be null</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void deleteVertex(string key, VertexDeleteOptions
             options)
        {
            this.executor.execute(this.deleteVertexRequest(key, options), 
                typeof(java.lang.Void));
        }
    }
}