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
	public class ArrayIterator : SliceIterator<VPackSlice
        >
    {
        /// <exception cref="VPackValueTypeException"/>
        public ArrayIterator(VPackSlice slice)
            : base(slice)
        {
            if (!slice.isArray())
            {
                throw new VPackValueTypeException(ValueType
                    .ARRAY);
            }
        }

        public override VPackSlice Current
        {
            get
            {
                VPackSlice next;
                if (this.MoveNext())
                {
                    next = this.slice.get((int)this.position++);
                }
                else
                {
                    throw new java.util.NoSuchElementException();
                }
                return next;
            }
        }

        public override void remove()
        {
            throw new System.NotSupportedException();
        }
    }
}