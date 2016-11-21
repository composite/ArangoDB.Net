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
    /// <author>Mark - mark at arangodb.com</author>
    public class DefaultVPackBuilderOptions : VPackBuilder.IBuilderOptions
    {
        private bool buildUnindexedArrays;

        private bool buildUnindexedObjects;

        public DefaultVPackBuilderOptions()
            : base()
        {
            this.buildUnindexedArrays = false;
            this.buildUnindexedObjects = false;
        }

        public virtual bool IsBuildUnindexedArrays()
        {
            return this.buildUnindexedArrays;
        }

        public virtual void SetBuildUnindexedArrays(bool buildUnindexedArrays)
        {
            this.buildUnindexedArrays = buildUnindexedArrays;
        }

        public virtual bool IsBuildUnindexedObjects()
        {
            return this.buildUnindexedObjects;
        }

        public virtual void SetBuildUnindexedObjects(bool buildUnindexedObjects)
        {
            this.buildUnindexedObjects = buildUnindexedObjects;
        }
    }
}