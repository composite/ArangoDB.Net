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

namespace ArangoDB
{
    using global::ArangoDB.Entity;
    using global::ArangoDB.Internal;

    /// <author>Mark - mark at arangodb.com</author>
	public class ArangoCursor<T> : System.Collections.Generic.IEnumerator<T>, java.io.Closeable
    {
        private readonly java.lang.Class type;

        protected internal readonly ArangoCursorIterator<T> iterator;

        private readonly string id;

        private readonly int count;

        private readonly CursorEntity.Extras extra;

        private readonly bool cached;

        private readonly ArangoCursorExecute execute;

        protected internal ArangoCursor(InternalArangoDatabase<object
            , object, object> db, ArangoCursorExecute execute, java.lang.Class
             type, CursorEntity result)
            : base()
        {
            this.execute = execute;
            this.type = type;
            this.count = result.getCount();
            this.extra = result.getExtra();
            this.cached = result.getCached();
            this.iterator = new ArangoCursorIterator<T>(this, execute, db,
                result);
            this.id = result.getId();
        }

        /// <returns>id of temporary cursor created on the server</returns>
        public virtual string getId()
        {
            return this.id;
        }

        public virtual java.lang.Class getType()
        {
            return this.type;
        }

        /// <returns>
        /// the total number of result documents available (only available if the query was executed with the count
        /// attribute set)
        /// </returns>
        public virtual int getCount()
        {
            return this.count;
        }

        public virtual CursorEntity.Stats getStats()
        {
            return this.extra != null ? this.extra.getStats() : null;
        }

        public virtual System.Collections.Generic.ICollection<CursorEntity.Warning
            > getWarnings()
        {
            return this.extra != null ? this.extra.getWarnings() : null;
        }

        /// <returns>indicating whether the query result was served from the query cache or not
        /// 	</returns>
        public virtual bool isCached()
        {
            return this.cached;
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void close()
        {
            this.execute.close(this.id);
        }

        public virtual bool MoveNext()
        {
            return this.iterator.MoveNext();
        }

        public virtual T Current
        {
            get
            {
                return this.iterator.Current;
            }
        }

        public virtual System.Collections.Generic.IList<T> asListRemaining()
        {
            System.Collections.Generic.IList<T> remaining = new System.Collections.Generic.List
                <T>();
            while (this.MoveNext())
            {
                remaining.add(this.Current);
            }
            return remaining;
        }

        public virtual void remove()
        {
            throw new System.NotSupportedException();
        }
    }
}