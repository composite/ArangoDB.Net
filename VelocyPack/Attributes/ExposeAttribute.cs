using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocyPack.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class ExposeAttribute : Attribute
    {
        public ExposeAttribute()
        {
            Serialize = true;
            Deserialize = true;
        }

        public bool Serialize { get; set; }
        public bool Deserialize { get; set; }
    }
}
