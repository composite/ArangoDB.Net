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

namespace ArangoDB.Internal.VelocyPack
{
    using global::ArangoDB.Entity;
    using global::ArangoDB.Internal.VelocyStream;
    using global::ArangoDB.Model;
    using global::ArangoDB.Velocypack;
    using global::ArangoDB.Velocypack.Exceptions;
    using global::ArangoDB.Velocypack.Internal.Util;
    using global::ArangoDB.Velocystream;

    /// <author>Mark - mark at arangodb.com</author>
	public class VPackConfigure
    {
        private const string ID = "_id";

        public static void configure(VPack.Builder builder, VPackParser
             vpackParser, CollectionCache cache)
        {
            builder.fieldNamingStrategy(new _VPackFieldNamingStrategy_64());
            builder.registerDeserializer(ID, typeof(string),
                new _VPackDeserializer_74(cache));
            vpackParser.registerDeserializer(ID, ValueType.CUSTOM, new
                _VPackJsonDeserializer_97(cache));
            builder.registerSerializer(typeof(Request
            ), VPackSerializers.REQUEST);
            builder.registerSerializer(typeof(AuthenticationRequest
            ), VPackSerializers.AUTH_REQUEST);
            builder.registerSerializer(typeof(CollectionType
            ), VPackSerializers.COLLECTION_TYPE);
            builder.registerSerializer(typeof(BaseDocument
            ), VPackSerializers.BASE_DOCUMENT);
            builder.registerSerializer(typeof(BaseEdgeDocument
            ), VPackSerializers.BASE_EDGE_DOCUMENT);
            builder.registerSerializer(typeof(TraversalOptions.Order
            ), VPackSerializers.TRAVERSAL_ORDER);
            builder.registerSerializer(typeof(LogLevel
            ), VPackSerializers.LOG_LEVEL);
            builder.registerDeserializer(typeof(Response
            ), VPackDeserializers.RESPONSE);
            builder.registerDeserializer(typeof(CollectionType
            ), VPackDeserializers.COLLECTION_TYPE);
            builder.registerDeserializer(typeof(CollectionStatus
            ), VPackDeserializers.COLLECTION_STATUS);
            builder.registerDeserializer(typeof(BaseDocument
            ), VPackDeserializers.BASE_DOCUMENT);
            builder.registerDeserializer(typeof(BaseEdgeDocument
            ), VPackDeserializers.BASE_EDGE_DOCUMENT);
            builder.registerDeserializer(QueryEntity.PROPERTY_STARTED, 
                typeof(System.DateTime), VPackDeserializers.
                DATE_STRING);
            builder.registerDeserializer(typeof(LogLevel
            ), VPackDeserializers.LOG_LEVEL);
        }

        private sealed class _VPackFieldNamingStrategy_64 : VPackFieldNamingStrategy
        {
            public _VPackFieldNamingStrategy_64()
            {
            }

            public string translateName(java.lang.reflect.Field field)
            {
                com.arangodb.entity.DocumentFieldAttribute annotation = field.getAnnotation<com.arangodb.entity.DocumentFieldAttribute
                    >();
                if (annotation != null)
                {
                    return annotation.value().getSerializeName();
                }
                return field.getName();
            }
        }

        private sealed class _VPackDeserializer_74 : VPackDeserializer
            <string>
        {
            public _VPackDeserializer_74(CollectionCache cache)
            {
                this.cache = cache;
            }

            /// <exception cref="VPackException"/>
            public string deserialize(VPackSlice parent, VPackSlice
                 vpack, VPackDeserializationContext context)
            {
                string id;
                if (vpack.isCustom())
                {
                    long idLong = NumberUtil.toLong(vpack.getBuffer
                        (), vpack.getStart() + 1, vpack.getByteSize() - 1);
                    string collectionName = this.cache.getCollectionName(idLong);
                    if (collectionName != null)
                    {
                        VPackSlice key = parent.get("_key");
                        id = string.format("%s/%s", collectionName, key.getAsString());
                    }
                    else
                    {
                        id = null;
                    }
                }
                else
                {
                    id = vpack.getAsString();
                }
                return id;
            }

            private readonly CollectionCache cache;
        }

        private sealed class _VPackJsonDeserializer_97 : VPackJsonDeserializer
        {
            public _VPackJsonDeserializer_97(CollectionCache cache)
            {
                this.cache = cache;
            }

            /// <exception cref="VPackException"/>
            public void deserialize(VPackSlice parent, string attribute
                , VPackSlice vpack, java.lang.StringBuilder json)
            {
                string id;
                long idLong = NumberUtil.toLong(vpack.getBuffer
                    (), vpack.getStart() + 1, vpack.getByteSize() - 1);
                string collectionName = this.cache.getCollectionName(idLong);
                if (collectionName != null)
                {
                    VPackSlice key = parent.get("_key");
                    id = string.format("%s/%s", collectionName, key.getAsString());
                }
                else
                {
                    id = null;
                }
                json.Append(org.json.simple.JSONValue.toJSONString(id));
            }

            private readonly CollectionCache cache;
        }
    }
}