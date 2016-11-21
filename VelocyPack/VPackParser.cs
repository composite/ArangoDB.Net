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
    using System.Collections.Generic;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using VelocyPack.Exceptions;
    using VelocyPack.Internal;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackParser
    {
        private const char OBJECT_OPEN = '{';

        private const char OBJECT_CLOSE = '}';

        private const char ARRAY_OPEN = '[';

        private const char ARRAY_CLOSE = ']';

        private const char FIELD = ':';

        private const char SEPARATOR = ',';

        private const string NULL = "null";

        private const string NON_REPRESENTABLE_TYPE = "(non-representable type)";

        private readonly IDictionary<ValueType, IVPackJsonDeserializer> deserializers;

        private readonly IDictionary<string, IDictionary<ValueType, IVPackJsonDeserializer>> deserializersByName;

        public VPackParser()
        {
            this.deserializers = new Dictionary<ValueType, IVPackJsonDeserializer>();
            this.deserializersByName = new Dictionary<string, IDictionary<ValueType, IVPackJsonDeserializer>>();
        }

        /// <exception cref="VPackException"/>
        public virtual string ToJson(VPackSlice vpack)
        {
            return this.ToJson(vpack, false);
        }

        /// <exception cref="VPackException"/>
        public virtual string ToJson(VPackSlice vpack, bool includeNullValues)
        {
            StringBuilder json = new StringBuilder();
            this.Parse(null, null, vpack, json, includeNullValues);
            return json.ToString();
        }

        public virtual VPackParser RegisterDeserializer(
            string attribute,
            ValueType type,
            IVPackJsonDeserializer deserializer)
        {
            IDictionary<ValueType, IVPackJsonDeserializer> byName = this.deserializersByName[attribute];
            if (byName == null)
            {
                byName = new Dictionary<ValueType, IVPackJsonDeserializer>();
                this.deserializersByName[attribute] = byName;
            }

            byName[type] = deserializer;
            return this;
        }

        public virtual VPackParser RegisterDeserializer(ValueType type, IVPackJsonDeserializer deserializer)
        {
            this.deserializers[type] = deserializer;
            return this;
        }

        private IVPackJsonDeserializer GetDeserializer(string attribute, ValueType type)
        {
            IVPackJsonDeserializer deserializer = null;
            IDictionary<ValueType, IVPackJsonDeserializer> byName = this.deserializersByName[attribute];
            if (byName != null)
            {
                deserializer = byName[type];
            }

            if (deserializer == null)
            {
                deserializer = this.deserializers[type];
            }

            return deserializer;
        }

        /// <exception cref="VPackException"/>
        private void Parse(
            VPackSlice parent,
            string attribute,
            VPackSlice value,
            StringBuilder json,
            bool includeNullValues)
        {
            IVPackJsonDeserializer deserializer = null;
            if (attribute != null)
            {
                AppendField(attribute, json);
                deserializer = this.GetDeserializer(attribute, value.Type());
            }

            if (deserializer != null)
            {
                deserializer.deserialize(parent, attribute, value, json);
            }
            else
            {
                if (value.IsObject)
                {
                    this.ParseObject(value, json, includeNullValues);
                }
                else
                {
                    if (value.IsArray)
                    {
                        this.ParseArray(value, json, includeNullValues);
                    }
                    else
                    {
                        if (value.IsBoolean)
                        {
                            json.Append(value.AsBoolean);
                        }
                        else
                        {
                            if (value.IsString)
                            {
                                json.Append(JsonConvert.ToString(value.AsString));
                            }
                            else
                            {
                                if (value.IsNumber)
                                {
                                    json.Append(value.AsNumber);
                                }
                                else
                                {
                                    if (value.IsNull)
                                    {
                                        json.Append(NULL);
                                    }
                                    else
                                    {
                                        json.Append(JsonConvert.ToString(NON_REPRESENTABLE_TYPE));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void AppendField(string attribute, StringBuilder json)
        {
            json.Append(JsonConvert.ToString(attribute));
            json.Append(FIELD);
        }

        /// <exception cref="VPackException"/>
        private void ParseObject(VPackSlice value, StringBuilder json, bool includeNullValues)
        {
            json.Append(OBJECT_OPEN);
            int added = 0;
            for (IEnumerator<IEntry<string, VPackSlice>> iterator = value.ObjectIterator(); iterator.MoveNext();)
            {
                IEntry<string, VPackSlice> next = iterator.Current;
                VPackSlice nextValue = next.Value;
                if (!nextValue.IsNull || includeNullValues)
                {
                    if (added++ > 0)
                    {
                        json.Append(SEPARATOR);
                    }

                    this.Parse(value, next.Key, nextValue, json, includeNullValues);
                }
            }

            json.Append(OBJECT_CLOSE);
        }

        /// <exception cref="VPackException"/>
        private void ParseArray(VPackSlice value, StringBuilder json, bool includeNullValues)
        {
            json.Append(ARRAY_OPEN);
            int added = 0;
            for (int i = 0; i < value.GetLength(); i++)
            {
                VPackSlice valueAt = value.Get(i);
                if (!valueAt.IsNull || includeNullValues)
                {
                    if (added++ > 0)
                    {
                        json.Append(SEPARATOR);
                    }

                    this.Parse(value, null, valueAt, json, includeNullValues);
                }
            }

            json.Append(ARRAY_CLOSE);
        }

        /// <exception cref="VPackException"/>
        public virtual VPackSlice FromJson(string json)
        {
            return this.FromJson(json, false);
        }

        /// <exception cref="VPackException"/>
        public virtual VPackSlice FromJson(string json, bool includeNullValues)
        {
            VPackBuilder builder = new VPackBuilder();
            try
            {
                new VPackJsonRParser(builder, includeNullValues).Parse(json);
            }
            catch (JsonReaderException e)
            {
                throw new VPackBuilderException(e);
            }

            return builder.Slice();
        }
    }
}