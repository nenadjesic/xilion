using System.Collections.Generic;
using NHibernate.Envers.Configuration.Attributes;
using NHibernate.Search.Attributes;
using Xilion.Models.Classifications;
using Xilion.Framework.Data.Search;

namespace Xilion.Models.Core.Domain
{
    /// <summary>
    ///   A <see cref="MetaDataEntity" /> that can be labeled (categorized, tagged).
    /// </summary>
    public abstract class LabeledEntity : MetaDataEntity, ILabeled
    {
        private IList<Label> _labels = new List<Label>();

        /// <summary>
        ///   Gets the list of labels (tags or categories) applied to this entity.
        /// </summary>
        [Field(Name = "labels", Index = Index.Tokenized)]
        [FieldBridge(typeof (EntityIdListFieldBridge))]
        [NotAudited]
        public virtual IList<Label> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }
    }
}