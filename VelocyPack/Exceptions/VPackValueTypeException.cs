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

namespace VelocyPack.Exceptions
{
    using System.Text;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackValueTypeException : VPackException
    {
        public VPackValueTypeException(params ValueType[] types)
            : base(createMessage(types))
        {
        }

        private static string createMessage(params ValueType[] types
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Expecting type ");
            for (int i = 0; i < types.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(" or ");
                }
                sb.Append(types[i]);
            }
            return sb.ToString();
        }
    }
}