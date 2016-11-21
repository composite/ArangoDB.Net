using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocyPack.Internal
{
    using System.IO;
    using System.Reflection;

    using Newtonsoft.Json;

    using VelocyPack.Exceptions;

    internal sealed class VPackJsonRParser
    {

        private readonly VPackBuilder builder;

        private string attribute;

        private readonly bool includeNullValues;

        public VPackJsonRParser(VPackBuilder builder, bool includeNullValues)
        {
            this.builder = builder;
            this.includeNullValues = includeNullValues;
            this.attribute = null;
        }

        public bool Parse(string json)
        {
            using (var reader = new JsonTextReader(new StringReader(json))) return Parse(reader);
        }

        public bool Parse(TextReader tr)
        {
            return Parse(new JsonTextReader(tr));
        }

        public bool Parse(JsonReader reader)
        {
            try
            {
                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonToken.StartObject:
                            this.builder.Add(ValueType.OBJECT);
                            break;
                        case JsonToken.StartArray:
                            this.builder.Add(ValueType.ARRAY);
                            break;
                        case JsonToken.PropertyName:
                            this.attribute = reader.Value.ToString();
                            break;
                        case JsonToken.EndObject:
                        case JsonToken.EndArray:
                            this.builder.Close();
                            break;
                        case JsonToken.Boolean:
                            this.builder.Add(this.attribute, (bool)reader.Value);
                            this.attribute = null;
                            break;
                        case JsonToken.Bytes:
                            this.builder.Add(this.attribute, (byte[])reader.Value);
                            this.attribute = null;
                            break;
                        case JsonToken.Integer:
                            this.builder.Add(this.attribute, (int)reader.Value);
                            this.attribute = null;
                            break;
                        case JsonToken.Date:
                            this.builder.Add(this.attribute, (DateTime)reader.Value);
                            this.attribute = null;
                            break;
                        case JsonToken.Float:
                            this.builder.Add(this.attribute, (float)reader.Value);
                            this.attribute = null;
                            break;
                        case JsonToken.Null:
                            if(this.includeNullValues) this.builder.Add(this.attribute, ValueType.NULL);
                            this.attribute = null;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                throw new JsonReaderException(string.Format("ERROR_UNEXPECTED_TOKEN {0} in depth {1}: {2}", reader.TokenType, reader.Depth, reader.Path), e);
            }
            return RaiseError(reader);
        }

        private bool RaiseError(JsonReader reader)
        {
            throw new JsonReaderException(string.Format("ERROR_UNEXPECTED_TOKEN {0} in depth {1}: {2}", reader.TokenType, reader.Depth, reader.Path));
        }
    }
}
