using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Media
{
    public class MediaSettings : ApplicationSettings
    {
        public MediaSettings(ISettingsRepository repository, string applicationName, string owner)
            : base(repository, applicationName, owner)
        {
        }

        public string Directory
        {
            get { return GetValue("Directory", "~/_content/_media/"); }
            set { SetValue("Directory", value); }
        }

        public string CachePath
        {
            get { return GetValue("CachePath", "~/_cache"); }
            set { SetValue("CachePath", value); }
        }

        public string MediaCollectionRoot
        {
            get { return GetValue(Keys.MediaCollectionRootKey, "~/_content/_media/"); }
            set { SetValue(Keys.MediaCollectionRootKey, value); }
        }

        #region Nested type: Keys

        private static class Keys
        {
            public const string MediaCollectionRootKey = "MediaCollectionRoot";
            public const string CachePathKey = "CachePath";
        }

        #endregion
    }
}