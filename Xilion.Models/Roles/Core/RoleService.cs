using System.Linq;
using System.Web;
using Xilion.Models.Classifications;
using Xilion.Models.Core;
using Xilion.Models.Core.Services;
using Xilion.Models.Media.Documents;
using Xilion.Models.Roles.Data;
using WebMatrix.WebData;
using System.Collections.Generic;

namespace Xilion.Models.Roles.Core
{
    /// <summary>
    ///   Represent service working with <see cref="Role" />
    /// </summary>
    public class RoleService : CmsService<Role>, IRoleService
    {
        private static bool _initialized;
        private readonly IRoleRepository _roleRepository;
        private readonly IUsersInRolesRepository _uirRepository;
        public RoleService(IRoleRepository roleRepository, IUsersInRolesRepository uirRepository) : base(roleRepository)
        {
            _roleRepository = roleRepository;
            _uirRepository = uirRepository;
        }


        public RoleSettings Settings
        {
            get { return (RoleSettings)CmsContext.Current.GetApplication<RoleApplication>().GetSettings(); }
        }


        /// <summary>
        ///   Gets Roles
        /// </summary>
        /// <param name="RoleName"> </param>
        /// <returns> </returns>
        public List<Role> GetRoles()
        {
            return _roleRepository.GetAll().ToList();
        }

        /// <summary>
        ///   Gets Role by Id
        /// </summary>
        /// <returns> </returns>
        public Role GetById(int roleId)
        {
            return _roleRepository.GetById(roleId);
        }

        /// <summary>
        ///   Gets Role by Id
        /// </summary>
        /// <returns> </returns>
        public bool DeleteRole(Role entity)
        {
            if (entity.IsPersistent)
            {
                return false;
            }
            else
            {
                base.Delete(entity);
                return true;
            }
        }


        public void Save(Role Role, string email, string password)
        {
            Save(Role);
        }

        public IQueryable<Role> GetByLabel()
        {
            return _roleRepository.Query()
                .Where(x => x.RoleName.Contains("Adminstaror"));
        }

        public IQueryable<Role> CheckRoleExits(string roleName)
        {
            return _roleRepository.Query().Where(x => x.RoleName.Contains(roleName));
        }


        /// <summary>
        ///   Gets usersRole
        /// </summary>
        /// <returns> </returns>
        public IQueryable<UsersInRoles> GetUsersRole()
        {
            return _uirRepository.GetAll();
        }

        /// <summary>
        ///   Gets usersRole
        /// </summary>
        /// <returns> </returns>
        public List<UsersInRoles> GetUsersInRole()
        {
            return _uirRepository.GetAll().ToList();
        }
    }
}