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

namespace ArangoDB.Velocypack.Internal
{
    using global::ArangoDB.Velocypack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
	public class VPackDeserializers
    {
        private VPackDeserializers()
            : base()
        {
        }

        private sealed class _VPackDeserializer_43 : VPackDeserializer
            <string>
        {
            public _VPackDeserializer_43()
            {
            }

            /// <exception cref="VPackException"/>
            public string deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsString();
            }
        }

        public static readonly VPackDeserializer<string> STRING =
            new _VPackDeserializer_43();

        private sealed class _VPackDeserializer_52 : VPackDeserializer
            <bool>
        {
            public _VPackDeserializer_52()
            {
            }

            /// <exception cref="VPackException"/>
            public bool deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsBoolean();
            }
        }

        public static readonly VPackDeserializer<bool> BOOLEAN =
            new _VPackDeserializer_52();

        private sealed class _VPackDeserializer_61 : VPackDeserializer
            <int>
        {
            public _VPackDeserializer_61()
            {
            }

            /// <exception cref="VPackException"/>
            public int deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsInt();
            }
        }

        public static readonly VPackDeserializer<int> INTEGER = new
            _VPackDeserializer_61();

        private sealed class _VPackDeserializer_70 : VPackDeserializer
            <long>
        {
            public _VPackDeserializer_70()
            {
            }

            /// <exception cref="VPackException"/>
            public long deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsLong();
            }
        }

        public static readonly VPackDeserializer<long> LONG = new
            _VPackDeserializer_70();

        private sealed class _VPackDeserializer_79 : VPackDeserializer
            <short>
        {
            public _VPackDeserializer_79()
            {
            }

            /// <exception cref="VPackException"/>
            public short deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsShort();
            }
        }

        public static readonly VPackDeserializer<short> SHORT = new
            _VPackDeserializer_79();

        private sealed class _VPackDeserializer_88 : VPackDeserializer
            <double>
        {
            public _VPackDeserializer_88()
            {
            }

            /// <exception cref="VPackException"/>
            public double deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsDouble();
            }
        }

        public static readonly VPackDeserializer<double> DOUBLE =
            new _VPackDeserializer_88();

        private sealed class _VPackDeserializer_97 : VPackDeserializer
            <float>
        {
            public _VPackDeserializer_97()
            {
            }

            /// <exception cref="VPackException"/>
            public float deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsFloat();
            }
        }

        public static readonly VPackDeserializer<float> FLOAT = new
            _VPackDeserializer_97();

        private sealed class _VPackDeserializer_106 : VPackDeserializer
            <java.math.BigInteger>
        {
            public _VPackDeserializer_106()
            {
            }

            /// <exception cref="VPackException"/>
            public java.math.BigInteger deserialize(VPackSlice parent
                , VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                return vpack.getAsBigInteger();
            }
        }

        public static readonly VPackDeserializer<java.math.BigInteger
            > BIG_INTEGER = new _VPackDeserializer_106();

        private sealed class _VPackDeserializer_115 : VPackDeserializer
            <java.math.BigDecimal>
        {
            public _VPackDeserializer_115()
            {
            }

            /// <exception cref="VPackException"/>
            public java.math.BigDecimal deserialize(VPackSlice parent
                , VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                return vpack.getAsBigDecimal();
            }
        }

        public static readonly VPackDeserializer<java.math.BigDecimal
            > BIG_DECIMAL = new _VPackDeserializer_115();

        private sealed class _VPackDeserializer_124 : VPackDeserializer
            <java.lang.Number>
        {
            public _VPackDeserializer_124()
            {
            }

            /// <exception cref="VPackException"/>
            public java.lang.Number deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsNumber();
            }
        }

        public static readonly VPackDeserializer<java.lang.Number
            > NUMBER = new _VPackDeserializer_124();

        private sealed class _VPackDeserializer_133 : VPackDeserializer
            <char>
        {
            public _VPackDeserializer_133()
            {
            }

            /// <exception cref="VPackException"/>
            public char deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsChar();
            }
        }

        public static readonly VPackDeserializer<char> CHARACTER =
            new _VPackDeserializer_133();

        private sealed class _VPackDeserializer_142 : VPackDeserializer
            <System.DateTime>
        {
            public _VPackDeserializer_142()
            {
            }

            /// <exception cref="VPackException"/>
            public System.DateTime deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsDate();
            }
        }

        public static readonly VPackDeserializer<System.DateTime>
             DATE = new _VPackDeserializer_142();

        private sealed class _VPackDeserializer_151 : VPackDeserializer
            <java.sql.Date>
        {
            public _VPackDeserializer_151()
            {
            }

            /// <exception cref="VPackException"/>
            public java.sql.Date deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                return vpack.getAsSQLDate();
            }
        }

        public static readonly VPackDeserializer<java.sql.Date> SQL_DATE
             = new _VPackDeserializer_151();

        private sealed class _VPackDeserializer_160 : VPackDeserializer
            <java.sql.Timestamp>
        {
            public _VPackDeserializer_160()
            {
            }

            /// <exception cref="VPackException"/>
            public java.sql.Timestamp deserialize(VPackSlice parent,
                VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                return vpack.getAsSQLTimestamp();
            }
        }

        public static readonly VPackDeserializer<java.sql.Timestamp
            > SQL_TIMESTAMP = new _VPackDeserializer_160();

        private sealed class _VPackDeserializer_169 : VPackDeserializer
            <VPackSlice>
        {
            public _VPackDeserializer_169()
            {
            }

            /// <exception cref="VPackException"/>
            public VPackSlice deserialize(VPackSlice
                 parent, VPackSlice vpack, VPackDeserializationContext
                 context)
            {
                return vpack;
            }
        }

        public static readonly VPackDeserializer<VPackSlice
            > VPACK = new _VPackDeserializer_169();
    }
}