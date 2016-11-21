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
    public class QueryTrackingPropertiesEntity
    {
        private bool enabled;

        private bool trackSlowQueries;

        private long maxSlowQueries;

        private long slowQueryThreshold;

        private long maxQueryStringLength;

        public QueryTrackingPropertiesEntity()
            : base()
        {
        }

        /// <returns>
        /// If set to true, then queries will be tracked. If set to false, neither queries nor slow queries will be
        /// tracked
        /// </returns>
        public virtual bool getEnabled()
        {
            return this.enabled;
        }

        /// <param name="enabled">
        /// If set to true, then queries will be tracked. If set to false, neither queries nor slow queries will
        /// be tracked
        /// </param>
        public virtual void setEnabled(bool enabled)
        {
            this.enabled = enabled;
        }

        /// <returns>
        /// If set to true, then slow queries will be tracked in the list of slow queries if their runtime exceeds
        /// the value set in slowQueryThreshold. In order for slow queries to be tracked, the enabled property must
        /// also be set to true.
        /// </returns>
        public virtual bool getTrackSlowQueries()
        {
            return this.trackSlowQueries;
        }

        /// <param name="trackSlowQueries">
        /// If set to true, then slow queries will be tracked in the list of slow queries if their runtime exceeds
        /// the value set in slowQueryThreshold. In order for slow queries to be tracked, the enabled property
        /// must also be set to true.
        /// </param>
        public virtual void setTrackSlowQueries(bool trackSlowQueries)
        {
            this.trackSlowQueries = trackSlowQueries;
        }

        /// <returns>
        /// The maximum number of slow queries to keep in the list of slow queries. If the list of slow queries is
        /// full, the oldest entry in it will be discarded when additional slow queries occur.
        /// </returns>
        public virtual long getMaxSlowQueries()
        {
            return this.maxSlowQueries;
        }

        /// <param name="maxSlowQueries">
        /// The maximum number of slow queries to keep in the list of slow queries. If the list of slow queries is
        /// full, the oldest entry in it will be discarded when additional slow queries occur.
        /// </param>
        public virtual void setMaxSlowQueries(long maxSlowQueries)
        {
            this.maxSlowQueries = maxSlowQueries;
        }

        /// <returns>
        /// The threshold value for treating a query as slow. A query with a runtime greater or equal to this
        /// threshold value will be put into the list of slow queries when slow query tracking is enabled. The value
        /// for slowQueryThreshold is specified in seconds.
        /// </returns>
        public virtual long getSlowQueryThreshold()
        {
            return this.slowQueryThreshold;
        }

        /// <param name="slowQueryThreshold">
        /// The threshold value for treating a query as slow. A query with a runtime greater or equal to this
        /// threshold value will be put into the list of slow queries when slow query tracking is enabled. The
        /// value for slowQueryThreshold is specified in seconds.
        /// </param>
        public virtual void setSlowQueryThreshold(long slowQueryThreshold)
        {
            this.slowQueryThreshold = slowQueryThreshold;
        }

        /// <returns>
        /// The maximum query string length to keep in the list of queries. Query strings can have arbitrary lengths,
        /// and this property can be used to save memory in case very long query strings are used. The value is
        /// specified in bytes.
        /// </returns>
        public virtual long getMaxQueryStringLength()
        {
            return this.maxQueryStringLength;
        }

        /// <param name="maxQueryStringLength">
        /// The maximum query string length to keep in the list of queries. Query strings can have arbitrary
        /// lengths, and this property can be used to save memory in case very long query strings are used. The
        /// value is specified in bytes.
        /// </param>
        public virtual void setMaxQueryStringLength(long maxQueryStringLength)
        {
            this.maxQueryStringLength = maxQueryStringLength;
        }
    }
}