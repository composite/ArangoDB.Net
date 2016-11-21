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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#explain-an-aql-query">API Documentation</a>
    /// 	</seealso>
    public class AqlQueryExplainOptions
    {
        private System.Collections.Generic.IDictionary<string, object> bindVars;

        private string query;

        private AqlQueryExplainOptions.Options options;

        public AqlQueryExplainOptions()
            : base()
        {
        }

        protected internal virtual System.Collections.Generic.IDictionary<string, object>
             getBindVars()
        {
            return bindVars;
        }

        /// <param name="bindVars">key/value pairs representing the bind parameters</param>
        /// <returns>options</returns>
        protected internal virtual AqlQueryExplainOptions bindVars(System.Collections.Generic.IDictionary
            <string, object> bindVars)
        {
            this.bindVars = bindVars;
            return this;
        }

        protected internal virtual string getQuery()
        {
            return query;
        }

        /// <param name="query">the query which you want explained</param>
        /// <returns>options</returns>
        protected internal virtual AqlQueryExplainOptions query(string
             query)
        {
            this.query = query;
            return this;
        }

        public virtual int getMaxNumberOfPlans()
        {
            return this.getOptions().maxNumberOfPlans;
        }

        /// <param name="maxNumberOfPlans">
        /// an optional maximum number of plans that the optimizer is allowed to generate. Setting this attribute
        /// to a low value allows to put a cap on the amount of work the optimizer does.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryExplainOptions maxNumberOfPlans(int maxNumberOfPlans
            )
        {
            this.getOptions().maxNumberOfPlans = maxNumberOfPlans;
            return this;
        }

        public virtual bool getAllPlans()
        {
            return this.getOptions().allPlans;
        }

        /// <param name="allPlans">
        /// if set to true, all possible execution plans will be returned. The default is false, meaning only the
        /// optimal plan will be returned.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryExplainOptions allPlans(bool allPlans)
        {
            this.getOptions().allPlans = allPlans;
            return this;
        }

        public virtual System.Collections.Generic.ICollection<string> getRules()
        {
            return this.getOptions().getOptimizer().rules;
        }

        /// <param name="rules">
        /// an array of to-be-included or to-be-excluded optimizer rules can be put into this attribute, telling
        /// the optimizer to include or exclude specific rules.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlQueryExplainOptions rules(System.Collections.Generic.ICollection
            <string> rules)
        {
            this.getOptions().getOptimizer().rules = rules;
            return this;
        }

        private AqlQueryExplainOptions.Options getOptions()
        {
            if (this.options == null)
            {
                this.options = new AqlQueryExplainOptions.Options();
            }
            return this.options;
        }

        private class Options
        {
            private AqlQueryExplainOptions.Optimizer optimizer;

            private int maxNumberOfPlans;

            private bool allPlans;

            protected internal virtual AqlQueryExplainOptions.Optimizer getOptimizer
                ()
            {
                if (this.optimizer == null)
                {
                    this.optimizer = new AqlQueryExplainOptions.Optimizer();
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