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
    using global::ArangoDB.Entity;

    /// <author>Mark - mark at arangodb.com</author>
	/// <seealso><a href="https://docs.arangodb.com/current/HTTP/Collection/Creating.html#create-collection">API
	/// *      Documentation</a></seealso>
	public class CollectionCreateOptions
    {
        private string name;

        private long journalSize;

        private int replicationFactor;

        private KeyOptions keyOptions;

        private bool waitForSync;

        private bool doCompact;

        private bool isVolatile;

        private string[] shardKeys;

        private int numberOfShards;

        private bool isSystem;

        private CollectionType type;

        private int indexBuckets;

        public CollectionCreateOptions()
            : base()
        {
        }

        protected internal virtual string getName()
        {
            return name;
        }

        /// <param name="name">The name of the collection</param>
        /// <returns>options</returns>
        protected internal virtual CollectionCreateOptions name(string
             name)
        {
            this.name = name;
            return this;
        }

        public virtual long getJournalSize()
        {
            return journalSize;
        }

        /// <param name="journalSize">The maximal size of a journal or datafile in bytes. The value must be at least 1048576 (1 MiB).
        /// 	</param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions journalSize(long journalSize
            )
        {
            this.journalSize = journalSize;
            return this;
        }

        public virtual int getReplicationFactor()
        {
            return replicationFactor;
        }

        /// <param name="replicationFactor">
        /// (The default is 1): in a cluster, this attribute determines how many copies of each shard are kept on
        /// different DBServers. The value 1 means that only one copy (no synchronous replication) is kept. A
        /// value of k means that k-1 replicas are kept. Any two copies reside on different DBServers. Replication
        /// between them is synchronous, that is, every write operation to the "leader" copy will be replicated to
        /// all "follower" replicas, before the write operation is reported successful. If a server fails, this is
        /// detected automatically and one of the servers holding copies take over, usually without an error being
        /// reported.
        /// </param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions replicationFactor(int replicationFactor
            )
        {
            this.replicationFactor = replicationFactor;
            return this;
        }

        public virtual KeyOptions getKeyOptions()
        {
            return keyOptions;
        }

        /// <param name="allowUserKeys">
        /// if set to true, then it is allowed to supply own key values in the _key attribute of a document. If
        /// set to false, then the key generator will solely be responsible for generating keys and supplying own
        /// key values in the _key attribute of documents is considered an error.
        /// </param>
        /// <param name="type">
        /// specifies the type of the key generator. The currently available generators are traditional and
        /// autoincrement.
        /// </param>
        /// <param name="increment">increment value for autoincrement key generator. Not used for other key generator types.
        /// 	</param>
        /// <param name="offset">Initial offset value for autoincrement key generator. Not used for other key generator types.
        /// 	</param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions keyOptions(bool allowUserKeys
            , KeyType type, int increment, int offset)
        {
            this.keyOptions = new KeyOptions(allowUserKeys, type, increment
                , offset);
            return this;
        }

        public virtual bool getWaitForSync()
        {
            return waitForSync;
        }

        /// <param name="waitForSync">
        /// If true then the data is synchronized to disk before returning from a document create, update, replace
        /// or removal operation. (default: false)
        /// </param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions waitForSync(bool waitForSync
            )
        {
            this.waitForSync = waitForSync;
            return this;
        }

        public virtual bool getDoCompact()
        {
            return doCompact;
        }

        /// <param name="doCompact">whether or not the collection will be compacted (default is true)
        /// 	</param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions doCompact(bool doCompact
            )
        {
            this.doCompact = doCompact;
            return this;
        }

        public virtual bool getIsVolatile()
        {
            return isVolatile;
        }

        /// <param name="isVolatile">
        /// If true then the collection data is kept in-memory only and not made persistent. Unloading the
        /// collection will cause the collection data to be discarded. Stopping or re-starting the server will
        /// also cause full loss of data in the collection. Setting this option will make the resulting collection
        /// be slightly faster than regular collections because ArangoDB does not enforce any synchronization to
        /// disk and does not calculate any CRC checksums for datafiles (as there are no datafiles). This option
        /// should therefore be used for cache-type collections only, and not for data that cannot be re-created
        /// otherwise. (The default is false)
        /// </param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions isVolatile(bool isVolatile
            )
        {
            this.isVolatile = isVolatile;
            return this;
        }

        public virtual string[] getShardKeys()
        {
            return shardKeys;
        }

        /// <param name="shardKeys">
        /// (The default is [ "_key" ]): in a cluster, this attribute determines which document attributes are
        /// used to determine the target shard for documents. Documents are sent to shards based on the values of
        /// their shard key attributes. The values of all shard key attributes in a document are hashed, and the
        /// hash value is used to determine the target shard. Note: Values of shard key attributes cannot be
        /// changed once set. This option is meaningless in a single server setup.
        /// </param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions shardKeys(string[] shardKeys
            )
        {
            this.shardKeys = shardKeys;
            return this;
        }

        public virtual int getNumberOfShards()
        {
            return numberOfShards;
        }

        /// <param name="numberOfShards">
        /// (The default is 1): in a cluster, this value determines the number of shards to create for the
        /// collection. In a single server setup, this option is meaningless.
        /// </param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions numberOfShards(int numberOfShards
            )
        {
            this.numberOfShards = numberOfShards;
            return this;
        }

        public virtual bool getIsSystem()
        {
            return isSystem;
        }

        /// <param name="isSystem">
        /// If true, create a system collection. In this case collection-name should start with an underscore. End
        /// users should normally create non-system collections only. API implementors may be required to create
        /// system collections in very special occasions, but normally a regular collection will do. (The default
        /// is false)
        /// </param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions isSystem(bool isSystem)
        {
            this.isSystem = isSystem;
            return this;
        }

        public virtual CollectionType getType()
        {
            return type;
        }

        /// <param name="type">
        /// (The default is
        /// <see cref="CollectionType.DOCUMENT"/>
        /// ): the type of the collection to create.
        /// </param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions type(CollectionType
             type)
        {
            this.type = type;
            return this;
        }

        public virtual int getIndexBuckets()
        {
            return indexBuckets;
        }

        /// <param name="indexBuckets">
        /// The: number of buckets into which indexes using a hash table are split. The default is 16 and this
        /// number has to be a power of 2 and less than or equal to 1024. For very large collections one should
        /// increase this to avoid long pauses when the hash table has to be initially built or resized, since
        /// buckets are resized individually and can be initially built in parallel. For example, 64 might be a
        /// sensible value for a collection with 100 000 000 documents. Currently, only the edge index respects
        /// this value, but other index types might follow in future ArangoDB versions. Changes (see below) are
        /// applied when the collection is loaded the next time.
        /// </param>
        /// <returns>options</returns>
        public virtual CollectionCreateOptions indexBuckets(int indexBuckets
            )
        {
            this.indexBuckets = indexBuckets;
            return this;
        }
    }
}