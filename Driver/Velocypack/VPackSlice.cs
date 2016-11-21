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
    using global::ArangoDB.Velocypack.Internal;
    using global::ArangoDB.Velocypack.Internal.Util;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackSlice
    {
        private const long serialVersionUID = -3452953589283603980L;

        public static readonly VPackAttributeTranslator attributeTranslator
             = new VPackAttributeTranslatorImpl();

        private readonly byte[] vpack;

        private readonly int start;

        protected internal VPackSlice()
            : this(new byte[] { unchecked((int)0x00) }, 0)
        {
        }

        public VPackSlice(byte[] vpack)
            : this(vpack, 0)
        {
        }

        public VPackSlice(byte[] vpack, int start)
            : base()
        {
            this.vpack = vpack;
            this.start = start;
        }

        public virtual byte head()
        {
            return this.vpack[this.start];
        }

        public virtual byte[] getBuffer()
        {
            return this.vpack;
        }

        public virtual int getStart()
        {
            return this.start;
        }

        protected internal virtual ValueType type()
        {
            return ValueTypeUtil.get(this.head());
        }

        private int length()
        {
            return ValueLengthUtil.get(this.head()) - 1;
        }

        private bool isType(ValueType type)
        {
            return type() == type;
        }

        public virtual bool isNone()
        {
            return this.isType(ValueType.NONE);
        }

        public virtual bool isNull()
        {
            return this.isType(ValueType.NULL);
        }

        public virtual bool isIllegal()
        {
            return this.isType(ValueType.ILLEGAL);
        }

        public virtual bool isBoolean()
        {
            return this.isType(ValueType.BOOL);
        }

        public virtual bool isTrue()
        {
            return this.head() == unchecked((int)0x1a);
        }

        public virtual bool isFalse()
        {
            return this.head() == unchecked((int)0x19);
        }

        public virtual bool isArray()
        {
            return this.isType(ValueType.ARRAY);
        }

        public virtual bool isObject()
        {
            return this.isType(ValueType.OBJECT);
        }

        public virtual bool isDouble()
        {
            return this.isType(ValueType.DOUBLE);
        }

        public virtual bool isDate()
        {
            return this.isType(ValueType.UTC_DATE);
        }

        public virtual bool isExternal()
        {
            return this.isType(ValueType.EXTERNAL);
        }

        public virtual bool isMinKey()
        {
            return this.isType(ValueType.MIN_KEY);
        }

        public virtual bool isMaxKey()
        {
            return this.isType(ValueType.MAX_KEY);
        }

        public virtual bool isInt()
        {
            return this.isType(ValueType.INT);
        }

        public virtual bool isUInt()
        {
            return this.isType(ValueType.UINT);
        }

        public virtual bool isSmallInt()
        {
            return this.isType(ValueType.SMALLINT);
        }

        public virtual bool isInteger()
        {
            return this.isInt() || this.isUInt() || this.isSmallInt();
        }

        public virtual bool isNumber()
        {
            return this.isInteger() || this.isDouble();
        }

        public virtual bool isString()
        {
            return this.isType(ValueType.STRING);
        }

        public virtual bool isBinary()
        {
            return this.isType(ValueType.BINARY);
        }

        public virtual bool isBCD()
        {
            return this.isType(ValueType.BCD);
        }

        public virtual bool isCustom()
        {
            return this.isType(ValueType.CUSTOM);
        }

        public virtual bool getAsBoolean()
        {
            if (!this.isBoolean())
            {
                throw new VPackValueTypeException(ValueType
                    .BOOL);
            }
            return this.isTrue();
        }

        public virtual double getAsDouble()
        {
            if (!this.isDouble())
            {
                throw new VPackValueTypeException(ValueType
                    .DOUBLE);
            }
            return this.getAsDoubleUnchecked();
        }

        private double getAsDoubleUnchecked()
        {
            return NumberUtil.toDouble(this.vpack, this.start +
                1, this.length());
        }

        public virtual java.math.BigDecimal getAsBigDecimal()
        {
            return new java.math.BigDecimal(this.getAsDouble());
        }

        private long getSmallInt()
        {
            byte head = head();
            long smallInt;
            if (head >= unchecked((int)0x30) && (sbyte)head <= unchecked((int)0x39))
            {
                smallInt = head - unchecked((int)0x30);
            }
            else
            {
                /* if (head >= 0x3a && head <= 0x3f) */
                smallInt = head - unchecked((int)0x3a) - 6;
            }
            return smallInt;
        }

        private long getInt()
        {
            return NumberUtil.toLong(this.vpack, this.start + 1,
                this.length());
        }

        private long getUInt()
        {
            return NumberUtil.toLong(this.vpack, this.start + 1,
                this.length());
        }

        public virtual java.lang.Number getAsNumber()
        {
            java.lang.Number result;
            if (this.isSmallInt())
            {
                result = this.getSmallInt();
            }
            else
            {
                if (this.isInt())
                {
                    result = this.getInt();
                }
                else
                {
                    if (this.isUInt())
                    {
                        result = this.getUInt();
                    }
                    else
                    {
                        if (this.isDouble())
                        {
                            result = this.getAsDouble();
                        }
                        else
                        {
                            throw new VPackValueTypeException(ValueType
                                .INT, ValueType.UINT, ValueType.
                                SMALLINT);
                        }
                    }
                }
            }
            return result;
        }

        public virtual long getAsLong()
        {
            return this.getAsNumber();
        }

        public virtual int getAsInt()
        {
            return this.getAsNumber();
        }

        public virtual float getAsFloat()
        {
            return (float)this.getAsDoubleUnchecked();
        }

        public virtual short getAsShort()
        {
            return this.getAsNumber();
        }

        public virtual java.math.BigInteger getAsBigInteger()
        {
            if (this.isSmallInt() || this.isInt())
            {
                return java.math.BigInteger.valueOf(this.getAsLong());
            }
            else
            {
                if (this.isUInt())
                {
                    return NumberUtil.toBigInteger(this.vpack, this.start
                         + 1, this.length());
                }
                else
                {
                    throw new VPackValueTypeException(ValueType
                        .INT, ValueType.UINT, ValueType.
                        SMALLINT);
                }
            }
        }

        public virtual System.DateTime getAsDate()
        {
            if (!this.isDate())
            {
                throw new VPackValueTypeException(ValueType
                    .UTC_DATE);
            }
            return DateUtil.toDate(this.vpack, this.start + 1, this.length
                ());
        }

        public virtual java.sql.Date getAsSQLDate()
        {
            if (!this.isDate())
            {
                throw new VPackValueTypeException(ValueType
                    .UTC_DATE);
            }
            return DateUtil.toSQLDate(this.vpack, this.start + 1
                , this.length());
        }

        public virtual java.sql.Timestamp getAsSQLTimestamp()
        {
            if (!this.isDate())
            {
                throw new VPackValueTypeException(ValueType
                    .UTC_DATE);
            }
            return DateUtil.toSQLTimestamp(this.vpack, this.start
                 + 1, this.length());
        }

        public virtual string getAsString()
        {
            if (!this.isString())
            {
                throw new VPackValueTypeException(ValueType
                    .STRING);
            }
            return this.isLongString() ? this.getLongString() : this.getShortString();
        }

        public virtual char getAsChar()
        {
            return this.getAsString()[0];
        }

        private bool isLongString()
        {
            return this.head() == unchecked((byte)unchecked((int)0xbf));
        }

        private string getShortString()
        {
            return StringUtil.toString(this.vpack, this.start +
                1, this.length());
        }

        private string getLongString()
        {
            return StringUtil.toString(this.vpack, this.start +
                9, this.getLongStringLength());
        }

        private int getLongStringLength()
        {
            return (int)NumberUtil.toLong(this.vpack, this.start
                 + 1, 8);
        }

        private int getStringLength()
        {
            return this.isLongString() ? this.getLongStringLength() : this.head() - unchecked((int)0x40);
        }

        public virtual byte[] getAsBinary()
        {
            if (!this.isBinary())
            {
                throw new VPackValueTypeException(ValueType
                    .BINARY);
            }
            byte[] binary = BinaryUtil.toBinary(this.vpack,
                this.start + 1 + this.head() - unchecked((byte)unchecked((int)0xbf)), this.getBinaryLength(
                ));
            return binary;
        }

        public virtual int getBinaryLength()
        {
            if (!this.isBinary())
            {
                throw new VPackValueTypeException(ValueType
                    .BINARY);
            }
            return this.getBinaryLengthUnchecked();
        }

        private int getBinaryLengthUnchecked()
        {
            return (int)NumberUtil.toLong(this.vpack, this.start
                 + 1, this.head() - unchecked((byte)unchecked((int)0xbf)));
        }

        /// <returns>the number of members for an Array, Object or String</returns>
        public virtual int getLength()
        {
            long length;
            if (this.isString())
            {
                length = this.getStringLength();
            }
            else
            {
                if (!this.isArray() && !this.isObject())
                {
                    throw new VPackValueTypeException(ValueType
                        .ARRAY, ValueType.OBJECT, ValueType
                        .STRING);
                }
                else
                {
                    byte head = head();
                    if (head == unchecked((int)0x01) || head == unchecked((int)0x0a))
                    {
                        // empty
                        length = 0;
                    }
                    else
                    {
                        if (head == unchecked((int)0x13) || head == unchecked((int)0x14))
                        {
                            // compact array or object
                            long end = NumberUtil.readVariableValueLength
                                (this.vpack, this.start + 1, false);
                            length = NumberUtil.readVariableValueLength
                                (this.vpack, (int)(this.start + end - 1), true);
                        }
                        else
                        {
                            int offsetsize = ObjectArrayUtil.getOffsetSize
                                (head);
                            long end = NumberUtil.toLong(this.vpack, this.start
                                + 1, offsetsize);
                            if ((sbyte)head <= unchecked((int)0x05))
                            {
                                // array with no offset table or length
                                int dataOffset = this.findDataOffset();
                                VPackSlice first = new VPackSlice
                                    (this.vpack, this.start + dataOffset);
                                length = (end - dataOffset) / first.getByteSize();
                            }
                            else
                            {
                                if (offsetsize < 8)
                                {
                                    length = NumberUtil.toLong(this.vpack, this.start +
                                        1 + offsetsize, offsetsize);
                                }
                                else
                                {
                                    length = NumberUtil.toLong(this.vpack, (int)(this.start
                                         + end - offsetsize), offsetsize);
                                }
                            }
                        }
                    }
                }
            }
            return (int)length;
        }

        public virtual int size()
        {
            return this.getLength();
        }

        /// <summary>Must be called for a nonempty array or object at start():</summary>
        protected internal virtual int findDataOffset()
        {
            int fsm = ObjectArrayUtil.getFirstSubMap(this.head
                ());
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

        public virtual int getByteSize()
        {
            long size;
            byte head = head();
            int valueLength = ValueLengthUtil.get(head
                );
            if (valueLength != 0)
            {
                size = valueLength;
            }
            else
            {
                switch (this.type())
                {
                    case ValueType.ARRAY:
                    case ValueType.OBJECT:
                        {
                            if (head == unchecked((int)0x13) || head == unchecked((int)0x14))
                            {
                                // compact Array or Object
                                size = NumberUtil.readVariableValueLength(
                                    this.vpack, this.start + 1, false);
                            }
                            else
                            {
                                /* if (head <= 0x14) */
                                size = NumberUtil.toLong(this.vpack, this.start + 1,
                                    ObjectArrayUtil.getOffsetSize(head));
                            }
                            break;
                        }

                    case ValueType.STRING:
                        {
                            // long UTF-8 String
                            size = this.getLongStringLength() + 1 + 8;
                            break;
                        }

                    case ValueType.BINARY:
                        {
                            size = 1 + head - unchecked((byte)unchecked((int)0xbf)) + this.getBinaryLengthUnchecked
                                ();
                            break;
                        }

                    case ValueType.BCD:
                        {
                            if ((sbyte)head <= unchecked((int)0xcf))
                            {
                                size = 1 + head + unchecked((byte)unchecked((int)0xc7)) + NumberUtil
                                    .toLong(this.vpack, this.start + 1, head - unchecked((byte)unchecked((int)0xc7)));
                            }
                            else
                            {
                                size = 1 + head - unchecked((byte)unchecked((int)0xcf)) + NumberUtil
                                    .toLong(this.vpack, this.start + 1, head - unchecked((byte)unchecked((int)0xcf)));
                            }
                            break;
                        }

                    case ValueType.CUSTOM:
                        {
                            if (head == unchecked((int)0xf4) || head == unchecked((int)0xf5) || head == unchecked(
                                (int)0xf6))
                            {
                                size = 2 + NumberUtil.toLong(this.vpack, this.start
                                    + 1, 1);
                            }
                            else
                            {
                                if (head == unchecked((int)0xf7) || head == unchecked((int)0xf8) || head == unchecked(
                                    (int)0xf9))
                                {
                                    size = 3 + NumberUtil.toLong(this.vpack, this.start
                                        + 1, 2);
                                }
                                else
                                {
                                    if (head == unchecked((int)0xfa) || head == unchecked((int)0xfb) || head == unchecked(
                                        (int)0xfc))
                                    {
                                        size = 5 + NumberUtil.toLong(this.vpack, this.start
                                            + 1, 4);
                                    }
                                    else
                                    {
                                        /* if (head == 0xfd || head == 0xfe || head == 0xff) */
                                        size = 9 + NumberUtil.toLong(this.vpack, this.start
                                            + 1, 8);
                                    }
                                }
                            }
                            break;
                        }

                    default:
                        {
                            // TODO
                            throw new java.lang.InternalError();
                        }
                }
            }
            return (int)size;
        }

        /// <returns>array value at the specified index</returns>
        /// <exception cref="VPackValueTypeException"/>
        public virtual VPackSlice get(int index)
        {
            if (!this.isArray())
            {
                throw new VPackValueTypeException(ValueType
                    .ARRAY);
            }
            return this.getNth(index);
        }

        /// <exception cref="VPackException"/>
        public virtual VPackSlice get(string attribute)
        {
            if (!this.isObject())
            {
                throw new VPackValueTypeException(ValueType
                    .OBJECT);
            }
            byte head = head();
            VPackSlice result = new VPackSlice
                ();
            if (head == unchecked((int)0x0a))
            {
                // special case, empty object
                result = new VPackSlice();
            }
            else
            {
                if (head == unchecked((int)0x14))
                {
                    // compact Object
                    result = this.getFromCompactObject(attribute);
                }
                else
                {
                    int offsetsize = ObjectArrayUtil.getOffsetSize
                        (head);
                    long end = NumberUtil.toLong(this.vpack, this.start
                        + 1, offsetsize);
                    long n;
                    if (offsetsize < 8)
                    {
                        n = NumberUtil.toLong(this.vpack, this.start + 1 + offsetsize
                            , offsetsize);
                    }
                    else
                    {
                        n = NumberUtil.toLong(this.vpack, (int)(this.start +
                             end - offsetsize), offsetsize);
                    }
                    if (n == 1)
                    {
                        // Just one attribute, there is no index table!
                        VPackSlice key = new VPackSlice(this.vpack
                            , this.start + this.findDataOffset());
                        if (key.isString())
                        {
                            if (key.isEqualString(attribute))
                            {
                                result = new VPackSlice(this.vpack, key.start + key.getByteSize
                                    ());
                            }
                            else
                            {
                                // no match
                                result = new VPackSlice();
                            }
                        }
                        else
                        {
                            if (key.isInteger())
                            {
                                // translate key
                                if (VPackSlice.attributeTranslator == null)
                                {
                                    throw new VPackNeedAttributeTranslatorException
                                        ();
                                }
                                if (key.translateUnchecked().isEqualString(attribute))
                                {
                                    result = new VPackSlice(this.vpack, key.start + key.getByteSize
                                        ());
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
                        bool sorted = head >= unchecked((int)0x0b) && (sbyte)head <= unchecked((int)0x0e);
                        if (sorted && n >= sortedSearchEntriesThreshold)
                        {
                            // This means, we have to handle the special case n == 1
                            // only in the linear search!
                            result = this.searchObjectKeyBinary(attribute, ieBase, offsetsize, n);
                        }
                        else
                        {
                            result = this.searchObjectKeyLinear(attribute, ieBase, offsetsize, n);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>translates an integer key into a string, without checks</summary>
        protected internal virtual VPackSlice translateUnchecked(
            )
        {
            VPackSlice result = attributeTranslator.translate(this.getAsInt
                ());
            return result != null ? result : new VPackSlice();
        }

        /// <exception cref="com.arangodb.velocypack.exception.VPackKeyTypeException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackNeedAttributeTranslatorException
        /// 	"/>
        protected internal virtual VPackSlice makeKey()
        {
            if (this.isString())
            {
                return this;
            }
            if (this.isInteger())
            {
                if (VPackSlice.attributeTranslator == null)
                {
                    throw new VPackNeedAttributeTranslatorException
                        ();
                }
                return this.translateUnchecked();
            }
            throw new VPackKeyTypeException("Cannot translate key of this type"
                );
        }

        /// <exception cref="com.arangodb.velocypack.exception.VPackKeyTypeException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackNeedAttributeTranslatorException
        /// 	"/>
        private VPackSlice getFromCompactObject(string attribute)
        {
            for (System.Collections.Generic.IEnumerator<KeyValuePair<string, VPackSlice>> iterator = this.objectIterator(); iterator
                .MoveNext();)
            {
                System.Collections.Generic.KeyValuePair<string, VPackSlice
                    > next = iterator.Current;
                if (next.Key.Equals(attribute))
                {
                    return next.Value;
                }
            }
            // not found
            return new VPackSlice();
        }

        /// <exception cref="com.arangodb.velocypack.exception.VPackValueTypeException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackNeedAttributeTranslatorException
        /// 	"/>
        private VPackSlice searchObjectKeyBinary(string attribute
            , long ieBase, int offsetsize, long n)
        {
            bool useTranslator = VPackSlice.attributeTranslator != null;
            VPackSlice result;
            long l = 0;
            long r = n - 1;
            for (;;)
            {
                // midpoint
                long index = l + (r - l) / 2;
                long offset = ieBase + index * offsetsize;
                long keyIndex = NumberUtil.toLong(this.vpack, (
                    int)(this.start + offset), offsetsize);
                VPackSlice key = new VPackSlice(this.vpack
                    , (int)(this.start + keyIndex));
                int res = 0;
                if (key.isString())
                {
                    res = key.compareString(attribute);
                }
                else
                {
                    if (key.isInteger())
                    {
                        // translate key
                        if (!useTranslator)
                        {
                            // no attribute translator
                            throw new VPackNeedAttributeTranslatorException
                                ();
                        }
                        res = key.translateUnchecked().compareString(attribute);
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
                    result = new VPackSlice(this.vpack, key.start + key.getByteSize
                        ());
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

        /// <exception cref="com.arangodb.velocypack.exception.VPackValueTypeException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackNeedAttributeTranslatorException
        /// 	"/>
        private VPackSlice searchObjectKeyLinear(string attribute
            , long ieBase, int offsetsize, long n)
        {
            bool useTranslator = VPackSlice.attributeTranslator != null;
            VPackSlice result = new VPackSlice
                ();
            for (long index = 0; index < n; index++)
            {
                long offset = ieBase + index * offsetsize;
                long keyIndex = NumberUtil.toLong(this.vpack, (
                    int)(this.start + offset), offsetsize);
                VPackSlice key = new VPackSlice(this.vpack
                    , (int)(this.start + keyIndex));
                if (key.isString())
                {
                    if (!key.isEqualString(attribute))
                    {
                        continue;
                    }
                }
                else
                {
                    if (key.isInteger())
                    {
                        // translate key
                        if (!useTranslator)
                        {
                            // no attribute translator
                            throw new VPackNeedAttributeTranslatorException
                                ();
                        }
                        if (!key.translateUnchecked().isEqualString(attribute))
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
                result = new VPackSlice(this.vpack, key.start + key.getByteSize
                    ());
                break;
            }
            return result;
        }

        public virtual VPackSlice keyAt(int index)
        {
            if (!this.isObject())
            {
                throw new VPackValueTypeException(ValueType
                    .OBJECT);
            }
            return this.getNthKey(index);
        }

        public virtual VPackSlice valueAt(int index)
        {
            if (!this.isObject())
            {
                throw new VPackValueTypeException(ValueType
                    .OBJECT);
            }
            VPackSlice key = this.getNthKey(index);
            return new VPackSlice(this.vpack, key.start + key.getByteSize(
                ));
        }

        private VPackSlice getNthKey(int index)
        {
            return new VPackSlice(this.vpack, this.start + this.getNthOffset(index));
        }

        private VPackSlice getNth(int index)
        {
            return new VPackSlice(this.vpack, this.start + this.getNthOffset(index));
        }

        /// <returns>the offset for the nth member from an Array or Object type</returns>
        private int getNthOffset(int index)
        {
            int offset;
            byte head = head();
            if (head == unchecked((int)0x13) || head == unchecked((int)0x14))
            {
                // compact Array or Object
                offset = this.getNthOffsetFromCompact(index);
            }
            else
            {
                if (head == unchecked((int)0x01) || head == unchecked((int)0x0a))
                {
                    // special case: empty Array or empty Object
                    throw new System.IndexOutOfRangeException();
                }
                else
                {
                    long n;
                    int offsetsize = ObjectArrayUtil.getOffsetSize
                        (head);
                    long end = NumberUtil.toLong(this.vpack, this.start
                        + 1, offsetsize);
                    int dataOffset = this.findDataOffset();
                    if ((sbyte)head <= unchecked((int)0x05))
                    {
                        // array with no offset table or length
                        VPackSlice first = new VPackSlice
                            (this.vpack, this.start + dataOffset);
                        n = (end - dataOffset) / first.getByteSize();
                    }
                    else
                    {
                        if (offsetsize < 8)
                        {
                            n = NumberUtil.toLong(this.vpack, this.start + 1 + offsetsize
                                , offsetsize);
                        }
                        else
                        {
                            n = NumberUtil.toLong(this.vpack, (int)(this.start +
                                 end - offsetsize), offsetsize);
                        }
                    }
                    if (index >= n)
                    {
                        throw new System.IndexOutOfRangeException();
                    }
                    if ((sbyte)head <= unchecked((int)0x05) || n == 1)
                    {
                        // no index table, but all array items have the same length
                        // or only one item is in the array
                        // now fetch first item and determine its length
                        if (dataOffset == 0)
                        {
                            dataOffset = this.findDataOffset();
                        }
                        offset = dataOffset + index * new VPackSlice(this.vpack, this.start
                             + dataOffset).getByteSize();
                    }
                    else
                    {
                        long ieBase = end - n * offsetsize + index * offsetsize - (offsetsize == 8 ? 8 :
                            0);
                        offset = (int)NumberUtil.toLong(this.vpack, (int
                            )(this.start + ieBase), offsetsize);
                    }
                }
            }
            return offset;
        }

        /// <returns>the offset for the nth member from a compact Array or Object type</returns>
        private int getNthOffsetFromCompact(int index)
        {
            long end = NumberUtil.readVariableValueLength
                (this.vpack, this.start + 1, false);
            long n = NumberUtil.readVariableValueLength
                (this.vpack, (int)(this.start + end - 1), true);
            if (index >= n)
            {
                throw new System.IndexOutOfRangeException();
            }
            byte head = head();
            long offset = 1 + NumberUtil.getVariableValueLength
                (end);
            long current = 0;
            while (current != index)
            {
                long byteSize = new VPackSlice(this.vpack, (int)(this.start + offset
                    )).getByteSize();
                offset += byteSize;
                if (head == unchecked((int)0x14))
                {
                    offset += byteSize;
                }
                ++current;
            }
            return (int)offset;
        }

        private bool isEqualString(string s)
        {
            string @string = this.getAsString();
            return @string.Equals(s);
        }

        private int compareString(string s)
        {
            string @string = this.getAsString();
            return string.CompareOrdinal(@string, s);
        }

        public virtual System.Collections.Generic.IEnumerator<VPackSlice
            > arrayIterator()
        {
            if (this.isArray())
            {
                return new ArrayIterator(this);
            }
            else
            {
                throw new VPackValueTypeException(ValueType
                    .ARRAY);
            }
        }

        public virtual System.Collections.Generic.IEnumerator<KeyValuePair<string, VPackSlice>> objectIterator()
        {
            if (this.isObject())
            {
                return new ObjectIterator(this);
            }
            else
            {
                throw new VPackValueTypeException(ValueType
                    .OBJECT);
            }
        }

        protected internal virtual byte[] getRawVPack()
        {
            return java.util.Arrays.copyOfRange(this.vpack, this.start, this.start + this.getByteSize());
        }

        public override string ToString()
        {
            try
            {
                return new VPackParser().toJson(this, true);
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
            result = prime * result + java.util.Arrays.hashCode(this.getRawVPack());
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
            if (Sharpen.Runtime.getClassForObject(this) != Sharpen.Runtime.getClassForObject(
                obj))
            {
                return false;
            }
            VPackSlice other = (VPackSlice)obj;
            if (this.start != other.start)
            {
                return false;
            }
            if (!java.util.Arrays.equals(this.getRawVPack(), other.getRawVPack()))
            {
                return false;
            }
            return true;
        }
    }
}