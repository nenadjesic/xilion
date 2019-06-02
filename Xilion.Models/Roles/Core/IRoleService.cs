using System.Collections.Generic;
using System.Linq;

namespace Xilion.Models.Roles.Core
{
    public interface IRoleService
    {
        RoleSettings Settings { get; }

        IQueryable<Role> CheckRoleExits(string roleName);
        bool DeleteRole(Role entity);
        Role GetById(int roleId);
        IQueryable<Role> GetByLabel();
        List<Role> GetRoles();
        List<UsersInRoles> GetUsersInRole();
        IQueryable<UsersInRoles> GetUsersRole();
        void Save(Role Role);
    }
}