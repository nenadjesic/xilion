using System;

namespace Xilion.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string Name { get; set; }

        public bool View { get; set; }
    }
}
