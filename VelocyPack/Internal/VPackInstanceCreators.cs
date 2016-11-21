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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <author>Mark - mark at arangodb.com</author>
    public static class VPackInstanceCreators
    {
        private sealed class _VPackInstanceCreator_44 : VPackInstanceCreator<IEnumerable>
        {
            public override IEnumerable CreateInstance()
            {
                return Enumerable.Empty<object>();
            }
        }

        public static readonly VPackInstanceCreator<IEnumerable> COLLECTION =
            new _VPackInstanceCreator_44();

        private sealed class _VPackInstanceCreator_50 : VPackInstanceCreator<IList>
        {
            public override IList CreateInstance()
            {
                return new List<object>(0);
            }
        }

        public static readonly VPackInstanceCreator<IList> LIST = new _VPackInstanceCreator_50();

        private sealed class _VPackInstanceCreator_62 : VPackInstanceCreator<IDictionary>
        {
            public override IDictionary CreateInstance()
            {
                return new Dictionary<object, object>(0);
            }
        }

        public static readonly VPackInstanceCreator<IDictionary> MAP = new _VPackInstanceCreator_62();
    }
}