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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlQuery/index.html#explain-an-aql-query">API Documentation</a>
    /// 	</seealso>
    public class AqlExecutionExplainEntity
    {
        public class ExecutionPlan
        {
            private System.Collections.Generic.ICollection<ExecutionNode
                > nodes;

            private System.Collections.Generic.ICollection<string> rules;

            private System.Collections.Generic.ICollection<ExecutionCollection
                > collections;

            private System.Collections.Generic.ICollection<ExecutionVariable
                > variables;

            private int estimatedCost;

            private int estimatedNrItems;

            public virtual System.Collections.Generic.ICollection<ExecutionNode
                > getNodes()
            {
                return this.nodes;
            }

            public virtual System.Collections.Generic.ICollection<string> getRules()
            {
                return this.rules;
            }

            public virtual System.Collections.Generic.ICollection<ExecutionCollection
                > getCollections()
            {
                return this.collections;
            }

            public virtual System.Collections.Generic.ICollection<ExecutionVariable
                > getVariables()
            {
                return this.variables;
            }

            public virtual int getEstimatedCost()
            {
                return this.estimatedCost;
            }

            public virtual int getEstimatedNrItems()
            {
                return this.estimatedNrItems;
            }
        }

        public class ExecutionNode
        {
            private string type;

            private System.Collections.Generic.ICollection<long> dependencies;

            private long id;

            private int estimatedCost;

            private int estimatedNrItems;

            private long depth;

            private string database;

            private string collection;

            private AqlExecutionExplainEntity.ExecutionVariable inVariable;

            private AqlExecutionExplainEntity.ExecutionVariable outVariable;

            private AqlExecutionExplainEntity.ExecutionVariable conditionVariable;

            private bool random;

            private long offset;

            private long limit;

            private bool fullCount;

            private AqlExecutionExplainEntity.ExecutionNode subquery;

            private bool isConst;

            private bool canThrow;

            private string expressionType;

            private IndexEntity indexes;

            private AqlExecutionExplainEntity.ExecutionExpression expression;

            private AqlExecutionExplainEntity.ExecutionCollection condition;

            private bool reverse;

            public virtual string getType()
            {
                return this.type;
            }

            public virtual System.Collections.Generic.ICollection<long> getDependencies()
            {
                return this.dependencies;
            }

            public virtual long getId()
            {
                return this.id;
            }

            public virtual int getEstimatedCost()
            {
                return this.estimatedCost;
            }

            public virtual int getEstimatedNrItems()
            {
                return this.estimatedNrItems;
            }

            public virtual long getDepth()
            {
                return this.depth;
            }

            public virtual string getDatabase()
            {
                return this.database;
            }

            public virtual string getCollection()
            {
                return this.collection;
            }

            public virtual AqlExecutionExplainEntity.ExecutionVariable getInVariable
                ()
            {
                return this.inVariable;
            }

            public virtual AqlExecutionExplainEntity.ExecutionVariable getOutVariable
                ()
            {
                return this.outVariable;
            }

            public virtual AqlExecutionExplainEntity.ExecutionVariable getConditionVariable
                ()
            {
                return this.conditionVariable;
            }

            public virtual bool getRandom()
            {
                return this.random;
            }

            public virtual long getOffset()
            {
                return this.offset;
            }

            public virtual long getLimit()
            {
                return this.limit;
            }

            public virtual bool getFullCount()
            {
                return this.fullCount;
            }

            public virtual AqlExecutionExplainEntity.ExecutionNode getSubquery
                ()
            {
                return this.subquery;
            }

            public virtual bool getIsConst()
            {
                return this.isConst;
            }

            public virtual bool getCanThrow()
            {
                return this.canThrow;
            }

            public virtual string getExpressionType()
            {
                return this.expressionType;
            }

            public virtual IndexEntity getIndexes()
            {
                return this.indexes;
            }

            public virtual AqlExecutionExplainEntity.ExecutionExpression
                getExpression()
            {
                return this.expression;
            }

            public virtual AqlExecutionExplainEntity.ExecutionCollection
                getCondition()
            {
                return this.condition;
            }

            public virtual bool getReverse()
            {
                return this.reverse;
            }
        }

        public class ExecutionVariable
        {
            private long id;

            private string name;

            public virtual long getId()
            {
                return this.id;
            }

            public virtual string getName()
            {
                return this.name;
            }
        }

        public class ExecutionExpression
        {
            private string type;

            private string name;

            private long id;

            private object value;

            private bool sorted;

            private string quantifier;

            private System.Collections.Generic.ICollection<long> levels;

            private System.Collections.Generic.ICollection<ExecutionExpression
                > subNodes;

            public virtual string getType()
            {
                return this.type;
            }

            public virtual string getName()
            {
                return this.name;
            }

            public virtual long getId()
            {
                return this.id;
            }

            public virtual object getValue()
            {
                return this.value;
            }

            public virtual bool getSorted()
            {
                return this.sorted;
            }

            public virtual string getQuantifier()
            {
                return this.quantifier;
            }

            public virtual System.Collections.Generic.ICollection<long> getLevels()
            {
                return this.levels;
            }

            public virtual System.Collections.Generic.ICollection<ExecutionExpression
                > getSubNodes()
            {
                return this.subNodes;
            }
        }

        public class ExecutionCollection
        {
            private string name;

            private string type;

            public virtual string getName()
            {
                return this.name;
            }

            public virtual string getType()
            {
                return this.type;
            }
        }

        public class ExecutionStats
        {
            private int rulesExecuted;

            private int rulesSkipped;

            private int plansCreated;

            public virtual int getRulesExecuted()
            {
                return this.rulesExecuted;
            }

            public virtual int getRulesSkipped()
            {
                return this.rulesSkipped;
            }

            public virtual int getPlansCreated()
            {
                return this.plansCreated;
            }
        }

        private AqlExecutionExplainEntity.ExecutionPlan plan;

        private System.Collections.Generic.ICollection<ExecutionPlan
            > plans;

        private System.Collections.Generic.ICollection<string> warnings;

        private AqlExecutionExplainEntity.ExecutionStats stats;

        private bool cacheable;

        public virtual AqlExecutionExplainEntity.ExecutionPlan getPlan
            ()
        {
            return this.plan;
        }

        public virtual System.Collections.Generic.ICollection<ExecutionPlan
            > getPlans()
        {
            return this.plans;
        }

        public virtual System.Collections.Generic.ICollection<string> getWarnings()
        {
            return this.warnings;
        }

        public virtual AqlExecutionExplainEntity.ExecutionStats getStats
            ()
        {
            return this.stats;
        }

        public virtual bool getCacheable()
        {
            return this.cacheable;
        }
    }
}