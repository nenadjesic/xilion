using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;
using Xilion.Models.Events;

namespace Xilion.Models.Events.Core
{
    public class EventApplication : Application
    {
        internal const string ApplicationName = "xilion/Events";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public EventApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> {GetEventType()};
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<EventSettings>(scope);
        }

        private static ApplicationEntityType GetEventType()
        {
            var type = new ApplicationEntityType(typeof(Event));
            type.Properties.Add(new MetaDataPropertyDefinition("Title") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Summary") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Content") { IsLocalized = true });
            return type;
        }
    }
}