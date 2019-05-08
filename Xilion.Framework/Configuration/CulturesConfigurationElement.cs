using System;
using System.Configuration;
using System.Web;

namespace Xilion.Framework.Configuration
{
    /// <summary>
    /// Represents culture definition in config file.
    /// </summary>
    public class CulturesConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the id of default culture.
        /// </summary>
        [ConfigurationProperty("defaultCulture", IsKey = true, IsRequired = true)]
        [DefaultSettingValue("en")]
        public string DefaultCulture
        {
            get
            {
                if (this["defaultCulture"] == null)
                    throw new Exception("defaultCulture not defined");

                return (string) this["defaultCulture"];
            }
            set { this["defaultCulture"] = value; }
        }

      
        /// <summary>
        /// Gets or sets all supported culture ids separated with comma (",").
        /// </summary>
        [ConfigurationProperty("supportedCultures")]
        [DefaultSettingValue("en")]
        public string SupportedCultures
        {
            get { return (string) this["supportedCultures"]; }
            set { this["supportedCultures"] = value; }
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }
}