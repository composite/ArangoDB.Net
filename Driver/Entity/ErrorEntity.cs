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
    public class ErrorEntity
    {
        private string errorMessage;

        private int code;

        private int errorNum;

        public ErrorEntity()
            : base()
        {
        }

        /// <returns>a descriptive error message</returns>
        public virtual string getErrorMessage()
        {
            return this.errorMessage;
        }

        /// <returns>the status code</returns>
        public virtual int getCode()
        {
            return this.code;
        }

        /// <returns>the server error number</returns>
        public virtual int getErrorNum()
        {
            return this.errorNum;
        }
    }
}