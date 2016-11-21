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

namespace ArangoDB.Velocystream
{
    /// <author>Mark - mark at arangodb.com</author>
    public sealed class RequestType
    {
        public static readonly RequestType DELETE = new RequestType
            (0);

        public static readonly RequestType GET = new RequestType
            (1);

        public static readonly RequestType POST = new RequestType
            (2);

        public static readonly RequestType PUT = new RequestType
            (3);

        public static readonly RequestType HEAD = new RequestType
            (4);

        public static readonly RequestType PATCH = new RequestType
            (5);

        public static readonly RequestType OPTIONS = new RequestType
            (6);

        public static readonly RequestType VSTREAM_CRED = new RequestType
            (7);

        public static readonly RequestType VSTREAM_REGISTER = new
            RequestType(8);

        public static readonly RequestType VSTREAM_STATUS = new
            RequestType(9);

        public static readonly RequestType ILLEGAL = new RequestType
            (10);

        private readonly int type;

        private RequestType(int type)
        {
            this.type = type;
        }

        public int getType()
        {
            return RequestType.type;
        }

        public static RequestType fromType(int type)
        {
            foreach (RequestType rType in RequestType
                .values())
            {
                if (rType.type == type)
                {
                    return rType;
                }
            }
            return null;
        }
    }
}