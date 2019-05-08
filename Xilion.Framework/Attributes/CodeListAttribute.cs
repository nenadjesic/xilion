using System;

namespace Xilion.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CodeListAttribute : Attribute
    {
        public int CodeLength { get; set; }

        public CodeListAttribute()
        {
            CodeLength = 20;
        }
    }
}
