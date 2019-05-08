using System.Collections.Generic;
using Xilion.Models.Classifications;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Media.Core
{
    public class MediaApplication : Application, IClassificated
    {
        internal const string ApplicationName = "sauti/Media";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public MediaApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        

        public IList<Classification> Classifications { get; set; }


        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> {GetGalleryType(), GetMediaItemType()};
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<MediaSettings>(scope);
        }

        private static ApplicationEntityType GetGalleryType()
        {
            var type = new ApplicationEntityType(typeof (Library));
            type.AddProperties(GetSharedProperties());
            return type;
        }

        private static ApplicationEntityType GetMediaItemType()
        {
            var type = new ApplicationEntityType(typeof (MediaItem));
            type.AddProperties(GetSharedProperties());
            type.Properties.Add(new MetaDataPropertyDefinition("ExternalID") {IsStored = false});
            type.Properties.Add(new MetaDataPropertyDefinition("ScribdID")
                                    {IsStored = false, Indexed = false, IsAnalyzed = false});
            type.Properties.Add(new MetaDataPropertyDefinition("ExternalTitle") {IsStored = false});
            type.Properties.Add(new MetaDataPropertyDefinition("Thumb") {Indexed = false});
            return type;
        }

        private static IEnumerable<MetaDataPropertyDefinition> GetSharedProperties()
        {
            yield return new MetaDataPropertyDefinition("Title") {IsStored = true, IsLocalized = true};
            yield return new MetaDataPropertyDefinition("Content") { IsStored = true, IsLocalized = true };
            yield return new MetaDataPropertyDefinition("Summary") {IsLocalized = true};
        }

        private static IEnumerable<MetaDataPropertyDefinition> GetLibraryType()
        {
            yield return new MetaDataPropertyDefinition("Title") { IsStored = true, IsLocalized = true };
        }
        
    }
}
