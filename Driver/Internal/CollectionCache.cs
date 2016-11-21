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

namespace ArangoDB.Internal
{
    using global::ArangoDB.Entity;
    using System.Collections.Generic;

    /// <author>Mark - mark at arangodb.com</author>
	public class CollectionCache
    {
        private static readonly org.slf4j.Logger LOGGER = org.slf4j.LoggerFactory.getLogger
            (typeof(CollectionCache)
            );

        private const long MAX_CACHE_TIME = 600000;

        private class CollectionInfo
        {
            private readonly string name;

            private readonly long time;

            public CollectionInfo(string name, long time)
                : base()
            {
                this.name = name;
                this.time = time;
            }
        }

        public interface DBAccess
        {
            ArangoDatabase db(string name);
        }

        private readonly System.Collections.Generic.IDictionary<string, IDictionary<long, CollectionInfo>> cache;

        private CollectionCache.DBAccess access;

        private string db;

        public CollectionCache()
            : base()
        {
            this.cache = new System.Collections.Generic.Dictionary<string, IDictionary<long, CollectionInfo>>();
        }

        public virtual void init(CollectionCache.DBAccess access)
        {
            this.access = access;
        }

        public virtual void setDb(string db)
        {
            this.db = db;
        }

        public virtual string getCollectionName(long id)
        {
            CollectionCache.CollectionInfo info = this.getInfo(id);
            return info != null ? info.name : null;
        }

        private CollectionCache.CollectionInfo getInfo(long id)
        {
            System.Collections.Generic.IDictionary<long, CollectionInfo
                > dbCache = this.cache[this.db];
            if (dbCache == null)
            {
                dbCache = new System.Collections.Generic.Dictionary<long, CollectionInfo
                    >();
                this.cache[this.db] = dbCache;
            }
            CollectionCache.CollectionInfo info = dbCache[id];
            if (info == null || this.isExpired(info.time))
            {
                try
                {
                    string name = this.execute(id);
                    info = new CollectionCache.CollectionInfo(name, new System.DateTime
                        ().getTime());
                    dbCache[id] = info;
                }
                catch (ArangoDBException e)
                {
                    LOGGER.error(e.Message, e);
                }
            }
            return info;
        }

        /// <exception cref="ArangoDBException"/>
        private string execute(long id)
        {
            CollectionEntity result = this.access.db(this.db).collection(Sharpen.Runtime.getStringValueOf
                (id)).getInfo();
            return result.getName();
        }

        private bool isExpired(long time)
        {
            return new System.DateTime().getTime() > time + MAX_CACHE_TIME;
        }
    }
}