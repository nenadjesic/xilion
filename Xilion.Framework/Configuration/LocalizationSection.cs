using Microsoft.Extensions.Configuration;

namespace Xilion.Framework.Configuration
{
    /// <summary>
    /// Configuration section defining the application localization functionality.
    /// </summary>
    public class LocalizationSection 
    {
        private static  IConfiguration Configuration { get; }
        object cultures = Configuration.GetSection("cultures");
        public CulturesConfigurationElement Cultures
        {
            get { return (CulturesConfigurationElement)cultures; }
        }
    }
}