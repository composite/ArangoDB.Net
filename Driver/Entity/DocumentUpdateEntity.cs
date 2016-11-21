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
    /// <?/>
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#update-document">API
    /// *      Documentation</a></seealso>
    public class DocumentUpdateEntity<T> : DocumentEntity
    {
        private string oldRev;

        private T newDocument;

        private T oldDocument;

        public DocumentUpdateEntity()
            : base()
        {
        }

        public virtual string getOldRev()
        {
            return this.oldRev;
        }

        /// <returns>If the query parameter returnNew is true, then the complete new document is returned.
        /// 	</returns>
        public virtual T getNew()
        {
            return this.newDocument;
        }

        public virtual void setNew(T newDocument)
        {
            this.newDocument = newDocument;
        }

        /// <returns>
        /// If the query parameter returnOld is true, then the complete previous revision of the document is
        /// returned.
        /// </returns>
        public virtual T getOld()
        {
            return this.oldDocument;
        }

        public virtual void setOld(T oldDocument)
        {
            this.oldDocument = oldDocument;
        }
    }
}