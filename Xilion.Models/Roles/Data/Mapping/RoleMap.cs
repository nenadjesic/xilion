﻿using Lucene.Net.Util;
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
            HasManyToMany(x => x.Users)
                    .Cascade.All()
                    .Table(TableNameConvention.Prefix + "UsersInRoles");
        }
    }
}
