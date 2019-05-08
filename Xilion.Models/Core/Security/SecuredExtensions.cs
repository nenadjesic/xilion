using System.Linq;
using System.Security.Principal;
using System.Web;
using Xilion.Models.Roles.Data;

namespace Xilion.Models.Core.Security
{
    public static class SecuredExtensions
    {
        /// <summary>
        ///   Checks if role has access to some action (AccessRight).
        /// </summary>
        /// <param name="secured"> Secured entity. </param>
        /// <param name="right"> AccessRight action </param>
        /// <param name="role"> Role to check against. </param>
        /// <returns> Value indicates if action is approved for selected role. </returns>
        public static bool IsAllowed(this ISecured secured, AccessRight right, string role)
        {
            if (CmsContext.UnrestrictedRoles.Any(x => x == role))
                return true;

            var access = GetAccess(secured, right, role);

            if (access == Access.Inherit)
                access = GetApplicationAccess(secured, right, role);

            return access == Access.Allow;
        }

        /// <summary>
        ///   Checks if principal Users has access to some action (AccessRight).
        /// </summary>
        /// <param name="secured"> Secured entity. </param>
        /// <param name="right"> AccessRight action </param>
        /// <param name="principal"> Current principal. </param>
        /// <returns> Value indicates if action is approved for selected role. </returns>
        //public static bool IsAllowed(this ISecured secured, AccessRight right, IPrincipal principal, IUsersInRolesRepository uic)
        //{
        //    var user = principal.Identity.Name;
        //    var roles = uic.GetAll();
        //    var v = roles.Any(role => secured.IsAllowed(right, user));
        //    return v;
        //}

        private static Access GetAccess(ISecured secured, AccessRight right, string role)
        {
            var access = secured.Permissions.GetAccess(right, role);

            if (access != Access.Inherit) return access;

            var securedChild = secured as ISecuredChild;

            return securedChild == null || securedChild.Parent == null
                       ? Access.Inherit
                       : GetAccess(securedChild.Parent, right, role);
        }

        private static Access GetApplicationAccess(ISecured secured, AccessRight right, string role)
        {
            var application = CmsContext.Current.GetApplication(secured.GetType()) as ISecured;
            return application == null ? Access.Deny : application.Permissions.GetAccess(right, role);
        }

    }
}