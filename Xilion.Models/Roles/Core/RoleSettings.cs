using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Roles.Core
{
    public class RoleSettings : ApplicationSettings
    {
        private GeneralSection _general;

        public RoleSettings(ISettingsRepository repository, string owner)
            : base(repository, RoleApplication.ApplicationName, owner)
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

            public int PageSize
            {
                get { return GetValue(Keys.PageSizeKey, 20); }
                set { SetValue(Keys.PageSizeKey, value); }
            }

            #region Nested type: Keys

            private static class Keys
            {
                public const string PageSizeKey = "PageSize";
                public const string Classifications = "Classifications";
            }

            #endregion
        }

        #endregion
    }
}