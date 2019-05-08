using System.Collections.Generic;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Media.Video
{
    public class VideoApplication : Application
    {
        internal const string ApplicationName = "Xilion/video";
        private readonly IApplicationSettingsFactory _settingsFactory;

        public VideoApplication(IApplicationSettingsFactory settingsFactory)
        {
            _settingsFactory = settingsFactory;
        }

        #region Overrides of Application

        public override string Name
        {
            get { return ApplicationName; }
        }

        protected override ApplicationSettings GetSettings(SettingsScope scope)
        {
            return _settingsFactory.GetSettings<VideoSettings>(scope);
        }

        protected override IList<ApplicationEntityType> GetTypes()
        {
            return new List<ApplicationEntityType>();
        }

        #endregion
    }
}