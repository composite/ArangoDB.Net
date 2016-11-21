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

    /// <author>Mark - mark at arangodb.com</author>
    public static class VPackDeserializers
    {
        public static readonly VPackDeserializer<BigInteger> BIG_INTEGER = new _VPackDeserializer_106();

        public static readonly VPackDeserializer<bool> BOOLEAN = new _VPackDeserializer_52();

        public static readonly VPackDeserializer<char> CHARACTER = new _VPackDeserializer_133();

        public static readonly VPackDeserializer<DateTime> DATE = new _VPackDeserializer_142();

        public static readonly VPackDeserializer<double> DOUBLE = new _VPackDeserializer_88();

        public static readonly VPackDeserializer<float> FLOAT = new _VPackDeserializer_97();

        public static readonly VPackDeserializer<int> INTEGER = new _VPackDeserializer_61();

        public static readonly VPackDeserializer<long> LONG = new _VPackDeserializer_70();

        public static readonly VPackDeserializer<decimal> NUMBER = new _VPackDeserializer_124();

        public static readonly VPackDeserializer<short> SHORT = new _VPackDeserializer_79();

        public static readonly VPackDeserializer<string> STRING = new _VPackDeserializer_43();

        public static readonly VPackDeserializer<VPackSlice> VPACK = new _VPackDeserializer_169();

        private sealed class _VPackDeserializer_106 : VPackDeserializer<BigInteger>
        {
            /// <exception cref="VPackException"/>
            public override BigInteger Deserialize(
                VPackSlice parent,
                VPackSlice vpack,
                VPackDeserializationContext context)
            {
                return vpack.AsBigInteger;
            }
        }

        private sealed class _VPackDeserializer_124 : VPackDeserializer<decimal>
        {
            /// <exception cref="VPackException"/>
            public override decimal Deserialize(
                VPackSlice parent,
                VPackSlice vpack,
                VPackDeserializationContext context)
            {
                return vpack.AsNumber;
            }
        }

        private sealed class _VPackDeserializer_133 : VPackDeserializer<char>
        {
            /// <exception cref="VPackException"/>
            public override char Deserialize(VPackSlice parent, VPackSlice vpack, VPackDeserializationContext context)
            {
                return vpack.AsChar;
            }
        }

        private sealed class _VPackDeserializer_142 : VPackDeserializer<DateTime>
        {
            /// <exception cref="VPackException"/>
            public override DateTime Deserialize(VPackSlice parent, VPackSlice vpack, VPackDeserializationContext context)
            {
                return vpack.AsDate;
            }
        }

        private sealed class _VPackDeserializer_169 : VPackDeserializer<VPackSlice>
        {
            /// <exception cref="VPackException"/>
            public override VPackSlice Deserialize(
                VPackSlice parent,
                VPackSlice vpack,
                VPackDeserializationContext context)
            {
                return vpack;
            }
        }

        private sealed class _VPackDeserializer_43 : VPackDeserializer<string>
        {
            /// <exception cref="VPackException"/>
            public override string Deserialize(VPackSlice parent, VPackSlice vpack, VPackDeserializationContext context)
            {
                return vpack.AsString;
            }
        }

        private sealed class _VPackDeserializer_52 : VPackDeserializer<bool>
        {
            /// <exception cref="VPackException"/>
            public override bool Deserialize(VPackSlice parent, VPackSlice vpack, VPackDeserializationContext context)
            {
                return vpack.AsBoolean;
            }
        }

        private sealed class _VPackDeserializer_61 : VPackDeserializer<int>
        {
            /// <exception cref="VPackException"/>
            public override int Deserialize(VPackSlice parent, VPackSlice vpack, VPackDeserializationContext context)
            {
                return vpack.AsInt;
            }
        }

        private sealed class _VPackDeserializer_70 : VPackDeserializer<long>
        {
            /// <exception cref="VPackException"/>
            public override long Deserialize(VPackSlice parent, VPackSlice vpack, VPackDeserializationContext context)
            {
                return vpack.AsLong;
            }
        }

        private sealed class _VPackDeserializer_79 : VPackDeserializer<short>
        {
            /// <exception cref="VPackException"/>
            public override short Deserialize(VPackSlice parent, VPackSlice vpack, VPackDeserializationContext context)
            {
                return vpack.AsShort;
            }
        }

        private sealed class _VPackDeserializer_88 : VPackDeserializer<double>
        {
            /// <exception cref="VPackException"/>
            public override double Deserialize(VPackSlice parent, VPackSlice vpack, VPackDeserializationContext context)
            {
                return vpack.AsDouble;
            }
        }

        private sealed class _VPackDeserializer_97 : VPackDeserializer<float>
        {
            /// <exception cref="VPackException"/>
            public override float Deserialize(VPackSlice parent, VPackSlice vpack, VPackDeserializationContext context)
            {
                return vpack.AsFloat;
            }
        }
    }
}