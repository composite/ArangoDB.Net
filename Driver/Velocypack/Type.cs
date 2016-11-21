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
    /// <author>Mark - mark at arangodb.com</author>
    public class Type<T>
    {
        private readonly global::System.Type type;

        protected internal Type()
            : base()
        {
            this.type = getTypeParameter(Sharpen.Runtime.getClassForObject(this));
        }

        protected internal Type(global::System.Type type)
            : base()
        {
            this.type = type;
        }

        private static global::System.Type getTypeParameter(java.lang.Class clazz)
        {
            global::System.Type superclass = clazz.getGenericSuperclass();
            if (superclass is java.lang.Class)
            {
                throw new System.Exception("Missing type parameter.");
            }
            return typeof(java.lang.reflect.ParameterizedType
            ).cast(superclass).getActualTypeArguments()[0];
        }

        public virtual global::System.Type getType()
        {
            return this.type;
        }
    }
}