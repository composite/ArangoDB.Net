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

namespace ArangoDB.Entity
{
    /// <author>Mark - mark at arangodb.com</author>
    /// <seealso><a href= "https://docs.arangodb.com/current/HTTP/Traversal/index.html#executes-a-traversal">API
    /// *      Documentation</a></seealso>
    public class TraversalEntity<V, E>
    {
        private System.Collections.Generic.ICollection<V> vertices;

        private System.Collections.Generic.ICollection<PathEntity<V, E>> paths;

        public TraversalEntity()
            : base()
        {
        }

        public virtual System.Collections.Generic.ICollection<V> getVertices()
        {
            return this.vertices;
        }

        public virtual void setVertices(System.Collections.Generic.ICollection<V> vertices
            )
        {
            this.vertices = vertices;
        }

        public virtual System.Collections.Generic.ICollection<PathEntity<V, E>> getPaths()
        {
            return this.paths;
        }

        public virtual void setPaths(System.Collections.Generic.ICollection<PathEntity<V, E>> paths)
        {
            this.paths = paths;
        }
    }
}