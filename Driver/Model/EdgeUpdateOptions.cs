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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Gharial/Edges.html#modify-an-edge">API Documentation</a>
    /// 	</seealso>
    public class EdgeUpdateOptions
    {
        private bool keepNull;

        private bool waitForSync;

        private string ifMatch;

        public EdgeUpdateOptions()
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
        public virtual EdgeUpdateOptions keepNull(bool keepNull)
        {
            this.keepNull = keepNull;
            return this;
        }

        public virtual bool getWaitForSync()
        {
            return waitForSync;
        }

        /// <param name="waitForSync">Wait until document has been synced to disk.</param>
        /// <returns>options</returns>
        public virtual EdgeUpdateOptions waitForSync(bool waitForSync)
        {
            this.waitForSync = waitForSync;
            return this;
        }

        public virtual string getIfMatch()
        {
            return ifMatch;
        }

        /// <param name="ifMatch">replace a document based on target revision</param>
        /// <returns>options</returns>
        public virtual EdgeUpdateOptions ifMatch(string ifMatch)
        {
            this.ifMatch = ifMatch;
            return this;
        }
    }
}