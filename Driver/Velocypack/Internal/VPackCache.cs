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
    using System.Collections.Generic;

    /// <author>Mark - mark at arangodb.com</author>
	public class VPackCache
    {
        public abstract class FieldInfo
        {
            private readonly global::System.Type type;

            private readonly string fieldName;

            private readonly bool serialize;

            private readonly bool deserialize;

            private FieldInfo(global::System.Type type, string fieldName, bool serialize,
                bool deserialize)
                : base()
            {
                this.type = type;
                this.fieldName = fieldName;
                this.serialize = serialize;
                this.deserialize = deserialize;
            }

            public virtual global::System.Type getType()
            {
                return this.type;
            }

            public virtual string getFieldName()
            {
                return this.fieldName;
            }

            public virtual bool isSerialize()
            {
                return this.serialize;
            }

            public virtual bool isDeserialize()
            {
                return this.deserialize;
            }

            /// <exception cref="java.lang.IllegalAccessException"/>
            public abstract void set(object obj, object value);

            /// <exception cref="java.lang.IllegalAccessException"/>
            public abstract object get(object obj);
        }

        private readonly System.Collections.Generic.IDictionary<global::System.Type, System.Collections.Generic.IDictionary
            <string, FieldInfo>> cache;

        private readonly java.util.Comparator<System.Collections.Generic.KeyValuePair<string
            , FieldInfo>> fieldComparator;

        private readonly VPackFieldNamingStrategy fieldNamingStrategy;

        public VPackCache(VPackFieldNamingStrategy fieldNamingStrategy
            )
            : base()
        {
            this.cache = new java.util.concurrent.ConcurrentHashMap<global::System.Type, System.Collections.Generic.IDictionary
                <string, FieldInfo>>();
            this.fieldComparator = new _Comparator_91();
            this.fieldNamingStrategy = fieldNamingStrategy;
        }

        private sealed class _Comparator_91 : java.util.Comparator<System.Collections.Generic.KeyValuePair
            <string, FieldInfo>>
        {
            public _Comparator_91()
            {
            }

            public int compare(System.Collections.Generic.KeyValuePair<string, FieldInfo
                > o1, System.Collections.Generic.KeyValuePair<string, FieldInfo
                > o2)
            {
                return string.CompareOrdinal(o1.Key, o2.Key);
            }
        }

        public virtual System.Collections.Generic.IDictionary<string, FieldInfo
            > getFields(global::System.Type entityClass)
        {
            System.Collections.Generic.IDictionary<string, FieldInfo
                > fields = this.cache[entityClass];
            if (fields == null)
            {
                fields = new System.Collections.Generic.Dictionary<string, FieldInfo
                    >();
                java.lang.Class tmp = (java.lang.Class)entityClass;
                while (tmp != null && tmp != typeof(object))
                {
                    java.lang.reflect.Field[] declaredFields = tmp.getDeclaredFields();
                    foreach (java.lang.reflect.Field field in declaredFields)
                    {
                        if (!field.isSynthetic() && !java.lang.reflect.Modifier.isStatic(field.getModifiers
                            ()))
                        {
                            field.setAccessible(true);
                            VPackCache.FieldInfo fieldInfo = this.createFieldInfo
                                (field);
                            if (fieldInfo.serialize || fieldInfo.deserialize)
                            {
                                fields[fieldInfo.getFieldName()] = fieldInfo;
                            }
                        }
                    }
                    tmp = tmp.getSuperclass();
                }
                fields = this.sort(fields);
                this.cache[entityClass] = fields;
            }
            return fields;
        }

        private System.Collections.Generic.IDictionary<string, FieldInfo
            > sort(System.Collections.Generic.ICollection<KeyValuePair<string, FieldInfo>> entrySet)
        {
            System.Collections.Generic.IDictionary<string, FieldInfo
                > sorted = new java.util.LinkedHashMap<string, VPackCache.FieldInfo
                >();
            System.Collections.Generic.IList<KeyValuePair<string, FieldInfo>> tmp = new System.Collections.Generic.List
                <KeyValuePair<string, FieldInfo>>(entrySet);
            tmp.Sort(this.fieldComparator);
            foreach (System.Collections.Generic.KeyValuePair<string, FieldInfo
                > entry in tmp)
            {
                sorted[entry.Key] = entry.Value;
            }
            return sorted;
        }

        private VPackCache.FieldInfo createFieldInfo(java.lang.reflect.Field
             field)
        {
            string fieldName = field.getName();
            if (this.fieldNamingStrategy != null)
            {
                fieldName = this.fieldNamingStrategy.translateName(field);
            }
            com.arangodb.velocypack.annotations.SerializedName annotationName = field.getAnnotation
                <com.arangodb.velocypack.annotations.SerializedName>();
            if (annotationName != null)
            {
                fieldName = annotationName.value();
            }
            com.arangodb.velocypack.annotations.Expose expose = field.getAnnotation<com.arangodb.velocypack.annotations.Expose
                >();
            bool serialize = expose != null ? expose.serialize() : true;
            bool deserialize = expose != null ? expose.deserialize() : true;
            java.lang.Class clazz = field.getType();
            global::System.Type type;
            if (typeof(System.Collections.ICollection).isAssignableFrom
                (clazz) || typeof(System.Collections.IDictionary
                ).isAssignableFrom(clazz))
            {
                type = (java.lang.reflect.ParameterizedType)field.getGenericType();
            }
            else
            {
                type = clazz;
            }
            return new _FieldInfo_153(field, type, fieldName, serialize, deserialize);
        }

        private sealed class _FieldInfo_153 : VPackCache.FieldInfo
        {
            public _FieldInfo_153(java.lang.reflect.Field field, global::System.Type baseArg1
                , string baseArg2, bool baseArg3, bool baseArg4)
                : base(baseArg1, baseArg2, baseArg3, baseArg4)
            {
                this.field = field;
            }

            /// <exception cref="java.lang.IllegalAccessException"/>
            public override void set(object obj, object value)
            {
                this.field.set(obj, value);
            }

            /// <exception cref="java.lang.IllegalAccessException"/>
            public override object get(object obj)
            {
                return this.field.get(obj);
            }

            private readonly java.lang.reflect.Field field;
        }
    }
}