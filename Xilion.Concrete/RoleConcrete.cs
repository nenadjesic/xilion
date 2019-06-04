using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Xilion.Interface;
using Xilion.Models;
using Xilion.Models.Roles.Core;

namespace Xilion.Concrete
{
    public class RoleConcrete
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;

        public RoleSettings Settings => throw new NotImplementedException();

        public RoleConcrete(IConfiguration configuration, IRoleService roleService)
        {

            _configuration = configuration;
            _roleService = roleService;
        }

        public bool CheckRoleExits(string roleName)
        {
            var result = (from role in _roleService.CheckRoleExits(roleName)
                          where role.RoleName == roleName
                          select role).Count();

            return result > 0 ? true : false;
        }

        public bool DeleteRole(Role role)
        {
            var roleData = _roleService.CheckRoleExits(role.RoleName);

            if (roleData == null)
            {
                _roleService.DeleteRole(role);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Role GetRolebyId(int roleId)
        {
            var result = _roleService.GetById(roleId);
            return result;
        }

        public IList<Role> GetAllRole()
        {
            var result = _roleService.GetAll();
            return result;
        }

        public void InsertRole(Role role)
        {
            _roleService.Save(role);
        }

        public bool UpdateRole(Role role)
        {
            _roleService.Save(role);
            return true;
        }

        public void Delete(Role role)
        {
            _roleService.DeleteRole(role);
        }

        public Role GetById(int roleId)
        {
            return _roleService.GetById(roleId);
        }


        public IList<Role> GetRoles()
        {
            return _roleService.GetAll();
        }

    }
}
