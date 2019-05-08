using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Classifications.Core
{
    public class ClassificationApplication : Application
    {
        internal const string ApplicationName = "xilion/Classifications";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public ClassificationApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        private static ApplicationEntityType GetClassificationTypes()
        {
            var type = new ApplicationEntityType(typeof (Classification));
            type.Properties.Add(new MetaDataPropertyDefinition("Name") {IsStored = true, IsLocalized = true});
            type.Properties.Add(new MetaDataPropertyDefinition("ItemName") {IsStored = true, IsLocalized = true});
            return type;
        }

        private static ApplicationEntityType GetClassificationItemTypes()
        {
            var type = new ApplicationEntityType(typeof (Label));
            type.Properties.Add(new MetaDataPropertyDefinition("Name") {IsStored = true, IsLocalized = true});
            return type;
        }

        #region Overrides of Application

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> {GetClassificationTypes(), GetClassificationItemTypes()};
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<ClassificationSettings>(scope);
        }

        #endregion
    }
}