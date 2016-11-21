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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Getting.html#reads-all-collections">API
    /// *      Documentation</a></seealso>
    public class CollectionsReadOptions
    {
        private bool excludeSystem;

        public CollectionsReadOptions()
            : base()
        {
        }

        public virtual bool getExcludeSystem()
        {
            return excludeSystem;
        }

        /// <param name="excludeSystem">Whether or not system collections should be excluded from the result.
        /// 	</param>
        /// <returns>options</returns>
        public virtual CollectionsReadOptions excludeSystem(bool excludeSystem
            )
        {
            this.excludeSystem = excludeSystem;
            return this;
        }
    }
}