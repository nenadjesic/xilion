using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Core.Applications;
using Xilion.Framework;
using Xilion.Framework.Data.Repositories;
using Xilion.Framework.Domain;

namespace Xilion.Models.Core.Security
{
    public class SecurityService<TApplication, TEntity, TAccessRight> 
        where TApplication : IApplication, ISecured 
        where TEntity : Entity, ISecured
        where TAccessRight : AccessRight
    {
        private readonly ICmsContext _cmsContext;
        private readonly IRepository<TEntity> _repository;

        public SecurityService(ICmsContext cmsContext, IRepository<TEntity> repository)
        {
            _cmsContext = cmsContext;
            _repository = repository;
        }

        public IList<PermissionInput> GetPermissions(string role, long securedId)
        {
            var permissions = securedId == 0
                                  ? _cmsContext.GetPermissionsFor<TApplication>()
                                  : _repository.GetById(securedId).Permissions;

            var rights = AccessRight.GetAllAccessRights<TAccessRight>();

            return rights
                .Select(x => new PermissionInput
                                 {
                                     AccessRightName = x.DisplayName,
                                     AccessRightValue = x.Value,
                                     AccessValue = GetAccess(permissions, x, role).Value
                                 })
                .ToList();
        }

        private static Access GetAccess(Permissions permissions, AccessRight accessRight, string role)
        {
            var accessPermission = permissions.AccessPermissions
                .SingleOrDefault(x => x.AccessRight == accessRight.Value && x.Role == role);
            return accessPermission == null ? Access.Inherit : accessPermission.Access;
        }

        public void SetPermissions(string role, IEnumerable<PermissionInput> permissionList, long securedId)
        {
            var entity = securedId == null ? null : _repository.GetById(securedId);

            var permissions = entity == null
                                  ? _cmsContext.GetPermissionsFor<TApplication>()
                                  : entity.Permissions;
            
            permissions.InheritFor(role);

            foreach (var permissionInput in permissionList)
            {
                var access = Enumeration.FromValue<Access>(permissionInput.AccessValue);
                if (access == Access.Inherit) continue;

                var accessRight = AccessRight.FromAccessRightValue<TAccessRight>(permissionInput.AccessRightValue);

                if (access == Access.Allow)
                    permissions.Allow(accessRight).To(role);
                else
                    permissions.Deny(accessRight).To(role);
            }

            if (entity == null)
                _cmsContext.SetPermissionsFor<TApplication>(permissions);
            else
            {
                entity.Permissions = permissions;
                _repository.Save(entity);
            }
        }
    }

    public class SecurityService<TApplication, TAccessRight>
        where TApplication : IApplication, ISecured
        where TAccessRight : AccessRight
    {
        private readonly ICmsContext _cmsContext;

        public SecurityService(ICmsContext cmsContext)
        {
            _cmsContext = cmsContext;
        }

        public IList<PermissionInput> GetPermissions(string role, long? securedId = null)
        {
            var permissions = _cmsContext.GetPermissionsFor<TApplication>();
                                  

            var rights = AccessRight.GetAllAccessRights<TAccessRight>();

            return rights
                .Select(x => new PermissionInput
                {
                    AccessRightName = x.DisplayName,
                    AccessRightValue = x.Value,
                    AccessValue = GetAccess(permissions, x, role).Value
                })
                .ToList();
        }

        private static Access GetAccess(Permissions permissions, AccessRight accessRight, string role)
        {
            var accessPermission = permissions.AccessPermissions
                .SingleOrDefault(x => x.AccessRight == accessRight.Value && x.Role == role);
            return accessPermission == null ? Access.Inherit : accessPermission.Access;
        }

        public void SetPermissions(string role, IEnumerable<PermissionInput> permissionList)
        {


            var permissions = _cmsContext.GetPermissionsFor<TApplication>();

            permissions.InheritFor(role);

            foreach (var permissionInput in permissionList)
            {
                var access = Enumeration.FromValue<Access>(permissionInput.AccessValue);
                if (access == Access.Inherit) continue;

                var accessRight = AccessRight.FromAccessRightValue<TAccessRight>(permissionInput.AccessRightValue);

                if (access == Access.Allow)
                    permissions.Allow(accessRight).To(role);
                else
                    permissions.Deny(accessRight).To(role);
            }
           
                _cmsContext.SetPermissionsFor<TApplication>(permissions);
        
        }
    }
}