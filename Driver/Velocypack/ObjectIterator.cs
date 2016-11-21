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
    using System.Collections.Generic;

    using global::ArangoDB.Velocypack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
	public class ObjectIterator : SliceIterator<KeyValuePair<string, VPackSlice>>
    {
        /// <exception cref="VPackValueTypeException"/>
        public ObjectIterator(VPackSlice slice)
            : base(slice)
        {
            if (!slice.isObject())
            {
                throw new VPackValueTypeException(ValueType
                    .OBJECT);
            }
            if (this.size > 0)
            {
                byte head = slice.head();
                if (head == unchecked((int)0x14))
                {
                    this.current = slice.keyAt(0).getStart();
                }
                else
                {
                    this.current = slice.getStart() + slice.findDataOffset();
                }
            }
        }

        public override System.Collections.Generic.KeyValuePair<string, VPackSlice
            > Current
        {
            get
            {
                if (this.position++ > 0)
                {
                    if (this.position <= this.size && this.current != 0)
                    {
                        // skip over key
                        this.current += this.getCurrent().getByteSize();
                        // skip over value
                        this.current += this.getCurrent().getByteSize();
                    }
                    else
                    {
                        throw new java.util.NoSuchElementException();
                    }
                }
                VPackSlice currentField = this.getCurrent();
                return new _KeyValuePair_64(currentField);
            }
        }

        private sealed class _KeyValuePair_64 : System.Collections.Generic.KeyValuePair<string
            , VPackSlice>
        {
            public _KeyValuePair_64(VPackSlice currentField)
            {
                this.currentField = currentField;
            }

            public VPackSlice setValue(VPackSlice
                 value)
            {
                throw new System.NotSupportedException();
            }

            public VPackSlice Value
            {
                get
                {
                    return new VPackSlice(this.currentField.getBuffer(), this.currentField
                        .getStart() + this.currentField.getByteSize());
                }
            }

            public string Key
            {
                get
                {
                    try
                    {
                        return this.currentField.makeKey().getAsString();
                    }
                    catch (VPackKeyTypeException)
                    {
                        throw new java.util.NoSuchElementException();
                    }
                    catch (VPackNeedAttributeTranslatorException)
                    {
                        throw new java.util.NoSuchElementException();
                    }
                }
            }

            private readonly VPackSlice currentField;
        }

        public override void remove()
        {
            throw new System.NotSupportedException();
        }
    }
}