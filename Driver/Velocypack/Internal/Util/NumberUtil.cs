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
    public class NumberUtil
    {
        private const int DOUBLE_BYTES = 8;

        private NumberUtil()
            : base()
        {
        }

        public static double toDouble(byte[] array, int offset, int length)
        {
            return double.longBitsToDouble(toLong(array, offset, DOUBLE_BYTES));
        }

        public static long toLong(byte[] array, int offset, int length)
        {
            long result = 0;
            for (int i = offset + length - 1; i >= offset; i--)
            {
                result <<= 8;
                result |= array[i] & unchecked((int)0xFF);
            }
            return result;
        }

        public static java.math.BigInteger toBigInteger(byte[] array, int offset, int length
            )
        {
            java.math.BigInteger result = new java.math.BigInteger(1, new byte[] { });
            for (int i = offset + length - 1; i >= offset; i--)
            {
                result = result.shiftLeft(8);
                result = result.or(java.math.BigInteger.valueOf(array[i] & unchecked((int)0xFF)
                    ));
            }
            return result;
        }

        /// <summary>read a variable length integer in unsigned LEB128 format</summary>
        public static long readVariableValueLength(byte[] array, int offset, bool reverse
            )
        {
            long len = 0;
            byte v;
            long p = 0;
            int i = offset;
            do
            {
                v = array[i];
                len += (long)(v & unchecked((byte)unchecked((int)0x7f))) << p;
                p += 7;
                if (reverse)
                {
                    --i;
                }
                else
                {
                    ++i;
                }
            }
            while ((v & unchecked((byte)unchecked((int)0x80))) != 0);
            return len;
        }

        /// <summary>calculate the length of a variable length integer in unsigned LEB128 format
        /// 	</summary>
        public static long getVariableValueLength(long value)
        {
            long len = 1;
            long val = value;
            while (val >= unchecked((int)0x80))
            {
                val >>= 7;
                ++len;
            }
            return len;
        }
    }
}