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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#removes-a-document">API
    /// *      Documentation</a></seealso>
    public class DocumentDeleteOptions
    {
        private bool waitForSync;

        private string ifMatch;

        private bool returnOld;

        public DocumentDeleteOptions()
            : base()
        {
        }

        public virtual bool getWaitForSync()
        {
            return waitForSync;
        }

        /// <param name="waitForSync">Wait until deletion operation has been synced to disk.</param>
        /// <returns>options</returns>
        public virtual DocumentDeleteOptions waitForSync(bool waitForSync
            )
        {
            this.waitForSync = waitForSync;
            return this;
        }

        public virtual string getIfMatch()
        {
            return ifMatch;
        }

        /// <param name="ifMatch">remove a document based on a target revision</param>
        /// <returns>options</returns>
        public virtual DocumentDeleteOptions ifMatch(string ifMatch)
        {
            this.ifMatch = ifMatch;
            return this;
        }

        public virtual bool getReturnOld()
        {
            return returnOld;
        }

        /// <param name="returnOld">
        /// Return additionally the complete previous revision of the changed document under the attribute old in
        /// the result.
        /// </param>
        /// <returns>options</returns>
        public virtual DocumentDeleteOptions returnOld(bool returnOld)
        {
            this.returnOld = returnOld;
            return this;
        }
    }
}