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
    /// <author>Mark - mark at arangodb.com</author>
    public static class ObjectArrayUtil
    {
        private static readonly System.Collections.Generic.IDictionary<byte, int> FIRST_SUB_MAP;
        private static readonly System.Collections.Generic.IDictionary<byte, int> OFFSET_SIZE;

        static ObjectArrayUtil()
        {
            FIRST_SUB_MAP = new System.Collections.Generic.Dictionary<byte, int>();
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x00))] = 0;
            // None
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x01))] = 1;
            // empty array
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x02))] = 2;
            // array without index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x03))] = 3;
            // array without index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x04))] = 5;
            // array without index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x05))] = 9;
            // array without index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x06))] = 3;
            // array with index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x07))] = 5;
            // array with index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x08))] = 9;
            // array with index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x09))] = 9;
            // array with index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x0a))] = 1;
            // empty object
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x0b))] = 3;
            // object with sorted index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x0c))] = 5;
            // object with sorted index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x0d))] = 9;
            // object with sorted index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x0e))] = 9;
            // object with sorted index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x0f))] = 3;
            // object with unsorted index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x10))] = 5;
            // object with unsorted index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x11))] = 9;
            // object with unsorted index table
            FIRST_SUB_MAP[unchecked((byte)unchecked((int)0x12))] = 9;

            OFFSET_SIZE = new System.Collections.Generic.Dictionary<byte, int>();
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x00))] = 0;
            // None
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x01))] = 1;
            // empty array
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x02))] = 1;
            // array without index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x03))] = 2;
            // array without index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x04))] = 4;
            // array without index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x05))] = 8;
            // array without index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x06))] = 1;
            // array with index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x07))] = 2;
            // array with index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x08))] = 4;
            // array with index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x09))] = 8;
            // array with index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x0a))] = 1;
            // empty object
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x0b))] = 1;
            // object with sorted index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x0c))] = 2;
            // object with sorted index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x0d))] = 4;
            // object with sorted index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x0e))] = 8;
            // object with sorted index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x0f))] = 1;
            // object with unsorted index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x10))] = 2;
            // object with unsorted index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x11))] = 4;
            // object with unsorted index table
            OFFSET_SIZE[unchecked((byte)unchecked((int)0x12))] = 8;
        }

        // object with unsorted index table
        public static int GetFirstSubMap(byte key)
        {
            return FIRST_SUB_MAP[key];
        }

        // object with unsorted index table
        public static int GetOffsetSize(byte key)
        {
            return OFFSET_SIZE[key];
        }
    }
}