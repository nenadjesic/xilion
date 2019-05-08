using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Media.Documents
{
    public class DocumentsSettings : ApplicationSettings
    {
        private GeneralSection _general;

        public DocumentsSettings(ISettingsRepository repository, string owner)
            : base(repository, DocumentsApplication.ApplicationName, owner)
        {
        }

        public GeneralSection General
        {
            get { return _general ?? (_general = new GeneralSection(Settings)); }
        }

        public string AllowedExtensions
        {
            get { return GetValue("AllowedExtensions", Default.AllowedExtensions); }
            set { SetValue("AllowedExtensions", value); }
        }

        public int MaxAllowedSize
        {
            get { return GetValue("MaxAllowedSize", Default.MaxAllowedSize); }
            set { SetValue("MaxAllowedSize", value);}
        }

        public string CachePath
        {
            get { return GetValue("CachePath", "~/_cache"); }
            set { SetValue("CachePath", value);}
        }

        public string Directory
        {
            get { return GetValue("Directory", "~/_content/_media"); }
            set{SetValue("Directory", value);}
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
            }

            #endregion
        }

        #endregion


        #region Nested type: Default

        private static class Default
        {
            public const string AllowedExtensions = "doc,docx,pdf,xls,xlsx,ppt,pptx,zip";
            public const int MaxAllowedSize = 2097152 ;
        }

        #endregion
    }
}