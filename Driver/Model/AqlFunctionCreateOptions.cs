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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/AqlUserFunctions/index.html#create-aql-user-function">API
    /// *      Documentation</a></seealso>
    public class AqlFunctionCreateOptions
    {
        private string name;

        private string code;

        private bool isDeterministic;

        public AqlFunctionCreateOptions()
            : base()
        {
        }

        /// <param name="name">the fully qualified name of the user functions</param>
        /// <returns>options</returns>
        protected internal virtual AqlFunctionCreateOptions name(string
             name)
        {
            this.name = name;
            return this;
        }

        protected internal virtual string getName()
        {
            return name;
        }

        /// <param name="code">a string representation of the function body</param>
        /// <returns>options</returns>
        protected internal virtual AqlFunctionCreateOptions code(string
             code)
        {
            this.code = code;
            return this;
        }

        protected internal virtual string getCode()
        {
            return code;
        }

        /// <param name="isDeterministic">
        /// an optional boolean value to indicate that the function results are fully deterministic (function
        /// return value solely depends on the input value and return value is the same for repeated calls with
        /// same input)
        /// </param>
        /// <returns>options</returns>
        public virtual AqlFunctionCreateOptions isDeterministic(bool isDeterministic
            )
        {
            this.isDeterministic = isDeterministic;
            return this;
        }

        public virtual bool getIsDeterministic()
        {
            return isDeterministic;
        }
    }
}