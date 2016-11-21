using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArangoDB.Util
{
    using System.Reflection;

    public static class MigrationUtil
    {
        public static TValue RemoveAndGet<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key)
        {
            TValue result = default(TValue);
            if (dic.ContainsKey(key)) result = dic[key];
            dic.Remove(key);
            return result;
        }

        public static bool IsCastableTo(this Type from, Type to, bool implicitly = false)
        {
            return to.IsAssignableFrom(from) || from.HasCastDefined(to, implicitly);
        }

        static bool HasCastDefined(this Type from, Type to, bool implicitly)
        {
            TypeInfo ifrom = from.GetTypeInfo(), ito = to.GetTypeInfo();
            if ((ifrom.IsPrimitive || ifrom.IsEnum) && (ito.IsPrimitive || ito.IsEnum))
            {
                if (!implicitly)
                    return from == to || (from != typeof(Boolean) && to != typeof(Boolean));

                Type[][] typeHierarchy = {
            new Type[] { typeof(Byte),  typeof(SByte), typeof(Char) },
            new Type[] { typeof(Int16), typeof(UInt16) },
            new Type[] { typeof(Int32), typeof(UInt32) },
            new Type[] { typeof(Int64), typeof(UInt64) },
            new Type[] { typeof(Single) },
            new Type[] { typeof(Double) }
        };
                IEnumerable<Type> lowerTypes = Enumerable.Empty<Type>();
                foreach (Type[] types in typeHierarchy)
                {
                    if (types.Any(t => t == to))
                        return lowerTypes.Any(t => t == from);
                    lowerTypes = lowerTypes.Concat(types);
                }

                return false;   // IntPtr, UIntPtr, Enum, Boolean
            }
            return IsCastDefined(to, m => m.GetParameters()[0].ParameterType, _ => from, implicitly, false)
                || IsCastDefined(from, _ => to, m => m.ReturnType, implicitly, true);
        }

        static bool IsCastDefined(Type type, Func<MethodInfo, Type> baseType,
                                Func<MethodInfo, Type> derivedType, bool implicitly, bool lookInBase)
        {
            var bindinFlags = BindingFlags.Public | BindingFlags.Static
                            | (lookInBase ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);
            return type.GetMethods(bindinFlags).Any(
                m => (m.Name == "op_Implicit" || (!implicitly && m.Name == "op_Explicit"))
                    && baseType(m).IsAssignableFrom(derivedType(m)));
        }

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentUnixTimestampMillis()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public static DateTime DateTimeFromUnixTimestampMillis(long millis)
        {
            return UnixEpoch.AddMilliseconds(millis);
        }

        public static long GetCurrentUnixTimestampSeconds()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
        }

        public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
        {
            return UnixEpoch.AddSeconds(seconds);
        }

        public static long GetUnixTimestampSeconds(this DateTime dt)
        {
            return (long)(dt.ToUniversalTime() - UnixEpoch).TotalSeconds;
        }

        public static long GetUnixTimestampMillis(this DateTime dt)
        {
            return (long)(dt.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
        }
    }
}
