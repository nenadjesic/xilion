using System.Collections.Generic;
using Xilion.Models.Classifications;
using NHibernate.Envers.Configuration.Attributes;
using Xilion.Models.Core.Domain;


namespace Xilion.Models.Media
{
    /// <summary>
    /// Represents abstract media item.
    /// </summary>
    public abstract class MediaItem : MetaDataEntity
    {
        private IList<Label> _labels = new List<Label>(); 

        /// <summary>
        /// Gets or sets item title.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets item file name.
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// Gets or sets item file path (file name or full path depending on provider).
        /// </summary>
        public virtual string FilePath
        {
            get { return FileName; }
        }

        /// <summary>
        /// Gets or sets item file extension (without dot ('.') as prefix).
        /// </summary>
        public virtual string Extension { get; set; }

        /// <summary>
        /// Gets or sets Library item belongs to.
        /// </summary>

        public virtual Library Library { get; set; }


        /// <summary>
        /// Gets or sets media ordering.
        /// </summary>
        public virtual int Ordinal { get; set; }


        [NotAudited]
        public virtual IList<Label> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }
    }
}