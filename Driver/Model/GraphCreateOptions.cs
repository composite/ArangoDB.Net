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
    using global::ArangoDB.Entity;

    /// <author>Mark - mark at arangodb.com</author>
	/// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#create-a-graph">API Documentation</a>
	/// 	</seealso>
	public class GraphCreateOptions
    {
        private string name;

        private System.Collections.Generic.ICollection<EdgeDefinition
            > edgeDefinitions;

        private System.Collections.Generic.ICollection<string> orphanCollections;

        private bool isSmart;

        private GraphCreateOptions.SmartOptions options;

        public GraphCreateOptions()
            : base()
        {
        }

        protected internal virtual string getName()
        {
            return name;
        }

        /// <param name="name">Name of the graph</param>
        /// <returns>options</returns>
        protected internal virtual GraphCreateOptions name(string name
            )
        {
            this.name = name;
            return this;
        }

        public virtual System.Collections.Generic.ICollection<EdgeDefinition
            > getEdgeDefinitions()
        {
            return edgeDefinitions;
        }

        /// <param name="edgeDefinitions">An array of definitions for the edge</param>
        /// <returns>options</returns>
        protected internal virtual GraphCreateOptions edgeDefinitions(
            System.Collections.Generic.ICollection<EdgeDefinition> edgeDefinitions
            )
        {
            this.edgeDefinitions = edgeDefinitions;
            return this;
        }

        public virtual System.Collections.Generic.ICollection<string> getOrphanCollections
            ()
        {
            return orphanCollections;
        }

        /// <param name="orphanCollections">Additional vertex collections</param>
        /// <returns>options</returns>
        public virtual GraphCreateOptions orphanCollections(params string
            [] orphanCollections)
        {
            this.orphanCollections = java.util.Arrays.asList(orphanCollections);
            return this;
        }

        public virtual bool getIsSmart()
        {
            return isSmart;
        }

        /// <param name="isSmart">Define if the created graph should be smart. This only has effect in Enterprise version.
        /// 	</param>
        /// <returns>options</returns>
        public virtual GraphCreateOptions isSmart(bool isSmart)
        {
            this.isSmart = isSmart;
            return this;
        }

        public virtual int getNumberOfShards()
        {
            return this.getOptions().getNumberOfShards();
        }

        /// <param name="numberOfShards">The number of shards that is used for every collection within this graph. Cannot be modified later.
        /// 	</param>
        /// <returns>options</returns>
        public virtual GraphCreateOptions numberOfShards(int numberOfShards
            )
        {
            this.getOptions().setNumberOfShards(numberOfShards);
            return this;
        }

        public virtual string getSmartGraphAttribute()
        {
            return this.getOptions().getSmartGraphAttribute();
        }

        /// <param name="smartGraphAttribute">
        /// The attribute name that is used to smartly shard the vertices of a graph. Every vertex in this Graph
        /// has to have this attribute. Cannot be modified later.
        /// </param>
        /// <returns>options</returns>
        public virtual GraphCreateOptions smartGraphAttribute(string smartGraphAttribute
            )
        {
            this.getOptions().setSmartGraphAttribute(smartGraphAttribute);
            return this;
        }

        private GraphCreateOptions.SmartOptions getOptions()
        {
            if (this.options == null)
            {
                this.options = new GraphCreateOptions.SmartOptions();
            }
            return this.options;
        }

        public class SmartOptions
        {
            private int numberOfShards;

            private string smartGraphAttribute;

            public SmartOptions()
                : base()
            {
            }

            public virtual int getNumberOfShards()
            {
                return this.numberOfShards;
            }

            public virtual void setNumberOfShards(int numberOfShards)
            {
                this.numberOfShards = numberOfShards;
            }

            public virtual string getSmartGraphAttribute()
            {
                return this.smartGraphAttribute;
            }

            public virtual void setSmartGraphAttribute(string smartGraphAttribute)
            {
                this.smartGraphAttribute = smartGraphAttribute;
            }
        }
    }
}