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
    /// <seealso><a href= "https://docs.arangodb.com/current/HTTP/Collection/Getting.html#read-properties-of-a-collection">API
    /// *      Documentation</a></seealso>
    public class CollectionPropertiesEntity : CollectionEntity
    {
        private bool doCompact;

        private long journalSize;

        private int indexBuckets;

        private KeyOptions keyOptions;

        private long count;

        private int numberOfShards;

        private System.Collections.Generic.ICollection<string> shardKeys;

        private int replicationFactor;

        public CollectionPropertiesEntity()
            : base()
        {
        }

        public virtual bool getDoCompact()
        {
            return this.doCompact;
        }

        public virtual void setDoCompact(bool doCompact)
        {
            this.doCompact = doCompact;
        }

        public virtual long getJournalSize()
        {
            return this.journalSize;
        }

        public virtual void setJournalSize(long journalSize)
        {
            this.journalSize = journalSize;
        }

        public virtual int getIndexBuckets()
        {
            return this.indexBuckets;
        }

        public virtual void setIndexBuckets(int indexBuckets)
        {
            this.indexBuckets = indexBuckets;
        }

        public virtual KeyOptions getKeyOptions()
        {
            return this.keyOptions;
        }

        public virtual void setKeyOptions(KeyOptions keyOptions)
        {
            this.keyOptions = keyOptions;
        }

        public virtual long getCount()
        {
            return this.count;
        }

        public virtual void setCount(long count)
        {
            this.count = count;
        }

        /// <returns>
        /// contains the names of document attributes that are used to determine the target shard for documents. Only
        /// in a cluster setup
        /// </returns>
        public virtual int getNumberOfShards()
        {
            return this.numberOfShards;
        }

        public virtual void setNumberOfShards(int numberOfShards)
        {
            this.numberOfShards = numberOfShards;
        }

        /// <returns>the number of shards of the collection. Only in a cluster setup.</returns>
        public virtual System.Collections.Generic.ICollection<string> getShardKeys()
        {
            return this.shardKeys;
        }

        public virtual void setShardKeys(System.Collections.Generic.ICollection<string> shardKeys
            )
        {
            this.shardKeys = shardKeys;
        }

        public virtual int getReplicationFactor()
        {
            return this.replicationFactor;
        }

        public virtual void setReplicationFactor(int replicationFactor)
        {
            this.replicationFactor = replicationFactor;
        }
    }
}