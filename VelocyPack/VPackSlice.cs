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
    using System.Linq;
    using System.Numerics;

    using VelocyPack.Exceptions;
    using VelocyPack.Internal;
    using VelocyPack.Internal.Util;
    using VelocyPack.Migration.Util;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackSlice
    {
        public static readonly VPackAttributeTranslator attributeTranslator = new VPackAttributeTranslatorImpl();

        private readonly byte[] vpack;

        private readonly int start;

        protected internal VPackSlice()
            : this(new byte[] { 0x00 }, 0)
        {
        }

        public VPackSlice(byte[] vpack)
            : this(vpack, 0)
        {
        }

        public VPackSlice(byte[] vpack, int start)
        {
            this.vpack = vpack;
            this.start = start;
        }

        public virtual byte Head
        {
            get
            {
                return this.vpack[this.start];
            }
        }

        public virtual byte[] Buffer
        {
            get
            {
                return this.vpack;
            }
        }

        public virtual int Start
        {
            get
            {
                return this.start;
            }
        }

        internal virtual ValueType Type()
        {
            return ValueTypeUtil.Get(this.Head);
        }

        internal int GetLength()
        {
            return ValueLengthUtil.Get(this.Head) - 1;
        }

        internal bool IsType(ValueType type)
        {
            return Type() == type;
        }

        public virtual bool IsNone
        {
            get
            {
                return this.IsType(ValueType.NONE);
            }
        }

        public virtual bool IsNull
        {
            get
            {
                return this.IsType(ValueType.NULL);
            }
        }

        public virtual bool IsIllegal
        {
            get
            {
                return this.IsType(ValueType.ILLEGAL);
            }
        }

        public virtual bool IsBoolean
        {
            get
            {
                return this.IsType(ValueType.BOOL);
            }
        }

        public virtual bool IsTrue
        {
            get
            {
                return this.Head == 0x1a;
            }
        }

        public virtual bool IsFalse
        {
            get
            {
                return this.Head == 0x19;
            }
        }

        public virtual bool IsArray
        {
            get
            {
                return this.IsType(ValueType.ARRAY);
            }
        }

        public virtual bool IsObject
        {
            get
            {
                return this.IsType(ValueType.OBJECT);
            }
        }

        public virtual bool IsDouble
        {
            get
            {
                return this.IsType(ValueType.DOUBLE);
            }
        }

        public virtual bool IsDate
        {
            get
            {
                return this.IsType(ValueType.UTC_DATE);
            }
        }

        public virtual bool IsExternal
        {
            get
            {
                return this.IsType(ValueType.EXTERNAL);
            }
        }

        public virtual bool IsMinKey
        {
            get
            {
                return this.IsType(ValueType.MIN_KEY);
            }
        }

        public virtual bool IsMaxKey
        {
            get
            {
                return this.IsType(ValueType.MAX_KEY);
            }
        }

        public virtual bool IsInt
        {
            get
            {
                return this.IsType(ValueType.INT);
            }
        }

        public virtual bool IsUInt
        {
            get
            {
                return this.IsType(ValueType.UINT);
            }
        }

        public virtual bool IsSmallInt
        {
            get
            {
                return this.IsType(ValueType.SMALLINT);
            }
        }

        public virtual bool IsInteger
        {
            get
            {
                return this.IsInt || this.IsUInt || this.IsSmallInt;
            }
        }

        public virtual bool IsNumber
        {
            get
            {
                return this.IsInteger || this.IsDouble;
            }
        }

        public virtual bool IsString
        {
            get
            {
                return this.IsType(ValueType.STRING);
            }
        }

        public virtual bool IsBinary
        {
            get
            {
                return this.IsType(ValueType.BINARY);
            }
        }

        public virtual bool IsBcd
        {
            get
            {
                return this.IsType(ValueType.BCD);
            }
        }

        public virtual bool IsCustom
        {
            get
            {
                return this.IsType(ValueType.CUSTOM);
            }
        }

        public virtual bool AsBoolean
        {
            get
            {
                if (!this.IsBoolean)
                {
                    throw new VPackValueTypeException(ValueType.BOOL);
                }

                return this.IsTrue;
            }
        }

        public virtual double AsDouble
        {
            get
            {
                if (!this.IsDouble)
                {
                    throw new VPackValueTypeException(ValueType.DOUBLE);
                }

                return this.AsDoubleUnchecked;
            }
        }

        private double AsDoubleUnchecked
        {
            get
            {
                return NumberUtil.ToDouble(this.vpack, this.start + 1, this.GetLength());
            }
        }

        private long SmallInt
        {
            get
            {
                byte head = Head;
                long smallInt;
                if (head >= 0x30 && (sbyte)head <= 0x39)
                {
                    smallInt = head - 0x30;
                }
                else
                {
                    /* if (head >= 0x3a && head <= 0x3f) */
                    smallInt = head - 0x3a - 6;
                }

                return smallInt;
            }
        }

        private long Int
        {
            get
            {
                return NumberUtil.ToLong(this.vpack, this.start + 1, this.GetLength());
            }
        }

        private long UInt
        {
            get
            {
                return NumberUtil.ToLong(this.vpack, this.start + 1, this.GetLength());
            }
        }

        public virtual decimal AsNumber
        {
            get
            {
                decimal result;
                if (this.IsSmallInt)
                {
                    result = this.SmallInt;
                }
                else
                {
                    if (this.IsInt)
                    {
                        result = this.Int;
                    }
                    else
                    {
                        if (this.IsUInt)
                        {
                            result = this.UInt;
                        }
                        else
                        {
                            if (this.IsDouble)
                            {
                                result = (decimal)this.AsDouble;
                            }
                            else
                            {
                                throw new VPackValueTypeException(ValueType.INT, ValueType.UINT, ValueType.SMALLINT);
                            }
                        }
                    }
                }

                return result;
            }
        }

        public virtual long AsLong
        {
            get
            {
                return (long)this.AsNumber;
            }
        }

        public virtual int AsInt
        {
            get
            {
                return (int)this.AsNumber;
            }
        }

        public virtual float AsFloat
        {
            get
            {
                return (float)this.AsDoubleUnchecked;
            }
        }

        public virtual short AsShort
        {
            get
            {
                return (short)this.AsNumber;
            }
        }

        public virtual BigInteger AsBigInteger
        {
            get
            {
                if (this.IsSmallInt || this.IsInt)
                {
                    return new BigInteger(this.AsLong);
                }

                if (this.IsUInt)
                {
                    return NumberUtil.ToBigInteger(this.vpack, this.start + 1, this.GetLength());
                }

                throw new VPackValueTypeException(ValueType.INT, ValueType.UINT, ValueType.SMALLINT);
            }
        }

        public virtual DateTime AsDate
        {
            get
            {
                if (!this.IsDate)
                {
                    throw new VPackValueTypeException(ValueType.UTC_DATE);
                }

                return DateUtil.ToDate(this.vpack, this.start + 1, this.GetLength());
            }
        }

        public virtual string AsString
        {
            get
            {
                if (!this.IsString)
                {
                    throw new VPackValueTypeException(ValueType.STRING);
                }

                return this.IsLongString ? this.LongString : this.ShortString;
            }
        }

        public virtual char AsChar
        {
            get
            {
                return this.AsString[0];
            }
        }

        private bool IsLongString
        {
            get
            {
                return this.Head == 0xbf;
            }
        }

        private string ShortString
        {
            get
            {
                return StringUtil.ToString(this.vpack, this.start + 1, this.GetLength());
            }
        }

        private string LongString
        {
            get
            {
                return StringUtil.ToString(this.vpack, this.start + 9, this.LongStringLength);
            }
        }

        private int LongStringLength
        {
            get
            {
                return (int)NumberUtil.ToLong(this.vpack, this.start + 1, 8);
            }
        }

        private int StringLength
        {
            get
            {
                return this.IsLongString ? this.LongStringLength : this.Head - 0x40;
            }
        }

        public virtual byte[] AsBinary
        {
            get
            {
                if (!this.IsBinary)
                {
                    throw new VPackValueTypeException(ValueType.BINARY);
                }

                byte[] binary = BinaryUtil.ToBinary(this.vpack, this.start + 1 + this.Head - 0xbf, this.BinaryLength);
                return binary;
            }
        }

        public virtual int BinaryLength
        {
            get
            {
                if (!this.IsBinary)
                {
                    throw new VPackValueTypeException(ValueType.BINARY);
                }

                return this.BinaryLengthUnchecked;
            }
        }

        private int BinaryLengthUnchecked
        {
            get
            {
                return (int)NumberUtil.ToLong(this.vpack, this.start + 1, this.Head - 0xbf);
            }
        }

        /// <returns>the number of members for an Array, Object or String</returns>
        public virtual int Length
        {
            get
            {
                long length;
                if (this.IsString)
                {
                    length = this.StringLength;
                }
                else
                {
                    if (!this.IsArray && !this.IsObject)
                    {
                        throw new VPackValueTypeException(ValueType.ARRAY, ValueType.OBJECT, ValueType.STRING);
                    }

                    byte head = this.Head;
                    if (head == 0x01 || head == 0x0a)
                    {
                        // empty
                        length = 0;
                    }
                    else
                    {
                        if (head == 0x13 || head == 0x14)
                        {
                            // compact array or object
                            long end = NumberUtil.ReadVariableValueLength(this.vpack, this.start + 1, false);
                            length = NumberUtil.ReadVariableValueLength(this.vpack, (int)(this.start + end - 1), true);
                        }
                        else
                        {
                            int offsetsize = ObjectArrayUtil.GetOffsetSize(head);
                            long end = NumberUtil.ToLong(this.vpack, this.start + 1, offsetsize);
                            if ((sbyte)head <= 0x05)
                            {
                                // array with no offset table or length
                                int dataOffset = this.FindDataOffset();
                                VPackSlice first = new VPackSlice(this.vpack, this.start + dataOffset);
                                length = (end - dataOffset) / first.ByteSize;
                            }
                            else
                            {
                                if (offsetsize < 8)
                                {
                                    length = NumberUtil.ToLong(this.vpack, this.start + 1 + offsetsize, offsetsize);
                                }
                                else
                                {
                                    length = NumberUtil.ToLong(
                                        this.vpack,
                                        (int)(this.start + end - offsetsize),
                                        offsetsize);
                                }
                            }
                        }
                    }
                }

                return (int)length;
            }
        }

        /// <summary>Must be called for a nonempty array or object at start():</summary>
        protected internal virtual int FindDataOffset()
        {
            int fsm = ObjectArrayUtil.GetFirstSubMap(this.Head);
            int offset;
            if (fsm <= 2 && this.vpack[this.start + 2] != 0)
            {
                offset = 2;
            }
            else
            {
                if (fsm <= 3 && this.vpack[this.start + 3] != 0)
                {
                    offset = 3;
                }
                else
                {
                    if (fsm <= 5 && this.vpack[this.start + 6] != 0)
                    {
                        offset = 5;
                    }
                    else
                    {
                        offset = 9;
                    }
                }
            }

            return offset;
        }

        public virtual int ByteSize
        {
            get
            {
                long size;
                byte head = Head;
                int valueLength = ValueLengthUtil.Get(head);
                if (valueLength != 0)
                {
                    size = valueLength;
                }
                else
                {
                    switch (this.Type())
                    {
                        case ValueType.ARRAY:
                        case ValueType.OBJECT:
                            {
                                if (head == 0x13 || head == 0x14)
                                {
                                    // compact Array or Object
                                    size = NumberUtil.ReadVariableValueLength(this.vpack, this.start + 1, false);
                                }
                                else
                                {
                                    /* if (head <= 0x14) */
                                    size = NumberUtil.ToLong(
                                        this.vpack,
                                        this.start + 1,
                                        ObjectArrayUtil.GetOffsetSize(head));
                                }

                                break;
                            }

                        case ValueType.STRING:
                            {
                                // long UTF-8 String
                                size = this.LongStringLength + 1 + 8;
                                break;
                            }

                        case ValueType.BINARY:
                            {
                                size = 1 + head - 0xbf + this.BinaryLengthUnchecked;
                                break;
                            }

                        case ValueType.BCD:
                            {
                                if ((sbyte)head <= 0xcf)
                                {
                                    size = 1 + head + 0xc7 + NumberUtil.ToLong(this.vpack, this.start + 1, head - 0xc7);
                                }
                                else
                                {
                                    size = 1 + head - 0xcf + NumberUtil.ToLong(this.vpack, this.start + 1, head - 0xcf);
                                }

                                break;
                            }

                        case ValueType.CUSTOM:
                            {
                                if (head == 0xf4 || head == 0xf5 || head == 0xf6)
                                {
                                    size = 2 + NumberUtil.ToLong(this.vpack, this.start + 1, 1);
                                }
                                else
                                {
                                    if (head == 0xf7 || head == 0xf8 || head == 0xf9)
                                    {
                                        size = 3 + NumberUtil.ToLong(this.vpack, this.start + 1, 2);
                                    }
                                    else
                                    {
                                        if (head == 0xfa || head == 0xfb || head == 0xfc)
                                        {
                                            size = 5 + NumberUtil.ToLong(this.vpack, this.start + 1, 4);
                                        }
                                        else
                                        {
                                            /* if (head == 0xfd || head == 0xfe || head == 0xff) */
                                            size = 9 + NumberUtil.ToLong(this.vpack, this.start + 1, 8);
                                        }
                                    }
                                }

                                break;
                            }

                        default:
                            {
                                // TODO
                                throw new InvalidOperationException();
                            }
                    }
                }

                return (int)size;
            }
        }

        /// <returns>array value at the specified index</returns>
        /// <exception cref="VPackValueTypeException"/>
        public virtual VPackSlice Get(int index)
        {
            if (!this.IsArray)
            {
                throw new VPackValueTypeException(ValueType.ARRAY);
            }

            return this.GetNth(index);
        }

        /// <exception cref="VPackException"/>
        public virtual VPackSlice Get(string attribute)
        {
            if (!this.IsObject)
            {
                throw new VPackValueTypeException(ValueType.OBJECT);
            }

            byte head = Head;
            VPackSlice result = new VPackSlice();
            if (head == 0x0a)
            {
                // special case, empty object
                result = new VPackSlice();
            }
            else
            {
                if (head == 0x14)
                {
                    // compact Object
                    result = this.GetFromCompactObject(attribute);
                }
                else
                {
                    int offsetsize = ObjectArrayUtil.GetOffsetSize(head);
                    long end = NumberUtil.ToLong(this.vpack, this.start + 1, offsetsize);
                    long n;
                    if (offsetsize < 8)
                    {
                        n = NumberUtil.ToLong(this.vpack, this.start + 1 + offsetsize, offsetsize);
                    }
                    else
                    {
                        n = NumberUtil.ToLong(this.vpack, (int)(this.start + end - offsetsize), offsetsize);
                    }

                    if (n == 1)
                    {
                        // Just one attribute, there is no index table!
                        VPackSlice key = new VPackSlice(this.vpack, this.start + this.FindDataOffset());
                        if (key.IsString)
                        {
                            if (key.IsEqualString(attribute))
                            {
                                result = new VPackSlice(this.vpack, key.start + key.ByteSize);
                            }
                            else
                            {
                                // no match
                                result = new VPackSlice();
                            }
                        }
                        else
                        {
                            if (key.IsInteger)
                            {
                                // translate key
                                if (attributeTranslator == null)
                                {
                                    throw new VPackNeedAttributeTranslatorException();
                                }

                                if (key.TranslateUnchecked().IsEqualString(attribute))
                                {
                                    result = new VPackSlice(this.vpack, key.start + key.ByteSize);
                                }
                                else
                                {
                                    // no match
                                    result = new VPackSlice();
                                }
                            }
                            else
                            {
                                // no match
                                result = new VPackSlice();
                            }
                        }
                    }
                    else
                    {
                        long ieBase = end - n * offsetsize - (offsetsize == 8 ? 8 : 0);

                        // only use binary search for attributes if we have at least
                        // this many entries
                        // otherwise we'll always use the linear search
                        long sortedSearchEntriesThreshold = 4;
                        bool sorted = head >= 0x0b && (sbyte)head <= 0x0e;
                        if (sorted && n >= sortedSearchEntriesThreshold)
                        {
                            // This means, we have to handle the special case n == 1
                            // only in the linear search!
                            result = this.SearchObjectKeyBinary(attribute, ieBase, offsetsize, n);
                        }
                        else
                        {
                            result = this.SearchObjectKeyLinear(attribute, ieBase, offsetsize, n);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>translates an integer key into a string, without checks</summary>
        internal virtual VPackSlice TranslateUnchecked()
        {
            VPackSlice result = attributeTranslator.Translate(this.AsInt);
            return result != null ? result : new VPackSlice();
        }

        /// <exception cref="VPackKeyTypeException"/>
        /// <exception cref="VPackNeedAttributeTranslatorException
        /// 	"/>
        internal virtual VPackSlice MakeKey()
        {
            if (this.IsString)
            {
                return this;
            }

            if (this.IsInteger)
            {
                if (attributeTranslator == null)
                {
                    throw new VPackNeedAttributeTranslatorException();
                }

                return this.TranslateUnchecked();
            }

            throw new VPackKeyTypeException("Cannot translate key of this type");
        }

        /// <exception cref="VPackKeyTypeException"/>
        /// <exception cref="VPackNeedAttributeTranslatorException
        /// 	"/>
        private VPackSlice GetFromCompactObject(string attribute)
        {
            for (IEnumerator<IEntry<string, VPackSlice>> iterator = this.ObjectIterator(); iterator.MoveNext();)
            {
                IEntry<string, VPackSlice> next = iterator.Current;
                if (next.Key.Equals(attribute))
                {
                    return next.Value;
                }
            }

            // not found
            return new VPackSlice();
        }

        /// <exception cref="VPackValueTypeException"/>
        /// <exception cref="VPackNeedAttributeTranslatorException
        /// 	"/>
        private VPackSlice SearchObjectKeyBinary(string attribute, long ieBase, int offsetsize, long n)
        {
            bool useTranslator = attributeTranslator != null;
            VPackSlice result;
            long l = 0;
            long r = n - 1;
            for (;;)
            {
                // midpoint
                long index = l + (r - l) / 2;
                long offset = ieBase + index * offsetsize;
                long keyIndex = NumberUtil.ToLong(this.vpack, (int)(this.start + offset), offsetsize);
                VPackSlice key = new VPackSlice(this.vpack, (int)(this.start + keyIndex));
                int res = 0;
                if (key.IsString)
                {
                    res = key.CompareString(attribute);
                }
                else
                {
                    if (key.IsInteger)
                    {
                        // translate key
                        if (!useTranslator)
                        {
                            // no attribute translator
                            throw new VPackNeedAttributeTranslatorException();
                        }

                        res = key.TranslateUnchecked().CompareString(attribute);
                    }
                    else
                    {
                        // invalid key
                        result = new VPackSlice();
                        break;
                    }
                }

                if (res == 0)
                {
                    // found
                    result = new VPackSlice(this.vpack, key.start + key.ByteSize);
                    break;
                }

                if (res > 0)
                {
                    if (index == 0)
                    {
                        result = new VPackSlice();
                        break;
                    }

                    r = index - 1;
                }
                else
                {
                    l = index + 1;
                }

                if (r < l)
                {
                    result = new VPackSlice();
                    break;
                }
            }

            return result;
        }

        /// <exception cref="VPackValueTypeException"/>
        /// <exception cref="VPackNeedAttributeTranslatorException
        /// 	"/>
        private VPackSlice SearchObjectKeyLinear(string attribute, long ieBase, int offsetsize, long n)
        {
            bool useTranslator = attributeTranslator != null;
            VPackSlice result = new VPackSlice();
            for (long index = 0; index < n; index++)
            {
                long offset = ieBase + index * offsetsize;
                long keyIndex = NumberUtil.ToLong(this.vpack, (int)(this.start + offset), offsetsize);
                VPackSlice key = new VPackSlice(this.vpack, (int)(this.start + keyIndex));
                if (key.IsString)
                {
                    if (!key.IsEqualString(attribute))
                    {
                        continue;
                    }
                }
                else
                {
                    if (key.IsInteger)
                    {
                        // translate key
                        if (!useTranslator)
                        {
                            // no attribute translator
                            throw new VPackNeedAttributeTranslatorException();
                        }

                        if (!key.TranslateUnchecked().IsEqualString(attribute))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        // invalid key type
                        result = new VPackSlice();
                        break;
                    }
                }

                // key is identical. now return value
                result = new VPackSlice(this.vpack, key.start + key.ByteSize);
                break;
            }

            return result;
        }

        public virtual VPackSlice KeyAt(int index)
        {
            if (!this.IsObject)
            {
                throw new VPackValueTypeException(ValueType.OBJECT);
            }

            return this.GetNthKey(index);
        }

        public virtual VPackSlice ValueAt(int index)
        {
            if (!this.IsObject)
            {
                throw new VPackValueTypeException(ValueType.OBJECT);
            }

            VPackSlice key = this.GetNthKey(index);
            return new VPackSlice(this.vpack, key.start + key.ByteSize);
        }

        private VPackSlice GetNthKey(int index)
        {
            return new VPackSlice(this.vpack, this.start + this.GetNthOffset(index));
        }

        private VPackSlice GetNth(int index)
        {
            return new VPackSlice(this.vpack, this.start + this.GetNthOffset(index));
        }

        /// <returns>the offset for the nth member from an Array or Object type</returns>
        private int GetNthOffset(int index)
        {
            int offset;
            byte head = Head;
            if (head == 0x13 || head == 0x14)
            {
                // compact Array or Object
                offset = this.GetNthOffsetFromCompact(index);
            }
            else
            {
                if (head == 0x01 || head == 0x0a)
                {
                    // special case: empty Array or empty Object
                    throw new IndexOutOfRangeException();
                }

                long n;
                int offsetsize = ObjectArrayUtil.GetOffsetSize(head);
                long end = NumberUtil.ToLong(this.vpack, this.start + 1, offsetsize);
                int dataOffset = this.FindDataOffset();
                if ((sbyte)head <= 0x05)
                {
                    // array with no offset table or length
                    VPackSlice first = new VPackSlice(this.vpack, this.start + dataOffset);
                    n = (end - dataOffset) / first.ByteSize;
                }
                else
                {
                    if (offsetsize < 8)
                    {
                        n = NumberUtil.ToLong(this.vpack, this.start + 1 + offsetsize, offsetsize);
                    }
                    else
                    {
                        n = NumberUtil.ToLong(this.vpack, (int)(this.start + end - offsetsize), offsetsize);
                    }
                }

                if (index >= n)
                {
                    throw new IndexOutOfRangeException();
                }

                if ((sbyte)head <= 0x05 || n == 1)
                {
                    // no index table, but all array items have the same length
                    // or only one item is in the array
                    // now fetch first item and determine its length
                    if (dataOffset == 0)
                    {
                        dataOffset = this.FindDataOffset();
                    }

                    offset = dataOffset + index * new VPackSlice(this.vpack, this.start + dataOffset).ByteSize;
                }
                else
                {
                    long ieBase = end - n * offsetsize + index * offsetsize - (offsetsize == 8 ? 8 : 0);
                    offset = (int)NumberUtil.ToLong(this.vpack, (int)(this.start + ieBase), offsetsize);
                }
            }

            return offset;
        }

        /// <returns>the offset for the nth member from a compact Array or Object type</returns>
        private int GetNthOffsetFromCompact(int index)
        {
            long end = NumberUtil.ReadVariableValueLength(this.vpack, this.start + 1, false);
            long n = NumberUtil.ReadVariableValueLength(this.vpack, (int)(this.start + end - 1), true);
            if (index >= n)
            {
                throw new IndexOutOfRangeException();
            }

            byte head = Head;
            long offset = 1 + NumberUtil.GetVariableValueLength(end);
            long current = 0;
            while (current != index)
            {
                long byteSize = new VPackSlice(this.vpack, (int)(this.start + offset)).ByteSize;
                offset += byteSize;
                if (head == 0x14)
                {
                    offset += byteSize;
                }

                ++current;
            }

            return (int)offset;
        }

        private bool IsEqualString(string s)
        {
            string @string = this.AsString;
            return @string.Equals(s);
        }

        private int CompareString(string s)
        {
            string @string = this.AsString;
            return string.CompareOrdinal(@string, s);
        }

        public virtual IEnumerator<VPackSlice> ArrayIterator()
        {
            if (this.IsArray)
            {
                return new ArrayIterator(this);
            }

            throw new VPackValueTypeException(ValueType.ARRAY);
        }

        public virtual IEnumerator<IEntry<string, VPackSlice>> ObjectIterator()
        {
            if (this.IsObject)
            {
                return new ObjectIterator(this);
            }

            throw new VPackValueTypeException(ValueType.OBJECT);
        }

        protected internal virtual byte[] GetRawVPack()
        {
            return this.vpack.CopyOfRange(this.start, this.start + this.ByteSize);
        }

        public override string ToString()
        {
            try
            {
                return new VPackParser().ToJson(this, true);
            }
            catch (VPackException)
            {
                return base.ToString();
            }
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + this.start;
            result = prime * result + this.GetRawVPack().ByteArrayHashCode();
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            VPackSlice other = (VPackSlice)obj;
            if (this.start != other.start)
            {
                return false;
            }

            if (!this.GetRawVPack().SequenceEqual(other.GetRawVPack()))
            {
                return false;
            }

            return true;
        }
    }
}