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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    using VelocyPack.Attributes;
    using VelocyPack.Migration.Util;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackCache
    {
        public abstract class FieldInfo
        {
            protected FieldInfo(Type type, string fieldName, bool serialize, bool deserialize)
            {
                this.Type = type;
                this.FieldName = fieldName;
                this.IsSerialize = serialize;
                this.IsDeserialize = deserialize;
            }

            public virtual Type Type { get; }

            public virtual string FieldName { get; }

            public virtual bool IsSerialize { get; }

            public virtual bool IsDeserialize { get; }

            /// <exception cref="java.lang.IllegalAccessException"/>
            public abstract void set(object obj, object value);

            /// <exception cref="java.lang.IllegalAccessException"/>
            public abstract object get(object obj);
        }

        private readonly IDictionary<Type, IDictionary<string, FieldInfo>> cache;

        private readonly IComparer<KeyValuePair<string, FieldInfo>> fieldComparator;

        private readonly IVPackFieldNamingStrategy fieldNamingStrategy;

        public VPackCache(IVPackFieldNamingStrategy fieldNamingStrategy)
        {
            this.cache = new ConcurrentDictionary<Type, IDictionary<string, FieldInfo>>();
            this.fieldComparator = new _Comparator_91();
            this.fieldNamingStrategy = fieldNamingStrategy;
        }

        private sealed class _Comparator_91 : IComparer<KeyValuePair<string, FieldInfo>>
        {
            public int Compare(KeyValuePair<string, FieldInfo> o1, KeyValuePair<string, FieldInfo> o2)
            {
                return string.CompareOrdinal(o1.Key, o2.Key);
            }
        }

        public virtual IDictionary<string, FieldInfo> getFields(Type entityClass)
        {
            IDictionary<string, FieldInfo> fields = this.cache[entityClass];
            if (fields == null)
            {
                fields = new Dictionary<string, FieldInfo>();
                Type tmp = entityClass;
                while (tmp != null && tmp != typeof(object))
                {
                    var info = tmp.GetTypeInfo();
                    var declaredFields =
                        tmp.GetRuntimeProperties()
                            .Where(p => p.IsAccessibleProperty(true, true))
                            .Cast<MemberInfo>()
                            .Union(tmp.GetRuntimeFields().Where(f => f.IsAccessibleField()));

                    foreach (var field in declaredFields)
                    {
                        FieldInfo fieldInfo = this.createFieldInfo(field);
                        if (fieldInfo.IsSerialize || fieldInfo.IsDeserialize)
                        {
                            fields[fieldInfo.FieldName] = fieldInfo;
                        }
                    }

                    tmp = tmp.GetTypeInfo().BaseType;
                }

                fields = this.sort(fields);
                this.cache[entityClass] = fields;
            }

            return fields;
        }

        private IDictionary<string, FieldInfo> sort(IEnumerable<KeyValuePair<string, FieldInfo>> entrySet)
        {
            IDictionary<string, FieldInfo> sorted = new LinkedDictionary<string, FieldInfo>();
            List<KeyValuePair<string, FieldInfo>> tmp = new List<KeyValuePair<string, FieldInfo>>(entrySet);
            tmp.Sort(this.fieldComparator);
            foreach (var entry in tmp)
            {
                sorted[entry.Key] = entry.Value;
            }

            return sorted;
        }

        private FieldInfo createFieldInfo(MemberInfo field)
        {
            string fieldName = field.Name;
            if (this.fieldNamingStrategy != null)
            {
                fieldName = this.fieldNamingStrategy.TranslateName(field);
            }

            SerializedNameAttribute annotationName = field.GetCustomAttribute<SerializedNameAttribute>();
            if (annotationName != null)
            {
                fieldName = annotationName.Name;
            }

            ExposeAttribute expose = field.GetCustomAttribute<ExposeAttribute>();
            bool serialize = expose != null ? expose.Serialize : true;
            bool deserialize = expose != null ? expose.Deserialize : true;
            Type clazz = field.DeclaringType;

            return new _FieldInfo_153(field, clazz, fieldName, serialize, deserialize);
        }

        private sealed class _FieldInfo_153 : FieldInfo
        {
            private readonly MemberAccessor member;

            public _FieldInfo_153(
                MemberInfo member,
                Type baseArg1,
                string baseArg2,
                bool baseArg3,
                bool baseArg4)
                : base(baseArg1, baseArg2, baseArg3, baseArg4)
            {
                if (member is System.Reflection.FieldInfo) this.member = new MemberAccessor((System.Reflection.FieldInfo)member);
                else if (member is PropertyInfo) this.member = new MemberAccessor((PropertyInfo)member);
                else
                    throw new ArgumentException(
                              string.Format("memberis not property or field. ({0})", member.GetType()),
                              "member");
            }

            /// <exception cref="java.lang.IllegalAccessException"/>
            public override void set(object obj, object value)
            {
                this.member.Set(obj, value);
            }

            /// <exception cref="java.lang.IllegalAccessException"/>
            public override object get(object obj)
            {
                return this.member.Get(obj);
            }
        }
    }
}