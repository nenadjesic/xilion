using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Media.Images
{
    public class ImageApplication : Application
    {
        internal const string ApplicationName = "Xilion/Image";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public ImageApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> {GetImageType()};
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<ImageSettings>(scope);
        }

        private static ApplicationEntityType GetImageType()
        {
            var type = new ApplicationEntityType(typeof (ImageItem));
            type.Properties.Add(new MetaDataPropertyDefinition("Title") {IsStored = true, IsLocalized = true});
            return type;
        }
    }
}