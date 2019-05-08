using System.Configuration;

namespace Xilion.Models.Core.Configuration
{
    /// <summary>
    ///   Internal class for working with config file.
    /// </summary>
    public static class CmsConfig
    {
        private static readonly CmsConfigurationSection _cmsConfigurationSection =
            (CmsConfigurationSection) ConfigurationManager.GetSection("Xilion/cms");

        /// <summary>
        ///   Gets the cms configuration section.
        /// </summary>
        public static CmsConfigurationSection CmsConfigurationSection
        {
            get { return _cmsConfigurationSection; }
        }
    }
}