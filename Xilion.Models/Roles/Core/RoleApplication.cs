using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;


namespace Xilion.Models.Roles.Core
{
    public class RoleApplication : SecuredApplication
    {
        internal const string ApplicationName = "xilion/Roles";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public RoleApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> {GetRoleType()};
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<RoleSettings>(scope);
        }

        private static ApplicationEntityType GetRoleType()
        {
            var type = new ApplicationEntityType(typeof(Role));
            type.Properties.Add(new MetaDataPropertyDefinition("Administrator") { IsStored = true, IsLocalized = true });
            return type;
        }

    }
}