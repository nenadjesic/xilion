using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Site.Core
{
    public class SiteApplication : Application
    {
        private readonly IApplicationSettingsFactory _settingsFactory;
        internal const string ApplicationName = "Xilion/site";
        public SiteApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> { GetPageType(), GetTemplateType ()};
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<PageSettings>(scope);
        }

       

        private static ApplicationEntityType GetTemplateType()
        {
            var type = new ApplicationEntityType(typeof (Page));
            type.Properties.Add(new MetaDataPropertyDefinition("Title") {IsStored = true, IsLocalized = true});
            type.Properties.Add(new MetaDataPropertyDefinition("Description") { IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("MenuName") { IsLocalized = true });
            return type;
        }

        private static ApplicationEntityType GetPageType()
        {
            var type = new ApplicationEntityType(typeof (PageTemplate));
            type.Properties.Add(new MetaDataPropertyDefinition("Title") {IsStored = true, IsLocalized = true});
            type.Properties.Add(new MetaDataPropertyDefinition("Description") {IsLocalized = true});
            return type;
        }
    }
}