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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/UserManagement/index.html">API Documentation</a>
    /// 	</seealso>
    public class UserEntity
    {
        private string user;

        private bool active;

        private System.Collections.Generic.IDictionary<string, object> extra;

        private bool changePassword;

        /// <returns>The name of the user as a string</returns>
        public virtual string getUser()
        {
            return this.user;
        }

        /// <returns>An flag that specifies whether the user is active</returns>
        public virtual bool getActive()
        {
            return this.active;
        }

        /// <returns>An object with arbitrary extra data about the user</returns>
        public virtual System.Collections.Generic.IDictionary<string, object> getExtra()
        {
            return this.extra;
        }

        public virtual bool getChangePassword()
        {
            return this.changePassword;
        }
    }
}