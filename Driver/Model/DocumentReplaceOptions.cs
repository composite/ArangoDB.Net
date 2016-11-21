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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#replace-document">API
    /// *      Documentation</a></seealso>
    public class DocumentReplaceOptions
    {
        private bool waitForSync;

        private bool ignoreRevs;

        private string ifMatch;

        private bool returnNew;

        private bool returnOld;

        public DocumentReplaceOptions()
            : base()
        {
        }

        public virtual bool getWaitForSync()
        {
            return waitForSync;
        }

        /// <param name="waitForSync">Wait until document has been synced to disk.</param>
        /// <returns>options</returns>
        public virtual DocumentReplaceOptions waitForSync(bool waitForSync
            )
        {
            this.waitForSync = waitForSync;
            return this;
        }

        public virtual bool getIgnoreRevs()
        {
            return ignoreRevs;
        }

        /// <param name="ignoreRevs">
        /// By default, or if this is set to true, the _rev attributes in the given document is ignored. If this
        /// is set to false, then the _rev attribute given in the body document is taken as a precondition. The
        /// document is only replaced if the current revision is the one specified.
        /// </param>
        /// <returns>options</returns>
        public virtual DocumentReplaceOptions ignoreRevs(bool ignoreRevs
            )
        {
            this.ignoreRevs = ignoreRevs;
            return this;
        }

        public virtual string getIfMatch()
        {
            return ifMatch;
        }

        /// <param name="ifMatch">replace a document based on target revision</param>
        /// <returns>options</returns>
        public virtual DocumentReplaceOptions ifMatch(string ifMatch)
        {
            this.ifMatch = ifMatch;
            return this;
        }

        public virtual bool getReturnNew()
        {
            return returnNew;
        }

        /// <param name="returnNew">Return additionally the complete new document under the attribute new in the result.
        /// 	</param>
        /// <returns>options</returns>
        public virtual DocumentReplaceOptions returnNew(bool returnNew
            )
        {
            this.returnNew = returnNew;
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
        public virtual DocumentReplaceOptions returnOld(bool returnOld
            )
        {
            this.returnOld = returnOld;
            return this;
        }
    }
}