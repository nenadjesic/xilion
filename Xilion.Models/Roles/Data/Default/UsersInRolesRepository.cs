using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Roles.Data.Default
{
    public class UsersInRolesRepository : Repository<UsersInRoles>, IUsersInRolesRepository
    {
        public UsersInRolesRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}
