using System.Collections.Generic;
using System.Linq;

namespace Xilion.Models.Core.Security
{
    public class AccessPermissionBuilder
    {
        private readonly IList<AccessPermission> _permissions;
        private readonly AccessRight _right;
        private readonly Access _access;

        internal AccessPermissionBuilder(IList<AccessPermission> permissions, AccessRight right, Access access)
        {
            _permissions = permissions;
            _right = right;
            _access = access;
        }

        public AccessPermissionBuilder To(params string[] roles)
        {
            foreach (var role in roles)
            {
                var permission = _permissions
                    .SingleOrDefault(x => x.AccessRight == _right.Value && x.Role == role);

                if (permission == null)
                {
                    permission = new AccessPermission{AccessRight = _right.Value, Role = role};
                    _permissions.Add(permission);
                }

                permission.Access = _access;
            }

            return this;
        }
    }
}