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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Reflection;

    using VelocyPack.Exceptions;
    using VelocyPack.Internal;
    using VelocyPack.Migration.Util;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPack
    {
        private const string ATTR_KEY = "key";

        private const string ATTR_VALUE = "value";

        private readonly IDictionary<Type, IVPackSerializer> serializers;

        private readonly IDictionary<Type, IVPackDeserializer> deserializers;

        private readonly IDictionary<string, IDictionary<Type, IVPackDeserializer>> deserializersByName;

        private readonly IDictionary<Type, IVPackInstanceCreator> instanceCreators;

        private readonly IDictionary<Type, IVPackKeyMapAdapter> keyMapAdapters;

        private readonly VPackBuilder.IBuilderOptions builderOptions;

        private readonly VPackCache cache;

        private readonly IVPackSerializationContext serializationContext;

        private readonly VPackDeserializationContext deserializationContext;

        private readonly bool serializeNullValues;

        public class Builder
        {
            private readonly IDictionary<Type, IVPackSerializer> serializers;

            private readonly IDictionary<Type, IVPackDeserializer> deserializers;

            private readonly IDictionary<string, IDictionary<Type, IVPackDeserializer>> deserializersByName;

            private readonly IDictionary<Type, IVPackInstanceCreator> instanceCreators;

            private readonly VPackBuilder.IBuilderOptions builderOptions;

            private bool serializeNullValues;

            private IVPackFieldNamingStrategy fieldNamingStrategy;

            public Builder()
            {
                this.serializers = new Dictionary<Type, IVPackSerializer>();
                this.deserializers = new Dictionary<Type, IVPackDeserializer>();
                this.deserializersByName = new Dictionary<string, IDictionary<Type, IVPackDeserializer>>();
                this.instanceCreators = new Dictionary<Type, IVPackInstanceCreator>();
                this.builderOptions = new DefaultVPackBuilderOptions();
                this.serializeNullValues = false;
                this.instanceCreators[typeof(IEnumerable)] = VPackInstanceCreators.COLLECTION;
                this.instanceCreators[typeof(IList)] = VPackInstanceCreators.LIST;
                this.instanceCreators[typeof(IDictionary)] = VPackInstanceCreators.MAP;
                this.serializers[typeof(string)] = VPackSerializers.STRING;
                this.serializers[typeof(bool)] = VPackSerializers.BOOLEAN;
                this.serializers[typeof(int)] = VPackSerializers.INTEGER;
                this.serializers[typeof(long)] = VPackSerializers.LONG;
                this.serializers[typeof(short)] = VPackSerializers.SHORT;
                this.serializers[typeof(double)] = VPackSerializers.DOUBLE;
                this.serializers[typeof(float)] = VPackSerializers.FLOAT;
                this.serializers[typeof(BigInteger)] = VPackSerializers.BIG_INTEGER;
                this.serializers[typeof(decimal)] = VPackSerializers.NUMBER;
                this.serializers[typeof(char)] = VPackSerializers.CHARACTER;
                this.serializers[typeof(DateTime)] = VPackSerializers.DATE;
                this.serializers[typeof(VPackSlice)] = VPackSerializers.VPACK;
                this.deserializers[typeof(string)] = VPackDeserializers.STRING;
                this.deserializers[typeof(bool)] = VPackDeserializers.BOOLEAN;
                this.deserializers[typeof(int)] = VPackDeserializers.INTEGER;
                this.deserializers[typeof(long)] = VPackDeserializers.LONG;
                this.deserializers[typeof(short)] = VPackDeserializers.SHORT;
                this.deserializers[typeof(double)] = VPackDeserializers.DOUBLE;
                this.deserializers[typeof(float)] = VPackDeserializers.FLOAT;
                this.deserializers[typeof(BigInteger)] = VPackDeserializers.BIG_INTEGER;
                this.deserializers[typeof(decimal)] = VPackDeserializers.NUMBER;
                this.deserializers[typeof(char)] = VPackDeserializers.CHARACTER;
                this.deserializers[typeof(DateTime)] = VPackDeserializers.DATE;
                this.deserializers[typeof(VPackSlice)] = VPackDeserializers.VPACK;
            }

            public virtual Builder RegisterSerializer<T>(Type type, IVPackSerializer serializer)
            {
                this.serializers[type] = serializer;
                return this;
            }

            public virtual Builder RegisterDeserializer<T>(Type type, VPackDeserializer<T> deserializer)
            {
                this.deserializers[type] = deserializer;
                return this;
            }

            public virtual Builder RegisterDeserializer<T>(
                string fieldName,
                Type type,
                VPackDeserializer<T> deserializer)
            {
                IDictionary<Type, IVPackDeserializer> byName = this.deserializersByName[fieldName];
                if (byName == null)
                {
                    byName = new Dictionary<Type, IVPackDeserializer>();
                    this.deserializersByName[fieldName] = byName;
                }

                byName[type] = deserializer;
                return this;
            }

            public virtual Builder RegisterInstanceCreator<T>(Type type, VPackInstanceCreator<T> creator)
            {
                this.instanceCreators[type] = creator;
                return this;
            }

            public virtual Builder BuildUnindexedArrays(bool buildUnindexedArrays)
            {
                this.builderOptions.SetBuildUnindexedArrays(buildUnindexedArrays);
                return this;
            }

            public virtual Builder BuildUnindexedObjects(bool buildUnindexedObjects)
            {
                this.builderOptions.SetBuildUnindexedObjects(buildUnindexedObjects);
                return this;
            }

            public virtual Builder SerializeNullValues(bool serializeNullValues)
            {
                this.serializeNullValues = serializeNullValues;
                return this;
            }

            public virtual Builder FieldNamingStrategy(IVPackFieldNamingStrategy fieldNamingStrategy)
            {
                this.fieldNamingStrategy = fieldNamingStrategy;
                return this;
            }

            public virtual VPack Build()
            {
                return new VPack(
                           this.serializers,
                           this.deserializers,
                           this.instanceCreators,
                           this.builderOptions,
                           this.serializeNullValues,
                           fieldNamingStrategy,
                           this.deserializersByName);
            }
        }

        private VPack(
            IDictionary<Type, IVPackSerializer> serializers,
            IDictionary<Type, IVPackDeserializer> deserializers,
            IDictionary<Type, IVPackInstanceCreator> instanceCreators,
            VPackBuilder.IBuilderOptions builderOptions,
            bool serializeNullValues,
            IVPackFieldNamingStrategy fieldNamingStrategy,
            IDictionary<string, IDictionary<Type, IVPackDeserializer>> deserializersByName)
        {
            this.serializers = serializers;
            this.deserializers = deserializers;
            this.instanceCreators = instanceCreators;
            this.builderOptions = builderOptions;
            this.serializeNullValues = serializeNullValues;
            this.deserializersByName = deserializersByName;
            this.keyMapAdapters = new Dictionary<Type, IVPackKeyMapAdapter>();
            this.cache = new VPackCache(fieldNamingStrategy);
            this.serializationContext = new _VPackSerializationContext_209(this);
            this.deserializationContext = new _VPackDeserializationContext_216(this);
            this.keyMapAdapters[typeof(string)] = VPackKeyMapAdapters.STRING;
            this.keyMapAdapters[typeof(bool)] = VPackKeyMapAdapters.BOOLEAN;
            this.keyMapAdapters[typeof(int)] = VPackKeyMapAdapters.INTEGER;
            this.keyMapAdapters[typeof(long)] = VPackKeyMapAdapters.LONG;
            this.keyMapAdapters[typeof(short)] = VPackKeyMapAdapters.SHORT;
            this.keyMapAdapters[typeof(double)] = VPackKeyMapAdapters.DOUBLE;
            this.keyMapAdapters[typeof(float)] = VPackKeyMapAdapters.FLOAT;
            this.keyMapAdapters[typeof(BigInteger)] = VPackKeyMapAdapters.BIG_INTEGER;
            this.keyMapAdapters[typeof(decimal)] = VPackKeyMapAdapters.NUMBER;
            this.keyMapAdapters[typeof(char)] = VPackKeyMapAdapters.CHARACTER;
        }

        private sealed class _VPackSerializationContext_209 : IVPackSerializationContext
        {
            public _VPackSerializationContext_209(VPack _enclosing)
            {
                this._enclosing = _enclosing;
            }

            /// <exception cref="VPackParserException"/>
            public void Serialize(VPackBuilder builder, string attribute, object entity)
            {
                this._enclosing.Serialize(
                    attribute,
                    entity,
                    entity.GetType(),
                    builder,
                    new Dictionary<string, object>());
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
            public T Deserialize<T>(VPackSlice vpack)
            {
                return this._enclosing.Deserialize<T>(vpack);
            }

            private readonly VPack _enclosing;
        }

        /// <exception cref="VPackParserException"/>
        public virtual T Deserialize<T>(VPackSlice vpack)
        {
            if (typeof(T) == typeof(VPackSlice))
            {
                return (T)(object)vpack;
            }

            T entity;
            try
            {
                entity = (T)GetValue(null, vpack, typeof(T), null);
            }
            catch (Exception e)
            {
                throw new VPackParserException(e);
            }

            return entity;
        }

        private IVPackDeserializer GetDeserializer(string fieldName, Type type)
        {
            IVPackDeserializer deserializer = null;
            IDictionary<Type, IVPackDeserializer> byName = this.deserializersByName[fieldName];
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
        /// <exception cref="VPackException"/>
        private object DeserializeObject(VPackSlice parent, VPackSlice vpack, Type type, string fieldName)
        {
            object entity;
            IVPackDeserializer deserializer = this.GetDeserializer(fieldName, type);
            if (deserializer != null)
            {
                entity = deserializer.Deserialize(parent, vpack, this.deserializationContext);
            }
            else
            {
                if (type == typeof(object))
                {
                    entity = GetValue(parent, vpack, this.getType(vpack), fieldName);
                }
                else
                {
                    entity = CreateInstance(type);
                    this.DeserializeFields(entity, vpack);
                }
            }

            return entity;
        }

        private Type getType(VPackSlice vpack)
        {
            Type type;
            if (vpack.IsObject)
            {
                type = typeof(IDictionary);
            }
            else
            {
                if (vpack.IsString)
                {
                    type = typeof(string);
                }
                else
                {
                    if (vpack.IsBoolean)
                    {
                        type = typeof(bool);
                    }
                    else
                    {
                        if (vpack.IsArray)
                        {
                            type = typeof(ICollection);
                        }
                        else
                        {
                            if (vpack.IsDate)
                            {
                                type = typeof(DateTime);
                            }
                            else
                            {
                                if (vpack.IsDouble)
                                {
                                    type = typeof(double);
                                }
                                else
                                {
                                    if (vpack.IsNumber)
                                    {
                                        type = typeof(decimal);
                                    }
                                    else
                                    {
                                        if (vpack.IsCustom)
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
        private object CreateInstance(Type type)
        {
            object entity = null;
            IVPackInstanceCreator creator = this.instanceCreators[type];
            if (creator != null)
            {
                entity = creator.CreateInstance();
            }
            else
            {
                if (type.IsGenericParameter && !type.IsConstructedGenericType)
                {
                    var info = type.GetTypeInfo();
                    if (typeof(IDictionary<,>).GetTypeInfo().IsAssignableFrom(info))
                    {
                        entity = new Dictionary<object, object>(0);
                    }
                    else if (typeof(IList<>).GetTypeInfo().IsAssignableFrom(info))
                    {
                        entity = new List<object>(0);
                    }
                    else if (typeof(IEnumerable<>).GetTypeInfo().IsAssignableFrom(info))
                    {
                        entity = Enumerable.Empty<object>();
                    }
                    else
                        throw new TypeLoadException(string.Format("Creating open generic types are not supported. ({0})", type));
                }
                else
                {
                    entity = Activator.CreateInstance(type);
                }
            }

            return entity;
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="VPackException"/>
        private void DeserializeFields(object entity, VPackSlice vpack)
        {
            IDictionary<string, VPackCache.FieldInfo> fields = this.cache.getFields(entity.GetType());
            for (IEnumerator<IEntry<string, VPackSlice>> iterator = vpack.ObjectIterator(); iterator.MoveNext();)
            {
                IEntry<string, VPackSlice> next = iterator.Current;
                VPackCache.FieldInfo fieldInfo = fields[next.Key];
                if (fieldInfo != null && fieldInfo.IsDeserialize)
                {
                    this.DeserializeField(vpack, next.Value, entity, fieldInfo);
                }
            }
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="VPackException"/>
        private void DeserializeField(
            VPackSlice parent,
            VPackSlice vpack,
            object entity,
            VPackCache.FieldInfo fieldInfo)
        {
            if (!vpack.IsNone)
            {
                object value = GetValue(parent, vpack, fieldInfo.Type, fieldInfo.FieldName);
                fieldInfo.set(entity, value);
            }
        }

        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="VPackException"/>
        private object GetValue(VPackSlice parent, VPackSlice vpack, Type type, string fieldName)
        {
            object value;
            Type tmpType;
            if (vpack.IsNull)
            {
                value = null;
            }
            else
            {
                IVPackDeserializer deserializer = this.GetDeserializer(fieldName, type);
                if (deserializer != null)
                {
                    value = deserializer.Deserialize(parent, vpack, this.deserializationContext);
                }
                else
                {
                    var info = type.GetTypeInfo();
                    if (type.IsGenericParameter)
                    {
                        if(!type.IsConstructedGenericType) throw new TypeLoadException(string.Format("Creating open generic types are not supported. ({0})", type));

                        Type[] gens;

                        if (typeof(IDictionary<,>).GetTypeInfo().IsAssignableFrom(info))
                        {
                            gens = type.GetTargetType(typeof(IDictionary<,>)).GenericTypeArguments;
                            value = DeserializeMap(parent, vpack, type, gens[0], gens[1]);
                        }
                        else if (typeof(IEnumerable<>).GetTypeInfo().IsAssignableFrom(info))
                        {
                            gens = type.GetTargetType(typeof(IEnumerable<>)).GenericTypeArguments;
                            value = DeserializeCollection(parent, vpack, type, gens[0]);
                        }
                        else value = DeserializeObject(parent, vpack, type, fieldName);
                    }
                    else
                    {
                        Type rawType = typeof(object);
                        if (typeof(IDictionary).GetTypeInfo().IsAssignableFrom(info))
                        {
                            value = DeserializeMap(parent, vpack, type, rawType, rawType);
                        }
                        else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(info))
                        {
                            value = DeserializeCollection(parent, vpack, type, rawType);
                        }
                        else if (info.IsArray)
                        {
                            value = DeserializeArray(parent, vpack, type);
                        }
                        else if (info.IsEnum)
                        {
                            value = Enum.Parse(type, vpack.AsString);
                        }
                        else
                        {
                            value = DeserializeObject(parent, vpack, type, fieldName);
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
        /// <exception cref="VPackException"/>
        private Array DeserializeArray(VPackSlice parent, VPackSlice vpack, Type type)
        {
            int length = vpack.GetLength();

            Type subType = type.GetElementType();
            Array value = Array.CreateInstance(subType, length);
            for (int i = 0; i < length; i++)
            {
                value.SetValue(GetValue(parent, vpack.Get(i), subType, null), i);
            }

            return value;
        }

        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="VPackException"/>
        private IEnumerable DeserializeCollection(VPackSlice parent, VPackSlice vpack, Type type, Type contentType)
        {
            var info = type.GetTypeInfo();
            long length = vpack.GetLength();
            IList value;
            if (typeof(IList).GetTypeInfo().IsAssignableFrom(info) && info.IsClass) value = (IList)CreateInstance(type);
            else value = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(contentType), (int)length);

            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    value.Add(GetValue(parent, vpack.Get(i), contentType, null));
                }
            }

            return value;
        }

        /// <exception cref="java.lang.InstantiationException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="VPackException"/>
        private IDictionary DeserializeMap(VPackSlice parent, VPackSlice vpack, Type type, Type keyType, Type valueType)
        {
            var info = type.GetTypeInfo();
            int length = vpack.GetLength();
            IDictionary value;
            if (typeof(IDictionary).GetTypeInfo().IsAssignableFrom(info) && info.IsClass) value = (IDictionary)CreateInstance(type);
            else value = new Dictionary<object, object>(length);
            if (length > 0)
            {
                IVPackKeyMapAdapter keyMapAdapter = this.GetKeyMapAdapter(keyType);
                if (keyMapAdapter != null)
                {
                    for (IEnumerator<IEntry<string, VPackSlice>> iterator = vpack.ObjectIterator();
                         iterator.MoveNext();)
                    {
                        IEntry<string, VPackSlice> next = iterator.Current;
                        object name = keyMapAdapter.Deserialize(next.Key);
                        value[name] = GetValue(vpack, next.Value, valueType, name.ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < vpack.GetLength(); i++)
                    {
                        VPackSlice entry = vpack.Get(i);
                        object mapKey = GetValue(parent, entry.Get(ATTR_KEY), keyType, null);
                        object mapValue = GetValue(parent, entry.Get(ATTR_VALUE), valueType, null);
                        value[mapKey] = mapValue;
                    }
                }
            }

            return value;
        }

        /// <exception cref="VPackParserException"/>
        public virtual VPackSlice Serialize(object entity)
        {
            return this.Serialize(entity, entity.GetType(), new Dictionary<string, object>());
        }

        /// <exception cref="VPackParserException"/>
        public virtual VPackSlice Serialize(object entity, IDictionary<string, object> additionalFields)
        {
            return this.Serialize(entity, entity.GetType(), additionalFields);
        }

        /// <exception cref="VPackParserException"/>
        public virtual VPackSlice Serialize(object entity, Type type)
        {
            return this.Serialize(entity, type, new Dictionary<string, object>());
        }

        /// <exception cref="VPackParserException"/>
        public virtual VPackSlice Serialize(object entity, Type type, IDictionary<string, object> additionalFields)
        {
            if (type == typeof(VPackSlice))
            {
                return (VPackSlice)entity;
            }

            VPackBuilder builder = new VPackBuilder(this.builderOptions);
            this.Serialize(null, entity, type, builder, new Dictionary<string, object>(additionalFields));
            return builder.Slice();
        }

        /// <exception cref="VPackParserException"/>
        private void Serialize(
            string name,
            object entity,
            Type type,
            VPackBuilder builder,
            IDictionary<string, object> additionalFields)
        {
            try
            {
                this.AddValue(name, type, entity, builder, null, additionalFields);
            }
            catch (Exception e)
            {
                throw new VPackParserException(e);
            }
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="VPackException"/>
        private void SerializeObject(
            string name,
            object entity,
            VPackBuilder builder,
            IDictionary<string, object> additionalFields)
        {
            IVPackSerializer serializer = this.serializers[entity.GetType()];
            if (serializer != null)
            {
                serializer.Serialize(builder, name, entity, this.serializationContext);
            }
            else
            {
                builder.Add(name, ValueType.OBJECT);
                this.SerializeFields(entity, builder, additionalFields);
                if (additionalFields.Count > 0)
                {
                    additionalFields.Clear();
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
        /// <exception cref="VPackException"/>
        private void SerializeFields(object entity, VPackBuilder builder, IDictionary<string, object> additionalFields)
        {
            IDictionary<string, VPackCache.FieldInfo> fields = this.cache.getFields(entity.GetType());
            foreach (VPackCache.FieldInfo fieldInfo in fields.Values)
            {
                if (fieldInfo.IsSerialize)
                {
                    this.SerializeField(entity, builder, fieldInfo, additionalFields);
                }
            }

            foreach (KeyValuePair<string, object> entry in additionalFields)
            {
                string key = entry.Key;
                if (!fields.ContainsKey(key))
                {
                    object value = entry.Value;
                    this.AddValue(key, value != null ? value.GetType() : null, value, builder, null, additionalFields);
                }
            }
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="VPackException"/>
        private void SerializeField(
            object entity,
            VPackBuilder builder,
            VPackCache.FieldInfo fieldInfo,
            IDictionary<string, object> additionalFields)
        {
            string fieldName = fieldInfo.FieldName;
            Type type = fieldInfo.Type;
            object value = fieldInfo.get(entity);
            this.AddValue(fieldName, type, value, builder, fieldInfo, additionalFields);
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="VPackException"/>
        private void AddValue(
            string name,
            Type type,
            object value,
            VPackBuilder builder,
            VPackCache.FieldInfo fieldInfo,
            IDictionary<string, object> additionalFields)
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
                IVPackSerializer serializer = this.serializers[type];
                if (serializer != null)
                {
                    serializer.Serialize(builder, name, value, this.serializationContext);
                }
                else
                {
                    var info = type.GetTypeInfo();
                    if (type.IsGenericParameter)
                    {
                        if(!type.IsConstructedGenericType) throw new TypeLoadException(string.Format("Creating open generic types are not supported. ({0})", type));

                        Type[] gens;

                        if (typeof(IDictionary<,>).GetTypeInfo().IsAssignableFrom(info))
                        {
                            gens = type.GetTargetType(typeof(IDictionary<,>)).GenericTypeArguments;
                            this.SerializeMap(name, value, builder, gens[0], additionalFields);
                        }
                        else if (typeof(IEnumerable<>).GetTypeInfo().IsAssignableFrom(info))
                        {
                            this.SerializeIterable(name, value, builder, additionalFields);
                        }
                        else if (type.IsConstructedGenericType)
                        {
                            this.SerializeObject(name, value, builder, additionalFields);
                        }
                    }
                    else
                    {
                        if (typeof(IDictionary).GetTypeInfo().IsAssignableFrom(info))
                        {
                            this.SerializeMap(name, value, builder, typeof(string), additionalFields);
                        }
                        else if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(info))
                        {
                            this.SerializeIterable(name, value, builder, additionalFields);
                        }
                        else if (info.IsArray)
                        {
                            this.SerializeArray(name, (Array)value, builder, additionalFields);
                        }
                        else
                        {
                            this.SerializeObject(name, value, builder, additionalFields);
                        }
                    }
                }
            }
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="VPackException"/>
        private void SerializeArray(
            string name,
            Array value,
            VPackBuilder builder,
            IDictionary<string, object> additionalFields)
        {
            builder.Add(name, ValueType.ARRAY);
            for (int i = 0; i < value.GetLength(0); i++)
            {
                object element = value.GetValue(i);
                this.AddValue(null, element.GetType(), element, builder, null, additionalFields);
            }

            builder.Close();
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="VPackException"/>
        private void SerializeIterable(
            string name,
            object value,
            VPackBuilder builder,
            IDictionary<string, object> additionalFields)
        {
            builder.Add(name, ValueType.ARRAY);
            for (IEnumerator iterator = ((IEnumerable)value).GetEnumerator(); iterator.MoveNext();)
            {
                object element = iterator.Current;
                this.AddValue(null, element.GetType(), element, builder, null, additionalFields);
            }

            builder.Close();
        }

        /// <exception cref="System.MissingMethodException"/>
        /// <exception cref="java.lang.IllegalAccessException"/>
        /// <exception cref="java.lang.reflect.InvocationTargetException"/>
        /// <exception cref="VPackException"/>
        private void SerializeMap(
            string name,
            object value,
            VPackBuilder builder,
            Type keyType,
            IDictionary<string, object> additionalFields)
        {
            IDictionary map = (IDictionary)value;
            if (map.Count > 0)
            {
                IVPackKeyMapAdapter keyMapAdapter = this.GetKeyMapAdapter(keyType);
                if (keyMapAdapter != null)
                {
                    builder.Add(name, ValueType.OBJECT);
                    foreach (KeyValuePair<object, object> entry in map)
                    {
                        object entryValue = entry.Value;
                        this.AddValue(
                            keyMapAdapter.Serialize(entry.Key),
                            entryValue != null ? entryValue.GetType() : typeof(object),
                            entry.Value,
                            builder,
                            null,
                            additionalFields);
                    }

                    builder.Close();
                }
                else
                {
                    builder.Add(name, ValueType.ARRAY);
                    foreach (KeyValuePair<object, object> entry in map)
                    {
                        string s = null;
                        builder.Add(s, ValueType.OBJECT);
                        this.AddValue(ATTR_KEY, entry.Key.GetType(), entry.Key, builder, null, additionalFields);
                        this.AddValue(ATTR_VALUE, entry.Value.GetType(), entry.Value, builder, null, additionalFields);
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

        private IVPackKeyMapAdapter GetKeyMapAdapter(Type type)
        {
            IVPackKeyMapAdapter adapter = this.keyMapAdapters[type];
            if (adapter == null && type.GetTypeInfo().IsEnum)
            {
                adapter = VPackKeyMapAdapters.CreateEnumAdapter(type);
            }

            return adapter;
        }
    }
}