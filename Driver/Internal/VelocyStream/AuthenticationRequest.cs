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

namespace ArangoDB.Internal.VelocyStream
{
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class AuthenticationRequest : Request
    {
        private readonly string user;

        private readonly string password;

        private readonly string encryption;

        public AuthenticationRequest(string user, string password, string encryption)
            : base(null, null, null)
        {
            // "plain"
            this.user = user;
            this.password = password;
            this.encryption = encryption;
            this.setType(1000);
        }

        public virtual string getUser()
        {
            return this.user;
        }

        public virtual string getPassword()
        {
            return this.password;
        }

        public virtual string getEncryption()
        {
            return this.encryption;
        }
    }
}