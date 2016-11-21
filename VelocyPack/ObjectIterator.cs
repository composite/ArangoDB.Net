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
    using System.Collections.Generic;

    using VelocyPack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
    public class ObjectIterator : SliceIterator<IEntry<string, VPackSlice>>
    {
        /// <exception cref="VPackValueTypeException"/>
        public ObjectIterator(VPackSlice slice)
            : base(slice)
        {
            if (!slice.IsObject)
            {
                throw new VPackValueTypeException(ValueType
                    .OBJECT);
            }
            if (this.size > 0)
            {
                byte head = slice.Head;
                if (head == unchecked((int)0x14))
                {
                    this.current = slice.KeyAt(0).Start;
                }
                else
                {
                    this.current = slice.Start+ slice.FindDataOffset();
                }
            }
        }

        public override IEntry<string, VPackSlice> Current
        {
            get
            {
                if (this.position++ > 0)
                {
                    if (this.position <= this.size && this.current != 0)
                    {
                        // skip over key
                        this.current += this.GetCurrent().ByteSize;
                        // skip over value
                        this.current += this.GetCurrent().ByteSize;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
                return new __ObjectIterator_Entry(this.GetCurrent());
            }
        }

        public void Remove()
        {
            throw new System.NotSupportedException();
        }

        private class __ObjectIterator_Entry : IEntry<string, VPackSlice>
        {
            private VPackSlice current;

            public __ObjectIterator_Entry(VPackSlice currnet)
            {
                this.current = currnet;
            }

            public string Key
            {
                get
                {
                    try
                    {
                        return current.MakeKey().AsString;
                    }
                    catch (VPackKeyTypeException e)
                    {
                        throw new InvalidOperationException();
                    }
                    catch (VPackNeedAttributeTranslatorException e)
                    {
                        throw new InvalidOperationException();
                    }
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            public VPackSlice Value
            {
                get
                {
                    return new VPackSlice(current.Buffer, current.Start+ current.ByteSize);
                }
                set
                {
                    throw new NotSupportedException();
                }
            }
        }

    }
}