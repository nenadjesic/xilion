using System.Collections.Generic;
using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Settings;
using System.Linq;

namespace Xilion.Models.Media.Images
{
    public class ImageSettings : ApplicationSettings
    {
        public ImageSettings(ISettingsRepository repository, string owner)
            : base(repository, ImageApplication.ApplicationName, owner)
        {
        }

        /// <summary>
        /// Gets or sets max allowed size in bytes
        /// </summary>
        public int MaxAllowedSize
        {
            get { return GetValue("MaxAllowedSize", Default.MaxAllowedSize); }
            set { SetValue("MaxAllowedSize", value); }
        }

        /// <summary>
        /// Gets or sets allowed extensions.
        /// </summary>
        public string AllowedExtensions
        {
            get { return GetValue("AllowedExtensions", Default.AllowedExtensions); }
            set { SetValue("AllowedExtensions", value); }
        }

        /// <summary>
        /// Gets or sets cache path.
        /// </summary>
        public string CachePath
        {
            get { return GetValue("CachePath", "~/_cache"); }
            set { SetValue("CachePath", value); }
        }

        /// <summary>
        /// Gets or sets directory path.
        /// </summary>
        public string Directory
        {
            get { return GetValue("Directory", "~/_content/_media/"); }
            set { SetValue("Directory", value); }
        }

        #region Nested type: Default

        private static class Default
        {
            public const int MaxAllowedSize = 4194304;
            public const string AllowedExtensions = "jpg,png,gif,bmp";
        }

        #endregion
    }
}