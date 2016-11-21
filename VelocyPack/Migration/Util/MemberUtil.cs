using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocyPack.Migration.Util
{
    using System.Reflection;

    internal static class MemberUtil
    {
        public static bool IsAccessibleProperty(this PropertyInfo property, bool readCheck, bool writeCheck)
        {
            bool result = true;
            MethodInfo tmp;
            if (readCheck)
            {
                tmp = property.GetMethod;
                result &= property.CanRead && tmp.IsPublic && !tmp.IsStatic;
            }
            if (writeCheck)
            {
                tmp = property.SetMethod;
                result &= property.CanWrite && tmp.IsPublic && !tmp.IsStatic;
            }

            return result;
        }

        public static bool IsAccessibleField(this FieldInfo field)
        {
            return !field.IsStatic && !field.IsLiteral && !field.IsInitOnly;
        }
    }
}
