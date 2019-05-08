using System.Collections.Generic;
using Xilion.Models.Classifications;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Media.Core
{
    public class LibraryTypeApplication : Application
    {
        private readonly IApplicationSettingsFactory _settingsFactory;
        internal const string ApplicationName = "Xilion/document";

        public LibraryTypeApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        private static ApplicationEntityType GetLibraryType()
        {
            var type = new ApplicationEntityType(typeof(LibraryType));
            type.Properties.Add(new MetaDataPropertyDefinition("Title") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Content") { IsStored = true, IsLocalized = true });
            return type;
        }

        #region Overrides of Application

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> { GetLibraryType() };
        }

        #endregion
    }
}
