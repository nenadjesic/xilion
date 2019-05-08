using Xilion.Framework;

namespace Xilion.Models.Classifications
{
    public class ClassificationType : Enumeration
    {
        public static readonly ClassificationType Flat = new ClassificationType(0, "Flat");
        public static readonly ClassificationType Hierarchy = new ClassificationType(1, "Hierarchy");

        public ClassificationType(int value, string name, string displayName)
            : base(value, name, displayName)
        {
        }

        public ClassificationType(int value, string name)
            : base(value, name)
        {
        }
    }
}