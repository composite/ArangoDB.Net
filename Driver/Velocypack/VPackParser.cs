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

        private readonly System.Collections.Generic.IDictionary<ValueType
            , VPackJsonDeserializer> deserializers;

        private readonly System.Collections.Generic.IDictionary<string, IDictionary<ValueType, VPackJsonDeserializer>> deserializersByName;

        public VPackParser()
            : base()
        {
            this.deserializers = new System.Collections.Generic.Dictionary<ValueType
                , VPackJsonDeserializer>();
            this.deserializersByName = new System.Collections.Generic.Dictionary<string, IDictionary<ValueType, VPackJsonDeserializer>>();
        }

        /// <exception cref="VPackException"/>
        public virtual string toJson(VPackSlice vpack)
        {
            return this.toJson(vpack, false);
        }

        /// <exception cref="VPackException"/>
        public virtual string toJson(VPackSlice vpack, bool includeNullValues
            )
        {
            java.lang.StringBuilder json = new java.lang.StringBuilder();
            this.parse(null, null, vpack, json, includeNullValues);
            return json.ToString();
        }

        public virtual VPackParser registerDeserializer(string attribute
            , ValueType type, VPackJsonDeserializer
             deserializer)
        {
            System.Collections.Generic.IDictionary<ValueType, VPackJsonDeserializer
                > byName = this.deserializersByName[attribute];
            if (byName == null)
            {
                byName = new System.Collections.Generic.Dictionary<ValueType
                    , VPackJsonDeserializer>();
                this.deserializersByName[attribute] = byName;
            }
            byName[type] = deserializer;
            return this;
        }

        public virtual VPackParser registerDeserializer(ValueType
             type, VPackJsonDeserializer deserializer)
        {
            this.deserializers[type] = deserializer;
            return this;
        }

        private VPackJsonDeserializer getDeserializer(string attribute
            , ValueType type)
        {
            VPackJsonDeserializer deserializer = null;
            System.Collections.Generic.IDictionary<ValueType, VPackJsonDeserializer
                > byName = this.deserializersByName[attribute];
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
        private void parse(VPackSlice parent, string attribute, VPackSlice
             value, java.lang.StringBuilder json, bool includeNullValues)
        {
            VPackJsonDeserializer deserializer = null;
            if (attribute != null)
            {
                appendField(attribute, json);
                deserializer = this.getDeserializer(attribute, value.type());
            }
            if (deserializer != null)
            {
                deserializer.deserialize(parent, attribute, value, json);
            }
            else
            {
                if (value.isObject())
                {
                    this.parseObject(value, json, includeNullValues);
                }
                else
                {
                    if (value.isArray())
                    {
                        this.parseArray(value, json, includeNullValues);
                    }
                    else
                    {
                        if (value.isBoolean())
                        {
                            json.Append(value.getAsBoolean());
                        }
                        else
                        {
                            if (value.isString())
                            {
                                json.Append(org.json.simple.JSONValue.toJSONString(value.getAsString()));
                            }
                            else
                            {
                                if (value.isNumber())
                                {
                                    json.Append(value.getAsNumber());
                                }
                                else
                                {
                                    if (value.isNull())
                                    {
                                        json.Append(NULL);
                                    }
                                    else
                                    {
                                        json.Append(org.json.simple.JSONValue.toJSONString(NON_REPRESENTABLE_TYPE));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void appendField(string attribute, java.lang.StringBuilder json)
        {
            json.Append(org.json.simple.JSONValue.toJSONString(attribute));
            json.Append(FIELD);
        }

        /// <exception cref="VPackException"/>
        private void parseObject(VPackSlice value, java.lang.StringBuilder
             json, bool includeNullValues)
        {
            json.Append(OBJECT_OPEN);
            int added = 0;
            for (System.Collections.Generic.IEnumerator<KeyValuePair<string, VPackSlice>> iterator = value.objectIterator();
                iterator.MoveNext();)
            {
                System.Collections.Generic.KeyValuePair<string, VPackSlice
                    > next = iterator.Current;
                VPackSlice nextValue = next.Value;
                if (!nextValue.isNull() || includeNullValues)
                {
                    if (added++ > 0)
                    {
                        json.Append(SEPARATOR);
                    }
                    this.parse(value, next.Key, nextValue, json, includeNullValues);
                }
            }
            json.Append(OBJECT_CLOSE);
        }

        /// <exception cref="VPackException"/>
        private void parseArray(VPackSlice value, java.lang.StringBuilder
             json, bool includeNullValues)
        {
            json.Append(ARRAY_OPEN);
            int added = 0;
            for (int i = 0; i < value.getLength(); i++)
            {
                VPackSlice valueAt = value.get(i);
                if (!valueAt.isNull() || includeNullValues)
                {
                    if (added++ > 0)
                    {
                        json.Append(SEPARATOR);
                    }
                    this.parse(value, null, valueAt, json, includeNullValues);
                }
            }
            json.Append(ARRAY_CLOSE);
        }

        /// <exception cref="VPackException"/>
        public virtual VPackSlice fromJson(string json)
        {
            return this.fromJson(json, false);
        }

        /// <exception cref="VPackException"/>
        public virtual VPackSlice fromJson(string json, bool includeNullValues
            )
        {
            VPackBuilder builder = new VPackBuilder
                ();
            org.json.simple.parser.JSONParser parser = new org.json.simple.parser.JSONParser(
                );
            org.json.simple.parser.ContentHandler contentHandler = new VPackParser.VPackContentHandler
                (builder, includeNullValues);
            try
            {
                parser.parse(json, contentHandler);
            }
            catch (org.json.simple.parser.ParseException e)
            {
                throw new VPackBuilderException(e);
            }
            return builder.Slice();
        }

        private class VPackContentHandler : org.json.simple.parser.ContentHandler
        {
            private readonly VPackBuilder builder;

            private string attribute;

            private readonly bool includeNullValues;

            public VPackContentHandler(VPackBuilder builder, bool includeNullValues
                )
            {
                this.builder = builder;
                this.includeNullValues = includeNullValues;
                this.attribute = null;
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            private void add(ValueType value)
            {
                try
                {
                    this.builder.Add(this.attribute, value);
                    this.attribute = null;
                }
                catch (VPackBuilderException)
                {
                    throw new org.json.simple.parser.ParseException(org.json.simple.parser.ParseException
                        .ERROR_UNEXPECTED_EXCEPTION);
                }
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            private void add(string value)
            {
                try
                {
                    this.builder.Add(this.attribute, value);
                    this.attribute = null;
                }
                catch (VPackBuilderException)
                {
                    throw new org.json.simple.parser.ParseException(org.json.simple.parser.ParseException
                        .ERROR_UNEXPECTED_EXCEPTION);
                }
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            private void add(bool value)
            {
                try
                {
                    this.builder.Add(this.attribute, value);
                    this.attribute = null;
                }
                catch (VPackBuilderException)
                {
                    throw new org.json.simple.parser.ParseException(org.json.simple.parser.ParseException
                        .ERROR_UNEXPECTED_EXCEPTION);
                }
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            private void add(double value)
            {
                try
                {
                    this.builder.Add(this.attribute, value);
                    this.attribute = null;
                }
                catch (VPackBuilderException)
                {
                    throw new org.json.simple.parser.ParseException(org.json.simple.parser.ParseException
                        .ERROR_UNEXPECTED_EXCEPTION);
                }
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            private void add(long value)
            {
                try
                {
                    this.builder.Add(this.attribute, value);
                    this.attribute = null;
                }
                catch (VPackBuilderException)
                {
                    throw new org.json.simple.parser.ParseException(org.json.simple.parser.ParseException
                        .ERROR_UNEXPECTED_EXCEPTION);
                }
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            private void close()
            {
                try
                {
                    this.builder.Close();
                }
                catch (VPackBuilderException)
                {
                    throw new org.json.simple.parser.ParseException(org.json.simple.parser.ParseException
                        .ERROR_UNEXPECTED_EXCEPTION);
                }
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            /// <exception cref="System.IO.IOException"/>
            public virtual void startJSON()
            {
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            /// <exception cref="System.IO.IOException"/>
            public virtual void endJSON()
            {
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            /// <exception cref="System.IO.IOException"/>
            public virtual bool startObject()
            {
                this.add(ValueType.OBJECT);
                return true;
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            /// <exception cref="System.IO.IOException"/>
            public virtual bool endObject()
            {
                this.close();
                return true;
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            /// <exception cref="System.IO.IOException"/>
            public virtual bool startObjectEntry(string key)
            {
                this.attribute = key;
                return true;
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            /// <exception cref="System.IO.IOException"/>
            public virtual bool endObjectEntry()
            {
                return true;
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            /// <exception cref="System.IO.IOException"/>
            public virtual bool startArray()
            {
                this.add(ValueType.ARRAY);
                return true;
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            /// <exception cref="System.IO.IOException"/>
            public virtual bool endArray()
            {
                this.close();
                return true;
            }

            /// <exception cref="org.json.simple.parser.ParseException"/>
            /// <exception cref="System.IO.IOException"/>
            public virtual bool primitive(object value)
            {
                if (value == null)
                {
                    if (this.includeNullValues)
                    {
                        this.add(ValueType.NULL);
                    }
                }
                else
                {
                    if (typeof(string).isAssignableFrom(Sharpen.Runtime.getClassForObject
                        (value)))
                    {
                        add(typeof(string).cast(value));
                    }
                    else
                    {
                        if (typeof(bool).isAssignableFrom(Sharpen.Runtime.getClassForObject
                            (value)))
                        {
                            add(typeof(bool).cast(value));
                        }
                        else
                        {
                            if (typeof(double).isAssignableFrom(Sharpen.Runtime.getClassForObject
                                (value)))
                            {
                                add(typeof(double).cast(value));
                            }
                            else
                            {
                                if (typeof(java.lang.Number).isAssignableFrom(Sharpen.Runtime.getClassForObject
                                    (value)))
                                {
                                    add(typeof(long).cast(value));
                                }
                            }
                        }
                    }
                }
                return true;
            }
        }
    }
}