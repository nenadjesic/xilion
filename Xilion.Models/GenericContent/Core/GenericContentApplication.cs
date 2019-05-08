using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.GenericContent.Core
{
    public class GenericContentApplication : Application 
    {
        internal const string ApplicationName = "Xilion/Generic_Contents";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public GenericContentApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> { GetContentType() };
        }

        private static ApplicationEntityType GetContentType()
        {
            var type = new ApplicationEntityType(typeof(GenericContent));
            type.Properties.Add(new MetaDataPropertyDefinition("Title") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Content") { IsStored = true, IsLocalized = true });
            return type;
        }
    }
}