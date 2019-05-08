using System.Collections.Generic;
using Xilion.Models;
using Xilion.ViewModels;

namespace Xilion.Interface
{
    public interface IUsersInRoles
    {
        bool AssignRole(UsersInRoles UsersInRoles);
        bool CheckRoleExists(UsersInRoles UsersInRoles);
        bool RemoveRole(UsersInRoles UsersInRoles);
        List<AssignRolesViewModel> GetAssignRoles();
    }
}