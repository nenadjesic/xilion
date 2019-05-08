using System;
using System.Configuration;

namespace Xilion.Models.Core.Configuration
{
    public class CmsConfigurationSection : ConfigurationSection
    {
        /// <summary>
        ///   Gets or sets the id of default culture.
        /// </summary>
        [ConfigurationProperty("XilionPath")]
        [DefaultSettingValue("~/Xilion")]
        public string XilionPath
        {
            get
            {
                if (this["XilionPath"] == null)
                    throw new Exception("XilionPath not defined");

                return (string) this["XilionPath"];
            }
            set { this["XilionPath"] = value; }
        }
    }
}