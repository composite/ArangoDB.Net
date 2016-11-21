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
    using System.Reflection;

    /// <author>Mark - mark at arangodb.com</author>
    public static class VPackKeyMapAdapters
    {
        public static readonly VPackKeyMapAdapter<BigInteger> BIG_INTEGER = new _VPackKeyMapAdapter_132();

        public static readonly VPackKeyMapAdapter<bool> BOOLEAN = new _VPackKeyMapAdapter_66();

        public static readonly VPackKeyMapAdapter<char> CHARACTER = new _VPackKeyMapAdapter_165();

        public static readonly VPackKeyMapAdapter<double> DOUBLE = new _VPackKeyMapAdapter_110();

        public static readonly VPackKeyMapAdapter<float> FLOAT = new _VPackKeyMapAdapter_121();

        public static readonly VPackKeyMapAdapter<int> INTEGER = new _VPackKeyMapAdapter_77();

        public static readonly VPackKeyMapAdapter<long> LONG = new _VPackKeyMapAdapter_88();

        public static readonly VPackKeyMapAdapter<decimal> NUMBER = new _VPackKeyMapAdapter_154();

        public static readonly VPackKeyMapAdapter<short> SHORT = new _VPackKeyMapAdapter_99();

        public static readonly VPackKeyMapAdapter<string> STRING = new _VPackKeyMapAdapter_55();

        public static VPackKeyMapAdapter<Enum> CreateEnumAdapter(Type type)
        {
            //if (!type.GetTypeInfo().IsEnum) throw new ArgumentException(string.Format("{0} is not enum.", type));
            return new _VPackKeyMapAdapter_40(type);
        }

        private sealed class _VPackKeyMapAdapter_110 : VPackKeyMapAdapter<double>
        {
            public override double Deserialize(string key)
            {
                return double.Parse(key);
            }

            public override string Serialize(double key)
            {
                return key.ToString();
            }
        }

        private sealed class _VPackKeyMapAdapter_121 : VPackKeyMapAdapter<float>
        {
            public override float Deserialize(string key)
            {
                return float.Parse(key);
            }

            public override string Serialize(float key)
            {
                return key.ToString();
            }
        }

        private sealed class _VPackKeyMapAdapter_132 : VPackKeyMapAdapter<BigInteger>
        {
            public override BigInteger Deserialize(string key)
            {
                return BigInteger.Parse(key);
            }

            public override string Serialize(BigInteger key)
            {
                return key.ToString();
            }
        }

        private sealed class _VPackKeyMapAdapter_154 : VPackKeyMapAdapter<decimal>
        {
            public override decimal Deserialize(string key)
            {
                return decimal.Parse(key);
            }

            public override string Serialize(decimal key)
            {
                return key.ToString();
            }
        }

        private sealed class _VPackKeyMapAdapter_165 : VPackKeyMapAdapter<char>
        {
            public override char Deserialize(string key)
            {
                return key[0];
            }

            public override string Serialize(char key)
            {
                return key.ToString();
            }
        }

        private sealed class _VPackKeyMapAdapter_40 : VPackKeyMapAdapter<Enum>
        {
            private readonly global::System.Type type;

            public _VPackKeyMapAdapter_40(Type type)
            {
                this.type = type;
            }

            public override Enum Deserialize(string key)
            {
                return (Enum)Enum.Parse(this.type, key);
            }

            public override string Serialize(Enum key)
            {
                return key.ToString();
            }
        }

        private sealed class _VPackKeyMapAdapter_55 : VPackKeyMapAdapter<string>
        {
            public override string Deserialize(string key)
            {
                return key;
            }

            public override string Serialize(string key)
            {
                return key;
            }
        }

        private sealed class _VPackKeyMapAdapter_66 : VPackKeyMapAdapter<bool>
        {
            public override bool Deserialize(string key)
            {
                return bool.Parse(key.Substring(0, 1).ToUpper() + key.Substring(1).ToLower());
            }

            public override string Serialize(bool key)
            {
                return key.ToString().ToLower();
            }
        }

        private sealed class _VPackKeyMapAdapter_77 : VPackKeyMapAdapter<int>
        {
            public override int Deserialize(string key)
            {
                return int.Parse(key);
            }

            public override string Serialize(int key)
            {
                return key.ToString();
            }
        }

        private sealed class _VPackKeyMapAdapter_88 : VPackKeyMapAdapter<long>
        {
            public override long Deserialize(string key)
            {
                return long.Parse(key);
            }

            public override string Serialize(long key)
            {
                return key.ToString();
            }
        }

        private sealed class _VPackKeyMapAdapter_99 : VPackKeyMapAdapter<short>
        {
            public override short Deserialize(string key)
            {
                return short.Parse(key);
            }

            public override string Serialize(short key)
            {
                return key.ToString();
            }
        }
    }
}