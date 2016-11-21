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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Creating.html">API Documentation</a>
    /// 	</seealso>
    public class CollectionEntity
    {
        private string id;

        private string name;

        private bool waitForSync;

        private bool isVolatile;

        private bool isSystem;

        private CollectionStatus status;

        private CollectionType type;

        public CollectionEntity()
            : base()
        {
        }

        public virtual string getId()
        {
            return this.id;
        }

        public virtual string getName()
        {
            return this.name;
        }

        public virtual bool getWaitForSync()
        {
            return this.waitForSync;
        }

        public virtual bool getIsVolatile()
        {
            return this.isVolatile;
        }

        public virtual bool getIsSystem()
        {
            return this.isSystem;
        }

        public virtual CollectionStatus getStatus()
        {
            return this.status;
        }

        public virtual CollectionType getType()
        {
            return this.type;
        }
    }
}