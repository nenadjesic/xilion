using System.Collections.Generic;

namespace Xilion.Models.Core.Settings
{
    public class SettingsProperties
    {
        public SettingsProperties()
        {
            Properties = new List<SettingsProperty>();
        }

        public bool IsChanged { get; set; }
        public IList<SettingsProperty> Properties { get; set; }
    }
}