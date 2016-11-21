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

namespace ArangoDB.Model
{
    /// <author>Mark - mark at arangodb.com</author>
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Traversal/index.html">API Documentation</a>
    /// 	</seealso>
    public class TraversalOptions
    {
        public enum Direction
        {
            outbound,
            inbound,
            any
        }

        public enum ItemOrder
        {
            forward,
            backward
        }

        public enum Strategy
        {
            depthfirst,
            breadthfirst
        }

        public enum UniquenessType
        {
            none,
            global,
            path
        }

        public enum Order
        {
            preorder,
            postorder,
            preorder_expander
        }

        private string sort;

        private TraversalOptions.Direction direction;

        private int minDepth;

        private string startVertex;

        private string visitor;

        private TraversalOptions.ItemOrder itemOrder;

        private TraversalOptions.Strategy strategy;

        private string filter;

        private string init;

        private int maxIterations;

        private int maxDepth;

        private TraversalOptions.Uniqueness uniqueness;

        private TraversalOptions.Order order;

        private string graphName;

        private string expander;

        private string edgeCollection;

        public virtual string getSort()
        {
            return sort;
        }

        /// <param name="sort">
        /// JavaScript code of a custom comparison function for the edges. The signature of this function is (l,
        /// r) -&gt; integer (where l and r are edges) and must return -1 if l is smaller than, +1 if l is greater
        /// than, and 0 if l and r are equal. The reason for this is the following: The order of edges returned
        /// for a certain vertex is undefined. This is because there is no natural order of edges for a vertex
        /// with multiple connected edges. To explicitly define the order in which edges on the vertex are
        /// followed, you can specify an edge comparator function with this attribute. Note that the value here
        /// has to be a string to conform to the JSON standard, which in turn is parsed as function body on the
        /// server side. Furthermore note that this attribute is only used for the standard expanders. If you use
        /// your custom expander you have to do the sorting yourself within the expander code.
        /// </param>
        /// <returns>options</returns>
        public virtual TraversalOptions sort(string sort)
        {
            this.sort = sort;
            return this;
        }

        public virtual TraversalOptions.Direction getDirection()
        {
            return direction;
        }

        /// <param name="direction">
        /// direction for traversal
        /// if set, must be either "outbound", "inbound", or "any"
        /// if not set, the expander attribute must be specified
        /// </param>
        /// <returns>options</returns>
        public virtual TraversalOptions direction(TraversalOptions.Direction
             direction)
        {
            this.direction = direction;
            return this;
        }

        public virtual int getMinDepth()
        {
            return minDepth;
        }

        /// <param name="minDepth">ANDed with any existing filters): visits only nodes in at least the given depth
        /// 	</param>
        /// <returns>options</returns>
        public virtual TraversalOptions minDepth(int minDepth)
        {
            this.minDepth = minDepth;
            return this;
        }

        public virtual string getStartVertex()
        {
            return startVertex;
        }

        /// <param name="startVertex">The id of the startVertex, e.g. "users/foo".</param>
        /// <returns>options</returns>
        public virtual TraversalOptions startVertex(string startVertex
            )
        {
            this.startVertex = startVertex;
            return this;
        }

        public virtual string getVisitor()
        {
            return visitor;
        }

        /// <param name="visitor">
        /// JavaScript code of custom visitor function function signature: (config, result, vertex, path,
        /// connected) -&gt; void The visitor function can do anything, but its return value is ignored. To populate
        /// a result, use the result variable by reference. Note that the connected argument is only populated
        /// when the order attribute is set to "preorder-expander".
        /// </param>
        /// <returns>options</returns>
        public virtual TraversalOptions visitor(string visitor)
        {
            this.visitor = visitor;
            return this;
        }

        public virtual TraversalOptions.ItemOrder getItemOrder()
        {
            return itemOrder;
        }

        /// <param name="itemOrder">The item iteration order can be "forward" or "backward"</param>
        /// <returns>options</returns>
        public virtual TraversalOptions itemOrder(TraversalOptions.ItemOrder
             itemOrder)
        {
            this.itemOrder = itemOrder;
            return this;
        }

        public virtual TraversalOptions.Strategy getStrategy()
        {
            return strategy;
        }

        /// <param name="strategy">The traversal strategy can be "depthfirst" or "breadthfirst"
        /// 	</param>
        /// <returns>options</returns>
        public virtual TraversalOptions strategy(TraversalOptions.Strategy
             strategy)
        {
            this.strategy = strategy;
            return this;
        }

        public virtual string getFilter()
        {
            return filter;
        }

        /// <param name="filter">
        /// default is to include all nodes: body (JavaScript code) of custom filter function function signature:
        /// (config, vertex, path) -&gt; mixed can return four different string values:
        /// "exclude" -&gt; this vertex will not be visited.
        /// "prune" -&gt; the edges of this vertex will not be followed.
        /// "" or undefined -&gt; visit the vertex and follow it's edges.
        /// Array -&gt; containing any combination of the above.
        /// If there is at least one "exclude" or "prune" respectivly is contained, it's effect will occur.
        /// </param>
        /// <returns>options</returns>
        public virtual TraversalOptions filter(string filter)
        {
            this.filter = filter;
            return this;
        }

        public virtual string getInit()
        {
            return init;
        }

        /// <param name="init">
        /// JavaScript code of custom result initialization function function signature: (config, result) -&gt; void
        /// initialize any values in result with what is required
        /// </param>
        /// <returns>options</returns>
        public virtual TraversalOptions init(string init)
        {
            this.init = init;
            return this;
        }

        public virtual int getMaxIterations()
        {
            return maxIterations;
        }

        /// <param name="maxIterations">
        /// Maximum number of iterations in each traversal. This number can be set to prevent endless loops in
        /// traversal of cyclic graphs. When a traversal performs as many iterations as the maxIterations value,
        /// the traversal will abort with an error. If maxIterations is not set, a server-defined value may be
        /// used.
        /// </param>
        /// <returns>options</returns>
        public virtual TraversalOptions maxIterations(int maxIterations
            )
        {
            this.maxIterations = maxIterations;
            return this;
        }

        public virtual int getMaxDepth()
        {
            return maxDepth;
        }

        /// <param name="maxDepth">ANDed with any existing filters visits only nodes in at most the given depth.
        /// 	</param>
        /// <returns>options</returns>
        public virtual TraversalOptions maxDepth(int maxDepth)
        {
            this.maxDepth = maxDepth;
            return this;
        }

        public virtual TraversalOptions.UniquenessType getVerticesUniqueness
            ()
        {
            return this.uniqueness != null ? this.uniqueness.vertices : null;
        }

        /// <param name="vertices">Specifies uniqueness for vertices can be "none", "global" or "path"
        /// 	</param>
        /// <returns>options</returns>
        public virtual TraversalOptions verticesUniqueness(TraversalOptions.UniquenessType
             vertices)
        {
            this.getUniqueness().setVertices(vertices);
            return this;
        }

        public virtual TraversalOptions.UniquenessType getEdgesUniqueness
            ()
        {
            return this.uniqueness != null ? this.uniqueness.edges : null;
        }

        /// <param name="edges">Specifies uniqueness for edges can be "none", "global" or "path"
        /// 	</param>
        /// <returns>options</returns>
        public virtual TraversalOptions edgesUniqueness(TraversalOptions.UniquenessType
             edges)
        {
            this.getUniqueness().setEdges(edges);
            return this;
        }

        public virtual TraversalOptions.Order getOrder()
        {
            return order;
        }

        /// <param name="order">The traversal order can be "preorder", "postorder" or "preorder-expander"
        /// 	</param>
        /// <returns>options</returns>
        public virtual TraversalOptions order(TraversalOptions.Order
             order)
        {
            this.order = order;
            return this;
        }

        public virtual string getGraphName()
        {
            return graphName;
        }

        /// <param name="graphName">
        /// The name of the graph that contains the edges. Either edgeCollection or graphName has to be given. In
        /// case both values are set the graphName is prefered.
        /// </param>
        /// <returns>options</returns>
        public virtual TraversalOptions graphName(string graphName)
        {
            this.graphName = graphName;
            return this;
        }

        public virtual string getExpander()
        {
            return expander;
        }

        /// <param name="expander">
        /// JavaScript code of custom expander function must be set if direction attribute is not set function
        /// signature: (config, vertex, path) -&gt; array expander must return an array of the connections for vertex
        /// each connection is an object with the attributes edge and vertex
        /// </param>
        /// <returns>options</returns>
        public virtual TraversalOptions expander(string expander)
        {
            this.expander = expander;
            return this;
        }

        public virtual string getEdgeCollection()
        {
            return edgeCollection;
        }

        /// <param name="edgeCollection">The name of the collection that contains the edges.</param>
        /// <returns>options</returns>
        public virtual TraversalOptions edgeCollection(string edgeCollection
            )
        {
            this.edgeCollection = edgeCollection;
            return this;
        }

        public class Uniqueness
        {
            private TraversalOptions.UniquenessType vertices;

            private TraversalOptions.UniquenessType edges;

            public virtual TraversalOptions.UniquenessType getVertices()
            {
                return this.vertices;
            }

            public virtual void setVertices(TraversalOptions.UniquenessType
                 vertices)
            {
                this.vertices = vertices;
            }

            public virtual TraversalOptions.UniquenessType getEdges()
            {
                return this.edges;
            }

            public virtual void setEdges(TraversalOptions.UniquenessType edges
                )
            {
                this.edges = edges;
            }
        }

        private TraversalOptions.Uniqueness getUniqueness()
        {
            if (this.uniqueness == null)
            {
                this.uniqueness = new TraversalOptions.Uniqueness();
                this.uniqueness.vertices = TraversalOptions.UniquenessType.none;
                this.uniqueness.edges = TraversalOptions.UniquenessType.none;
            }
            return this.uniqueness;
        }
    }
}