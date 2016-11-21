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
namespace VelocyPack.Internal
{
    using System;
    using System.Numerics;

    using VelocyPack.Exceptions;

    using ValueType = global::VelocyPack.ValueType;

    /// <author>Mark - mark at arangodb.com</author>
    internal class Value
    {
        private readonly object value;

        private Value(object value, ValueType type, Type clazz)
            : this(value, type, clazz, false)
        {
        }

        private Value(object value, ValueType type, Type clazz, bool unindexed)
            : base()
        {
            this.value = value;
            this.Type = type;
            this.Class = clazz;
            this.IsUnindexed = unindexed;
        }

        public Value(ValueType type)
            : this(type, false)
        {
        }

        /// <exception cref="VPackValueTypeException"/>
        public Value(ValueType type, bool unindexed)
            : this(null, type, null, unindexed)
        {
            if (type != ValueType.ARRAY && type != ValueType.OBJECT && type != ValueType.NULL)
            {
                throw new VPackValueTypeException(ValueType.ARRAY, ValueType.OBJECT, ValueType.NULL);
            }
        }

        /// <exception cref="VPackValueTypeException"/>
        public Value(long value, ValueType type)
            : this(value, type, typeof(long))
        {
            if (type != ValueType.INT && type != ValueType.UINT && type != ValueType.SMALLINT)
            {
                throw new VPackValueTypeException(ValueType.INT, ValueType.UINT, ValueType.SMALLINT);
            }
        }

        /// <exception cref="VPackValueTypeException"/>
        public Value(BigInteger value, ValueType type)
            : this(value, type, typeof(BigInteger))
        {
            if (type != ValueType.INT && type != ValueType.UINT && type != ValueType.SMALLINT)
            {
                throw new VPackValueTypeException(ValueType.INT, ValueType.UINT, ValueType.SMALLINT);
            }
        }

        /// <exception cref="VPackValueTypeException"/>
        public Value(decimal value, ValueType type)
            : this(value, type, typeof(decimal))
        {
            if (type != ValueType.INT && type != ValueType.UINT && type != ValueType.SMALLINT)
            {
                throw new VPackValueTypeException(ValueType.INT, ValueType.UINT, ValueType.SMALLINT);
            }
        }

        public virtual ValueType Type { get; }

        public virtual Type Class { get; }

        public virtual bool IsUnindexed { get; }

        public virtual decimal GetNumber()
        {
            return (decimal)this.value;
        }

        public virtual long GetLong()
        {
            return (long)this.value;
        }

        public virtual BigInteger GetBigInteger()
        {
            return (BigInteger)this.value;
        }
    }
}