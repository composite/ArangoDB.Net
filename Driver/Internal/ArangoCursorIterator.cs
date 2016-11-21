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
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using global::ArangoDB.Internal.VelocyStream;

    /// <author>Mark - mark at arangodb.com</author>
    public class ArangoCursorIterator<T> : IEnumerator<T>
    {
        private CursorEntity result;

        private int pos;

        private readonly ArangoCursor<T> cursor;

        private readonly InternalArangoDatabase<ArangoExecutor<object, Connection>, object, Connection> db;

        private readonly ArangoCursorExecute execute;

        public ArangoCursorIterator(ArangoCursor<T> cursor, ArangoCursorExecute
             execute, InternalArangoDatabase<ArangoExecutor<object, Connection>, object, Connection> db, CursorEntity result)
            : base()
        {
            this.cursor = cursor;
            this.execute = execute;
            this.db = db;
            this.result = result;
            this.pos = 0;
        }

        public bool MoveNext()
        {
            return this.pos < this.result.getResult().size() || this.result.getHasMore();
        }

        public void Reset()
        {
            //TODO
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public T Current
        {
            get
            {
                if (this.pos >= this.result.getResult().size() && this.result.getHasMore())
                {
                    this.result = this.execute.next(this.cursor.getId());
                    this.pos = 0;
                }
                if (!this.MoveNext())
                {
                    throw new InvalidOperationException();
                }
                return this.db.executor.Deserialize<T>(this.result.getResult().get(this.pos++));
            }
        }

        public void Remove()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
            var id = this.cursor.getId();
            this.cursor.close();
            this.execute.close(id);
        }
    }
}