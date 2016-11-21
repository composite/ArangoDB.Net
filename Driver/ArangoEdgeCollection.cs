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
	public class ArangoEdgeCollection : InternalArangoEdgeCollection
        <ArangoExecutorSync, Response,
        ConnectionSync>
    {
        protected internal ArangoEdgeCollection(ArangoGraph graph, string name
            )
            : base(graph.executor(), graph.db(), graph.name(), name)
        {
        }

        /// <summary>Creates a new edge in the collection</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#create-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="value">A representation of a single edge (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>information about the edge</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual EdgeEntity insertEdge<T>(T value)
        {
            return this.executor.execute(this.insertEdgeRequest(value, new EdgeCreateOptions
                ()), this.insertEdgeResponseDeserializer(value));
        }

        /// <summary>Creates a new edge in the collection</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#create-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="value">A representation of a single edge (POJO, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the edge</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual EdgeEntity insertEdge<T>(T value, EdgeCreateOptions
             options)
        {
            return this.executor.execute(this.insertEdgeRequest(value, options), this.insertEdgeResponseDeserializer
                (value));
        }

        /// <summary>Fetches an existing edge</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#get-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the edge</param>
        /// <param name="type">The type of the edge-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>the edge identified by the key</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual T getEdge<T>(string key)
        {
            System.Type type = typeof(T);
            return this.executor.execute(this.getEdgeRequest(key, new DocumentReadOptions
                ()), getEdgeResponseDeserializer(type));
        }

        /// <summary>Fetches an existing edge</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#get-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the edge</param>
        /// <param name="type">The type of the edge-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>the edge identified by the key</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual T getEdge<T>(string key, DocumentReadOptions options
            )
        {
            System.Type type = typeof(T);
            return this.executor.execute(this.getEdgeRequest(key, options), getEdgeResponseDeserializer
                (type));
        }

        /// <summary>
        /// Replaces the edge with key with the one in the body, provided there is such a edge and no precondition is
        /// violated
        /// </summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#replace-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the edge</param>
        /// <param name="type">The type of the edge-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>information about the edge</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual EdgeUpdateEntity replaceEdge<T>(string key, T
            value)
        {
            return this.executor.execute(this.replaceEdgeRequest(key, value, new EdgeReplaceOptions
                ()), this.replaceEdgeResponseDeserializer(value));
        }

        /// <summary>
        /// Replaces the edge with key with the one in the body, provided there is such a edge and no precondition is
        /// violated
        /// </summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#replace-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the edge</param>
        /// <param name="type">The type of the edge-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the edge</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual EdgeUpdateEntity replaceEdge<T>(string key, T
            value, EdgeReplaceOptions options)
        {
            return this.executor.execute(this.replaceEdgeRequest(key, value, options), this.replaceEdgeResponseDeserializer
                (value));
        }

        /// <summary>Partially updates the edge identified by document-key.</summary>
        /// <remarks>
        /// Partially updates the edge identified by document-key. The value must contain a document with the attributes to
        /// patch (the patch document). All attributes from the patch document will be added to the existing document if they
        /// do not yet exist, and overwritten in the existing document if they do exist there.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#modify-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the edge</param>
        /// <param name="type">The type of the edge-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <returns>information about the edge</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual EdgeUpdateEntity updateEdge<T>(string key, T value
            )
        {
            return this.executor.execute(this.updateEdgeRequest(key, value, new EdgeUpdateOptions
                ()), this.updateEdgeResponseDeserializer(value));
        }

        /// <summary>Partially updates the edge identified by document-key.</summary>
        /// <remarks>
        /// Partially updates the edge identified by document-key. The value must contain a document with the attributes to
        /// patch (the patch document). All attributes from the patch document will be added to the existing document if they
        /// do not yet exist, and overwritten in the existing document if they do exist there.
        /// </remarks>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#modify-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the edge</param>
        /// <param name="type">The type of the edge-document (POJO class, VPackSlice or String for Json)
        /// 	</param>
        /// <param name="options">Additional options, can be null</param>
        /// <returns>information about the edge</returns>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="com.arangodb.ArangoDBException"/>
        public virtual EdgeUpdateEntity updateEdge<T>(string key, T value
            , EdgeUpdateOptions options)
        {
            return this.executor.execute(this.updateEdgeRequest(key, value, options), this.updateEdgeResponseDeserializer
                (value));
        }

        /// <summary>Removes a edge</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#remove-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the edge</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void deleteEdge(string key)
        {
            this.executor.execute(this.deleteEdgeRequest(key, new EdgeDeleteOptions(
                )), typeof(java.lang.Void));
        }

        /// <summary>Removes a edge</summary>
        /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#remove-an-edge">API Documentation</a>
        /// 	</seealso>
        /// <param name="key">The key of the edge</param>
        /// <param name="options">Additional options, can be null</param>
        /// <exception cref="ArangoDBException"/>
        /// <exception cref="ArangoDBException"/>
        public virtual void deleteEdge(string key, EdgeDeleteOptions options
            )
        {
            this.executor.execute(this.deleteEdgeRequest(key, options), 
                typeof(java.lang.Void));
        }
    }
}