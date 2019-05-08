using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;
using Xilion.Models.User.Core;

namespace Xilion.Models.User.Core
{
    public class UserApplication : SecuredApplication
    {
        internal const string ApplicationName = "xilion/Users";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public UserApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> {GetUserType()};
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<UserSettings>(scope);
        }

        private static ApplicationEntityType GetUserType()
        {
            var type = new ApplicationEntityType(typeof(Users));
            type.Properties.Add(new MetaDataPropertyDefinition("FirstName") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("LastName") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Country") { IsLocalized = true, IsStored = true });
            type.Properties.Add(new MetaDataPropertyDefinition("FullName") { IsLocalized = true, IsStored = true });

            return type;
        }

    }
}