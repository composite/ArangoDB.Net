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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#create-a-graph">API Documentation</a>
    /// 	</seealso>
    public class GraphEntity
    {
        private string name;

        private System.Collections.Generic.ICollection<EdgeDefinition
            > edgeDefinitions;

        private System.Collections.Generic.ICollection<string> orphanCollections;

        private bool isSmart;

        private int numberOfShards;

        private string smartGraphAttribute;

        public virtual string getName()
        {
            return this.name;
        }

        public virtual System.Collections.Generic.ICollection<EdgeDefinition
            > getEdgeDefinitions()
        {
            return this.edgeDefinitions;
        }

        public virtual System.Collections.Generic.ICollection<string> getOrphanCollections
            ()
        {
            return this.orphanCollections;
        }

        public virtual bool getIsSmart()
        {
            return this.isSmart;
        }

        public virtual int getNumberOfShards()
        {
            return this.numberOfShards;
        }

        public virtual string getSmartGraphAttribute()
        {
            return this.smartGraphAttribute;
        }
    }
}