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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Document/WorkingWithDocuments.html#update-document">API
    /// *      Documentation</a></seealso>
    public class DocumentUpdateOptions
    {
        private bool keepNull;

        private bool mergeObjects;

        private bool waitForSync;

        private bool ignoreRevs;

        private string ifMatch;

        private bool returnNew;

        private bool returnOld;

        private bool serializeNull;

        public DocumentUpdateOptions()
            : base()
        {
        }

        public virtual bool getKeepNull()
        {
            return keepNull;
        }

        /// <param name="keepNull">
        /// If the intention is to delete existing attributes with the patch command, the URL query parameter
        /// keepNull can be used with a value of false. This will modify the behavior of the patch command to
        /// remove any attributes from the existing document that are contained in the patch document with an
        /// attribute value of null.
        /// </param>
        /// <returns>options</returns>
        public virtual DocumentUpdateOptions keepNull(bool keepNull)
        {
            this.keepNull = keepNull;
            return this;
        }

        public virtual bool getMergeObjects()
        {
            return mergeObjects;
        }

        /// <param name="mergeObjects">
        /// Controls whether objects (not arrays) will be merged if present in both the existing and the patch
        /// document. If set to false, the value in the patch document will overwrite the existing document's
        /// value. If set to true, objects will be merged. The default is true.
        /// </param>
        /// <returns>options</returns>
        public virtual DocumentUpdateOptions mergeObjects(bool mergeObjects
            )
        {
            this.mergeObjects = mergeObjects;
            return this;
        }

        public virtual bool getWaitForSync()
        {
            return waitForSync;
        }

        /// <param name="waitForSync">Wait until document has been synced to disk.</param>
        /// <returns>options</returns>
        public virtual DocumentUpdateOptions waitForSync(bool waitForSync
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
        /// document is only updated if the current revision is the one specified.
        /// </param>
        /// <returns>options</returns>
        public virtual DocumentUpdateOptions ignoreRevs(bool ignoreRevs
            )
        {
            this.ignoreRevs = ignoreRevs;
            return this;
        }

        public virtual string getIfMatch()
        {
            return ifMatch;
        }

        /// <param name="ifMatch">update a document based on target revision</param>
        /// <returns>options</returns>
        public virtual DocumentUpdateOptions ifMatch(string ifMatch)
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
        public virtual DocumentUpdateOptions returnNew(bool returnNew)
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
        public virtual DocumentUpdateOptions returnOld(bool returnOld)
        {
            this.returnOld = returnOld;
            return this;
        }

        public virtual bool getSerializeNull()
        {
            return serializeNull;
        }

        /// <param name="serializeNull">
        /// By default, or if this is set to true, all fields of the document which have null values are
        /// serialized to VelocyPack otherwise they are excluded from serialization. Use this to update single
        /// fields from a stored document.
        /// </param>
        /// <returns>options</returns>
        public virtual DocumentUpdateOptions serializeNull(bool serializeNull
            )
        {
            this.serializeNull = serializeNull;
            return this;
        }
    }
}