using System.Runtime.Serialization;
using Xilion.Framework;

namespace Xilion.Models.Core.Security
{
    [DataContract]
    public class Access : Enumeration
    {
        public static readonly Access Inherit = new Access(0, "Inherit");
        public static readonly Access Allow = new Access(1, "Allow");
        public static readonly Access Deny = new Access(2, "Deny");

        public Access(int value, string displayName) : base(value, displayName)
        {
        }
    }
}