using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Roles.Data.Default
{
    public class RoleRepository: Repository<Role>, IRoleRepository
    {
        public RoleRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}
