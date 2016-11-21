using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocyPack.Migration.Util
{
    using System.Reflection;

    public static class TypeUtil
    {
        public static Type GetTargetType(this Type type, Type target)
        {
            if(type == null) throw new ArgumentNullException("type");
            if(target == null) throw new ArgumentNullException("target");

            if (type.TypeEquals(target)) return target;

            TypeInfo i1 = type.GetTypeInfo(), i2 = target.GetTypeInfo();

            if(i1.IsInterface && !i2.IsInterface) throw new ArgumentException(string.Format("interface '{0}' cannot scanning class type '{1}'.", type, target));

            if (i2.IsInterface)
            {
                return i1.ImplementedInterfaces.FirstOrDefault(t => t.TypeEquals(target));
            }
            else
            {
                Type tmp = i1.BaseType;
                while (tmp != typeof(object))
                {
                    if (tmp.TypeEquals(target)) return tmp;
                    tmp = tmp.GetTypeInfo().BaseType;
                }
            }

            return null;
        }

        public static bool TypeEquals(this Type type1, Type type2)
        {
            if (type1 == null || type2 == null) return false;

            if (type1 == type2) return true;

            if (type1.IsGenericParameter)
            {
                if (type1.GetGenericTypeDefinition() == type2) return true;
                if (type1 == type2.GetGenericTypeDefinition()) return true;
            }

            return false;
        }
    }
}
