using Xilion.Framework;

namespace Xilion.Models.Site
{
    public class PageType : Enumeration
    {
        public static readonly PageType Normal = new PageType(1, "Normal");
        public static readonly PageType Group = new PageType(2, "Group");
        public static readonly PageType External = new PageType(3, "External");
        public static readonly PageType InternalRedirect = new PageType(4, "InternalRedirect");

        public PageType(int value, string displayName)
            : base(value, displayName)
        {
        }
    }
}