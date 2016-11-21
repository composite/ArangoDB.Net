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

namespace ArangoDB.Internal
{
    /// <author>Mark - mark at arangodb.com</author>
    public class DocumentCache
    {
        private readonly System.Collections.Generic.IDictionary<java.lang.Class, System.Collections.Generic.IDictionary
            <com.arangodb.entity.DocumentFieldAttribute.Type, java.lang.reflect.Field>> cache;

        public DocumentCache()
            : base()
        {
            this.cache = new System.Collections.Generic.Dictionary<java.lang.Class, System.Collections.Generic.IDictionary
                <com.arangodb.entity.DocumentFieldAttribute.Type, java.lang.reflect.Field>>();
        }

        /// <exception cref="ArangoDBException"/>
        public virtual void setValues(object doc, System.Collections.Generic.IDictionary<
            com.arangodb.entity.DocumentFieldAttribute.Type, string> values)
        {
            try
            {
                System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type, java.lang.reflect.Field
                    > fields = this.getFields(Sharpen.Runtime.getClassForObject(doc));
                foreach (System.Collections.Generic.KeyValuePair<com.arangodb.entity.DocumentFieldAttribute.Type
                    , string> value in values)
                {
                    java.lang.reflect.Field field = fields[value.Key];
                    if (field != null)
                    {
                        field.set(doc, value.Value);
                    }
                }
            }
            catch (System.ArgumentException e)
            {
                throw new ArangoDBException(e);
            }
            catch (java.lang.IllegalAccessException e)
            {
                throw new ArangoDBException(e);
            }
        }

        private System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type
            , java.lang.reflect.Field> getFields(java.lang.Class clazz)
        {
            System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type, java.lang.reflect.Field
                > fields = new System.Collections.Generic.Dictionary<com.arangodb.entity.DocumentFieldAttribute.Type
                , java.lang.reflect.Field>();
            if (!this.isTypeRestricted(clazz))
            {
                fields = this.cache[clazz];
                if (fields == null)
                {
                    fields = this.createFields(clazz);
                    this.cache[clazz] = fields;
                }
            }
            return fields;
        }

        private bool isTypeRestricted(java.lang.Class type)
        {
            return typeof(System.Collections.IDictionary).isAssignableFrom
                (type) || typeof(System.Collections.ICollection).isAssignableFrom(type);
        }

        private System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type
            , java.lang.reflect.Field> createFields(java.lang.Class clazz)
        {
            System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type, java.lang.reflect.Field
                > fields = new System.Collections.Generic.Dictionary<com.arangodb.entity.DocumentFieldAttribute.Type
                , java.lang.reflect.Field>();
            java.lang.Class tmp = clazz;
            System.Collections.Generic.ICollection<com.arangodb.entity.DocumentFieldAttribute.Type> values
                 = new System.Collections.Generic.List<com.arangodb.entity.DocumentFieldAttribute.Type>(java.util.Arrays
                .asList(com.arangodb.entity.DocumentFieldAttribute.Type.values()));
            while (tmp != null && tmp != typeof(object) && values
                .Count > 0)
            {
                java.lang.reflect.Field[] declaredFields = tmp.getDeclaredFields();
                for (int i = 0; i < declaredFields.Length && values.Count > 0; i++)
                {
                    this.findAnnotation(values, fields, declaredFields[i]);
                }
                tmp = tmp.getSuperclass();
            }
            return fields;
        }

        private void findAnnotation(System.Collections.Generic.ICollection<com.arangodb.entity.DocumentFieldAttribute.Type
            > values, System.Collections.Generic.IDictionary<com.arangodb.entity.DocumentFieldAttribute.Type
            , java.lang.reflect.Field> fields, java.lang.reflect.Field field)
        {
            com.arangodb.entity.DocumentFieldAttribute annotation = field.getAnnotation<com.arangodb.entity.DocumentFieldAttribute
                >();
            if (annotation != null && !field.isSynthetic() && !java.lang.reflect.Modifier.isStatic
                (field.getModifiers()) && typeof(string).isAssignableFrom
                (field.getType()))
            {
                com.arangodb.entity.DocumentFieldAttribute.Type value = annotation.value();
                if (values.contains(value))
                {
                    field.setAccessible(true);
                    fields[value] = field;
                    values.remove(value);
                }
            }
        }
    }
}