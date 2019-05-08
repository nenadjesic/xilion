using NHibernate.Search.Attributes;

namespace Xilion.Models.Media.Video
{
    [Indexed]
    public class VideoItem : MediaItem
    {
        /// <summary>
        /// Gets or sets video duration in seconds. 
        /// </summary>
        public virtual int Duration { get; set; }
    }
}