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
    /// <seealso><a href=
    /// *      "https://docs.arangodb.com/current/HTTP/AqlUserFunctions/index.html#remove-existing-aql-user-function">API
    /// *      Documentation</a></seealso>
    public class AqlFunctionDeleteOptions
    {
        private bool group;

        public AqlFunctionDeleteOptions()
            : base()
        {
        }

        public virtual bool getGroup()
        {
            return group;
        }

        /// <param name="group">
        /// If set to true, then the function name provided in name is treated as a namespace prefix, and all
        /// functions in the specified namespace will be deleted. If set to false, the function name provided in
        /// name must be fully qualified, including any namespaces.
        /// </param>
        /// <returns>options</returns>
        public virtual AqlFunctionDeleteOptions group(bool group)
        {
            this.group = group;
            return this;
        }
    }
}