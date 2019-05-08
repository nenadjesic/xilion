using System;

namespace Xilion.Models.Core.Settings
{
    public class SettingsSection
    {
        private readonly string _sectionName;
        private readonly Settings _settings;

        public SettingsSection(Settings settings, string sectionName)
        {
            _sectionName = sectionName;
            _settings = settings;
        }

        protected T GetValue<T>(string propertyName)
        {
            return _settings.GetValue<T>(GetFullPropertyName(propertyName));
        }

        protected T GetValue<T>(string propertyName, T defaultValue)
        {
            return _settings.GetValue(GetFullPropertyName(propertyName), defaultValue);
        }

        protected void SetValue<T>(string propertyName, T value)
        {
            _settings.SetValue(GetFullPropertyName(propertyName), value);
        }

        private string GetFullPropertyName(string key)
        {
            return String.Concat(_sectionName, ".", key);
        }
    }
}