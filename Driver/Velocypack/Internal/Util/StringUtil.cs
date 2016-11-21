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

namespace ArangoDB.Velocypack.Internal.Util
{
    /// <author>Mark - mark at arangodb.com</author>
    public class StringUtil
    {
        private StringUtil()
            : base()
        {
        }

        public static string toString(byte[] array, int offset, int length)
        {
            return Sharpen.Runtime.getStringForBytes(array, offset, length);
        }
    }
}