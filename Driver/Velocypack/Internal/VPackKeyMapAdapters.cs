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
    /// <author>Mark - mark at arangodb.com</author>
    public class VPackKeyMapAdapters
    {
        private VPackKeyMapAdapters()
            : base()
        {
        }

        public static VPackKeyMapAdapter<java.lang.Enum<object>>
            createEnumAdapter(global::System.Type type)
        {
            return new _VPackKeyMapAdapter_40(type);
        }

        private sealed class _VPackKeyMapAdapter_40 : VPackKeyMapAdapter
            <java.lang.Enum<object>>
        {
            public _VPackKeyMapAdapter_40(global::System.Type type)
            {
                this.type = type;
            }

            public string serialize<_T0>(java.lang.Enum<_T0> key)
                where _T0 : java.lang.Enum<E>
            {
                return key.name();
            }

            public java.lang.Enum<object> deserialize(string key)
            {
                java.lang.Class enumType = (java.lang.Class)this.type;
                return java.lang.Enum.valueOf(enumType, key);
            }

            private readonly global::System.Type type;
        }

        private sealed class _VPackKeyMapAdapter_55 : VPackKeyMapAdapter
            <string>
        {
            public _VPackKeyMapAdapter_55()
            {
            }

            public string serialize(string key)
            {
                return key;
            }

            public string deserialize(string key)
            {
                return key;
            }
        }

        public static readonly VPackKeyMapAdapter<string> STRING =
            new _VPackKeyMapAdapter_55();

        private sealed class _VPackKeyMapAdapter_66 : VPackKeyMapAdapter
            <bool>
        {
            public _VPackKeyMapAdapter_66()
            {
            }

            public string serialize(bool key)
            {
                return key.ToString();
            }

            public bool deserialize(string key)
            {
                return bool.valueOf(key);
            }
        }

        public static readonly VPackKeyMapAdapter<bool> BOOLEAN =
            new _VPackKeyMapAdapter_66();

        private sealed class _VPackKeyMapAdapter_77 : VPackKeyMapAdapter
            <int>
        {
            public _VPackKeyMapAdapter_77()
            {
            }

            public string serialize(int key)
            {
                return key.ToString();
            }

            public int deserialize(string key)
            {
                return int.Parse(key);
            }
        }

        public static readonly VPackKeyMapAdapter<int> INTEGER =
            new _VPackKeyMapAdapter_77();

        private sealed class _VPackKeyMapAdapter_88 : VPackKeyMapAdapter
            <long>
        {
            public _VPackKeyMapAdapter_88()
            {
            }

            public string serialize(long key)
            {
                return key.ToString();
            }

            public long deserialize(string key)
            {
                return long.valueOf(key);
            }
        }

        public static readonly VPackKeyMapAdapter<long> LONG = new
            _VPackKeyMapAdapter_88();

        private sealed class _VPackKeyMapAdapter_99 : VPackKeyMapAdapter
            <short>
        {
            public _VPackKeyMapAdapter_99()
            {
            }

            public string serialize(short key)
            {
                return key.ToString();
            }

            public short deserialize(string key)
            {
                return short.valueOf(key);
            }
        }

        public static readonly VPackKeyMapAdapter<short> SHORT =
            new _VPackKeyMapAdapter_99();

        private sealed class _VPackKeyMapAdapter_110 : VPackKeyMapAdapter
            <double>
        {
            public _VPackKeyMapAdapter_110()
            {
            }

            public string serialize(double key)
            {
                return key.ToString();
            }

            public double deserialize(string key)
            {
                return double.valueOf(key);
            }
        }

        public static readonly VPackKeyMapAdapter<double> DOUBLE =
            new _VPackKeyMapAdapter_110();

        private sealed class _VPackKeyMapAdapter_121 : VPackKeyMapAdapter
            <float>
        {
            public _VPackKeyMapAdapter_121()
            {
            }

            public string serialize(float key)
            {
                return key.ToString();
            }

            public float deserialize(string key)
            {
                return float.valueOf(key);
            }
        }

        public static readonly VPackKeyMapAdapter<float> FLOAT =
            new _VPackKeyMapAdapter_121();

        private sealed class _VPackKeyMapAdapter_132 : VPackKeyMapAdapter
            <java.math.BigInteger>
        {
            public _VPackKeyMapAdapter_132()
            {
            }

            public string serialize(java.math.BigInteger key)
            {
                return key.ToString();
            }

            public java.math.BigInteger deserialize(string key)
            {
                return new java.math.BigInteger(key);
            }
        }

        public static readonly VPackKeyMapAdapter<java.math.BigInteger
            > BIG_INTEGER = new _VPackKeyMapAdapter_132();

        private sealed class _VPackKeyMapAdapter_143 : VPackKeyMapAdapter
            <java.math.BigDecimal>
        {
            public _VPackKeyMapAdapter_143()
            {
            }

            public string serialize(java.math.BigDecimal key)
            {
                return key.ToString();
            }

            public java.math.BigDecimal deserialize(string key)
            {
                return new java.math.BigDecimal(key);
            }
        }

        public static readonly VPackKeyMapAdapter<java.math.BigDecimal
            > BIG_DECIMAL = new _VPackKeyMapAdapter_143();

        private sealed class _VPackKeyMapAdapter_154 : VPackKeyMapAdapter
            <java.lang.Number>
        {
            public _VPackKeyMapAdapter_154()
            {
            }

            public string serialize(java.lang.Number key)
            {
                return key.ToString();
            }

            public java.lang.Number deserialize(string key)
            {
                return double.valueOf(key);
            }
        }

        public static readonly VPackKeyMapAdapter<java.lang.Number
            > NUMBER = new _VPackKeyMapAdapter_154();

        private sealed class _VPackKeyMapAdapter_165 : VPackKeyMapAdapter
            <char>
        {
            public _VPackKeyMapAdapter_165()
            {
            }

            public string serialize(char key)
            {
                return key.ToString();
            }

            public char deserialize(string key)
            {
                return key[0];
            }
        }

        public static readonly VPackKeyMapAdapter<char> CHARACTER
             = new _VPackKeyMapAdapter_165();
    }
}