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
    public sealed class CollectionType
    {
        public static readonly CollectionType DOCUMENT = new CollectionType
            (2);

        public static readonly CollectionType EDGES = new CollectionType
            (3);

        private readonly int type;

        private CollectionType(int type)
        {
            this.type = type;
        }

        public int getType()
        {
            return CollectionType.type;
        }

        public static CollectionType fromType(int type)
        {
            foreach (CollectionType cType in CollectionType
                .values())
            {
                if (cType.type == type)
                {
                    return cType;
                }
            }
            return null;
        }
    }
}