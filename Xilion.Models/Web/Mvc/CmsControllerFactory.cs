using System;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Xilion.Models.Web.Mvc
{
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
     AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class CmsControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null || controllerType == typeof(CmsPageController))
            {
                requestContext.RouteData.Values["controller"] = "CmsPage";
                requestContext.RouteData.Values["action"] = "Page";
                //requestContext.RouteData.DataTokens["Namespaces"] = "Xilion.WebApp.Controllers";
                return DependencyResolver.Current.GetService<CmsPageController>();
            }

            return DependencyResolver.Current.GetService(controllerType) as IController;
        }

        public Type GetControllerTypeFromName(RequestContext requestContext, string controllerName)
        {
            return GetControllerType(requestContext, controllerName);
        }
    }
}