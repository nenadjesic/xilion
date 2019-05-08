using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Site.Core
{
    public class PageSettings : ApplicationSettings
    {
        private CurrentUserSection _currentUser;

        public PageSettings(ISettingsRepository repository,  string owner)
            : base(repository, SiteApplication.ApplicationName, owner)
        {
        }

        public CurrentUserSection CurrentUser
        {
            get { return _currentUser ?? (_currentUser = new CurrentUserSection(Settings)); }
        }

        public class CurrentUserSection : SettingsSection
        {
            private const string SectionName = "CurrentUser";

            public CurrentUserSection(Settings settings)
                : base(settings, SectionName)
            {
            }

            public string InitialState
            {
               get { return GetValue(Keys.InitialStateKey, ""); }
               set { SetValue(Keys.InitialStateKey, value);}
            }

            private static class  Keys
            {
                public const string InitialStateKey = "InitialState";
            }
        }
    }
}
