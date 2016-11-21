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

namespace ArangoDB.Velocypack.Exceptions
{
    using System;
    using System.Text;

    using ValueType = global::ArangoDB.Velocypack.ValueType;

    /// <author>Mark - mark at arangodb.com</author>
    public class VPackBuilderUnexpectedValueException : VPackBuilderException
    {
        private const long serialVersionUID = -7365305871886897353L;

        public VPackBuilderUnexpectedValueException(ValueType type
            , params Type[] classes)
            : base(createMessage(type, null, classes))
        {
        }

        public VPackBuilderUnexpectedValueException(ValueType type
            , string specify, params Type[] classes)
            : base(createMessage(type, specify, classes))
        {
        }

        private static string createMessage(ValueType type, string
             specify, params Type[] classes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Must give ");
            if (specify != null)
            {
                sb.Append(specify);
                sb.Append(" ");
            }
            for (int i = 0; i < classes.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(" or ");
                }
                sb.Append(classes[i].Name);
            }
            sb.Append(" for ");
            sb.Append(typeof(ValueType).Name);
            sb.Append(".");
            sb.Append(type);
            return sb.ToString();
        }
    }
}