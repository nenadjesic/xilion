using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xilion.Interface;
using Xilion.Models;
using Xilion.Models.Roles.Core;
using Xilion.Models.User.Core;
using Xilion.ViewModels;

namespace Xilion.Concrete
{
    public class UsersInRolesConcrete 
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        public UsersInRolesConcrete(DatabaseContext context, IConfiguration config,IRoleService roleService, IUserService userService)
        {
            _configuration = config;
            _roleService = roleService;
            _userService = userService;
        }

        public bool AssignRole(UsersInRoles usersInRoles)
        {
            _roleService.SaveUserInRole(usersInRoles);
            return true;
        }

        public bool CheckRoleExists(UsersInRoles usersInRoles)
        {
            var result = (from userrole in _roleService.GetUserRoles()
                          where userrole.UserId == usersInRoles.UserId && userrole.RoleId == usersInRoles.RoleId
                          select userrole).Count();

            return result > 0 ? true : false;
        }

        public bool RemoveRole(UsersInRoles usersInRoles)
        {
            var role = (from userrole in _roleService.GetUserRoles()
                        where userrole.UserId == usersInRoles.UserId && userrole.RoleId == usersInRoles.RoleId
                        select userrole).FirstOrDefault();
            if (role != null)
            {
                _roleService.DeleteUsersInRole(role);
                return true;
                
            }
            else
            {
                return false;
            }
        }

        public List<AssignRolesViewModel> GetAssignRoles()
        {
            var result = (from usertb in _roleService.GetUserRoles()
                          join role in _roleService.GetAll() on usertb.RoleId equals role.Id
                          join user in _userService.GetAll() on usertb.UserId equals user.Id
                          select new AssignRolesViewModel()
                          {
                              RoleName = role.RoleName,
                              RoleId = usertb.RoleId,
                              UserName = role.RoleName,
                              UserId = usertb.UserId,
                              UserRoleId = usertb.Id

                          }).ToList();

            return result;
        }
    }
}
