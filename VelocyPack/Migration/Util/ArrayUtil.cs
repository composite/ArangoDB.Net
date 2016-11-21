using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocyPack.Migration.Util
{
    public static class ArrayUtil
    {
        public static int ByteArrayHashCode(this byte[] bytes)
        {
            unchecked
            {
                var result = 0;
                foreach (byte b in bytes)
                    result = (result * 31) ^ b;
                return result;
            }
        }

        public static T[] CopyOfRange<T>(this T[] src, int start, int end)
        {
            int len = end - start;
            T[] dest = new T[len];
            // note i is always from 0
            for (int i = 0; i < len; i++)
            {
                dest[i] = src[start + i]; // so 0..n = 0+x..n+x
            }
            return dest;
        }
    }
}
