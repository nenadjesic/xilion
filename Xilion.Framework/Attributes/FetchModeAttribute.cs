using System;
using Xilion.Framework.Enums;

namespace Xilion.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class FetchModeAttribute : Attribute
    {
        public FetchModeAttribute(FetchMode mode)
        {
            Mode = mode;
        }

        public FetchMode Mode { get; private set; }
    }
}
