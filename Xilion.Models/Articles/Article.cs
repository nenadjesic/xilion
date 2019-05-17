using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NHibernate.Envers.Configuration.Attributes;
using Xilion.Models.Classifications;
using Xilion.Models.Core.Domain;
using Xilion.Models.Media;
using Xilion.Models.Media.Images;
using Xilion.Models.Karte;

using Xilion.Models.Media.Documents;

namespace Xilion.Models.Articles
{
    /// <summary>
    ///   Represents Article object.
    /// </summary>
    [Audited]
    public class Article : MetaDataEntity, ILockable, IHaveWorkflow, IAliased
    {
        private IList<Label> _labels = new List<Label>();
        private string _lockedBy = String.Empty;

        /// <summary>
        ///   Gets or sets article title.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        ///   Gets or sets article summary.
        /// </summary>
        public virtual string Summary { get; set; }

        /// <summary>
        ///   Gets or sets article content.
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        ///   Gets or sets article external link.
        /// </summary>
        public virtual string ExternalLink { get; set; }

        /// <summary>
        ///   Gets or sets article source.
        /// </summary>
        public virtual string Source { get; set; }

        /// <summary>
        ///   Gets or sets article author.
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        ///   Gets or sets article thumb, generated from article photography.
        /// </summary>
        [Audited(TargetAuditMode = RelationTargetAuditMode.NotAudited)]
        public virtual ImageItem Thumb { get; set; }

        [Audited(TargetAuditMode = RelationTargetAuditMode.NotAudited)]
        public virtual Library Library { get; set; }

        [Audited(TargetAuditMode = RelationTargetAuditMode.NotAudited)]
        public virtual DocumentItem Document { get; set; }

        [Audited(TargetAuditMode = RelationTargetAuditMode.NotAudited)]
        public virtual Library LibraryDocument { get; set; }

        [Audited(TargetAuditMode = RelationTargetAuditMode.NotAudited)]
        public virtual Karta Karta { get; set; }

        #region IAliased Members

        /// <summary>
        ///   Gets or sets article alias.
        /// </summary>
        public virtual string Alias { get; set; }

        #endregion

        #region IHaveWorkflow Members

        /// <summary>
        ///   Gets or sets date and time when article is published.
        /// </summary>

        public virtual DateTime PublishedOn { get; set; }

        /// <summary>
        ///   Gets or sets date and time when article expires.
        /// </summary>

        public virtual DateTime? ExpiresOn { get; set; }

        /// <summary>
        ///   Gets or sets article status.
        /// </summary>

        public virtual WorkflowStatus Status { get; set; }

        #endregion

        #region ILockable Members

        /// <summary>
        ///   Gets or sets the name of the user that locked this entity, if entity implements <see cref="ILockable" />.
        /// </summary>
        [IgnoreDataMember]
        public virtual string LockedBy
        {
            get { return _lockedBy; }
            set { _lockedBy = value; }
        }

        /// <summary>
        ///   Gets or sets the time stamp when this entity is locked, if entity implements <see cref="ILockable" />.
        /// </summary>
        [IgnoreDataMember]
        public virtual DateTime? LockedOn { get; set; }

        #endregion
      
        #region Implementation of ILabeled


        [NotAudited]
        public virtual IList<Label> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }

        #endregion
    }
}