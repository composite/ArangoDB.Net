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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#create-document">API
    /// *      Documentation</a></seealso>
    public class DocumentCreateOptions
    {
        private bool waitForSync;

        private bool returnNew;

        public DocumentCreateOptions()
            : base()
        {
        }

        public virtual bool getWaitForSync()
        {
            return waitForSync;
        }

        /// <param name="waitForSync">Wait until document has been synced to disk.</param>
        /// <returns>options</returns>
        public virtual DocumentCreateOptions waitForSync(bool waitForSync
            )
        {
            this.waitForSync = waitForSync;
            return this;
        }

        public virtual bool getReturnNew()
        {
            return returnNew;
        }

        /// <param name="returnNew">Return additionally the complete new document under the attribute new in the result.
        /// 	</param>
        /// <returns>options</returns>
        public virtual DocumentCreateOptions returnNew(bool returnNew)
        {
            this.returnNew = returnNew;
            return this;
        }
    }
}