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

namespace ArangoDB.Velocypack
{
    using global::ArangoDB.Velocypack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
	public abstract class SliceIterator<E> : System.Collections.Generic.IEnumerator<E
        >
    {
        protected internal readonly VPackSlice slice;

        protected internal readonly long size;

        protected internal long position;

        protected internal long current;

        /// <exception cref="VPackValueTypeException"/>
        protected internal SliceIterator(VPackSlice slice)
            : base()
        {
            this.slice = slice;
            this.size = slice.getLength();
            this.position = 0;
        }

        public override bool MoveNext()
        {
            return this.position < this.size;
        }

        protected internal virtual VPackSlice getCurrent()
        {
            return new VPackSlice(this.slice.getBuffer(), (int)this.current);
        }

        public abstract E Current();

        public abstract void remove();
    }
}