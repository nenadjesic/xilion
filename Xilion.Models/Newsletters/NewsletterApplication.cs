using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;
using Xilion.Models.Newsletters;

namespace Xilion.Models.Newsletters
{
    public class NewsletterApplication : Application
    {
        internal const string ApplicationName = "Xilion/Newsletters";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public NewsletterApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> { GetNewsLetterType() };
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<NewsletterSettings>(scope);
        }

        private static ApplicationEntityType GetNewsLetterType()
        {
            var type = new ApplicationEntityType(typeof (Newsletter));
            type.Properties.Add(new MetaDataPropertyDefinition("Ime") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Prezime") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Email") { IsStored = true, IsLocalized = true });
            return type;
        }
    }
}