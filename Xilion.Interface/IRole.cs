using System.Collections.Generic;
using Xilion.Models;

namespace Xilion.Interface
{
    public interface IRole
    {
        void InsertRole(Role role);
        bool CheckRoleExits(string roleName);
        Role GetRolebyId(int roleId);
        bool DeleteRole(int roleId);
        bool UpdateRole(Role role);
        List<Role> GetAllRole();
    }
}