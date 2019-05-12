using System.Web;
using StructureMap;
using Microsoft.AspNetCore.Http;
using HttpContext = Xilion.Framework.Web.HttpContext;

namespace Xilion.Models.Core.Settings
{
    public class ApplicationSettingsFactory : IApplicationSettingsFactory
    {
        #region IApplicationSettingsFactory Members

        private static Container _container;
        public T GetSettings<T>(SettingsScope scope) where T : ApplicationSettings
        {
            string owner = scope == SettingsScope.AllUsers
                               ? Settings.AllUsers
                               : HttpContext.Current.User.Identity.Name;

            return _container
                .With<string>(owner)
                .GetInstance<T>(scope.ToString());
        }

        #endregion
    }
}