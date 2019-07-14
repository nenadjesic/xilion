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
        //private readonly IUsersInRolesRepository _uirRepository;
        public RoleService(IRoleRepository roleRepository) : base(roleRepository)
        {
            _roleRepository = roleRepository;
        }


        public RoleSettings Settings
        {
            get { return (RoleSettings)CmsContext.Current.GetApplication<RoleApplication>().GetSettings(); }
        }


        public override IList<Role> GetAll()
        {
            return _roleRepository.GetAll().ToList();
        }


        public Role GetById(int roleId)
        {
            return _roleRepository.GetById(roleId);
        }


        public bool DeleteRole(Role entity)
        {
            if (entity.IsPersistent)
                return false;
            _roleRepository.Delete(entity);
            return true;
        }


        public override void Save(Role Role)
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
    }
}