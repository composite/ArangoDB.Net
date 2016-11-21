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
    using global::ArangoDB.Velocypack.Exceptions;

    /// <author>Mark - mark at arangodb.com</author>
	public class VPackAttributeTranslatorImpl : VPackAttributeTranslator
    {
        private const string KEY = "_key";

        private const string REV = "_rev";

        private const string ID = "_id";

        private const string FROM = "_from";

        private const string TO = "_to";

        private const byte KEY_ATTRIBUTE = unchecked((int)0x31);

        private const byte REV_ATTRIBUTE = unchecked((int)0x32);

        private const byte ID_ATTRIBUTE = unchecked((int)0x33);

        private const byte FROM_ATTRIBUTE = unchecked((int)0x34);

        private const byte TO_ATTRIBUTE = unchecked((int)0x35);

        private const byte ATTRIBUTE_BASE = unchecked((int)0x30);

        private VPackBuilder builder;

        private readonly System.Collections.Generic.IDictionary<string, VPackSlice
            > attributeToKey;

        private readonly System.Collections.Generic.IDictionary<int, VPackSlice
            > keyToAttribute;

        public VPackAttributeTranslatorImpl()
            : base()
        {
            this.builder = null;
            this.attributeToKey = new System.Collections.Generic.Dictionary<string, VPackSlice
                >();
            this.keyToAttribute = new System.Collections.Generic.Dictionary<int, VPackSlice
                >();
            try
            {
                this.add(KEY, KEY_ATTRIBUTE - ATTRIBUTE_BASE);
                this.add(REV, REV_ATTRIBUTE - ATTRIBUTE_BASE);
                this.add(ID, ID_ATTRIBUTE - ATTRIBUTE_BASE);
                this.add(FROM, FROM_ATTRIBUTE - ATTRIBUTE_BASE);
                this.add(TO, TO_ATTRIBUTE - ATTRIBUTE_BASE);
                this.seal();
            }
            catch (VPackException e)
            {
                throw new System.Exception(e);
            }
        }

        /// <exception cref="VPackException"/>
        public virtual void add(string attribute, int key)
        {
            if (this.builder == null)
            {
                this.builder = new VPackBuilder();
                this.builder.Add(ValueType.OBJECT);
            }
            this.builder.Add(attribute, key);
        }

        /// <exception cref="VPackException"/>
        public virtual void seal()
        {
            if (this.builder == null)
            {
                return;
            }
            this.builder.Close();
            VPackSlice slice = this.builder.Slice();
            for (int i = 0; i < slice.getLength(); i++)
            {
                VPackSlice key = slice.keyAt(i);
                VPackSlice value = slice.valueAt(i);
                this.attributeToKey[key.getAsString()] = value;
                this.keyToAttribute[value.getAsInt()] = key;
            }
        }

        public virtual VPackSlice translate(string attribute)
        {
            return this.attributeToKey[attribute];
        }

        public virtual VPackSlice translate(int key)
        {
            return this.keyToAttribute[key];
        }
    }
}