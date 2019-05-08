namespace Xilion.Models.Core.Security
{
    public class SecurityAccessRight : AccessRight
    {
        public static readonly AccessRight SetAccessRight = new AccessRight(200, "SetAccessRight");

        public SecurityAccessRight(int value, string displayName) : base(value, displayName)
        {
        }
    }
}