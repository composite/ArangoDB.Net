using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocyPack.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class SerializedNameAttribute : Attribute
    {
        public SerializedNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
