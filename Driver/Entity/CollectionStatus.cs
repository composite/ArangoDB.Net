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
    public sealed class CollectionStatus
    {
        public static readonly CollectionStatus NEW_BORN_COLLECTION =
            new CollectionStatus(1);

        public static readonly CollectionStatus UNLOADED = new CollectionStatus
            (2);

        public static readonly CollectionStatus LOADED = new CollectionStatus
            (3);

        public static readonly CollectionStatus IN_THE_PROCESS_OF_BEING_UNLOADED
             = new CollectionStatus(4);

        public static readonly CollectionStatus DELETED = new CollectionStatus
            (5);

        private readonly int status;

        private CollectionStatus(int status)
        {
            this.status = status;
        }

        public int getStatus()
        {
            return CollectionStatus.status;
        }

        public static CollectionStatus fromStatus(int status)
        {
            foreach (CollectionStatus cStatus in CollectionStatus
                .values())
            {
                if (cStatus.status == status)
                {
                    return cStatus;
                }
            }
            return null;
        }
    }
}