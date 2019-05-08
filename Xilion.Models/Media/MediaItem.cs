using System.Collections.Generic;
using NHibernate.Search.Attributes;
using Xilion.Models.Classifications;
using NHibernate.Envers.Configuration.Attributes;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Data.Search;

namespace Xilion.Models.Media
{
    /// <summary>
    /// Represents abstract media item.
    /// </summary>
    [Indexed]
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
        [Field(Name = "filename")]
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
        [Field(Name = "extension")]
        public virtual string Extension { get; set; }

        /// <summary>
        /// Gets or sets Library item belongs to.
        /// </summary>
        [Field(Name = "library", Index = Index.Tokenized)]
        [FieldBridge(typeof (EntityIdFieldBridge))]
        public virtual Library Library { get; set; }


        /// <summary>
        /// Gets or sets media ordering.
        /// </summary>
        public virtual int Ordinal { get; set; }

        [IndexedEmbedded(Depth = 1, Prefix = "label_")]
        [NotAudited]
        public virtual IList<Label> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }
    }
}