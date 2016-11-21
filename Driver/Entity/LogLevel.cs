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
    public sealed class LogLevel
    {
        public static readonly LogLevel FATAL = new LogLevel
            (0);

        public static readonly LogLevel ERROR = new LogLevel
            (1);

        public static readonly LogLevel WARNING = new LogLevel
            (2);

        public static readonly LogLevel INFO = new LogLevel
            (3);

        public static readonly LogLevel DEBUG = new LogLevel
            (4);

        private readonly int level;

        private LogLevel(int level)
        {
            this.level = level;
        }

        public int getLevel()
        {
            return LogLevel.level;
        }

        public static LogLevel fromLevel(int level)
        {
            foreach (LogLevel logLevel in LogLevel.values
                ())
            {
                if (logLevel.level == level)
                {
                    return logLevel;
                }
            }
            return null;
        }
    }
}