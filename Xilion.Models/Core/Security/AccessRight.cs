using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Xilion.Framework;

namespace Xilion.Models.Core.Security
{
    [DataContract]
    public class AccessRight : Enumeration
    {
        public static readonly AccessRight View = new AccessRight(0, "View");
        public static readonly AccessRight Modify = new AccessRight(1, "Modify");
        public static readonly AccessRight Create = new AccessRight(2, "Create");
        public static readonly AccessRight Delete = new AccessRight(3, "Delete");
        public static readonly AccessRight Permissions = new AccessRight(100, "Permissions");

        public AccessRight(int value, string displayName) : base(value, displayName)
        {
        }

        public static AccessRight FromAccessRightName<T>(string displayName) where T : AccessRight
        {
            return GetAllAccessRights<T>().FirstOrDefault(x => x.DisplayName.ToLower() == displayName.ToLower());
        }

        public static AccessRight FromAccessRightValue<T>(int value) where T : AccessRight
        {
            return GetAllAccessRights<T>().FirstOrDefault(x => x.Value == value);
        }

        public static IEnumerable<AccessRight> GetAllAccessRights<T>() where T : AccessRight
        {
            return typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Select(info => info.GetValue(null))
                .Cast<AccessRight>()
                .OrderBy(x => x.Value);
        }
    }
}