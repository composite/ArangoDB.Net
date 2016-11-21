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
    using global::ArangoDB.Entity;

    /// <author>Mark - mark at arangodb.com</author>
	/// <seealso><a href=
	/// *      "https://docs.arangodb.com/current/HTTP/AdministrationAndMonitoring/index.html#read-global-logs-from-the-server">API
	/// *      Documentation</a></seealso>
	public class LogOptions
    {
        public const string PROPERTY_UPTO = "upto";

        public const string PROPERTY_LEVEL = "level";

        public const string PROPERTY_START = "start";

        public const string PROPERTY_SIZE = "size";

        public const string PROPERTY_OFFSET = "offset";

        public const string PROPERTY_SEARCH = "search";

        public const string PROPERTY_SORT = "sort";

        public enum SortOrder
        {
            asc,
            desc
        }

        private LogLevel upto;

        private LogLevel level;

        private long start;

        private int size;

        private int offset;

        private string search;

        private LogOptions.SortOrder sort;

        public LogOptions()
            : base()
        {
        }

        public virtual LogLevel getUpto()
        {
            return upto;
        }

        /// <param name="upto">Returns all log entries up to log level upto</param>
        /// <returns>options</returns>
        public virtual LogOptions upto(LogLevel upto
            )
        {
            this.upto = upto;
            return this;
        }

        public virtual LogLevel getLevel()
        {
            return level;
        }

        /// <param name="level">
        /// Returns all log entries of log level level. Note that the query parameters upto and level are mutually
        /// exclusive
        /// </param>
        /// <returns>options</returns>
        public virtual LogOptions level(LogLevel level
            )
        {
            this.level = level;
            return this;
        }

        public virtual long getStart()
        {
            return start;
        }

        /// <param name="start">Returns all log entries such that their log entry identifier (lid value) is greater or equal to start
        /// 	</param>
        /// <returns>options</returns>
        public virtual LogOptions start(long start)
        {
            this.start = start;
            return this;
        }

        public virtual int getSize()
        {
            return size;
        }

        /// <param name="size">Restricts the result to at most size log entries</param>
        /// <returns>options</returns>
        public virtual LogOptions size(int size)
        {
            this.size = size;
            return this;
        }

        public virtual int getOffset()
        {
            return offset;
        }

        /// <param name="offset">
        /// Starts to return log entries skipping the first offset log entries. offset and size can be used for
        /// pagination
        /// </param>
        /// <returns>options</returns>
        public virtual LogOptions offset(int offset)
        {
            this.offset = offset;
            return this;
        }

        public virtual string getSearch()
        {
            return search;
        }

        /// <param name="search">Only return the log entries containing the text specified in search
        /// 	</param>
        /// <returns>options</returns>
        public virtual LogOptions search(string search)
        {
            this.search = search;
            return this;
        }

        public virtual LogOptions.SortOrder getSort()
        {
            return sort;
        }

        /// <param name="sort">
        /// Sort the log entries either ascending (if sort is asc) or descending (if sort is desc) according to
        /// their lid values. Note that the lid imposes a chronological order. The default value is asc
        /// </param>
        /// <returns>options</returns>
        public virtual LogOptions sort(LogOptions.SortOrder
             sort)
        {
            this.sort = sort;
            return this;
        }
    }
}