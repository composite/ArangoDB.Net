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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlQueryCursor/AccessingCursors.html#create-cursor">API
    /// *      Documentation</a></seealso>
    public class AqlQueryOptions
    {
        private bool count;

        private int ttl;

        private int batchSize;

        private bool cache;

        private System.Collections.Generic.IDictionary<string, object> bindVars;

        private string query;

        private AqlQueryOptions.Options options;

        public AqlQueryOptions()
            : base()
        {
        }

        public virtual bool getCount()
        {
            return count;
        }

        /// <param name="count">
        /// indicates whether the number of documents in the result set should be returned in the "count"
        /// attribute of the result. Calculating the "count" attribute might have a performance impact for some
        /// queries in the future so this option is turned off by default, and "count" is only returned when
        /// requested.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryOptions count(bool count)
        {
            this.count = count;
            return this;
        }

        public virtual int getTtl()
        {
            return ttl;
        }

        /// <param name="ttl">
        /// The time-to-live for the cursor (in seconds). The cursor will be removed on the server automatically
        /// after the specified amount of time. This is useful to ensure garbage collection of cursors that are
        /// not fully fetched by clients. If not set, a server-defined value will be used.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryOptions ttl(int ttl)
        {
            this.ttl = ttl;
            return this;
        }

        public virtual int getBatchSize()
        {
            return batchSize;
        }

        /// <param name="batchSize">
        /// maximum number of result documents to be transferred from the server to the client in one roundtrip.
        /// If this attribute is not set, a server-controlled default value will be used. A batchSize value of 0
        /// is disallowed.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryOptions batchSize(int batchSize)
        {
            this.batchSize = batchSize;
            return this;
        }

        public virtual bool getCache()
        {
            return cache;
        }

        /// <param name="cache">
        /// flag to determine whether the AQL query cache shall be used. If set to false, then any query cache
        /// lookup will be skipped for the query. If set to true, it will lead to the query cache being checked
        /// for the query if the query cache mode is either on or demand.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryOptions cache(bool cache)
        {
            this.cache = cache;
            return this;
        }

        protected internal virtual System.Collections.Generic.IDictionary<string, object>
             getBindVars()
        {
            return bindVars;
        }

        /// <param name="bindVars">key/value pairs representing the bind parameters</param>
        /// <returns>options</returns>
        protected internal virtual AqlQueryOptions bindVars(System.Collections.Generic.IDictionary
            <string, object> bindVars)
        {
            this.bindVars = bindVars;
            return this;
        }

        protected internal virtual string getQuery()
        {
            return query;
        }

        /// <param name="query">the query which you want parse</param>
        /// <returns>options</returns>
        protected internal virtual AqlQueryOptions query(string query)
        {
            this.query = query;
            return this;
        }

        /// <returns>
        /// If set to true, then the additional query profiling information will be returned in the sub-attribute
        /// profile of the extra return attribute if the query result is not served from the query cache.
        /// </returns>
        public virtual bool getProfile()
        {
            return this.options != null ? this.options.profile : null;
        }

        /// <param name="profile">
        /// If set to true, then the additional query profiling information will be returned in the sub-attribute
        /// profile of the extra return attribute if the query result is not served from the query cache.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryOptions profile(bool profile)
        {
            this.getOptions().profile = profile;
            return this;
        }

        public virtual bool getFullCount()
        {
            return this.options != null ? this.options.fullCount : null;
        }

        /// <param name="fullCount">
        /// if set to true and the query contains a LIMIT clause, then the result will have an extra attribute
        /// with the sub-attributes stats and fullCount, { ... , "extra": { "stats": { "fullCount": 123 } } }. The
        /// fullCount attribute will contain the number of documents in the result before the last LIMIT in the
        /// query was applied. It can be used to count the number of documents that match certain filter criteria,
        /// but only return a subset of them, in one go. It is thus similar to MySQL's SQL_CALC_FOUND_ROWS hint.
        /// Note that setting the option will disable a few LIMIT optimizations and may lead to more documents
        /// being processed, and thus make queries run longer. Note that the fullCount attribute will only be
        /// present in the result if the query has a LIMIT clause and the LIMIT clause is actually used in the
        /// query.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryOptions fullCount(bool fullCount)
        {
            this.getOptions().fullCount = fullCount;
            return this;
        }

        public virtual int getMaxPlans()
        {
            return this.options != null ? this.options.maxPlans : null;
        }

        /// <param name="maxPlans">Limits the maximum number of plans that are created by the AQL query optimizer.
        /// 	</param>
        /// <returns>options</returns>
        public virtual AqlQueryOptions maxPlans(int maxPlans)
        {
            this.getOptions().maxPlans = maxPlans;
            return this;
        }

        public virtual System.Collections.Generic.ICollection<string> getRules()
        {
            return this.options != null ? this.options.optimizer != null ? this.options.optimizer.rules : null
                 : null;
        }

        /// <param name="rules">
        /// A list of to-be-included or to-be-excluded optimizer rules can be put into this attribute, telling the
        /// optimizer to include or exclude specific rules. To disable a rule, prefix its name with a -, to enable
        /// a rule, prefix it with a +. There is also a pseudo-rule all, which will match all optimizer rules
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryOptions rules(System.Collections.Generic.ICollection
            <string> rules)
        {
            this.getOptions().getOptimizer().rules = rules;
            return this;
        }

        private AqlQueryOptions.Options getOptions()
        {
            if (this.options == null)
            {
                this.options = new AqlQueryOptions.Options();
            }
            return this.options;
        }

        private class Options
        {
            private bool profile;

            private AqlQueryOptions.Optimizer optimizer;

            private bool fullCount;

            private int maxPlans;

            protected internal virtual AqlQueryOptions.Optimizer getOptimizer
                ()
            {
                if (this.optimizer == null)
                {
                    this.optimizer = new AqlQueryOptions.Optimizer();
                }
                return this.optimizer;
            }
        }

        private class Optimizer
        {
            private System.Collections.Generic.ICollection<string> rules;
        }
    }
}