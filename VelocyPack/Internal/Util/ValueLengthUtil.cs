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
    using System.Collections.Generic;

    /// <author>Mark - mark at arangodb.com</author>
    public static class ValueLengthUtil
    {
        private const int DOUBLE_BYTES = 8;

        private const int LONG_BYTES = 8;

        private const int CHARACTER_BYTES = 2;

        private static readonly IDictionary<byte, int> MAP;

        static ValueLengthUtil()
        {
            MAP = new Dictionary<byte, int>();
            MAP[0x00] = 1;
            MAP[0x01] = 1;
            MAP[0x02] = 0;
            MAP[0x03] = 0;
            MAP[0x04] = 0;
            MAP[0x05] = 0;
            MAP[0x06] = 0;
            MAP[0x07] = 0;
            MAP[0x08] = 0;
            MAP[0x09] = 0;
            MAP[0x0a] = 1;
            MAP[0x0b] = 0;
            MAP[0x0c] = 0;
            MAP[0x0d] = 0;
            MAP[0x0e] = 0;
            MAP[0x0f] = 0;
            MAP[0x10] = 0;
            MAP[0x11] = 0;
            MAP[0x12] = 0;
            MAP[0x13] = 0;
            MAP[0x14] = 0;
            MAP[0x15] = 0;
            MAP[0x16] = 0;
            MAP[0x17] = 1;
            MAP[0x18] = 1;
            MAP[0x19] = 1;
            MAP[0x1a] = 1;
            MAP[0x1b] = 1 + DOUBLE_BYTES;
            MAP[0x1c] = 1 + LONG_BYTES;
            MAP[0x1d] = 1 + CHARACTER_BYTES;
            MAP[0x1e] = 1;
            MAP[0x1f] = 1;
            MAP[0x20] = 2;
            MAP[0x21] = 3;
            MAP[0x22] = 4;
            MAP[0x23] = 5;
            MAP[0x24] = 6;
            MAP[0x25] = 7;
            MAP[0x26] = 8;
            MAP[0x27] = 9;
            MAP[0x28] = 2;
            MAP[0x29] = 3;
            MAP[0x2a] = 4;
            MAP[0x2b] = 5;
            MAP[0x2c] = 6;
            MAP[0x2d] = 7;
            MAP[0x2e] = 8;
            MAP[0x2f] = 9;
            MAP[0x30] = 1;
            MAP[0x31] = 1;
            MAP[0x32] = 1;
            MAP[0x33] = 1;
            MAP[0x34] = 1;
            MAP[0x35] = 1;
            MAP[0x36] = 1;
            MAP[0x37] = 1;
            MAP[0x38] = 1;
            MAP[0x39] = 1;
            MAP[0x3a] = 1;
            MAP[0x3b] = 1;
            MAP[0x3c] = 1;
            MAP[0x3d] = 1;
            MAP[0x3e] = 1;
            MAP[0x3f] = 1;
            MAP[0x40] = 1;
            MAP[0x41] = 2;
            MAP[0x42] = 3;
            MAP[0x43] = 4;
            MAP[0x44] = 5;
            MAP[0x45] = 6;
            MAP[0x46] = 7;
            MAP[0x47] = 8;
            MAP[0x48] = 9;
            MAP[0x49] = 10;
            MAP[0x4a] = 11;
            MAP[0x4b] = 12;
            MAP[0x4c] = 13;
            MAP[0x4d] = 14;
            MAP[0x4e] = 15;
            MAP[0x4f] = 16;
            MAP[0x50] = 17;
            MAP[0x51] = 18;
            MAP[0x52] = 19;
            MAP[0x53] = 20;
            MAP[0x54] = 21;
            MAP[0x55] = 22;
            MAP[0x56] = 23;
            MAP[0x57] = 24;
            MAP[0x58] = 25;
            MAP[0x59] = 26;
            MAP[0x5a] = 27;
            MAP[0x5b] = 28;
            MAP[0x5c] = 29;
            MAP[0x5d] = 30;
            MAP[0x5e] = 31;
            MAP[0x5f] = 32;
            MAP[0x60] = 33;
            MAP[0x61] = 34;
            MAP[0x62] = 35;
            MAP[0x63] = 36;
            MAP[0x64] = 37;
            MAP[0x65] = 38;
            MAP[0x66] = 39;
            MAP[0x67] = 40;
            MAP[0x68] = 41;
            MAP[0x69] = 42;
            MAP[0x6a] = 43;
            MAP[0x6b] = 44;
            MAP[0x6c] = 45;
            MAP[0x6d] = 46;
            MAP[0x6e] = 47;
            MAP[0x6f] = 48;
            MAP[0x70] = 49;
            MAP[0x71] = 50;
            MAP[0x72] = 51;
            MAP[0x73] = 52;
            MAP[0x74] = 53;
            MAP[0x75] = 54;
            MAP[0x76] = 55;
            MAP[0x77] = 56;
            MAP[0x78] = 57;
            MAP[0x79] = 58;
            MAP[0x7a] = 59;
            MAP[0x7b] = 60;
            MAP[0x7c] = 61;
            MAP[0x7d] = 62;
            MAP[0x7e] = 63;
            MAP[0x7f] = 64;
            MAP[0x80] = 65;
            MAP[0x81] = 66;
            MAP[0x82] = 67;
            MAP[0x83] = 68;
            MAP[0x84] = 69;
            MAP[0x85] = 70;
            MAP[0x86] = 71;
            MAP[0x87] = 72;
            MAP[0x88] = 73;
            MAP[0x89] = 74;
            MAP[0x8a] = 75;
            MAP[0x8b] = 76;
            MAP[0x8c] = 77;
            MAP[0x8d] = 78;
            MAP[0x8e] = 79;
            MAP[0x8f] = 80;
            MAP[0x90] = 81;
            MAP[0x91] = 82;
            MAP[0x92] = 83;
            MAP[0x93] = 84;
            MAP[0x94] = 85;
            MAP[0x95] = 86;
            MAP[0x96] = 87;
            MAP[0x97] = 88;
            MAP[0x98] = 89;
            MAP[0x99] = 90;
            MAP[0x9a] = 91;
            MAP[0x9b] = 92;
            MAP[0x9c] = 93;
            MAP[0x9d] = 94;
            MAP[0x9e] = 95;
            MAP[0x9f] = 96;
            MAP[0xa0] = 97;
            MAP[0xa1] = 98;
            MAP[0xa2] = 99;
            MAP[0xa3] = 100;
            MAP[0xa4] = 101;
            MAP[0xa5] = 102;
            MAP[0xa6] = 103;
            MAP[0xa7] = 104;
            MAP[0xa8] = 105;
            MAP[0xa9] = 106;
            MAP[0xaa] = 107;
            MAP[0xab] = 108;
            MAP[0xac] = 109;
            MAP[0xad] = 110;
            MAP[0xae] = 111;
            MAP[0xaf] = 112;
            MAP[0xb0] = 113;
            MAP[0xb1] = 114;
            MAP[0xb2] = 115;
            MAP[0xb3] = 116;
            MAP[0xb4] = 117;
            MAP[0xb5] = 118;
            MAP[0xb6] = 119;
            MAP[0xb7] = 120;
            MAP[0xb8] = 121;
            MAP[0xb9] = 122;
            MAP[0xba] = 123;
            MAP[0xbb] = 124;
            MAP[0xbc] = 125;
            MAP[0xbd] = 126;
            MAP[0xbe] = 127;
            MAP[0xbf] = 0;
            MAP[0xc0] = 0;
            MAP[0xc1] = 0;
            MAP[0xc2] = 0;
            MAP[0xc3] = 0;
            MAP[0xc4] = 0;
            MAP[0xc5] = 0;
            MAP[0xc6] = 0;
            MAP[0xc7] = 0;
            MAP[0xc8] = 0;
            MAP[0xc9] = 0;
            MAP[0xca] = 0;
            MAP[0xcb] = 0;
            MAP[0xcc] = 0;
            MAP[0xcd] = 0;
            MAP[0xce] = 0;
            MAP[0xcf] = 0;
            MAP[0xd0] = 0;
            MAP[0xd1] = 0;
            MAP[0xd2] = 0;
            MAP[0xd3] = 0;
            MAP[0xd4] = 0;
            MAP[0xd5] = 0;
            MAP[0xd6] = 0;
            MAP[0xd7] = 0;
            MAP[0xd8] = 0;
            MAP[0xd9] = 0;
            MAP[0xda] = 0;
            MAP[0xdb] = 0;
            MAP[0xdc] = 0;
            MAP[0xdd] = 0;
            MAP[0xde] = 0;
            MAP[0xdf] = 0;
            MAP[0xe0] = 0;
            MAP[0xe1] = 0;
            MAP[0xe2] = 0;
            MAP[0xe3] = 0;
            MAP[0xe4] = 0;
            MAP[0xe5] = 0;
            MAP[0xe6] = 0;
            MAP[0xe7] = 0;
            MAP[0xe8] = 0;
            MAP[0xe9] = 0;
            MAP[0xea] = 0;
            MAP[0xeb] = 0;
            MAP[0xec] = 0;
            MAP[0xed] = 0;
            MAP[0xee] = 0;
            MAP[0xef] = 0;
            MAP[0xf0] = 2;
            MAP[0xf1] = 3;
            MAP[0xf2] = 5;
            MAP[0xf3] = 9;
            MAP[0xf4] = 0;
            MAP[0xf5] = 0;
            MAP[0xf6] = 0;
            MAP[0xf7] = 0;
            MAP[0xf8] = 0;
            MAP[0xf9] = 0;
            MAP[0xfa] = 0;
            MAP[0xfb] = 0;
            MAP[0xfc] = 0;
            MAP[0xfd] = 0;
            MAP[0xfe] = 0;
            MAP[0xff] = 0;
        }

        public static int Get(byte key)
        {
            return MAP[key];
        }
    }
}