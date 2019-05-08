using System.Web;
using StructureMap;
using Microsoft.AspNetCore.Http;

namespace Xilion.Models.Core.Settings
{
    public class ApplicationSettingsFactory : IApplicationSettingsFactory
    {
        #region IApplicationSettingsFactory Members
        private static IHttpContextAccessor _httpContextAccessor;
        private static Container _container;
        public T GetSettings<T>(SettingsScope scope) where T : ApplicationSettings
        {
            string owner = scope == SettingsScope.AllUsers
                               ? Settings.AllUsers
                               : _httpContextAccessor.HttpContext.User.Identity.Name;

            return _container
                .With<string>(owner)
                .GetInstance<T>(scope.ToString());
        }

        #endregion
    }
}