using System.Configuration;

namespace Xilion.Framework.Configuration
{
    /// <summary>
    /// Internal class for working with config file.
    /// </summary>
    public static class ConfigHelper
    {
        private static readonly LocalizationSection _localizationSection =
            (LocalizationSection) ConfigurationManager.GetSection("Xilion/localization");

        /// <summary>
        /// Gets the localization configuration section.
        /// </summary>
        public static LocalizationSection LocalizationSection
        {
            get { return _localizationSection; }
        }
    }
}