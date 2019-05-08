using System.Linq;
using System.Security.Principal;
using Xilion.Models.Core.Domain;
using Xilion.Models.Core.Security;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Core.Applications
{
    public abstract class SecuredApplication : Application, ISecured
    {
        private Permissions _permissions;

        public virtual Permissions Permissions
        {
            get
            {
                if (_permissions == null)
                {
                    _permissions = GetSettings().Permissions;

                    if (_permissions.AccessPermissions.Count(x => x.Role == UsersRoles.Guest) == 0)
                    {
                        _permissions.Deny(AccessRight.View).To(UsersRoles.Guest);
                        _permissions.Deny(AccessRight.Permissions).To(UsersRoles.Guest);
                        _permissions.Deny(AccessRight.Create).To(UsersRoles.Guest);
                        _permissions.Deny(AccessRight.Modify).To(UsersRoles.Guest);
                        _permissions.Deny(AccessRight.Delete).To(UsersRoles.Guest);
                    }

                    if (_permissions.GetAccess(AccessRight.View, UsersRoles.Guest) == Access.Inherit)
                        _permissions.Allow(AccessRight.View).To(UsersRoles.Guest);
                }
                return _permissions;
            }
            set
            {
                ApplicationSettings settings = GetSettings();
                settings.Permissions = value;
                settings.Save();
            }
        }

    }
}