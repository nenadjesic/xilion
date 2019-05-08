using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Events.Core
{
    public class EventSettings : ApplicationSettings
    {
        private GeneralSection _general;

        public EventSettings(ISettingsRepository repository, string owner)
            : base(repository, EventApplication.ApplicationName, owner)
        {
        }

        public GeneralSection General
        {
            get { return _general ?? (_general = new GeneralSection(Settings)); }
        }

        #region Nested type: GeneralSection

        public class GeneralSection : SettingsSection
        {
            private const string SectionName = "General";

            public GeneralSection(Settings settings)
                : base(settings, SectionName)
            {
            }

            public long PageSize
            {
                get { return GetValue(Keys.PageSizeKey, 10L); }
                set { SetValue(Keys.PageSizeKey, value); }
            }

            #region Nested type: Keys

            private static class Keys
            {
                public const string PageSizeKey = "PageSize";
            }

            #endregion
        }

        #endregion
    }
}