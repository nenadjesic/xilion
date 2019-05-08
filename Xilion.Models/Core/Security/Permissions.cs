using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Framework.Serialization;

namespace Xilion.Models.Core.Security
{
    [Serializable]
    public class Permissions
    {
        public Permissions()
        {
            AccessPermissions = new List<AccessPermission>();
        }

        public IList<AccessPermission> AccessPermissions { get; private set; }

        public AccessPermissionBuilder Allow(AccessRight right)
        {
            return new AccessPermissionBuilder(AccessPermissions, right, Access.Allow);
        }

        public AccessPermissionBuilder Deny(AccessRight right)
        {
            return new AccessPermissionBuilder(AccessPermissions, right, Access.Deny);
        }

        public bool IsDefined(AccessRight right, string role)
        {
            return AccessPermissions.Any(x => x.AccessRight == right.Value && x.Role == role);
            //return (AccessPermissions.Count(x => x.AccessRight == right.Value && x.Role == role) == 0);
        }

        internal Access GetAccess(AccessRight right, string role)
        {
            AccessPermission permission =
                //AccessPermissions.SingleOrDefault(x => x.AccessRight == right.Value && x.Role == role) ??
                AccessPermissions
                    .Where(x => x.AccessRight >= right.Value && x.Access != Access.Inherit && x.Role == role)
                    .OrderBy(x => x.AccessRight)
                    .FirstOrDefault();

            return permission == null ? Access.Inherit : permission.Access;
        }

        /// <summary>
        /// Removes all access permissions, efectively making the permissions inherited from parent.
        /// </summary>
        public void Inherit()
        {
            AccessPermissions.Clear();
        }

        /// <summary>
        /// Removes all access permissions for the given role, efectively making the permissions inherited from parent.
        /// </summary>
        /// <param name="role">Role to remove access permissions for.</param>
        public void InheritFor(string role)
        {
            for (int i = AccessPermissions.Count - 1; i >= 0; i--)
            {
                if (AccessPermissions[i].Role == role)
                    AccessPermissions.RemoveAt(i);
            }
        }

        public static Permissions Deserialize(string serializedPermissions)
        {
            return new Permissions
                       {
                           AccessPermissions = String.IsNullOrWhiteSpace(serializedPermissions)
                                                   ? new List<AccessPermission>()
                                                   : Serializer.Default()
                                                         .Deserialize<IList<AccessPermission>>(serializedPermissions)
                       };
        }

        public string Serialize()
        {
            return Serializer.Default().Serialize<IList<AccessPermission>>(AccessPermissions);
        }
    }
}