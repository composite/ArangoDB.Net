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
    /// <seealso><a href=
    /// *      "https://docs.arangodb.com/current/HTTP/AqlQueryCache/index.html#http-interface-for-the-aql-query-cache">API
    /// *      Documentation</a></seealso>
    public class QueryCachePropertiesEntity
    {
        public enum CacheMode
        {
            off,
            on,
            demand
        }

        private QueryCachePropertiesEntity.CacheMode mode;

        private long maxResults;

        public QueryCachePropertiesEntity()
            : base()
        {
        }

        /// <returns>the mode the AQL query cache operates in. The mode is one of the following values: off, on or demand
        /// 	</returns>
        public virtual QueryCachePropertiesEntity.CacheMode getMode()
        {
            return this.mode;
        }

        /// <param name="mode">the mode the AQL query cache operates in. The mode is one of the following values: off, on or demand
        /// 	</param>
        public virtual void setMode(QueryCachePropertiesEntity.CacheMode
             mode)
        {
            this.mode = mode;
        }

        /// <returns>the maximum number of query results that will be stored per database-specific cache
        /// 	</returns>
        public virtual long getMaxResults()
        {
            return this.maxResults;
        }

        /// <param name="maxResults">the maximum number of query results that will be stored per database-specific cache
        /// 	</param>
        public virtual void setMaxResults(long maxResults)
        {
            this.maxResults = maxResults;
        }
    }
}