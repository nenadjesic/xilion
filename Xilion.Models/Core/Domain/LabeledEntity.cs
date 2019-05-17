using System.Collections.Generic;
using NHibernate.Envers.Configuration.Attributes;
using Xilion.Models.Classifications;


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
        [NotAudited]
        public virtual IList<Label> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }
    }
}