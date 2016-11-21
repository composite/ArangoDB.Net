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
    using System.Collections.Generic;

    /// <author>Mark - mark at arangodb.com</author>
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Management.html#create-a-graph">API Documentation</a>
    /// 	</seealso>
    public class EdgeDefinition
    {
        private ICollection<string> from;

        private ICollection<string> to;

        public virtual string Collection { get; private set; }

        public virtual EdgeDefinition collection(string collection)
        {
            this.Collection = collection;
            return this;
        }

        public virtual ICollection<string> GetFrom()
        {
            return this.from;
        }

        public virtual EdgeDefinition From(params string[] from)
        {
            this.from = new List<string>(from);
            return this;
        }

        public virtual ICollection<string> GetTo()
        {
            return this.to;
        }

        public virtual EdgeDefinition To(params string[] to)
        {
            this.to = new List<string>(to);
            return this;
        }
    }
}