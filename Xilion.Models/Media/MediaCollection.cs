using System;
using System.Collections.Generic;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Media
{
    public class MediaCollection : WorkflowEntity
    {
        private IList<MediaItem> _items = new List<MediaItem>();
        private MediaType _mediaType = MediaType.Image;

        public MediaCollection()
        {
        }

        public MediaCollection(MediaType mediaType)
        {
            _mediaType = mediaType;
        }

        public virtual bool IsSystem { get; set; }

        public virtual IList<MediaItem> Items
        {
            get { return _items; }
            protected set { _items = value; }
        }

        public virtual MediaType MediaType
        {
            get { return _mediaType; }
            set { _mediaType = value; }
        }

        public virtual string Title { get; set; }

        public virtual string Summary { get; set; }

        /// <summary>
        /// Creates new instance of system collection. 
        /// </summary>
        /// <returns>Instance of MediaCollection.</returns>
        public static MediaCollection CreateSystem()
        {
            return new MediaCollection
                       {
                           Alias = "",
                           MediaType = MediaType.Image,
                           IsSystem = true,
                           Status = WorkflowStatus.Live
                       };
        }
    }
}