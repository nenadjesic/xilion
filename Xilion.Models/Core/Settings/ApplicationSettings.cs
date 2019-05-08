using System;
using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Security;

namespace Xilion.Models.Core.Settings
{
    public abstract class ApplicationSettings
    {
        private readonly string _applicationName;
        private readonly string _owner;
        private readonly ISettingsRepository _repository;
        private Settings _settings;

        protected ApplicationSettings(ISettingsRepository repository, string applicationName, string owner)
        {
            _repository = repository;
            _applicationName = applicationName;
            _owner = owner;
        }

        protected Settings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = _repository.GetByApplicationNameAndOwner(_applicationName, _owner);

                if (_settings == null)
                {
                    if (_owner != Settings.AllUsers)
                    {
                        Settings allUsersettings = _repository.GetByApplicationNameAndOwner(_applicationName,
                                                                                            Settings.AllUsers);

                        if (allUsersettings != null)
                        {
                            _settings = allUsersettings.Clone();
                            _settings.Owner = _owner;
                        }
                    }

                    if (_settings == null)
                    {
                        _settings = new Settings {ApplicationName = _applicationName, Owner = _owner};
                    }
                }

                return _settings;
            }
        }


        public Permissions Permissions
        {
            get { return Permissions.Deserialize(GetValue(Keys.PermissionsKey, String.Empty)); }
            set { SetValue(Keys.PermissionsKey, value == null ? String.Empty : value.Serialize()); }
        }

        public virtual void Save()
        {
            _repository.Save(Settings);
        }

        public virtual void Delete()
        {
            _repository.Delete(Settings);
        }

        protected T GetValue<T>(string propertyName)
        {
            return Settings.GetValue<T>(propertyName);
        }

        protected T GetValue<T>(string propertyName, T defaultValue)
        {
            return Settings.GetValue(propertyName, defaultValue);
        }

        protected void SetValue<T>(string propertyName, T value)
        {
            Settings.SetValue(propertyName, value);
        }

        #region Nested type: Keys

        private static class Keys
        {
            public const string PermissionsKey = "Permissions";
        }

        #endregion
    }
}