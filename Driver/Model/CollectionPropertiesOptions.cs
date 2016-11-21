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
    /// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Modifying.html#change-properties-of-a-collection">API
    /// *      Documentation</a></seealso>
    public class CollectionPropertiesOptions
    {
        private bool waitForSync;

        private long journalSize;

        public CollectionPropertiesOptions()
            : base()
        {
        }

        public virtual bool getWaitForSync()
        {
            return waitForSync;
        }

        /// <param name="waitForSync">If true then creating or changing a document will wait until the data has been synchronized to disk.
        /// 	</param>
        /// <returns>options</returns>
        public virtual CollectionPropertiesOptions waitForSync(bool waitForSync
            )
        {
            this.waitForSync = waitForSync;
            return this;
        }

        public virtual long getJournalSize()
        {
            return journalSize;
        }

        /// <param name="journalSize">
        /// The maximal size of a journal or datafile in bytes. The value must be at least 1048576 (1 MB). Note
        /// that when changing the journalSize value, it will only have an effect for additional journals or
        /// datafiles that are created. Already existing journals or datafiles will not be affected.
        /// </param>
        /// <returns>options</returns>
        public virtual CollectionPropertiesOptions journalSize(long journalSize
            )
        {
            this.journalSize = journalSize;
            return this;
        }
    }
}