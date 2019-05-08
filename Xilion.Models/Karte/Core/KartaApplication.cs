using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;
using Xilion.Models.Karte;

namespace Xilion.Models.Karte.Core
{
    public class KartaApplication : Application
    {
        internal const string ApplicationName = "qusion/Karta";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public KartaApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> { GetKartaType() };
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<KartaSettings>(scope);
        }

        private static ApplicationEntityType GetKartaType()
        {
            var type = new ApplicationEntityType(typeof (Karta));
            type.Properties.Add(new MetaDataPropertyDefinition("Title") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Summary") { IsStored = true, IsLocalized = true });
            return type;
        }
    }
}