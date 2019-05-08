using System.Collections.Generic;
using Xilion.Models.Classifications;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Articles.Core
{
    public class ArticleApplication : Application
    {
        internal const string ApplicationName = "xilion/Articles";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public ArticleApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType> {GetArticleType()};
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<ArticleSettings>(scope);
        }

        private static ApplicationEntityType GetArticleType()
        {
            var type = new ApplicationEntityType(typeof (Article));
            type.Properties.Add(new MetaDataPropertyDefinition("Title") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Summary") { IsStored = true, IsLocalized = true });
            type.Properties.Add(new MetaDataPropertyDefinition("Content") { IsLocalized = true });
            return type;
        }

      
    }
}