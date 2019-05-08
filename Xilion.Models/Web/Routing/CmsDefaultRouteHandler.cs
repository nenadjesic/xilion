using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Xilion.Models.Localization;

namespace Xilion.Models.Web.Routing
{
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
     AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class CmsDefaultRouteHandler : IRouteHandler
    {
        #region IRouteHandler Members

        public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var culture = requestContext.RouteData.Values["culture"] as string;
            if (culture == null)
            {
                culture = LocalizationManager.DefaultCulture.Name;
                requestContext.RouteData.Values["culture"] = culture;
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            return new MvcHandler(requestContext);
        }

        #endregion
    }
}