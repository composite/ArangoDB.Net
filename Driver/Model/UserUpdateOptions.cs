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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/UserManagement/index.html#replace-user">API Documentation</a>
    /// 	</seealso>
    public class UserUpdateOptions
    {
        private string passwd;

        private bool active;

        private System.Collections.Generic.IDictionary<string, object> extra;

        public UserUpdateOptions()
            : base()
        {
        }

        public virtual string getPasswd()
        {
            return passwd;
        }

        /// <param name="passwd">The user password</param>
        /// <returns>options</returns>
        public virtual UserUpdateOptions passwd(string passwd)
        {
            this.passwd = passwd;
            return this;
        }

        public virtual bool getActive()
        {
            return active;
        }

        /// <param name="active">
        /// An optional flag that specifies whether the user is active. If not specified, this will default to
        /// true
        /// </param>
        /// <returns>options</returns>
        public virtual UserUpdateOptions active(bool active)
        {
            this.active = active;
            return this;
        }

        public virtual System.Collections.Generic.IDictionary<string, object> getExtra()
        {
            return extra;
        }

        /// <param name="extra">Optional data about the user</param>
        /// <returns>options</returns>
        public virtual UserUpdateOptions extra(System.Collections.Generic.IDictionary
            <string, object> extra)
        {
            this.extra = extra;
            return this;
        }
    }
}