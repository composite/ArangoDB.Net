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
    using System;
    using System.Numerics;

    using global::ArangoDB.Velocypack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackSerializers
    {
        private VPackSerializers()
            : base()
        {
        }

        private sealed class _VPackSerializer_44 : VPackSerializer
            <string>
        {
            public _VPackSerializer_44()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , string value, VPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<string> STRING = new _VPackSerializer_44
            ();

        private sealed class _VPackSerializer_54 : VPackSerializer
            <bool>
        {
            public _VPackSerializer_54()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , bool value, VPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<bool> BOOLEAN = new _VPackSerializer_54
            ();

        private sealed class _VPackSerializer_64 : VPackSerializer
            <int>
        {
            public _VPackSerializer_64()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , int value, VPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<int> INTEGER = new _VPackSerializer_64
            ();

        private sealed class _VPackSerializer_74 : VPackSerializer
            <long>
        {
            public _VPackSerializer_74()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , long value, VPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<long> LONG = new _VPackSerializer_74
            ();

        private sealed class _VPackSerializer_84 : VPackSerializer
            <short>
        {
            public _VPackSerializer_84()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , short value, VPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<short> SHORT = new _VPackSerializer_84
            ();

        private sealed class _VPackSerializer_94 : VPackSerializer
            <double>
        {
            public _VPackSerializer_94()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , double value, VPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<double> DOUBLE = new _VPackSerializer_94
            ();

        private sealed class _VPackSerializer_104 : VPackSerializer
            <float>
        {
            public _VPackSerializer_104()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , float value, VPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<float> FLOAT = new _VPackSerializer_104
            ();

        private sealed class _VPackSerializer_124 : VPackSerializer
            <decimal>
        {
            public _VPackSerializer_124()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute, decimal value, VPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<decimal> BIG_DECIMAL
             = new _VPackSerializer_124();

        private sealed class _VPackSerializer_144 : VPackSerializer
            <char>
        {
            public _VPackSerializer_144()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , char value, VPackSerializationContext context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<char> CHARACTER = new _VPackSerializer_144
            ();

        private sealed class _VPackSerializer_154 : VPackSerializer
            <System.DateTime>
        {
            public _VPackSerializer_154()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , System.DateTime value, VPackSerializationContext context
                )
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<System.DateTime> DATE = new
            _VPackSerializer_154();

        private sealed class _VPackSerializer_164 : VPackSerializer<DateTime>
        {
            public _VPackSerializer_164()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , DateTime value, VPackSerializationContext context
                )
            {
                builder.Add(attribute, value);
            }
        }

        private sealed class _VPackSerializer_184 : VPackSerializer<VPackSlice>
        {
            public _VPackSerializer_184()
            {
            }

            /// <exception cref="VPackException"/>
            public void serialize(VPackBuilder builder, string attribute
                , VPackSlice value, VPackSerializationContext
                 context)
            {
                builder.Add(attribute, value);
            }
        }

        public static VPackSerializer<VPackSlice> VPACK = new _VPackSerializer_184();
    }
}