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
    public static class VPackSerializers
    {
        private sealed class _VPackSerializer_44 : VPackSerializer<string>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                string value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<string> STRING = new _VPackSerializer_44();

        private sealed class _VPackSerializer_54 : VPackSerializer<bool>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                bool value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<bool> BOOLEAN = new _VPackSerializer_54();

        private sealed class _VPackSerializer_64 : VPackSerializer<int>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(VPackBuilder builder, string attribute, int value, IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<int> INTEGER = new _VPackSerializer_64();

        private sealed class _VPackSerializer_74 : VPackSerializer<long>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                long value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<long> LONG = new _VPackSerializer_74();

        private sealed class _VPackSerializer_84 : VPackSerializer<short>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                short value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<short> SHORT = new _VPackSerializer_84();

        private sealed class _VPackSerializer_94 : VPackSerializer<double>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                double value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<double> DOUBLE = new _VPackSerializer_94();

        private sealed class _VPackSerializer_104 : VPackSerializer<float>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                float value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<float> FLOAT = new _VPackSerializer_104();

        private sealed class _VPackSerializer_114 : VPackSerializer<BigInteger>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                BigInteger value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<BigInteger> BIG_INTEGER = new _VPackSerializer_114();

        private sealed class _VPackSerializer_124 : VPackSerializer<decimal>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                decimal value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<decimal> NUMBER = new _VPackSerializer_124();

        private sealed class _VPackSerializer_144 : VPackSerializer<char>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                char value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<char> CHARACTER = new _VPackSerializer_144();

        private sealed class _VPackSerializer_154 : VPackSerializer<DateTime>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                DateTime value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<DateTime> DATE = new _VPackSerializer_154();

        private sealed class _VPackSerializer_164 : VPackSerializer<DateTime>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                DateTime value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        private sealed class _VPackSerializer_184 : VPackSerializer<VPackSlice>
        {
            /// <exception cref="VPackException"/>
            public override void Serialize(
                VPackBuilder builder,
                string attribute,
                VPackSlice value,
                IVPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<VPackSlice> VPACK = new _VPackSerializer_184();
    }
}