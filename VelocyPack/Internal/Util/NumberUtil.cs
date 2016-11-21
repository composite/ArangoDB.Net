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

namespace VelocyPack.Internal.Util
{
    using System;
    using System.Numerics;

    //http://jonskeet.uk/csharp/miscutil/

    /// <author>Mark - mark at arangodb.com</author>
    public static class NumberUtil
    {
        private const int DOUBLE_BYTES = 8;

        public static double ToDouble(byte[] array, int offset, int length)
        {
            return BitConverter.Int64BitsToDouble(ToLong(array, offset, DOUBLE_BYTES));
        }

        public static long ToLong(byte[] array, int offset, int length)
        {
            long ret = 0;
            for (int i = 0; i < length; i++)
            {
                ret = unchecked((ret << 8) | array[offset + i]);
            }
            return ret;
        }

        public static BigInteger ToBigInteger(byte[] array, int offset, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(array, offset, result, 0, length);
            return new BigInteger(result);
        }

        /// <summary>read a variable length integer in unsigned LEB128 format</summary>
        public static long ReadVariableValueLength(byte[] array, int offset, bool reverse)
        {
            long len = 0;
            byte v;
            int p = 0;
            int i = offset;
            do
            {
                v = array[i];
                len += unchecked(v & (byte)0x7f) << p;
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
            while (unchecked(v & (byte)0x80) != 0);
            return len;
        }

        /// <summary>calculate the length of a variable length integer in unsigned LEB128 format
        /// 	</summary>
        public static long GetVariableValueLength(long value)
        {
            long len = 1;
            long val = value;
            while (val >= 0x80)
            {
                val >>= 7;
                ++len;
            }
            return len;
        }
    }
}