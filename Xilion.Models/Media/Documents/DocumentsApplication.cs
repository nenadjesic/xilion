using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Media.Documents
{
    public class DocumentsApplication : Application
    {
        private readonly IApplicationSettingsFactory _settingsFactory;
        internal const string ApplicationName = "Xilion/document";

        public DocumentsApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        private static ApplicationEntityType GetDocumentType()
        {
            var type = new ApplicationEntityType(typeof(DocumentItem));
            type.Properties.Add(new MetaDataPropertyDefinition("Title") { IsStored = true, IsLocalized = true });
            return type;
        }

        #region Overrides of Application

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType>{GetDocumentType()};
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<DocumentsSettings>(scope);
        }

        

        #endregion
    }
}