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
    /// *      "https://docs.arangodb.com/current/HTTP/Database/DatabaseManagement.html#information-of-the-database">API
    /// *      Documentation</a></seealso>
    public class DatabaseEntity
    {
        private string id;

        private string name;

        private string path;

        private bool isSystem;

        /// <returns>the id of the database</returns>
        public virtual string getId()
        {
            return this.id;
        }

        /// <returns>the name of the database</returns>
        public virtual string getName()
        {
            return this.name;
        }

        /// <returns>the filesystem path of the database</returns>
        public virtual string getPath()
        {
            return this.path;
        }

        /// <returns>whether or not the database is the _system database</returns>
        public virtual bool getIsSystem()
        {
            return this.isSystem;
        }
    }
}