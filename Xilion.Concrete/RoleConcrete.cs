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
    public class RoleConcrete : IRoleService
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

        public List<Role> GetAllRole()
        {
            var result = _roleService.GetRoles();
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

        IQueryable<Role> IRoleService.CheckRoleExits(string roleName)
        {
            throw new NotImplementedException();
        }

        public void Delete(Role entity)
        {
            throw new NotImplementedException();
        }

        public Role GetById(int roleId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Role> GetByLabel()
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public List<UsersInRoles> GetUsersInRole()
        {
            throw new NotImplementedException();
        }

        public IQueryable<UsersInRoles> GetUsersRole()
        {
            throw new NotImplementedException();
        }

        public void Save(Role Role)
        {
            throw new NotImplementedException();
        }
    }
}
