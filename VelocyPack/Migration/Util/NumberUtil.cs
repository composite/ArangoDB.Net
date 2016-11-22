using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocyPack.Migration.Util
{
    using System.Numerics;

    public static class NumberUtil
    {
        private static readonly BigInteger MaxUInt64AsBigInteger = ulong.MaxValue;

        public static long ToLongUnchecked(this BigInteger bi)
        {
            unchecked
            {
                return (long)(ulong)(bi & MaxUInt64AsBigInteger);
            }
        }
    }
}
