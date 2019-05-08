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
    public class RoleService : CmsService<Role>
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
        ///   Gets Role by account Rolename.
        /// </summary>
        /// <param name="RoleName"> </param>
        /// <returns> </returns>
        public List<Role> Get()
        {   
            return _roleRepository.GetAll().ToList();
        }


        public void Save(Role Role, string email, string password)
        {
            Save(Role);
            WebSecurity.CreateAccount(Role.RoleName, password);
        }

        public IQueryable<Role> GetByLabel()
        {
            return _roleRepository.Query()
                .Where(x => x.RoleName.Contains("Adminstaror"));
        }
    }
}