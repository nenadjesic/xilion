using Lucene.Net.Util;
using Xilion.Models.Core.Data.Mappings;
using Xilion.Framework.Data.Mappings.Conventions;
using Xilion.Models;

namespace Xilion.Models.Roles.Data.Mapping
{
    public class UsersInRolesMap : CmsEntityMap<UsersInRoles>
    {
        public UsersInRolesMap()
        {
            Map(x => x.UserRoleId).Generated.Insert();
            Map(x => x.UserId);
            Map(x => x.RoleId);
        }
    }
}
