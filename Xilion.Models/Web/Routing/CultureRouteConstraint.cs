using System;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Xilion.Models.Localization;

namespace Xilion.Models.Web.Routing
{
    /// <summary>
    /// Takes care of dealing only with supported cultures defined in localization section of web.config
    /// </summary>
    public class CultureRouteConstraint : IRouteConstraint
    {
        #region IRouteConstraint Members

        public virtual bool Match(HttpContextBase httpContext, Route route, string parameterName,
                                  RouteValueDictionary values, RouteDirection routeDirection)
        {
            return
                LocalizationManager.Cultures.Any(
                    c => c.Name.Equals((string) values[parameterName], StringComparison.OrdinalIgnoreCase));
        }

        #endregion
    }
}