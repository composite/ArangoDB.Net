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
    using System.Collections.Generic;
    using System.Linq;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackInstanceCreators
    {
        private VPackInstanceCreators()
            : base()
        {
        }

        private sealed class _VPackInstanceCreator_44 : VPackInstanceCreator<System.Collections.IEnumerable>
        {
            public _VPackInstanceCreator_44()
            {
            }

            public System.Collections.IEnumerable createInstance()
            {
                return Enumerable.Empty<object>();
            }
        }

        public static readonly VPackInstanceCreator<System.Collections.IEnumerable> COLLECTION = new _VPackInstanceCreator_44();

        private sealed class _VPackInstanceCreator_50 : VPackInstanceCreator<System.Collections.IList>
        {
            public _VPackInstanceCreator_50()
            {
            }

            public System.Collections.IList createInstance()
            {
                return new List<object>();
            }
        }

        public static readonly VPackInstanceCreator<System.Collections.IList> LIST = new _VPackInstanceCreator_50();

        private sealed class _VPackInstanceCreator_62 : VPackInstanceCreator
            <System.Collections.IDictionary>
        {
            public _VPackInstanceCreator_62()
            {
            }

            public System.Collections.IDictionary createInstance()
            {
                return new Dictionary<object, object>();
            }
        }

        public static readonly VPackInstanceCreator<System.Collections.IDictionary> MAP = new _VPackInstanceCreator_62();
    }
}