using System.Collections.Generic;
using System.Linq;

namespace Xilion.Models.Roles.Core
{
    public interface IRoleService
    {
        RoleSettings Settings { get; }

        IQueryable<Role> CheckRoleExits(string roleName);
        bool DeleteRole(Role entity);
        //void DeleteUsersInRole(UsersInRoles userInRole);
        IList<Role> GetAll();
        Role GetById(int roleId);
        IQueryable<Role> GetByLabel();
        //IList<UsersInRoles> GetUserRoles();
        void Save(Role Role);
        //void SaveUserInRole(UsersInRoles userInRole);
    }
}