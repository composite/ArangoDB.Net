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

namespace VelocyPack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using VelocyPack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
    public abstract class SliceIterator<E> : IEnumerator<E>
    {
        protected readonly VPackSlice slice;

        protected readonly long size;

        protected long position;

        protected long current;

        /// <exception cref="VPackValueTypeException"/>
        protected SliceIterator(VPackSlice slice)
        {
            this.slice = slice;
            this.size = slice.GetLength();
            this.position = 0;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        public bool MoveNext()
        {
            return this.position < this.size;
        }

        protected virtual VPackSlice GetCurrent()
        {
            return new VPackSlice(this.slice.Buffer, (int)this.current);
        }

        public abstract E Current { get; }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        void IDisposable.Dispose()
        {
            // ...
        }
    }
}