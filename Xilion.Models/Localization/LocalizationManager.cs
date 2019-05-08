using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using Xilion.Framework.Configuration;

namespace Xilion.Models.Localization
{
    /// <summary>
    ///   Provides the main entry point for the localization functionality of the application.
    /// </summary>
    public static class LocalizationManager
    {
        public const string CmsContentCultureKey = "cmsContentCulture";
        private static readonly CultureInfo _defaultCulture;
        private static readonly CultureInfo[] _cultures;
        private static CultureInfo _culture;
        private static IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        ///   Initialization.
        /// </summary>
        static LocalizationManager()
        {
            var defaultCultureID = ConfigHelper.LocalizationSection.Cultures.DefaultCulture;
            var supportedCultures = ConfigHelper.LocalizationSection.Cultures.SupportedCultures;

            _defaultCulture = CultureInfo.GetCultureInfo(defaultCultureID);

            if (string.IsNullOrEmpty(supportedCultures))
                _cultures = new CultureInfo[0];
            else
            {
                var cultures = supportedCultures.Split(',');
                var cultureInfos = new List<CultureInfo>(cultures.Length);

                var defaultAdded = false;
                foreach (var culture in cultures)
                {
                    if (culture.Length > 0)
                        cultureInfos.Add(CultureInfo.GetCultureInfo(culture.Trim()));
                    if (culture == defaultCultureID)
                        defaultAdded = true;
                }

                if (!defaultAdded) cultureInfos.Insert(0, _defaultCulture);
                _cultures = cultureInfos.ToArray();
            }
        }

        /// <summary>
        ///   Gets the list of supported cultures.
        /// </summary>
        public static CultureInfo[] Cultures
        {
            get { return _cultures; }
        }

        /// <summary>
        ///   Gets the current culture.
        /// </summary>
        public static CultureInfo CurrentContentCulture
        {       
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;
                _culture = (httpContext.Items[CmsContentCultureKey] == null)
                               ? DefaultCulture
                               : (CultureInfo)httpContext.Items[CmsContentCultureKey];
                return _culture;
            }
        }

        /// <summary>
        ///   Gets the default application culture.
        /// </summary>
        public static CultureInfo DefaultCulture
        {
            get {
                CultureInfo cultureinfo = new CultureInfo(ConfigHelper.LocalizationSection.Cultures.DefaultCulture);
                return cultureinfo;
            }
        }

        /// <summary>
        ///   Returns the supported culture with the given name. If no culture is found, 
        ///   <see cref="DefaultCulture" /> is returned.
        /// </summary>
        /// <param name="name"> Name of the culture to find. </param>
        /// <returns> Supported culture with the given name. </returns>
        public static CultureInfo GetCulture(string name)
        {
            return _cultures.SingleOrDefault(x => x.Name == name) ?? DefaultCulture;
        }

        /// <summary>
        ///   Returns the neutral culture from the given culture. If the given culture itself is neutral, it will be
        ///   returned.
        /// </summary>
        /// <param name="culture"> Culture to check for neutral. </param>
        /// <returns> The neutral culture for the given culture. </returns>
        public static CultureInfo GetNeutralCulture(CultureInfo culture)
        {
            if (culture.IsNeutralCulture) return culture;

            while (!(culture = culture.Parent).IsNeutralCulture)
            {
            }

            return culture;
        }
    }
}