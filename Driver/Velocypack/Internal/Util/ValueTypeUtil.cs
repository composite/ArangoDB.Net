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
    public class ValueTypeUtil
    {
        private static readonly System.Collections.Generic.IDictionary<byte, ValueType
            > MAP;

        static ValueTypeUtil()
        {
            MAP = new System.Collections.Generic.Dictionary<byte, ValueType
                >();
            MAP[unchecked((byte)unchecked((int)0x00))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0x01))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x02))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x03))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x04))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x05))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x06))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x07))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x08))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x09))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x0a))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x0b))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x0c))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x0d))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x0e))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x0f))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x10))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x11))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x12))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x13))] = ValueType.
                ARRAY;
            MAP[unchecked((byte)unchecked((int)0x14))] = ValueType.
                OBJECT;
            MAP[unchecked((byte)unchecked((int)0x15))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0x16))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0x17))] = ValueType.
                ILLEGAL;
            MAP[unchecked((byte)unchecked((int)0x18))] = ValueType.
                NULL;
            MAP[unchecked((byte)unchecked((int)0x19))] = ValueType.
                BOOL;
            MAP[unchecked((byte)unchecked((int)0x1a))] = ValueType.
                BOOL;
            MAP[unchecked((byte)unchecked((int)0x1b))] = ValueType.
                DOUBLE;
            MAP[unchecked((byte)unchecked((int)0x1c))] = ValueType.
                UTC_DATE;
            MAP[unchecked((byte)unchecked((int)0x1d))] = ValueType.
                EXTERNAL;
            MAP[unchecked((byte)unchecked((int)0x1e))] = ValueType.
                MIN_KEY;
            MAP[unchecked((byte)unchecked((int)0x1f))] = ValueType.
                MAX_KEY;
            MAP[unchecked((byte)unchecked((int)0x20))] = ValueType.
                INT;
            MAP[unchecked((byte)unchecked((int)0x21))] = ValueType.
                INT;
            MAP[unchecked((byte)unchecked((int)0x22))] = ValueType.
                INT;
            MAP[unchecked((byte)unchecked((int)0x23))] = ValueType.
                INT;
            MAP[unchecked((byte)unchecked((int)0x24))] = ValueType.
                INT;
            MAP[unchecked((byte)unchecked((int)0x25))] = ValueType.
                INT;
            MAP[unchecked((byte)unchecked((int)0x26))] = ValueType.
                INT;
            MAP[unchecked((byte)unchecked((int)0x27))] = ValueType.
                INT;
            MAP[unchecked((byte)unchecked((int)0x28))] = ValueType.
                UINT;
            MAP[unchecked((byte)unchecked((int)0x29))] = ValueType.
                UINT;
            MAP[unchecked((byte)unchecked((int)0x2a))] = ValueType.
                UINT;
            MAP[unchecked((byte)unchecked((int)0x2b))] = ValueType.
                UINT;
            MAP[unchecked((byte)unchecked((int)0x2c))] = ValueType.
                UINT;
            MAP[unchecked((byte)unchecked((int)0x2d))] = ValueType.
                UINT;
            MAP[unchecked((byte)unchecked((int)0x2e))] = ValueType.
                UINT;
            MAP[unchecked((byte)unchecked((int)0x2f))] = ValueType.
                UINT;
            MAP[unchecked((byte)unchecked((int)0x30))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x31))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x32))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x33))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x34))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x35))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x36))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x37))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x38))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x39))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x3a))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x3b))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x3c))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x3d))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x3e))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x3f))] = ValueType.
                SMALLINT;
            MAP[unchecked((byte)unchecked((int)0x40))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x41))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x42))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x43))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x44))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x45))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x46))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x47))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x48))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x49))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x4a))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x4b))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x4c))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x4d))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x4e))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x4f))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x50))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x51))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x52))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x53))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x54))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x55))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x56))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x57))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x58))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x59))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x5a))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x5b))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x5c))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x5d))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x5e))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x5f))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x60))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x61))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x62))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x63))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x64))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x65))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x66))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x67))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x68))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x69))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x6a))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x6b))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x6c))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x6d))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x6e))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x6f))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x70))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x71))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x72))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x73))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x74))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x75))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x76))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x77))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x78))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x79))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x7a))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x7b))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x7c))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x7d))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x7e))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x7f))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x80))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x81))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x82))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x83))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x84))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x85))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x86))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x87))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x88))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x89))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x8a))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x8b))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x8c))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x8d))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x8e))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x8f))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x90))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x91))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x92))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x93))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x94))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x95))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x96))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x97))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x98))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x99))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x9a))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x9b))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x9c))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x9d))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x9e))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0x9f))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa0))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa1))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa2))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa3))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa4))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa5))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa6))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa7))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa8))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xa9))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xaa))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xab))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xac))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xad))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xae))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xaf))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb0))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb1))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb2))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb3))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb4))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb5))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb6))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb7))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb8))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xb9))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xba))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xbb))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xbc))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xbd))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xbe))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xbf))] = ValueType.
                STRING;
            MAP[unchecked((byte)unchecked((int)0xc0))] = ValueType.
                BINARY;
            MAP[unchecked((byte)unchecked((int)0xc1))] = ValueType.
                BINARY;
            MAP[unchecked((byte)unchecked((int)0xc2))] = ValueType.
                BINARY;
            MAP[unchecked((byte)unchecked((int)0xc3))] = ValueType.
                BINARY;
            MAP[unchecked((byte)unchecked((int)0xc4))] = ValueType.
                BINARY;
            MAP[unchecked((byte)unchecked((int)0xc5))] = ValueType.
                BINARY;
            MAP[unchecked((byte)unchecked((int)0xc6))] = ValueType.
                BINARY;
            MAP[unchecked((byte)unchecked((int)0xc7))] = ValueType.
                BINARY;
            MAP[unchecked((byte)unchecked((int)0xc8))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xc9))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xca))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xcb))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xcc))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xcd))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xce))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xcf))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xd0))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xd1))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xd2))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xd3))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xd4))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xd5))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xd6))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xd7))] = ValueType.
                BCD;
            MAP[unchecked((byte)unchecked((int)0xd8))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xd9))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xda))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xdb))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xdc))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xdd))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xde))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xdf))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe0))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe1))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe2))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe3))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe4))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe5))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe6))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe7))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe8))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xe9))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xea))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xeb))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xec))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xed))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xee))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xef))] = ValueType.
                NONE;
            MAP[unchecked((byte)unchecked((int)0xf0))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xf1))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xf2))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xf3))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xf4))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xf5))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xf6))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xf7))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xf8))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xf9))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xfa))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xfb))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xfc))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xfd))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xfe))] = ValueType.
                CUSTOM;
            MAP[unchecked((byte)unchecked((int)0xff))] = ValueType.
                CUSTOM;
        }

        private ValueTypeUtil()
            : base()
        {
        }

        public static ValueType get(byte key)
        {
            return MAP[key];
        }
    }
}