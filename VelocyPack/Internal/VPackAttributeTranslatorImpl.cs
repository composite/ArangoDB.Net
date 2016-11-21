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
    using System.Collections.Generic;

    using VelocyPack.Exceptions;

    using ValueType = VelocyPack.ValueType;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackAttributeTranslatorImpl : VPackAttributeTranslator
    {
        private const string KEY = "_key";

        private const string REV = "_rev";

        private const string ID = "_id";

        private const string FROM = "_from";

        private const string TO = "_to";

        private const byte KEY_ATTRIBUTE = 0x31;

        private const byte REV_ATTRIBUTE = 0x32;

        private const byte ID_ATTRIBUTE = 0x33;

        private const byte FROM_ATTRIBUTE = 0x34;

        private const byte TO_ATTRIBUTE = 0x35;

        private const byte ATTRIBUTE_BASE = 0x30;

        private VPackBuilder builder;

        private readonly IDictionary<string, VPackSlice> attributeToKey;

        private readonly IDictionary<int, VPackSlice> keyToAttribute;

        public VPackAttributeTranslatorImpl()
        {
            this.builder = null;
            this.attributeToKey = new Dictionary<string, VPackSlice>();
            this.keyToAttribute = new Dictionary<int, VPackSlice>();
            try
            {
                this.Add(KEY, KEY_ATTRIBUTE - ATTRIBUTE_BASE);
                this.Add(REV, REV_ATTRIBUTE - ATTRIBUTE_BASE);
                this.Add(ID, ID_ATTRIBUTE - ATTRIBUTE_BASE);
                this.Add(FROM, FROM_ATTRIBUTE - ATTRIBUTE_BASE);
                this.Add(TO, TO_ATTRIBUTE - ATTRIBUTE_BASE);
                this.Seal();
            }
            catch (VPackException e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <exception cref="VPackException"/>
        public virtual void Add(string attribute, int key)
        {
            if (this.builder == null)
            {
                this.builder = new VPackBuilder();
                this.builder.Add(ValueType.OBJECT);
            }

            this.builder.Add(attribute, key);
        }

        /// <exception cref="VPackException"/>
        public virtual void Seal()
        {
            if (this.builder == null)
            {
                return;
            }

            this.builder.Close();
            VPackSlice slice = this.builder.Slice();
            for (int i = 0; i < slice.GetLength(); i++)
            {
                VPackSlice key = slice.KeyAt(i);
                VPackSlice value = slice.ValueAt(i);
                this.attributeToKey[key.AsString] = value;
                this.keyToAttribute[value.AsInt] = key;
            }
        }

        public virtual VPackSlice Translate(string attribute)
        {
            return this.attributeToKey[attribute];
        }

        public virtual VPackSlice Translate(int key)
        {
            return this.keyToAttribute[key];
        }
    }
}