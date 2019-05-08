using System;
using System.Linq;
using Xilion.Framework;
using Xilion.Framework.Domain;

namespace Xilion.Models.Core.Settings
{
    /// <summary>
    /// Represents settings entity point for all settings data.
    /// </summary>
    public class Settings : Entity
    {
        /// <summary>
        /// Constant represents settings property name for settings used by all Users.
        /// Default is an empty string.
        /// </summary>
        public const string AllUsers = "";

        private SettingsProperties _properties = new SettingsProperties();

        /// <summary>
        /// 
        /// </summary>
        public virtual string ApplicationName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Owner { get; set; }

        protected internal virtual SettingsProperties Properties
        {
            get { return _properties; }
            protected set { _properties = value; }
        }

        /// <summary>
        /// Creates and returns exact clne of current Settings instance.
        /// </summary>
        /// <returns></returns>
        public virtual Settings Clone()
        {
            return new Settings {ApplicationName = ApplicationName, Owner = Owner, Properties = Properties};
        }

        /// <summary>
        /// Gets a settings for propertyName.
        /// </summary>
        /// <param name="propertyName">Unique settings identifier.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetValue<T>(string propertyName)
        {
            return GetValue(propertyName, default(T));
        }

        /// <summary>
        /// Gets a settings for propertyName. If no settings found default value is used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">Settings unique name.</param>
        /// <param name="defaultValue">Default value if there is no settings for property name found.</param>
        /// <returns></returns>
        public virtual T GetValue<T>(string propertyName, T defaultValue)
        {
            SettingsProperty parameter = EnsurePropertyExists(propertyName);
            return parameter == null
                       ? defaultValue
                       : ObjectBuilder.BuildObjectValue(parameter.Value, defaultValue);
        }

        /// <summary>
        /// Sets settings value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">Settings unique name.</param>
        /// <param name="value"></param>
        public virtual void SetValue<T>(string propertyName, T value)
        {
            SettingsProperty setting = EnsurePropertyExists(propertyName);

            object oldValue = setting.Value;
            var newValue = ObjectBuilder.BuildObjectValue<T>(value);

            setting.Value = newValue;

            // ReSharper disable CompareNonConstrainedGenericWithNull
            bool changed = oldValue == null
                               ? newValue != null
                               : !oldValue.Equals(newValue);
            // ReSharper restore CompareNonConstrainedGenericWithNull

            Properties.IsChanged = Properties.IsChanged || changed;
        }

        /// <summary>
        /// Ensures settings with provided properyName exists. 
        /// </summary>
        /// <param name="propertyName">Settings unique name.</param>
        /// <returns></returns>
        private SettingsProperty EnsurePropertyExists(string propertyName)
        {
            SettingsProperty setting = Properties.Properties
                .FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

            if (setting == null)
            {
                setting = new SettingsProperty {Name = propertyName};
                Properties.Properties.Add(setting);
            }

            return setting;
        }
    }
}