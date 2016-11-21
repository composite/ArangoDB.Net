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
    /// *      "https://docs.arangodb.com/current/HTTP/AdministrationAndMonitoring/index.html#read-global-logs-from-the-server">API
    /// *      Documentation</a></seealso>
    public class LogEntity
    {
        private System.Collections.Generic.IList<long> lid;

        private System.Collections.Generic.IList<LogLevel> level;

        private System.Collections.Generic.IList<long> timestamp;

        private System.Collections.Generic.IList<string> text;

        private long totalAmount;

        /// <returns>
        /// a list of log entry identifiers. Each log message is uniquely identified by its @LIT{lid} and the
        /// identifiers are in ascending order
        /// </returns>
        public virtual System.Collections.Generic.IList<long> getLid()
        {
            return this.lid;
        }

        /// <returns>a list of the log-levels for all log entries</returns>
        public virtual System.Collections.Generic.IList<LogLevel> getLevel
            ()
        {
            return this.level;
        }

        /// <returns>a list of the timestamps as seconds since 1970-01-01 for all log entries
        /// 	</returns>
        public virtual System.Collections.Generic.IList<long> getTimestamp()
        {
            return this.timestamp;
        }

        /// <returns>a list of the texts of all log entries</returns>
        public virtual System.Collections.Generic.IList<string> getText()
        {
            return this.text;
        }

        /// <returns>the total amount of log entries before pagination</returns>
        public virtual long getTotalAmount()
        {
            return this.totalAmount;
        }
    }
}