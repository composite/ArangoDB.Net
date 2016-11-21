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
    public class ValueLengthUtil
    {
        private const int DOUBLE_BYTES = 8;

        private const int LONG_BYTES = 8;

        private const int CHARACTER_BYTES = 2;

        private static readonly System.Collections.Generic.IDictionary<byte, int> MAP;

        static ValueLengthUtil()
        {
            MAP = new System.Collections.Generic.Dictionary<byte, int>();
            MAP[unchecked((byte)unchecked((int)0x00))] = 1;
            MAP[unchecked((byte)unchecked((int)0x01))] = 1;
            MAP[unchecked((byte)unchecked((int)0x02))] = 0;
            MAP[unchecked((byte)unchecked((int)0x03))] = 0;
            MAP[unchecked((byte)unchecked((int)0x04))] = 0;
            MAP[unchecked((byte)unchecked((int)0x05))] = 0;
            MAP[unchecked((byte)unchecked((int)0x06))] = 0;
            MAP[unchecked((byte)unchecked((int)0x07))] = 0;
            MAP[unchecked((byte)unchecked((int)0x08))] = 0;
            MAP[unchecked((byte)unchecked((int)0x09))] = 0;
            MAP[unchecked((byte)unchecked((int)0x0a))] = 1;
            MAP[unchecked((byte)unchecked((int)0x0b))] = 0;
            MAP[unchecked((byte)unchecked((int)0x0c))] = 0;
            MAP[unchecked((byte)unchecked((int)0x0d))] = 0;
            MAP[unchecked((byte)unchecked((int)0x0e))] = 0;
            MAP[unchecked((byte)unchecked((int)0x0f))] = 0;
            MAP[unchecked((byte)unchecked((int)0x10))] = 0;
            MAP[unchecked((byte)unchecked((int)0x11))] = 0;
            MAP[unchecked((byte)unchecked((int)0x12))] = 0;
            MAP[unchecked((byte)unchecked((int)0x13))] = 0;
            MAP[unchecked((byte)unchecked((int)0x14))] = 0;
            MAP[unchecked((byte)unchecked((int)0x15))] = 0;
            MAP[unchecked((byte)unchecked((int)0x16))] = 0;
            MAP[unchecked((byte)unchecked((int)0x17))] = 1;
            MAP[unchecked((byte)unchecked((int)0x18))] = 1;
            MAP[unchecked((byte)unchecked((int)0x19))] = 1;
            MAP[unchecked((byte)unchecked((int)0x1a))] = 1;
            MAP[unchecked((byte)unchecked((int)0x1b))] = 1 + DOUBLE_BYTES;
            MAP[unchecked((byte)unchecked((int)0x1c))] = 1 + LONG_BYTES;
            MAP[unchecked((byte)unchecked((int)0x1d))] = 1 + CHARACTER_BYTES;
            MAP[unchecked((byte)unchecked((int)0x1e))] = 1;
            MAP[unchecked((byte)unchecked((int)0x1f))] = 1;
            MAP[unchecked((byte)unchecked((int)0x20))] = 2;
            MAP[unchecked((byte)unchecked((int)0x21))] = 3;
            MAP[unchecked((byte)unchecked((int)0x22))] = 4;
            MAP[unchecked((byte)unchecked((int)0x23))] = 5;
            MAP[unchecked((byte)unchecked((int)0x24))] = 6;
            MAP[unchecked((byte)unchecked((int)0x25))] = 7;
            MAP[unchecked((byte)unchecked((int)0x26))] = 8;
            MAP[unchecked((byte)unchecked((int)0x27))] = 9;
            MAP[unchecked((byte)unchecked((int)0x28))] = 2;
            MAP[unchecked((byte)unchecked((int)0x29))] = 3;
            MAP[unchecked((byte)unchecked((int)0x2a))] = 4;
            MAP[unchecked((byte)unchecked((int)0x2b))] = 5;
            MAP[unchecked((byte)unchecked((int)0x2c))] = 6;
            MAP[unchecked((byte)unchecked((int)0x2d))] = 7;
            MAP[unchecked((byte)unchecked((int)0x2e))] = 8;
            MAP[unchecked((byte)unchecked((int)0x2f))] = 9;
            MAP[unchecked((byte)unchecked((int)0x30))] = 1;
            MAP[unchecked((byte)unchecked((int)0x31))] = 1;
            MAP[unchecked((byte)unchecked((int)0x32))] = 1;
            MAP[unchecked((byte)unchecked((int)0x33))] = 1;
            MAP[unchecked((byte)unchecked((int)0x34))] = 1;
            MAP[unchecked((byte)unchecked((int)0x35))] = 1;
            MAP[unchecked((byte)unchecked((int)0x36))] = 1;
            MAP[unchecked((byte)unchecked((int)0x37))] = 1;
            MAP[unchecked((byte)unchecked((int)0x38))] = 1;
            MAP[unchecked((byte)unchecked((int)0x39))] = 1;
            MAP[unchecked((byte)unchecked((int)0x3a))] = 1;
            MAP[unchecked((byte)unchecked((int)0x3b))] = 1;
            MAP[unchecked((byte)unchecked((int)0x3c))] = 1;
            MAP[unchecked((byte)unchecked((int)0x3d))] = 1;
            MAP[unchecked((byte)unchecked((int)0x3e))] = 1;
            MAP[unchecked((byte)unchecked((int)0x3f))] = 1;
            MAP[unchecked((byte)unchecked((int)0x40))] = 1;
            MAP[unchecked((byte)unchecked((int)0x41))] = 2;
            MAP[unchecked((byte)unchecked((int)0x42))] = 3;
            MAP[unchecked((byte)unchecked((int)0x43))] = 4;
            MAP[unchecked((byte)unchecked((int)0x44))] = 5;
            MAP[unchecked((byte)unchecked((int)0x45))] = 6;
            MAP[unchecked((byte)unchecked((int)0x46))] = 7;
            MAP[unchecked((byte)unchecked((int)0x47))] = 8;
            MAP[unchecked((byte)unchecked((int)0x48))] = 9;
            MAP[unchecked((byte)unchecked((int)0x49))] = 10;
            MAP[unchecked((byte)unchecked((int)0x4a))] = 11;
            MAP[unchecked((byte)unchecked((int)0x4b))] = 12;
            MAP[unchecked((byte)unchecked((int)0x4c))] = 13;
            MAP[unchecked((byte)unchecked((int)0x4d))] = 14;
            MAP[unchecked((byte)unchecked((int)0x4e))] = 15;
            MAP[unchecked((byte)unchecked((int)0x4f))] = 16;
            MAP[unchecked((byte)unchecked((int)0x50))] = 17;
            MAP[unchecked((byte)unchecked((int)0x51))] = 18;
            MAP[unchecked((byte)unchecked((int)0x52))] = 19;
            MAP[unchecked((byte)unchecked((int)0x53))] = 20;
            MAP[unchecked((byte)unchecked((int)0x54))] = 21;
            MAP[unchecked((byte)unchecked((int)0x55))] = 22;
            MAP[unchecked((byte)unchecked((int)0x56))] = 23;
            MAP[unchecked((byte)unchecked((int)0x57))] = 24;
            MAP[unchecked((byte)unchecked((int)0x58))] = 25;
            MAP[unchecked((byte)unchecked((int)0x59))] = 26;
            MAP[unchecked((byte)unchecked((int)0x5a))] = 27;
            MAP[unchecked((byte)unchecked((int)0x5b))] = 28;
            MAP[unchecked((byte)unchecked((int)0x5c))] = 29;
            MAP[unchecked((byte)unchecked((int)0x5d))] = 30;
            MAP[unchecked((byte)unchecked((int)0x5e))] = 31;
            MAP[unchecked((byte)unchecked((int)0x5f))] = 32;
            MAP[unchecked((byte)unchecked((int)0x60))] = 33;
            MAP[unchecked((byte)unchecked((int)0x61))] = 34;
            MAP[unchecked((byte)unchecked((int)0x62))] = 35;
            MAP[unchecked((byte)unchecked((int)0x63))] = 36;
            MAP[unchecked((byte)unchecked((int)0x64))] = 37;
            MAP[unchecked((byte)unchecked((int)0x65))] = 38;
            MAP[unchecked((byte)unchecked((int)0x66))] = 39;
            MAP[unchecked((byte)unchecked((int)0x67))] = 40;
            MAP[unchecked((byte)unchecked((int)0x68))] = 41;
            MAP[unchecked((byte)unchecked((int)0x69))] = 42;
            MAP[unchecked((byte)unchecked((int)0x6a))] = 43;
            MAP[unchecked((byte)unchecked((int)0x6b))] = 44;
            MAP[unchecked((byte)unchecked((int)0x6c))] = 45;
            MAP[unchecked((byte)unchecked((int)0x6d))] = 46;
            MAP[unchecked((byte)unchecked((int)0x6e))] = 47;
            MAP[unchecked((byte)unchecked((int)0x6f))] = 48;
            MAP[unchecked((byte)unchecked((int)0x70))] = 49;
            MAP[unchecked((byte)unchecked((int)0x71))] = 50;
            MAP[unchecked((byte)unchecked((int)0x72))] = 51;
            MAP[unchecked((byte)unchecked((int)0x73))] = 52;
            MAP[unchecked((byte)unchecked((int)0x74))] = 53;
            MAP[unchecked((byte)unchecked((int)0x75))] = 54;
            MAP[unchecked((byte)unchecked((int)0x76))] = 55;
            MAP[unchecked((byte)unchecked((int)0x77))] = 56;
            MAP[unchecked((byte)unchecked((int)0x78))] = 57;
            MAP[unchecked((byte)unchecked((int)0x79))] = 58;
            MAP[unchecked((byte)unchecked((int)0x7a))] = 59;
            MAP[unchecked((byte)unchecked((int)0x7b))] = 60;
            MAP[unchecked((byte)unchecked((int)0x7c))] = 61;
            MAP[unchecked((byte)unchecked((int)0x7d))] = 62;
            MAP[unchecked((byte)unchecked((int)0x7e))] = 63;
            MAP[unchecked((byte)unchecked((int)0x7f))] = 64;
            MAP[unchecked((byte)unchecked((int)0x80))] = 65;
            MAP[unchecked((byte)unchecked((int)0x81))] = 66;
            MAP[unchecked((byte)unchecked((int)0x82))] = 67;
            MAP[unchecked((byte)unchecked((int)0x83))] = 68;
            MAP[unchecked((byte)unchecked((int)0x84))] = 69;
            MAP[unchecked((byte)unchecked((int)0x85))] = 70;
            MAP[unchecked((byte)unchecked((int)0x86))] = 71;
            MAP[unchecked((byte)unchecked((int)0x87))] = 72;
            MAP[unchecked((byte)unchecked((int)0x88))] = 73;
            MAP[unchecked((byte)unchecked((int)0x89))] = 74;
            MAP[unchecked((byte)unchecked((int)0x8a))] = 75;
            MAP[unchecked((byte)unchecked((int)0x8b))] = 76;
            MAP[unchecked((byte)unchecked((int)0x8c))] = 77;
            MAP[unchecked((byte)unchecked((int)0x8d))] = 78;
            MAP[unchecked((byte)unchecked((int)0x8e))] = 79;
            MAP[unchecked((byte)unchecked((int)0x8f))] = 80;
            MAP[unchecked((byte)unchecked((int)0x90))] = 81;
            MAP[unchecked((byte)unchecked((int)0x91))] = 82;
            MAP[unchecked((byte)unchecked((int)0x92))] = 83;
            MAP[unchecked((byte)unchecked((int)0x93))] = 84;
            MAP[unchecked((byte)unchecked((int)0x94))] = 85;
            MAP[unchecked((byte)unchecked((int)0x95))] = 86;
            MAP[unchecked((byte)unchecked((int)0x96))] = 87;
            MAP[unchecked((byte)unchecked((int)0x97))] = 88;
            MAP[unchecked((byte)unchecked((int)0x98))] = 89;
            MAP[unchecked((byte)unchecked((int)0x99))] = 90;
            MAP[unchecked((byte)unchecked((int)0x9a))] = 91;
            MAP[unchecked((byte)unchecked((int)0x9b))] = 92;
            MAP[unchecked((byte)unchecked((int)0x9c))] = 93;
            MAP[unchecked((byte)unchecked((int)0x9d))] = 94;
            MAP[unchecked((byte)unchecked((int)0x9e))] = 95;
            MAP[unchecked((byte)unchecked((int)0x9f))] = 96;
            MAP[unchecked((byte)unchecked((int)0xa0))] = 97;
            MAP[unchecked((byte)unchecked((int)0xa1))] = 98;
            MAP[unchecked((byte)unchecked((int)0xa2))] = 99;
            MAP[unchecked((byte)unchecked((int)0xa3))] = 100;
            MAP[unchecked((byte)unchecked((int)0xa4))] = 101;
            MAP[unchecked((byte)unchecked((int)0xa5))] = 102;
            MAP[unchecked((byte)unchecked((int)0xa6))] = 103;
            MAP[unchecked((byte)unchecked((int)0xa7))] = 104;
            MAP[unchecked((byte)unchecked((int)0xa8))] = 105;
            MAP[unchecked((byte)unchecked((int)0xa9))] = 106;
            MAP[unchecked((byte)unchecked((int)0xaa))] = 107;
            MAP[unchecked((byte)unchecked((int)0xab))] = 108;
            MAP[unchecked((byte)unchecked((int)0xac))] = 109;
            MAP[unchecked((byte)unchecked((int)0xad))] = 110;
            MAP[unchecked((byte)unchecked((int)0xae))] = 111;
            MAP[unchecked((byte)unchecked((int)0xaf))] = 112;
            MAP[unchecked((byte)unchecked((int)0xb0))] = 113;
            MAP[unchecked((byte)unchecked((int)0xb1))] = 114;
            MAP[unchecked((byte)unchecked((int)0xb2))] = 115;
            MAP[unchecked((byte)unchecked((int)0xb3))] = 116;
            MAP[unchecked((byte)unchecked((int)0xb4))] = 117;
            MAP[unchecked((byte)unchecked((int)0xb5))] = 118;
            MAP[unchecked((byte)unchecked((int)0xb6))] = 119;
            MAP[unchecked((byte)unchecked((int)0xb7))] = 120;
            MAP[unchecked((byte)unchecked((int)0xb8))] = 121;
            MAP[unchecked((byte)unchecked((int)0xb9))] = 122;
            MAP[unchecked((byte)unchecked((int)0xba))] = 123;
            MAP[unchecked((byte)unchecked((int)0xbb))] = 124;
            MAP[unchecked((byte)unchecked((int)0xbc))] = 125;
            MAP[unchecked((byte)unchecked((int)0xbd))] = 126;
            MAP[unchecked((byte)unchecked((int)0xbe))] = 127;
            MAP[unchecked((byte)unchecked((int)0xbf))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc0))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc1))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc2))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc3))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc4))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc5))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc6))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc7))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc8))] = 0;
            MAP[unchecked((byte)unchecked((int)0xc9))] = 0;
            MAP[unchecked((byte)unchecked((int)0xca))] = 0;
            MAP[unchecked((byte)unchecked((int)0xcb))] = 0;
            MAP[unchecked((byte)unchecked((int)0xcc))] = 0;
            MAP[unchecked((byte)unchecked((int)0xcd))] = 0;
            MAP[unchecked((byte)unchecked((int)0xce))] = 0;
            MAP[unchecked((byte)unchecked((int)0xcf))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd0))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd1))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd2))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd3))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd4))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd5))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd6))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd7))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd8))] = 0;
            MAP[unchecked((byte)unchecked((int)0xd9))] = 0;
            MAP[unchecked((byte)unchecked((int)0xda))] = 0;
            MAP[unchecked((byte)unchecked((int)0xdb))] = 0;
            MAP[unchecked((byte)unchecked((int)0xdc))] = 0;
            MAP[unchecked((byte)unchecked((int)0xdd))] = 0;
            MAP[unchecked((byte)unchecked((int)0xde))] = 0;
            MAP[unchecked((byte)unchecked((int)0xdf))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe0))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe1))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe2))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe3))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe4))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe5))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe6))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe7))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe8))] = 0;
            MAP[unchecked((byte)unchecked((int)0xe9))] = 0;
            MAP[unchecked((byte)unchecked((int)0xea))] = 0;
            MAP[unchecked((byte)unchecked((int)0xeb))] = 0;
            MAP[unchecked((byte)unchecked((int)0xec))] = 0;
            MAP[unchecked((byte)unchecked((int)0xed))] = 0;
            MAP[unchecked((byte)unchecked((int)0xee))] = 0;
            MAP[unchecked((byte)unchecked((int)0xef))] = 0;
            MAP[unchecked((byte)unchecked((int)0xf0))] = 2;
            MAP[unchecked((byte)unchecked((int)0xf1))] = 3;
            MAP[unchecked((byte)unchecked((int)0xf2))] = 5;
            MAP[unchecked((byte)unchecked((int)0xf3))] = 9;
            MAP[unchecked((byte)unchecked((int)0xf4))] = 0;
            MAP[unchecked((byte)unchecked((int)0xf5))] = 0;
            MAP[unchecked((byte)unchecked((int)0xf6))] = 0;
            MAP[unchecked((byte)unchecked((int)0xf7))] = 0;
            MAP[unchecked((byte)unchecked((int)0xf8))] = 0;
            MAP[unchecked((byte)unchecked((int)0xf9))] = 0;
            MAP[unchecked((byte)unchecked((int)0xfa))] = 0;
            MAP[unchecked((byte)unchecked((int)0xfb))] = 0;
            MAP[unchecked((byte)unchecked((int)0xfc))] = 0;
            MAP[unchecked((byte)unchecked((int)0xfd))] = 0;
            MAP[unchecked((byte)unchecked((int)0xfe))] = 0;
            MAP[unchecked((byte)unchecked((int)0xff))] = 0;
        }

        private ValueLengthUtil()
            : base()
        {
        }

        public static int get(byte key)
        {
            return MAP[key];
        }
    }
}