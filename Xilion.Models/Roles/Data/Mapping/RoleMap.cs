using Lucene.Net.Util;
using Xilion.Models.Core.Data.Mappings;
using Xilion.Framework.Data.Mappings.Conventions;
using Xilion.Models;

namespace Xilion.Models.Roles.Data.Mapping
{
    public class RoleMap : CmsEntityMap<Role>
    {
        public RoleMap()
        {
            Map(x => x.RoleName);
            Map(x => x.Status);
            References(x => x.Id)
                .ForeignKey("FK_Role_UserInRole")
                .Nullable()
                .Cascade.SaveUpdate()
                .Column("RoleId");
        }
    }
}
