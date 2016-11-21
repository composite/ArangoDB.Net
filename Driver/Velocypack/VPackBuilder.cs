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
    using System;
    using System.Numerics;

    using global::ArangoDB.Util;
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocypack.Internal;
    using global::ArangoDB.Velocypack.Internal.Util;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackBuilder
    {
        private const int INTEGER_BYTES = sizeof(int);

        private const int LONG_BYTES = sizeof(long);

        private const int DOUBLE_BYTES = sizeof(double);

        public interface IBuilderOptions
        {
            bool IsBuildUnindexedArrays();

            void SetBuildUnindexedArrays(bool buildUnindexedArrays);

            bool IsBuildUnindexedObjects();

            void SetBuildUnindexedObjects(bool buildUnindexedObjects);
        }

        public interface IAppender<T>
        {
            /// <exception cref="VPackBuilderException"/>
            void Append(VPackBuilder builder, T value);
        }

        private sealed class _Appender_74 : VPackBuilder.IAppender
            <Value>
        {
            public _Appender_74()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, Value
                 value)
            {
                builder.Set(value);
            }
        }

        private static readonly VPackBuilder.IAppender<Value
            > VALUE = new _Appender_74();

        private sealed class _Appender_80 : VPackBuilder.IAppender
            <ValueType>
        {
            public _Appender_80()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, ValueType
                 value)
            {
                switch (value)
                {
                    case ValueType.NULL:
                        {
                            builder.AppendNull();
                            break;
                        }

                    case ValueType.ARRAY:
                        {
                            builder.AddArray(false);
                            break;
                        }

                    case ValueType.OBJECT:
                        {
                            builder.AddObject(false);
                            break;
                        }

                    default:
                        {
                            throw new VPackValueTypeException(ValueType
                                .ARRAY, ValueType.OBJECT, ValueType
                                .NULL);
                        }
                }
            }
        }

        private static readonly VPackBuilder.IAppender<ValueType
            > VALUE_TYPE = new _Appender_80();

        private sealed class _Appender_98 : VPackBuilder.IAppender
            <bool>
        {
            public _Appender_98()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, bool value)
            {
                builder.AppendBoolean(value);
            }
        }

        private static readonly VPackBuilder.IAppender<bool> BOOLEAN
             = new _Appender_98();

        private sealed class _Appender_104 : VPackBuilder.IAppender
            <double>
        {
            public _Appender_104()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, double value)
            {
                builder.AppendDouble(value);
            }
        }

        private static readonly VPackBuilder.IAppender<double> DOUBLE
             = new _Appender_104();

        private sealed class _Appender_110 : VPackBuilder.IAppender
            <float>
        {
            public _Appender_110()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, float value)
            {
                builder.AppendDouble(value);
            }
        }

        private static readonly VPackBuilder.IAppender<float> FLOAT
             = new _Appender_110();

        private sealed class _Appender_116 : VPackBuilder.IAppender
            <decimal>
        {
            public _Appender_116()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, decimal
                 value)
            {
                builder.AppendDouble((double)value);
            }
        }

        private static readonly VPackBuilder.IAppender<decimal
            > BIG_DECIMAL = new _Appender_116();

        private sealed class _Appender_122 : VPackBuilder.IAppender
            <long>
        {
            public _Appender_122()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, long value)
            {
                if (value <= 9 && value >= -6)
                {
                    builder.AppendSmallInt(value);
                }
                else
                {
                    builder.Add(unchecked((byte)unchecked((int)0x23)));
                    builder.Append(value, VPackBuilder.INTEGER_BYTES);
                }
            }
        }

        private static readonly VPackBuilder.IAppender<long> LONG =
            new _Appender_122();

        private sealed class _Appender_133 : VPackBuilder.IAppender
            <int>
        {
            public _Appender_133()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, int value)
            {
                if (value <= 9 && value >= -6)
                {
                    builder.AppendSmallInt(value);
                }
                else
                {
                    builder.Add(unchecked((byte)unchecked((int)0x23)));
                    builder.Append(value, VPackBuilder.INTEGER_BYTES);
                }
            }
        }

        private static readonly VPackBuilder.IAppender<int> INTEGER
             = new _Appender_133();

        private sealed class _Appender_144 : VPackBuilder.IAppender
            <short>
        {
            public _Appender_144()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, short value)
            {
                if (value <= 9 && value >= -6)
                {
                    builder.AppendSmallInt(value);
                }
                else
                {
                    builder.Add(unchecked((byte)unchecked((int)0x23)));
                    builder.Append(value, VPackBuilder.INTEGER_BYTES);
                }
            }
        }

        private static readonly VPackBuilder.IAppender<short> SHORT
             = new _Appender_144();

        private sealed class _Appender_166 : VPackBuilder.IAppender
            <System.DateTime>
        {
            public _Appender_166()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, System.DateTime
                value)
            {
                builder.AppendDate(value);
            }
        }

        private static readonly VPackBuilder.IAppender<System.DateTime
            > DATE = new _Appender_166();

        private sealed class _Appender_172 : VPackBuilder.IAppender
            <DateTime>
        {
            public _Appender_172()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, DateTime value
                )
            {
                builder.AppendSqlDate(value);
            }
        }

        private static readonly VPackBuilder.IAppender<DateTime
            > SQL_DATE = new _Appender_172();

        private sealed class _Appender_184 : VPackBuilder.IAppender
            <string>
        {
            public _Appender_184()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, string value)
            {
                builder.AppendString(value);
            }
        }

        private static readonly VPackBuilder.IAppender<string> STRING
             = new _Appender_184();

        private sealed class _Appender_190 : VPackBuilder.IAppender
            <char>
        {
            public _Appender_190()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, char value)
            {
                builder.AppendString(value.ToString());
            }
        }

        private static readonly VPackBuilder.IAppender<char> CHARACTER
             = new _Appender_190();

        private sealed class _Appender_196 : VPackBuilder.IAppender
            <byte[]>
        {
            public _Appender_196()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, byte[] value)
            {
                builder.AppendBinary(value);
            }
        }

        private static readonly VPackBuilder.IAppender<byte[]> BYTE_ARRAY
             = new _Appender_196();

        private sealed class _Appender_202 : VPackBuilder.IAppender
            <VPackSlice>
        {
            public _Appender_202()
            {
            }

            /// <exception cref="VPackBuilderException"/>
            public void Append(VPackBuilder builder, VPackSlice
                 value)
            {
                builder.AppendVPack(value);
            }
        }

        private static readonly VPackBuilder.IAppender<VPackSlice
            > VPACK = new _Appender_202();

        private byte[] buffer;

        private int size;

        private readonly System.Collections.Generic.IList<int> stack;

        private readonly System.Collections.Generic.IDictionary<int, System.Collections.Generic.IList
            <int>> index;

        private bool keyWritten;

        private readonly VPackBuilder.IBuilderOptions options;

        public VPackBuilder()
            : this(new DefaultVPackBuilderOptions())
        {
        }

        public VPackBuilder(VPackBuilder.IBuilderOptions options)
            : base()
        {
            // Here we collect the result
            // Start positions of open
            // objects/arrays
            // Indices for starts
            // of
            // subindex
            // indicates that in the current object the key
            // has been written but the value not yet
            this.options = options;
            this.size = 0;
            this.buffer = new byte[10];
            this.stack = new System.Collections.Generic.List<int>();
            this.index = new System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList
                <int>>();
        }

        public virtual VPackBuilder.IBuilderOptions GetOptions()
        {
            return this.options;
        }

        private void Add(byte b)
        {
            this.EnsureCapacity(this.size + 1);
            this.buffer[this.size++] = b;
        }

        private void AddUnchecked(byte b)
        {
            this.buffer[this.size++] = b;
        }

        private void Remove(int index)
        {
            int numMoved = this.size - index - 1;
            if (numMoved > 0)
            {
                System.Array.Copy(this.buffer, index + 1, this.buffer, index, numMoved);
            }
            this.buffer[--this.size] = 0;
        }

        private void EnsureCapacity(int minCapacity)
        {
            int oldCapacity = this.buffer.Length;
            if (minCapacity > oldCapacity)
            {
                byte[] oldData = this.buffer;
                int newCapacity = oldCapacity * 3 / 2 + 1;
                if (newCapacity < minCapacity)
                {
                    newCapacity = minCapacity;
                }
                Array.Copy(oldData, this.buffer, newCapacity);
            }
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(ValueType
             value)
        {
            return this.AddInternal(VALUE_TYPE, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(ValueType
             value, bool unindexed)
        {
            return this.AddInternal(VALUE, new Value(value, unindexed
                ));
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(bool value)
        {
            return this.AddInternal(BOOLEAN, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(double value)
        {
            return this.AddInternal(DOUBLE, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(float value)
        {
            return this.AddInternal(FLOAT, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(decimal value
            )
        {
            return this.AddInternal(BIG_DECIMAL, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(long value)
        {
            return this.AddInternal(LONG, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(long value, ValueType
             type)
        {
            return this.AddInternal(VALUE, new Value(value, type
                ));
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(int value)
        {
            return this.AddInternal(INTEGER, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(short value)
        {
            return this.AddInternal(SHORT, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(System.DateTime value)
        {
            return this.AddInternal(DATE, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string value)
        {
            return this.AddInternal(STRING, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(char value)
        {
            return this.AddInternal(CHARACTER, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(byte[] value)
        {
            return this.AddInternal(BYTE_ARRAY, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(VPackSlice
             value)
        {
            return this.AddInternal(VPACK, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, ValueType
             value)
        {
            return this.AddInternal(attribute, VALUE_TYPE, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, ValueType
             value, bool unindexed)
        {
            return this.AddInternal(attribute, VALUE, new Value(
                value, unindexed));
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, bool value
            )
        {
            return this.AddInternal(attribute, BOOLEAN, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, double
            value)
        {
            return this.AddInternal(attribute, DOUBLE, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, float value
            )
        {
            return this.AddInternal(attribute, FLOAT, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, decimal value)
        {
            return this.AddInternal(attribute, BIG_DECIMAL, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, long value
            )
        {
            return this.AddInternal(attribute, LONG, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, long value
            , ValueType type)
        {
            return this.AddInternal(attribute, VALUE, new Value(
                value, type));
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, int value
            )
        {
            return this.AddInternal(attribute, INTEGER, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, short value
            )
        {
            return this.AddInternal(attribute, SHORT, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, string
            value)
        {
            return this.AddInternal(attribute, STRING, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, char value
            )
        {
            return this.AddInternal(attribute, CHARACTER, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, DateTime value)
        {
            return this.AddInternal(attribute, DATE, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, byte[]
            value)
        {
            return this.AddInternal(attribute, BYTE_ARRAY, value);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Add(string attribute, VPackSlice
             value)
        {
            return this.AddInternal(attribute, VPACK, value);
        }

        /// <exception cref="VPackBuilderException"/>
        private VPackBuilder AddInternal<T>(VPackBuilder.IAppender<T> appender, T value)
        {
            bool haveReported = false;
            if (this.stack.Count > 0 && !this.keyWritten)
            {
                this.ReportAdd();
                haveReported = true;
            }
            try
            {
                if (value == null)
                {
                    this.AppendNull();
                }
                else
                {
                    appender.Append(this, value);
                }
            }
            catch (VPackBuilderException e)
            {
                // clean up in case of an exception
                if (haveReported)
                {
                    this.CleanupAdd();
                }
                throw;
            }
            return this;
        }

        /// <exception cref="VPackBuilderException"/>
        private VPackBuilder AddInternal<T>(string attribute, VPackBuilder.IAppender
            <T> appender, T value)
        {
            if (attribute != null)
            {
                bool haveReported = false;
                if (this.stack.Count > 0)
                {
                    byte head = Head();
                    if (head != unchecked((int)0x0b) && head != unchecked((int)0x14))
                    {
                        throw new VPackBuilderNeedOpenObjectException();
                    }
                    if (this.keyWritten)
                    {
                        throw new VPackBuilderKeyAlreadyWrittenException
                            ();
                    }
                    this.ReportAdd();
                    haveReported = true;
                }
                try
                {
                    if (VPackSlice.attributeTranslator != null)
                    {
                        VPackSlice translate = VPackSlice
                            .attributeTranslator.translate(attribute);
                        if (translate != null)
                        {
                            byte[] trValue = translate.getRawVPack();
                            this.EnsureCapacity(this.size + trValue.Length);
                            for (int i = 0; i < trValue.Length; i++)
                            {
                                this.AddUnchecked(trValue[i]);
                            }
                            this.keyWritten = true;
                            if (value == null)
                            {
                                this.AppendNull();
                            }
                            else
                            {
                                appender.Append(this, value);
                            }
                            return this;
                        }
                    }
                    // otherwise fall through to regular behavior
                    STRING.Append(this, attribute);
                    this.keyWritten = true;
                    if (value == null)
                    {
                        this.AppendNull();
                    }
                    else
                    {
                        appender.Append(this, value);
                    }
                }
                catch (VPackBuilderException e)
                {
                    // clean up in case of an exception
                    if (haveReported)
                    {
                        this.CleanupAdd();
                    }
                    throw;
                }
                finally
                {
                    this.keyWritten = false;
                }
            }
            else
            {
                this.AddInternal(appender, value);
            }
            return this;
        }

        /// <exception cref="VPackBuilderException"/>
        private void Set(Value item)
        {
            Type clazz = item.Class;
            switch (item.Type)
            {
                case ValueType.NULL:
                    {
                        this.AppendNull();
                        break;
                    }

                case ValueType.ARRAY:
                    {
                        this.AddArray(item.IsUnindexed);
                        break;
                    }

                case ValueType.OBJECT:
                    {
                        this.AddObject(item.IsUnindexed);
                        break;
                    }

                case ValueType.SMALLINT:
                    {
                        long vSmallInt = item.getLong();
                        if (vSmallInt < -6 || vSmallInt > 9)
                        {
                            throw new VPackBuilderNumberOutOfRangeException
                                (ValueType.SMALLINT);
                        }
                        this.AppendSmallInt(vSmallInt);
                        break;
                    }

                case ValueType.INT:
                    {
                        int length;
                        if (clazz == typeof(long))
                        {
                            this.Add(unchecked((byte)unchecked((int)0x27)));
                            length = LONG_BYTES;
                        }
                        else
                        {
                            throw new VPackBuilderUnexpectedValueException(
                                ValueType.INT, typeof(long
                                ), typeof(int),
                                typeof(java.math.BigInteger), typeof(short));
                        }
                        this.Append(item.getNumber(), length);
                        break;
                    }

                case ValueType.UINT:
                    {
                        java.math.BigInteger vUInt;
                        if (clazz == typeof(long))
                        {
                            vUInt = java.math.BigInteger.valueOf(item.getLong());
                        }
                        else
                        {
                            if (clazz == typeof(java.math.BigInteger))
                            {
                                vUInt = item.getBigInteger();
                            }
                            else
                            {
                                throw new VPackBuilderUnexpectedValueException(
                                    ValueType.UINT, typeof(long
                                    ), typeof(int),
                                    typeof(java.math.BigInteger));
                            }
                        }
                        if (-1 == vUInt.compareTo(java.math.BigInteger.ZERO))
                        {
                            throw new VPackBuilderUnexpectedValueException(
                                ValueType.UINT, "non-negative",
                                typeof(long), typeof(int),
                                typeof(java.math.BigInteger));
                        }
                        this.AppendUInt(vUInt);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        private void AppendNull()
        {
            this.Add(unchecked((byte)unchecked((int)0x18)));
        }

        private void AppendBoolean(bool value)
        {
            if (value)
            {
                this.Add(unchecked((byte)unchecked((int)0x1a)));
            }
            else
            {
                this.Add(unchecked((byte)unchecked((int)0x19)));
            }
        }

        private void AppendDouble(double value)
        {
            this.Add(unchecked((byte)unchecked((int)0x1b)));
            this.Append(value);
        }

        private void Append(double value)
        {
            this.Append(double.doubleToRawLongBits(value), DOUBLE_BYTES);
        }

        private void AppendSmallInt(long value)
        {
            if (value >= 0)
            {
                this.Add(unchecked((byte)(value + unchecked((int)0x30))));
            }
            else
            {
                this.Add(unchecked((byte)(value + unchecked((int)0x40))));
            }
        }

        private void AppendUInt(uint value)
        {
            this.Add(unchecked((byte)unchecked((int)0x2f)));
            this.Append(value, LONG_BYTES);
        }

        private void Append(long value, int length)
        {
            this.EnsureCapacity(this.size + length);
            for (int i = length - 1; i >= 0; i--)
            {
                this.AddUnchecked(unchecked((byte)(value >> (length - i - 1 << 3))));
            }
        }

        private void AppendDate(System.DateTime value)
        {
            this.Add(unchecked((byte)unchecked((int)0x1c)));
            this.Append(value.GetUnixTimestampMillis(), LONG_BYTES);
        }

        private void AppendSqlDate(DateTime value)
        {
            this.Add(unchecked((byte)unchecked((int)0x1c)));
            this.Append(value.GetUnixTimestampMillis(), LONG_BYTES);
        }

        /// <exception cref="VPackBuilderException"/>
        private void AppendString(string value)
        {
            int length = Sharpen.Runtime.getBytesForString(value).Length;
            if (length <= 126)
            {
                // short string
                this.Add(unchecked((byte)(unchecked((int)0x40) + length)));
            }
            else
            {
                // long string
                this.Add(unchecked((byte)unchecked((int)0xbf)));
                this.AppendLength(length);
            }
            try
            {
                this.Append(value);
            }
            catch (java.io.UnsupportedEncodingException e)
            {
                throw new VPackBuilderException(e);
            }
        }

        private void AppendBinary(byte[] value)
        {
            this.Add(unchecked((byte)unchecked((int)0xc3)));
            this.Append(value.Length, INTEGER_BYTES);
            this.EnsureCapacity(this.size + value.Length);
            System.Array.Copy(value, 0, this.buffer, this.size, value.Length);
            this.size += value.Length;
        }

        private void AppendVPack(VPackSlice value)
        {
            byte[] vpack = value.getRawVPack();
            this.EnsureCapacity(this.size + vpack.Length);
            System.Array.Copy(vpack, 0, this.buffer, this.size, vpack.Length);
            this.size += vpack.Length;
        }

        /// <exception cref="java.io.UnsupportedEncodingException"/>
        private void Append(string value)
        {
            byte[] bytes = Sharpen.Runtime.getBytesForString(value, "UTF-8");
            this.EnsureCapacity(this.size + bytes.Length);
            System.Array.Copy(bytes, 0, this.buffer, this.size, bytes.Length);
            this.size += bytes.Length;
        }

        private void AddArray(bool unindexed)
        {
            this.AddCompoundValue(unchecked((byte)(unindexed ? unchecked((int)0x13) : unchecked(
                (int)0x06))));
        }

        private void AddObject(bool unindexed)
        {
            this.AddCompoundValue(unchecked((byte)(unindexed ? unchecked((int)0x14) : unchecked(
                (int)0x0b))));
        }

        private void AddCompoundValue(byte head)
        {
            // an Array or Object is started:
            this.stack.add(this.size);
            this.index[this.stack.Count - 1] = new System.Collections.Generic.List<int>();
            this.Add(head);
            // Will be filled later with bytelength and nr subs
            this.size += 8;
            this.EnsureCapacity(this.size);
        }

        private void AppendLength(long length)
        {
            this.Append(length, LONG_BYTES);
        }

        private void ReportAdd()
        {
            System.Collections.Generic.ICollection<int> depth = this.index[this.stack.Count - 1];
            depth.add(this.size - this.stack[this.stack.Count - 1]);
        }

        private void CleanupAdd()
        {
            System.Collections.Generic.IList<int> depth = this.index[this.stack.Count - 1];
            depth.remove(depth.Count - 1);
        }

        /// <exception cref="VPackBuilderException"/>
        public virtual VPackBuilder Close()
        {
            try
            {
                return this.Close(true);
            }
            catch (VPackKeyTypeException e)
            {
                throw new VPackBuilderException(e);
            }
            catch (VPackNeedAttributeTranslatorException e)
            {
                throw new VPackBuilderException(e);
            }
        }

        /// <exception cref="VPackBuilderNeedOpenCompoundException
        /// 	"/>
        /// <exception cref="VPackKeyTypeException"/>
        /// <exception cref="VPackNeedAttributeTranslatorException
        /// 	"/>
        protected internal virtual VPackBuilder Close(bool sort)
        {
            if (this.IsClosed())
            {
                throw new VPackBuilderNeedOpenCompoundException
                    ();
            }
            byte head = head();
            bool isArray = head == unchecked((int)0x06) || head == unchecked((int)0x13);
            System.Collections.Generic.IList<int> @in = this.index[this.stack.Count - 1];
            int tos = this.stack[this.stack.Count - 1];
            if (@in.isEmpty())
            {
                return this.CloseEmptyArrayOrObject(tos, isArray);
            }
            if (head == unchecked((int)0x13) || head == unchecked((int)0x14) || (head ==
                unchecked((int)0x06) && this.options.IsBuildUnindexedArrays()) || head == unchecked(
                (int)0x0b) && (this.options.IsBuildUnindexedObjects() || @in.Count == 1))
            {
                if (this.CloseCompactArrayOrObject(tos, isArray, @in))
                {
                    return this;
                }
            }
            // This might fall through, if closeCompactArrayOrObject gave up!
            if (isArray)
            {
                return this.CloseArray(tos, @in);
            }
            // fix head byte in case a compact Array / Object was originally
            // requested
            this.buffer[tos] = unchecked((byte)unchecked((int)0x0b));
            // First determine byte length and its format:
            int offsetSize;
            // can be 1, 2, 4 or 8 for the byte width of the offsets,
            // the byte length and the number of subvalues:
            if (this.size - 1 - tos + @in.Count - 6 <= unchecked((int)0xff))
            {
                // We have so far used _pos - tos bytes, including the reserved 8
                // bytes for byte length and number of subvalues. In the 1-byte
                // number
                // case we would win back 6 bytes but would need one byte per
                // subvalue
                // for the index table
                offsetSize = 1;
            }
            else
            {
                if (this.size - 1 - tos + 2 * @in.Count <= unchecked((int)0xffff))
                {
                    offsetSize = 2;
                }
                else
                {
                    if ((this.size - 1 - tos) / 2 + 4 * @in.Count / 2 <= int.MaxValue)
                    {
                        /* 0xffffffffu */
                        offsetSize = 4;
                    }
                    else
                    {
                        offsetSize = 8;
                    }
                }
            }
            // Maybe we need to move down data
            if (offsetSize == 1)
            {
                int targetPos = 3;
                if (this.size - 1 > tos + 9)
                {
                    for (int i = tos + targetPos; i < tos + 9; i++)
                    {
                        this.Remove(tos + targetPos);
                    }
                }
                int diff = 9 - targetPos;
                int n = @in.Count;
                for (int i_1 = 0; i_1 < n; i_1++)
                {
                    @in.set(i_1, @in[i_1] - diff);
                }
            }
            // One could move down things in the offsetSize == 2 case as well,
            // since we only need 4 bytes in the beginning. However, saving these
            // 4 bytes has been sacrificed on the Altar of Performance.
            // Now build the table:
            if (sort && @in.Count >= 2)
            {
                // Object
                this.SortObjectIndex(tos, @in);
            }
            // final int tableBase = size;
            for (int i_2 = 0; i_2 < @in.Count; i_2++)
            {
                long x = @in[i_2];
                this.EnsureCapacity(this.size + offsetSize);
                for (int j = 0; j < offsetSize; j++)
                {
                    this.AddUnchecked(unchecked((byte)(x & unchecked((int)0xff))));
                    /* tableBase + offsetSize * i + j, */
                    x >>= 8;
                }
            }
            // Finally fix the byte width in the type byte:
            if (offsetSize > 1)
            {
                if (offsetSize == 2)
                {
                    this.buffer[tos] = unchecked((byte)(this.buffer[tos] + 1));
                }
                else
                {
                    if (offsetSize == 4)
                    {
                        this.buffer[tos] = unchecked((byte)(this.buffer[tos] + 2));
                    }
                    else
                    {
                        // offsetSize == 8
                        this.buffer[tos] = unchecked((byte)(this.buffer[tos] + 3));
                        this.AppendLength(@in.Count);
                    }
                }
            }
            // Fix the byte length in the beginning
            long x_1 = this.size - tos;
            for (int i_3 = 1; i_3 <= offsetSize; i_3++)
            {
                this.buffer[tos + i_3] = unchecked((byte)(x_1 & unchecked((int)0xff)));
                x_1 >>= 8;
            }
            // set the number of items in the beginning
            if (offsetSize < 8)
            {
                x_1 = @in.Count;
                for (int i = offsetSize + 1; i_3 <= 2 * offsetSize; i_3++)
                {
                    this.buffer[tos + i_3] = unchecked((byte)(x_1 & unchecked((int)0xff)));
                    x_1 >>= 8;
                }
            }
            this.stack.remove(this.stack.Count - 1);
            return this;
        }

        private VPackBuilder CloseEmptyArrayOrObject(int tos, bool
             isArray)
        {
            // empty Array or Object
            this.buffer[tos] = unchecked((byte)(isArray ? unchecked((int)0x01) : unchecked((int)
                0x0a)));
            // no bytelength and number subvalues needed
            for (int i = 1; i <= 8; i++)
            {
                this.Remove(tos + 1);
            }
            this.stack.remove(this.stack.Count - 1);
            return this;
        }

        private bool CloseCompactArrayOrObject(int tos, bool isArray, System.Collections.Generic.IList
            <int> @in)
        {
            // use the compact Array / Object format
            long nLen = NumberUtil.getVariableValueLength
                (@in.Count);
            long byteSize = this.size - (tos + 8) + nLen;
            long bLen = NumberUtil.getVariableValueLength
                (byteSize);
            byteSize += bLen;
            if (NumberUtil.getVariableValueLength(byteSize
                ) != bLen)
            {
                byteSize += 1;
                bLen += 1;
            }
            if (bLen < 9)
            {
                // can only use compact notation if total byte length is at most
                // 8 bytes long
                this.buffer[tos] = unchecked((byte)(isArray ? unchecked((int)0x13) : unchecked((int)
                    0x14)));
                int targetPos = (int)(1 + bLen);
                if (this.size - 1 > tos + 9)
                {
                    for (int i = tos + targetPos; i < tos + 9; i++)
                    {
                        this.Remove(tos + targetPos);
                    }
                }
                // store byte length
                this.StoreVariableValueLength(tos, byteSize, false);
                // need additional memory for storing the number of values
                if (nLen > 8 - bLen)
                {
                    this.EnsureCapacity((int)(this.size + nLen));
                }
                // store number of values
                this.StoreVariableValueLength((int)(tos + byteSize), @in.Count, true);
                this.size += nLen;
                this.stack.remove(this.stack.Count - 1);
                return true;
            }
            return false;
        }

        private void StoreVariableValueLength(int offset, long value, bool reverse)
        {
            int i = offset;
            long val = value;
            if (reverse)
            {
                while (val >= unchecked((int)0x80))
                {
                    this.buffer[--i] = unchecked((byte)(unchecked((byte)(val & unchecked((int)0x7f))) |
                        unchecked((byte)unchecked((int)0x80))));
                    val >>= 7;
                }
                this.buffer[--i] = unchecked((byte)(val & unchecked((int)0x7f)));
            }
            else
            {
                while (val >= unchecked((int)0x80))
                {
                    this.buffer[++i] = unchecked((byte)(unchecked((byte)(val & unchecked((int)0x7f))) |
                        unchecked((byte)unchecked((int)0x80))));
                    val >>= 7;
                }
                this.buffer[++i] = unchecked((byte)(val & unchecked((int)0x7f)));
            }
        }

        private VPackBuilder CloseArray(int tos, System.Collections.Generic.IList
            <int> @in)
        {
            // fix head byte in case a compact Array was originally
            // requested
            this.buffer[tos] = unchecked((byte)unchecked((int)0x06));
            bool needIndexTable = true;
            bool needNrSubs = true;
            int n = @in.Count;
            if (n == 1)
            {
                needIndexTable = false;
                needNrSubs = false;
            }
            else
            {
                if (this.size - tos - @in[0] == n * (@in[1] - @in[0]))
                {
                    // In this case it could be that all entries have the same length
                    // and we do not need an offset table at all:
                    bool noTable = true;
                    int subLen = @in[1] - @in[0];
                    if (this.size - tos - @in[n - 1] != subLen)
                    {
                        noTable = false;
                    }
                    else
                    {
                        for (int i = 1; i < n - 1; i++)
                        {
                            if (@in[i + 1] - @in[i] != subLen)
                            {
                                noTable = false;
                                break;
                            }
                        }
                    }
                    if (noTable)
                    {
                        needIndexTable = false;
                        needNrSubs = false;
                    }
                }
            }
            // First determine byte length and its format:
            int offsetSize;
            // can be 1, 2, 4 or 8 for the byte width of the offsets,
            // the byte length and the number of subvalues:
            if (this.size - 1 - tos + (needIndexTable ? n : 0) - (needNrSubs ? 6 : 7) <= unchecked(
                (int)0xff))
            {
                // We have so far used _pos - tos bytes, including the reserved 8
                // bytes for byte length and number of subvalues. In the 1-byte
                // number
                // case we would win back 6 bytes but would need one byte per
                // subvalue
                // for the index table
                offsetSize = 1;
            }
            else
            {
                if (this.size - 1 - tos + (needIndexTable ? 2 * n : 0) <= unchecked((int)0xffff))
                {
                    offsetSize = 2;
                }
                else
                {
                    if ((this.size - 1 - tos) / 2 + (needIndexTable ? 4 * n : 0) / 2 <= int.MaxValue)
                    {
                        /* 0xffffffffu */
                        offsetSize = 4;
                    }
                    else
                    {
                        offsetSize = 8;
                    }
                }
            }
            // Maybe we need to move down data
            if (offsetSize == 1)
            {
                int targetPos = 3;
                if (!needIndexTable)
                {
                    targetPos = 2;
                }
                if (this.size - 1 > tos + 9)
                {
                    for (int i = tos + targetPos; i < tos + 9; i++)
                    {
                        this.Remove(tos + targetPos);
                    }
                }
                int diff = 9 - targetPos;
                if (needIndexTable)
                {
                    for (int i = 0; i < n; i++)
                    {
                        @in.set(i, @in[i] - diff);
                    }
                }
            }
            // Note: if !needIndexTable the index is now wrong!
            // One could move down things in the offsetSize == 2 case as well,
            // since we only need 4 bytes in the beginning. However, saving these
            // 4 bytes has been sacrificed on the Altar of Performance.
            // Now build the table:
            if (needIndexTable)
            {
                // final int tableBase = size;
                for (int i = 0; i < n; i++)
                {
                    long x = @in[i];
                    this.EnsureCapacity(this.size + offsetSize);
                    for (int j = 0; j < offsetSize; j++)
                    {
                        this.AddUnchecked(unchecked((byte)(x & unchecked((int)0xff))));
                        /* tableBase + offsetSize * i + j, */
                        x >>= 8;
                    }
                }
            }
            else
            {
                // no index table
                this.buffer[tos] = unchecked((byte)unchecked((int)0x02));
            }
            // Finally fix the byte width in the type byte:
            if (offsetSize > 1)
            {
                if (offsetSize == 2)
                {
                    this.buffer[tos] = unchecked((byte)(this.buffer[tos] + 1));
                }
                else
                {
                    if (offsetSize == 4)
                    {
                        this.buffer[tos] = unchecked((byte)(this.buffer[tos] + 2));
                    }
                    else
                    {
                        // offsetSize == 8
                        this.buffer[tos] = unchecked((byte)(this.buffer[tos] + 3));
                        if (needNrSubs)
                        {
                            this.AppendLength(n);
                        }
                    }
                }
            }
            // Fix the byte length in the beginning
            long x_1 = this.size - tos;
            for (int i_1 = 1; i_1 <= offsetSize; i_1++)
            {
                this.buffer[tos + i_1] = unchecked((byte)(x_1 & unchecked((int)0xff)));
                x_1 >>= 8;
            }
            // set the number of items in the beginning
            if (offsetSize < 8 && needNrSubs)
            {
                x_1 = n;
                for (int i = offsetSize + 1; i_1 <= 2 * offsetSize; i_1++)
                {
                    this.buffer[tos + i_1] = unchecked((byte)(x_1 & unchecked((int)0xff)));
                    x_1 >>= 8;
                }
            }
            this.stack.remove(this.stack.Count - 1);
            return this;
        }

        private class SortEntry
        {
            private readonly VPackSlice slice;

            private readonly int offset;

            public SortEntry(VPackSlice slice, int offset)
                : base()
            {
                this.slice = slice;
                this.offset = offset;
            }
        }

        /// <exception cref="VPackKeyTypeException"/>
        /// <exception cref="VPackNeedAttributeTranslatorException
        /// 	"/>
        private void SortObjectIndex(int start, System.Collections.Generic.IList<int> offsets
            )
        {
            System.Collections.Generic.IList<SortEntry>
                attributes = new System.Collections.Generic.List<SortEntry
                >();
            foreach (int offset in offsets)
            {
                attributes.add(new VPackBuilder.SortEntry(new VPackSlice
                    (this.buffer, start + offset).makeKey(), offset));
            }
            java.util.Comparator<VPackBuilder.SortEntry> comparator =
                new _Comparator_999();
            attributes.Sort(comparator);
            offsets.clear();
            foreach (VPackBuilder.SortEntry sortEntry in attributes)
            {
                offsets.add(sortEntry.offset);
            }
        }

        private sealed class _Comparator_999 : java.util.Comparator<VPackBuilder.SortEntry
            >
        {
            public _Comparator_999()
            {
            }

            public int Compare(VPackBuilder.SortEntry o1, VPackBuilder.SortEntry
                 o2)
            {
                return string.CompareOrdinal(o1.slice.getAsString(), o2.slice.getAsString());
            }
        }

        public static int CompareTo(byte[] b1, int b1Index, int b1Length, byte[] b2, int
            b2Index, int b2Length)
        {
            int commonLength = System.Math.min(b1Length, b2Length);
            for (int i = 0; i < commonLength; i++)
            {
                byte byte1 = b1[b1Index + i];
                byte byte2 = b2[b2Index + i];
                if (byte1 != byte2)
                {
                    return (sbyte)byte1 < byte2 ? -1 : 1;
                }
            }
            if (b1Length != b2Length)
            {
                return b1Length < b2Length ? -2 : 2;
            }
            return 0;
        }

        private bool IsClosed()
        {
            return this.stack.isEmpty();
        }

        private byte Head()
        {
            int @in = this.stack[this.stack.Count - 1];
            return this.buffer[@in];
        }

        public virtual VPackSlice Slice()
        {
            return new VPackSlice(this.buffer);
        }

        public virtual int GetVpackSize()
        {
            return this.size;
        }
    }
}