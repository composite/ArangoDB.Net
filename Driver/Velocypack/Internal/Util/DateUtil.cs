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
    public class DateUtil
    {
        private DateUtil()
            : base()
        {
        }

        public static System.DateTime toDate(byte[] array, int offset, int length)
        {
            long milliseconds = NumberUtil.toLong(array
                , offset, length);
            return new System.DateTime(milliseconds);
        }

        public static java.sql.Date toSQLDate(byte[] array, int offset, int length)
        {
            long milliseconds = NumberUtil.toLong(array
                , offset, length);
            return new java.sql.Date(milliseconds);
        }

        public static java.sql.Timestamp toSQLTimestamp(byte[] array, int offset, int length
            )
        {
            long milliseconds = NumberUtil.toLong(array
                , offset, length);
            return new java.sql.Timestamp(milliseconds);
        }
    }
}