using NHibernate.Search.Attributes;
using Xilion.Models.Classifications;

namespace Xilion.Models.Media.Images
{
    [Indexed]
    public class ImageItem : MediaItem
    {
        /// <summary>
        /// Gets or sets image width. 
        /// </summary>
        public virtual int Width { get; set; }

        /// <summary>
        /// Gets or sets image height.
        /// </summary>
        public virtual int Height { get; set; }


    }
}