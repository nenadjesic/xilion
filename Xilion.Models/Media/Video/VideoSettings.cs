using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Media.Video
{
    public class VideoSettings : ApplicationSettings 
    {
        public VideoSettings(ISettingsRepository repository, string owner)
            : base(repository, VideoApplication.ApplicationName, owner)
        {
        }

        public string AllowedExtensions
        {
            get { return GetValue("AllowedExtensions", string.Join(",",Default.AllowedExtensions)); }
            set { SetValue("AllowedExtensions", value); }
        }

        public int MaxAllowedSize
        {
            get { return GetValue("MaxAllowedSize", Default.MaxAllowedSize); }
            set { SetValue("MaxAllowedSize", value); }
        }

        public string CachePath
        {
            get { return GetValue("CachePath", "~/_cache"); }
            set { SetValue("CachePath", value); }
        }

        public string Directory
        {
            get { return GetValue("Directory", "~/_content/_media"); }
            set { SetValue("Directory", value); }
        }

        #region Nested type: Default

        private static class Default
        {
            public static readonly string[] AllowedExtensions = { "ogg", "mp4" };
            public const int MaxAllowedSize = 20971520;
        }

        #endregion
    }
}