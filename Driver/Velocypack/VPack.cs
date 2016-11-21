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
    using global::ArangoDB.Velocypack.Internal;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPack
    {
        private const string ATTR_KEY = "key";

        private const string ATTR_VALUE = "value";

        private readonly System.Collections.Generic.IDictionary<global::System.Type, VPackSerializer
            <object>> serializers;

        private readonly System.Collections.Generic.IDictionary<global::System.Type, VPackDeserializer
            <object>> deserializers;

        private readonly System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary
            <global::System.Type, VPackDeserializer<object>>> deserializersByName;

        private readonly System.Collections.Generic.IDictionary<global::System.Type, VPackInstanceCreator
            <object>> instanceCreators;

        private readonly System.Collections.Generic.IDictionary<global::System.Type, VPackKeyMapAdapter
            <object>> keyMapAdapters;

        private readonly VPackBuilder.IBuilderOptions builderOptions;

        private readonly VPackCache cache;

        private readonly VPackSerializationContext serializationContext;

        private readonly VPackDeserializationContext deserializationContext;

        private readonly bool serializeNullValues;

        public class Builder
        {
            private readonly System.Collections.Generic.IDictionary<global::System.Type, VPackSerializer
                <object>> serializers;

            private readonly System.Collections.Generic.IDictionary<global::System.Type, VPackDeserializer
                <object>> deserializers;

            private readonly System.Collections.Generic.IDictionary<string, System.Collections.Generic.IDictionary
                <global::System.Type, VPackDeserializer<object>>> deserializersByName;

            private readonly System.Collections.Generic.IDictionary<global::System.Type, VPackInstanceCreator
                <object>> instanceCreators;

            private readonly VPackBuilder.IBuilderOptions builderOptions;

            private bool serializeNullValues;

            private VPackFieldNamingStrategy fieldNamingStrategy;

            public Builder()
                : base()
            {
                this.serializers = new System.Collections.Generic.Dictionary<global::System.Type, VPackSerializer
                    <object>>();
                this.deserializers = new System.Collections.Generic.Dictionary<global::System.Type,
                    VPackDeserializer<object>>();
                this.deserializersByName = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.IDictionary
                    <global::System.Type, VPackDeserializer<object>>>();
                this.instanceCreators = new System.Collections.Generic.Dictionary<global::System.Type
                    , VPackInstanceCreator<object>>();
                this.builderOptions = new DefaultVPackBuilderOptions
                    ();
                serializeNullValues = false;
                this.instanceCreators[typeof(System.Collections.IEnumerable)] = VPackInstanceCreators.COLLECTION;
                this.instanceCreators[typeof(System.Collections.IList)] = VPackInstanceCreators.LIST;
                this.instanceCreators[typeof(System.Collections.IDictionary)] = VPackInstanceCreators.MAP;
                this.serializers[typeof(string)] = VPackSerializers
                    .STRING;
                this.serializers[typeof(bool)] = VPackSerializers
                    .BOOLEAN;
                this.serializers[typeof(bool)] = VPackSerializers
                    .BOOLEAN;
                this.serializers[typeof(int)] = VPackSerializers
                    .INTEGER;
                this.serializers[typeof(int)] = VPackSerializers
                    .INTEGER;
                this.serializers[typeof(long)] = VPackSerializers
                    .LONG;
                this.serializers[typeof(long)] = VPackSerializers
                    .LONG;
                this.serializers[typeof(short)] = VPackSerializers
                    .SHORT;
                this.serializers[typeof(short)] = VPackSerializers
                    .SHORT;
                this.serializers[typeof(double)] = VPackSerializers
                    .DOUBLE;
                this.serializers[typeof(double)] = VPackSerializers
                    .DOUBLE;
                this.serializers[typeof(float)] = VPackSerializers
                    .FLOAT;
                this.serializers[typeof(float)] = VPackSerializers
                    .FLOAT;
                this.serializers[typeof(java.math.BigInteger)] = VPackSerializers
                    .BIG_INTEGER;
                this.serializers[typeof(java.math.BigDecimal)] = VPackSerializers
                    .BIG_DECIMAL;
                this.serializers[typeof(java.lang.Number)] = VPackSerializers
                    .NUMBER;
                this.serializers[typeof(char)] = VPackSerializers
                    .CHARACTER;
                this.serializers[typeof(char)] = VPackSerializers
                    .CHARACTER;
                this.serializers[typeof(System.DateTime)] = VPackSerializers
                    .DATE;
                this.serializers[typeof(java.sql.Date)] = VPackSerializers
                    .SQL_DATE;
                this.serializers[typeof(java.sql.Timestamp)] = VPackSerializers
                    .SQL_TIMESTAMP;
                this.serializers[typeof(VPackSlice
                )] = VPackSerializers.VPACK;
                this.deserializers[typeof(string)] = VPackDeserializers
                    .STRING;
                this.deserializers[typeof(bool)] = VPackDeserializers
                    .BOOLEAN;
                this.deserializers[typeof(bool)] = VPackDeserializers
                    .BOOLEAN;
                this.deserializers[typeof(int)] = VPackDeserializers
                    .INTEGER;
                this.deserializers[typeof(int)] = VPackDeserializers
                    .INTEGER;
                this.deserializers[typeof(long)] = VPackDeserializers
                    .LONG;
                this.deserializers[typeof(long)] = VPackDeserializers
                    .LONG;
                this.deserializers[typeof(short)] = VPackDeserializers
                    .SHORT;
                this.deserializers[typeof(short)] = VPackDeserializers
                    .SHORT;
                this.deserializers[typeof(double)] = VPackDeserializers
                    .DOUBLE;
                this.deserializers[typeof(double)] = VPackDeserializers
                    .DOUBLE;
                this.deserializers[typeof(float)] = VPackDeserializers
                    .FLOAT;
                this.deserializers[typeof(float)] = VPackDeserializers
                    .FLOAT;
                this.deserializers[typeof(java.math.BigInteger)] = VPackDeserializers
                    .BIG_INTEGER;
                this.deserializers[typeof(java.math.BigDecimal)] = VPackDeserializers
                    .BIG_DECIMAL;
                this.deserializers[typeof(java.lang.Number)] = VPackDeserializers
                    .NUMBER;
                this.deserializers[typeof(char)] = VPackDeserializers
                    .CHARACTER;
                this.deserializers[typeof(char)] = VPackDeserializers
                    .CHARACTER;
                this.deserializers[typeof(System.DateTime)] = VPackDeserializers
                    .DATE;
                this.deserializers[typeof(java.sql.Date)] = VPackDeserializers
                    .SQL_DATE;
                this.deserializers[typeof(java.sql.Timestamp)] = VPackDeserializers
                    .SQL_TIMESTAMP;
                this.deserializers[typeof(VPackSlice
                )] = VPackDeserializers.VPACK;
            }

            public virtual VPack.Builder registerSerializer<T>(global::System.Type
                 type, VPackSerializer<T> serializer)
            {
                this.serializers[type] = serializer;
                return this;
            }

            public virtual VPack.Builder registerDeserializer<T>(global::System.Type
                 type, VPackDeserializer<T> deserializer)
            {
                this.deserializers[type] = deserializer;
                return this;
            }

            public virtual VPack.Builder registerDeserializer<T>(string
                 fieldName, global::System.Type type, VPackDeserializer
                <T> deserializer)
            {
                System.Collections.Generic.IDictionary<global::System.Type, VPackDeserializer
                    <object>> byName = this.deserializersByName[fieldName];
                if (byName == null)
                {
                    byName = new System.Collections.Generic.Dictionary<global::System.Type, VPackDeserializer
                        <object>>();
                    this.deserializersByName[fieldName] = byName;
                }
                byName[type] = deserializer;
                return this;
            }

            public virtual VPack.Builder registerInstanceCreator<T>(global::System.Type
                 type, VPackInstanceCreator<T> creator)
            {
                this.instanceCreators[type] = creator;
                return this;
            }

            public virtual VPack.Builder buildUnindexedArrays(bool buildUnindexedArrays
                )
            {
                this.builderOptions.SetBuildUnindexedArrays(buildUnindexedArrays);
                return this;
            }

            public virtual VPack.Builder buildUnindexedObjects(bool buildUnindexedObjects
                )
            {
                this.builderOptions.SetBuildUnindexedObjects(buildUnindexedObjects);
                return this;
            }

            public virtual VPack.Builder SerializeNullValues(bool serializeNullValues
                )
            {
                this.serializeNullValues = serializeNullValues;
                return this;
            }

            public virtual VPack.Builder fieldNamingStrategy(VPackFieldNamingStrategy
                 fieldNamingStrategy)
            {
                this.fieldNamingStrategy = fieldNamingStrategy;
                return this;
            }

            public virtual VPack build()
            {
                return new VPack(this.serializers, this.deserializers, this.instanceCreators
                    , this.builderOptions, serializeNullValues, fieldNamingStrategy, this.deserializersByName);
            }
        }

        private VPack(System.Collections.Generic.IDictionary<global::System.Type, VPackSerializer
            <object>> serializers, System.Collections.Generic.IDictionary<global::System.Type
            , VPackDeserializer<object>> deserializers, System.Collections.Generic.IDictionary
            <global::System.Type, VPackInstanceCreator<object>> instanceCreators
            , VPackBuilder.IBuilderOptions builderOptions, bool serializeNullValues
            , VPackFieldNamingStrategy fieldNamingStrategy, System.Collections.Generic.IDictionary
            <string, System.Collections.Generic.IDictionary<global::System.Type, VPackDeserializer
            <object>>> deserializersByName)
            : base()
        {
            this.serializers = serializers;
            this.deserializers = deserializers;
            this.instanceCreators = instanceCreators;
            this.builderOptions = builderOptions;
            this.serializeNullValues = serializeNullValues;
            this.deserializersByName = deserializersByName;
            this.keyMapAdapters = new System.Collections.Generic.Dictionary<global::System.Type
                , VPackKeyMapAdapter<object>>();
            this.cache = new VPackCache(fieldNamingStrategy);
            this.serializationContext = new _VPackSerializationContext_209(this);
            this.deserializationContext = new _VPackDeserializationContext_216(this);
            this.keyMapAdapters[typeof(string)] = VPackKeyMapAdapters
                .STRING;
            this.keyMapAdapters[typeof(bool)] = VPackKeyMapAdapters
                .BOOLEAN;
            this.keyMapAdapters[typeof(int)] = VPackKeyMapAdapters
                .INTEGER;
            this.keyMapAdapters[typeof(long)] = VPackKeyMapAdapters
                .LONG;
            this.keyMapAdapters[typeof(short)] = VPackKeyMapAdapters
                .SHORT;
            this.keyMapAdapters[typeof(double)] = VPackKeyMapAdapters
                .DOUBLE;
            this.keyMapAdapters[typeof(float)] = VPackKeyMapAdapters
                .FLOAT;
            this.keyMapAdapters[typeof(java.math.BigInteger)] = VPackKeyMapAdapters
                .BIG_INTEGER;
            this.keyMapAdapters[typeof(java.math.BigDecimal)] = VPackKeyMapAdapters
                .BIG_DECIMAL;
            this.keyMapAdapters[typeof(java.lang.Number)] = VPackKeyMapAdapters
                .NUMBER;
            this.keyMapAdapters[typeof(char)] = VPackKeyMapAdapters
                .CHARACTER;
        }

        private sealed class _VPackSerializationContext_209 : VPackSerializationContext
        {
            public _VPackSerializationContext_209(VPack _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackParserException"/>
            public void serialize(VPackBuilder builder, string attribute
                , object entity)
            {
                this._enclosing.serialize(attribute, entity, Sharpen.Runtime.getClassForObject(entity
                    ), builder, new System.Collections.Generic.Dictionary<string, object>());
            }

            private readonly VPack _enclosing;
        }

        private sealed class _VPackDeserializationContext_216 : VPackDeserializationContext
        {
            public _VPackDeserializationContext_216(VPack _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackParserException"/>
            public T deserialize<T>(VPackSlice vpack)
            {
                System.Type type = typeof(T);
                return this._enclosing.deserialize(vpack, type);
            }

            private readonly VPack _enclosing;
        }

        /// <exception cref="VPackParserException"/>
        public virtual T deserialize<T>(VPackSlice vpack)
        {
            if (typeof(T) == typeof(VPackSlice))
            {
                return (T)(object)vpack;
            }
            T entity;
            try
            {
                entity = (T)getValue(null, vpack, type, null);
            }
            catch (System.Exception e)
            {
                throw new VPackParserException(e);
            }
            return entity;
        }

        private VPackDeserializer<object> getDeserializer(string
            fieldName, global::System.Type type)
        {
            VPackDeserializer<object> deserializer = null;
            System.Collections.Generic.IDictionary<global::System.Type, VPackDeserializer
                <object>> byName = this.deserializersByName[fieldName];
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

        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private T deserializeObject<T>(VPackSlice parent, VPackSlice
             vpack, global::System.Type type, string fieldName)
        {
            T entity;
            VPackDeserializer<object> deserializer = this.getDeserializer(
                fieldName, type);
            if (deserializer != null)
            {
                entity = ((VPackDeserializer<T>)deserializer).deserialize
                    (parent, vpack, this.deserializationContext);
            }
            else
            {
                if (type == typeof(object))
                {
                    entity = (T)getValue(parent, vpack, this.getType(vpack), fieldName);
                }
                else
                {
                    entity = createInstance(type);
                    this.deserializeFields(entity, vpack);
                }
            }
            return entity;
        }

        private global::System.Type getType(VPackSlice vpack)
        {
            global::System.Type type;
            if (vpack.isObject())
            {
                type = typeof(System.Collections.IDictionary);
            }
            else
            {
                if (vpack.isString())
                {
                    type = typeof(string);
                }
                else
                {
                    if (vpack.isBoolean())
                    {
                        type = typeof(bool);
                    }
                    else
                    {
                        if (vpack.isArray())
                        {
                            type = typeof(System.Collections.ICollection);
                        }
                        else
                        {
                            if (vpack.isDate())
                            {
                                type = typeof(System.DateTime);
                            }
                            else
                            {
                                if (vpack.isDouble())
                                {
                                    type = typeof(double);
                                }
                                else
                                {
                                    if (vpack.isNumber())
                                    {
                                        type = typeof(java.lang.Number);
                                    }
                                    else
                                    {
                                        if (vpack.isCustom())
                                        {
                                            type = typeof(string);
                                        }
                                        else
                                        {
                                            type = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return type;
        }

        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        private T createInstance<T>(global::System.Type type)
        {
            T entity;
            VPackInstanceCreator<object> creator = this.instanceCreators[type
                ];
            if (creator != null)
            {
                entity = (T)creator.createInstance();
            }
            else
            {
                if (type is java.lang.reflect.ParameterizedType)
                {
                    entity = createInstance(((java.lang.reflect.ParameterizedType)type).getRawType());
                }
                else
                {
                    entity = ((java.lang.Class)type).newInstance();
                }
            }
            return entity;
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private void deserializeFields(object entity, VPackSlice
            vpack)
        {
            System.Collections.Generic.IDictionary<string, VPackCache.FieldInfo
                > fields = this.cache.getFields(Sharpen.Runtime.getClassForObject(entity));
            for (System.Collections.Generic.IEnumerator<KeyValuePair<string, VPackSlice>> iterator = vpack.objectIterator();
                iterator.MoveNext();)
            {
                System.Collections.Generic.KeyValuePair<string, VPackSlice
                    > next = iterator.Current;
                VPackCache.FieldInfo fieldInfo = fields[next.Key
                    ];
                if (fieldInfo != null && fieldInfo.isDeserialize())
                {
                    this.deserializeField(vpack, next.Value, entity, fieldInfo);
                }
            }
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private void deserializeField(VPackSlice parent, VPackSlice
             vpack, object entity, VPackCache.FieldInfo fieldInfo
            )
        {
            if (!vpack.isNone())
            {
                object value = getValue(parent, vpack, fieldInfo.getType(), fieldInfo.getFieldName
                    ());
                fieldInfo.set(entity, value);
            }
        }

        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private object getValue<T>(VPackSlice parent, VPackSlice
             vpack, global::System.Type type, string fieldName)
        {
            object value;
            if (vpack.isNull())
            {
                value = null;
            }
            else
            {
                VPackDeserializer<object> deserializer = this.getDeserializer(
                    fieldName, type);
                if (deserializer != null)
                {
                    value = ((VPackDeserializer<object>)deserializer).deserialize
                        (parent, vpack, this.deserializationContext);
                }
                else
                {
                    if (type is java.lang.reflect.ParameterizedType)
                    {
                        java.lang.reflect.ParameterizedType pType = typeof(
                            java.lang.reflect.ParameterizedType).cast(type);
                        global::System.Type rawType = pType.getRawType();
                        if (typeof(System.Collections.ICollection).isAssignableFrom
                            ((java.lang.Class)rawType))
                        {
                            value = deserializeCollection(parent, vpack, type, pType.getActualTypeArguments()
                                [0]);
                        }
                        else
                        {
                            if (typeof(System.Collections.IDictionary).isAssignableFrom
                                ((java.lang.Class)rawType))
                            {
                                global::System.Type[] parameterizedTypes = pType.getActualTypeArguments();
                                value = deserializeMap(parent, vpack, type, parameterizedTypes[0], parameterizedTypes
                                    [1]);
                            }
                            else
                            {
                                value = deserializeObject(parent, vpack, type, fieldName);
                            }
                        }
                    }
                    else
                    {
                        if (typeof(System.Collections.ICollection).isAssignableFrom
                            ((java.lang.Class)type))
                        {
                            value = deserializeCollection(parent, vpack, type, 
                                typeof(object));
                        }
                        else
                        {
                            if (typeof(System.Collections.IDictionary).isAssignableFrom
                                ((java.lang.Class)type))
                            {
                                value = deserializeMap(parent, vpack, type, typeof(
                                    string), typeof(object));
                            }
                            else
                            {
                                if (((java.lang.Class)type).isArray())
                                {
                                    value = deserializeArray(parent, vpack, type);
                                }
                                else
                                {
                                    if (((java.lang.Class)type).isEnum())
                                    {
                                        value = java.lang.Enum.valueOf((java.lang.Class)type, vpack.getAsString());
                                    }
                                    else
                                    {
                                        value = deserializeObject(parent, vpack, type, fieldName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return value;
        }

        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private object deserializeArray<T>(VPackSlice parent, VPackSlice
             vpack, global::System.Type type)
        {
            int length = (int)vpack.getLength();
            java.lang.Class componentType = ((java.lang.Class)type).getComponentType();
            object value = java.lang.reflect.Array.newInstance(componentType, length);
            for (int i = 0; i < length; i++)
            {
                java.lang.reflect.Array.set(value, i, getValue(parent, vpack.get(i), componentType
                    , null));
            }
            return value;
        }

        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private object deserializeCollection<T, C>(VPackSlice parent
            , VPackSlice vpack, global::System.Type type, global::System.Type
             contentType)
        {
            System.Collections.ICollection value = (System.Collections.ICollection)createInstance
                (type);
            long length = vpack.getLength();
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    value.add(getValue(parent, vpack.get(i), contentType, null));
                }
            }
            return value;
        }

        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private object deserializeMap<T, K, C>(VPackSlice parent,
            VPackSlice vpack, global::System.Type type, global::System.Type
             keyType, global::System.Type valueType)
        {
            int length = (int)vpack.getLength();
            System.Collections.IDictionary value = (System.Collections.IDictionary)createInstance
                (type);
            if (length > 0)
            {
                VPackKeyMapAdapter<object> keyMapAdapter = this.getKeyMapAdapter
                    (keyType);
                if (keyMapAdapter != null)
                {
                    for (System.Collections.Generic.IEnumerator<KeyValuePair<string, VPackSlice>> iterator = vpack.objectIterator();
                        iterator.MoveNext();)
                    {
                        System.Collections.Generic.KeyValuePair<string, VPackSlice
                            > next = iterator.Current;
                        object name = keyMapAdapter.deserialize(next.Key);
                        value[name] = getValue(vpack, next.Value, valueType, name.ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < vpack.getLength(); i++)
                    {
                        VPackSlice entry = vpack.get(i);
                        object mapKey = getValue(parent, entry.get(ATTR_KEY), keyType, null);
                        object mapValue = getValue(parent, entry.get(ATTR_VALUE), valueType, null);
                        value[mapKey] = mapValue;
                    }
                }
            }
            return value;
        }

        /// <exception cref="VPackParserException"/>
        public virtual VPackSlice serialize(object entity)
        {
            return this.serialize(entity, Sharpen.Runtime.getClassForObject(entity), new System.Collections.Generic.Dictionary
                <string, object>());
        }

        /// <exception cref="VPackParserException"/>
        public virtual VPackSlice serialize(object entity, System.Collections.Generic.IDictionary
            <string, object> additionalFields)
        {
            return this.serialize(entity, Sharpen.Runtime.getClassForObject(entity), additionalFields
                );
        }

        /// <exception cref="VPackParserException"/>
        public virtual VPackSlice serialize(object entity, global::System.Type
             type)
        {
            return this.serialize(entity, type, new System.Collections.Generic.Dictionary<string,
                object>());
        }

        /// <exception cref="VPackParserException"/>
        public virtual VPackSlice serialize(object entity, global::System.Type
             type, System.Collections.Generic.IDictionary<string, object> additionalFields)
        {
            if (type == typeof(VPackSlice
                ))
            {
                return (VPackSlice)entity;
            }
            VPackBuilder builder = new VPackBuilder
                (this.builderOptions);
            this.serialize(null, entity, type, builder, new System.Collections.Generic.Dictionary<
                string, object>(additionalFields));
            return builder.Slice();
        }

        /// <exception cref="VPackParserException"/>
        private void serialize(string name, object entity, global::System.Type type, VPackBuilder
             builder, System.Collections.Generic.IDictionary<string, object> additionalFields
            )
        {
            try
            {
                this.addValue(name, type, entity, builder, null, additionalFields);
            }
            catch (System.Exception e)
            {
                throw new VPackParserException(e);
            }
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private void serializeObject(string name, object entity, VPackBuilder
             builder, System.Collections.Generic.IDictionary<string, object> additionalFields
            )
        {
            VPackSerializer<object> serializer = this.serializers[Sharpen.Runtime.getClassForObject
                (entity)];
            if (serializer != null)
            {
                ((VPackSerializer<object>)serializer).serialize(builder,
                    name, entity, this.serializationContext);
            }
            else
            {
                builder.Add(name, ValueType.OBJECT);
                this.serializeFields(entity, builder, additionalFields);
                if (!additionalFields.isEmpty())
                {
                    additionalFields.clear();
                    builder.Close(true);
                }
                else
                {
                    builder.Close(false);
                }
            }
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private void serializeFields(object entity, VPackBuilder
            builder, System.Collections.Generic.IDictionary<string, object> additionalFields
            )
        {
            System.Collections.Generic.IDictionary<string, VPackCache.FieldInfo
                > fields = this.cache.getFields(Sharpen.Runtime.getClassForObject(entity));
            foreach (VPackCache.FieldInfo fieldInfo in fields
                .Values)
            {
                if (fieldInfo.isSerialize())
                {
                    this.serializeField(entity, builder, fieldInfo, additionalFields);
                }
            }
            foreach (System.Collections.Generic.KeyValuePair<string, object> entry in additionalFields)
            {
                string key = entry.Key;
                if (!fields.Contains(key))
                {
                    object value = entry.Value;
                    this.addValue(key, value != null ? Sharpen.Runtime.getClassForObject(value) : null, value
                        , builder, null, additionalFields);
                }
            }
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private void serializeField(object entity, VPackBuilder builder
            , VPackCache.FieldInfo fieldInfo, System.Collections.Generic.IDictionary
            <string, object> additionalFields)
        {
            string fieldName = fieldInfo.getFieldName();
            global::System.Type type = fieldInfo.getType();
            object value = fieldInfo.get(entity);
            this.addValue(fieldName, type, value, builder, fieldInfo, additionalFields);
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private void addValue(string name, global::System.Type type, object value, VPackBuilder
             builder, VPackCache.FieldInfo fieldInfo, System.Collections.Generic.IDictionary
            <string, object> additionalFields)
        {
            if (value == null)
            {
                if (this.serializeNullValues)
                {
                    builder.Add(name, ValueType.NULL);
                }
            }
            else
            {
                VPackSerializer<object> serializer = this.serializers[type];
                if (serializer != null)
                {
                    ((VPackSerializer<object>)serializer).serialize(builder,
                        name, value, this.serializationContext);
                }
                else
                {
                    if (type is java.lang.reflect.ParameterizedType)
                    {
                        java.lang.reflect.ParameterizedType pType = typeof(
                            java.lang.reflect.ParameterizedType).cast(type);
                        global::System.Type rawType = pType.getRawType();
                        if (typeof(System.Collections.IEnumerable).isAssignableFrom
                            ((java.lang.Class)rawType))
                        {
                            this.serializeIterable(name, value, builder, additionalFields);
                        }
                        else
                        {
                            if (typeof(System.Collections.IDictionary).isAssignableFrom
                                ((java.lang.Class)rawType))
                            {
                                this.serializeMap(name, value, builder, pType.getActualTypeArguments()[0], additionalFields
                                    );
                            }
                            else
                            {
                                this.serializeObject(name, value, builder, additionalFields);
                            }
                        }
                    }
                    else
                    {
                        if (typeof(System.Collections.IEnumerable).isAssignableFrom
                            ((java.lang.Class)type))
                        {
                            this.serializeIterable(name, value, builder, additionalFields);
                        }
                        else
                        {
                            if (typeof(System.Collections.IDictionary).isAssignableFrom
                                ((java.lang.Class)type))
                            {
                                this.serializeMap(name, value, builder, typeof(string
                                    ), additionalFields);
                            }
                            else
                            {
                                if (((java.lang.Class)type).isArray())
                                {
                                    this.serializeArray(name, value, builder, additionalFields);
                                }
                                else
                                {
                                    if (((java.lang.Class)type).isEnum())
                                    {
                                        builder.add(name, typeof(java.lang.Enum).cast(value
                                            ).name());
                                    }
                                    else
                                    {
                                        this.serializeObject(name, value, builder, additionalFields);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private void serializeArray(string name, object value, VPackBuilder
             builder, System.Collections.Generic.IDictionary<string, object> additionalFields
            )
        {
            builder.Add(name, ValueType.ARRAY);
            for (int i = 0; i < java.lang.reflect.Array.getLength(value); i++)
            {
                object element = java.lang.reflect.Array.get(value, i);
                this.addValue(null, Sharpen.Runtime.getClassForObject(element), element, builder, null
                    , additionalFields);
            }
            builder.Close();
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private void serializeIterable(string name, object value, VPackBuilder
             builder, System.Collections.Generic.IDictionary<string, object> additionalFields
            )
        {
            builder.Add(name, ValueType.ARRAY);
            for (System.Collections.IEnumerator iterator = typeof(
                     System.Collections.IEnumerable).cast(value).GetEnumerator(); iterator.MoveNext(
                );)
            {
                object element = iterator.Current;
                this.addValue(null, Sharpen.Runtime.getClassForObject(element), element, builder, null
                    , additionalFields);
            }
            builder.Close();
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="com.arangodb.velocypack.exception.VPackException"/>
        private void serializeMap(string name, object value, VPackBuilder
             builder, global::System.Type keyType, System.Collections.Generic.IDictionary
            <string, object> additionalFields)
        {
            System.Collections.IDictionary map = typeof(System.Collections.IDictionary
            ).cast(value);
            if (map.Count > 0)
            {
                VPackKeyMapAdapter<object> keyMapAdapter = this.getKeyMapAdapter
                    (keyType);
                if (keyMapAdapter != null)
                {
                    builder.Add(name, ValueType.OBJECT);
                    System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object
                        , object>> entrySet = map;
                    foreach (System.Collections.Generic.KeyValuePair<object, object> entry in entrySet)
                    {
                        object entryValue = entry.Value;
                        this.addValue(keyMapAdapter.serialize(entry.Key), entryValue != null ? Sharpen.Runtime.getClassForObject
                            (entryValue) : typeof(object), entry.Value, builder
                            , null, additionalFields);
                    }
                    builder.Close();
                }
                else
                {
                    builder.Add(name, ValueType.ARRAY);
                    System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object
                        , object>> entrySet = map;
                    foreach (System.Collections.Generic.KeyValuePair<object, object> entry in entrySet)
                    {
                        string s = null;
                        builder.Add(s, ValueType.OBJECT);
                        this.addValue(ATTR_KEY, Sharpen.Runtime.getClassForObject(entry.Key), entry.Key, builder
                            , null, additionalFields);
                        this.addValue(ATTR_VALUE, Sharpen.Runtime.getClassForObject(entry.Value), entry.Value,
                            builder, null, additionalFields);
                        builder.Close();
                    }
                    builder.Close();
                }
            }
            else
            {
                builder.Add(name, ValueType.OBJECT);
                builder.Close();
            }
        }

        private VPackKeyMapAdapter<object> getKeyMapAdapter(global::System.Type
             type)
        {
            VPackKeyMapAdapter<object> adapter = this.keyMapAdapters[type];
            if (adapter == null && typeof(java.lang.Enum).isAssignableFrom
                ((java.lang.Class)type))
            {
                adapter = VPackKeyMapAdapters.createEnumAdapter
                    (type);
            }
            return (VPackKeyMapAdapter<object>)adapter;
        }
    }
}