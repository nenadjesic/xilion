using Xilion.Models.Core.Data;
using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Newsletters
{
    /// <summary>
    /// Represent blog settings paramas.
    /// </summary>
    public class NewsletterSettings : ApplicationSettings
    {
        private GeneralSection _general;

        public NewsletterSettings(ISettingsRepository repository, string owner)
            : base(repository, NewsletterApplication.ApplicationName, owner)
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

            public GeneralSection(Settings settings) : base(settings, SectionName)
            {
            }

            /// <summary>
            /// Gets or sets number of post per page.
            /// </summary>
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