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
    using global::ArangoDB.Velocypack;

    /// <author>Mark - mark at arangodb.com</author>
	/// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlQueryCursor/AccessingCursors.html#create-cursor">API
	/// *      Documentation</a></seealso>
	public class CursorEntity
    {
        private string id;

        private int count;

        private CursorEntity.Extras extra;

        private bool cached;

        private bool hasMore;

        private VPackSlice result;

        public virtual string getId()
        {
            return this.id;
        }

        /// <returns>
        /// the total number of result documents available (only available if the query was executed with the count
        /// attribute set)
        /// </returns>
        public virtual int getCount()
        {
            return this.count;
        }

        /// <returns>
        /// an optional object with extra information about the query result contained in its stats sub-attribute.
        /// For data-modification queries, the extra.stats sub-attribute will contain the number of modified
        /// documents and the number of documents that could not be modified due to an error (if ignoreErrors query
        /// option is specified)
        /// </returns>
        public virtual CursorEntity.Extras getExtra()
        {
            return this.extra;
        }

        /// <returns>
        /// a boolean flag indicating whether the query result was served from the query cache or not. If the query
        /// result is served from the query cache, the extra return attribute will not contain any stats
        /// sub-attribute and no profile sub-attribute.
        /// </returns>
        public virtual bool getCached()
        {
            return this.cached;
        }

        /// <returns>A boolean indicator whether there are more results available for the cursor on the server
        /// 	</returns>
        public virtual bool getHasMore()
        {
            return this.hasMore;
        }

        /// <returns>an vpack-array of result documents (might be empty if query has no results)
        /// 	</returns>
        public virtual VPackSlice getResult()
        {
            return this.result;
        }

        public class Warning
        {
            private int code;

            private string message;

            public virtual int getCode()
            {
                return this.code;
            }

            public virtual string getMessage()
            {
                return this.message;
            }
        }

        public class Extras
        {
            private CursorEntity.Stats stats;

            private System.Collections.Generic.ICollection<Warning
                > warnings;

            public virtual CursorEntity.Stats getStats()
            {
                return this.stats;
            }

            public virtual System.Collections.Generic.ICollection<Warning
                > getWarnings()
            {
                return this.warnings;
            }
        }

        public class Stats
        {
            private long writesExecuted;

            private long writesIgnored;

            private long scannedFull;

            private long scannedIndex;

            private long filtered;

            private long fullCount;

            private double executionTime;

            public virtual long getWritesExecuted()
            {
                return this.writesExecuted;
            }

            public virtual long getWritesIgnored()
            {
                return this.writesIgnored;
            }

            public virtual long getScannedFull()
            {
                return this.scannedFull;
            }

            public virtual long getScannedIndex()
            {
                return this.scannedIndex;
            }

            public virtual long getFiltered()
            {
                return this.filtered;
            }

            public virtual long getFullCount()
            {
                return this.fullCount;
            }

            public virtual double getExecutionTime()
            {
                return this.executionTime;
            }
        }
    }
}