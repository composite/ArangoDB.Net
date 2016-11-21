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

namespace ArangoDB.Entity
{
    /// <author>Mark - mark at arangodb.com</author>
    public class KeyOptions
    {
        private bool allowUserKeys;

        private KeyType type;

        private int increment;

        private int offset;

        public KeyOptions()
            : base()
        {
        }

        public KeyOptions(bool allowUserKeys, KeyType type, int increment
            , int offset)
            : base()
        {
            this.allowUserKeys = allowUserKeys;
            this.type = type;
            this.increment = increment;
            this.offset = offset;
        }

        public virtual bool getAllowUserKeys()
        {
            return this.allowUserKeys;
        }

        public virtual void setAllowUserKeys(bool allowUserKeys)
        {
            this.allowUserKeys = allowUserKeys;
        }

        public virtual KeyType getType()
        {
            return this.type;
        }

        public virtual void setType(KeyType type)
        {
            this.type = type;
        }

        public virtual int getIncrement()
        {
            return this.increment;
        }

        public virtual void setIncrement(int increment)
        {
            this.increment = increment;
        }

        public virtual int getOffset()
        {
            return this.offset;
        }

        public virtual void setOffset(int offset)
        {
            this.offset = offset;
        }
    }
}