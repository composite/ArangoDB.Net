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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/MiscellaneousFunctions/index.html#return-server-version">API
    /// *      Documentation</a></seealso>
    public class ArangoDBVersion
    {
        private string server;

        private string version;

        public ArangoDBVersion()
            : base()
        {
        }

        /// <returns>will always contain arango</returns>
        public virtual string getServer()
        {
            return this.server;
        }

        /// <returns>
        /// the server version string. The string has the format "major.minor.sub". major and minor will be numeric,
        /// and sub may contain a number or a textual version.
        /// </returns>
        public virtual string getVersion()
        {
            return this.version;
        }
    }
}