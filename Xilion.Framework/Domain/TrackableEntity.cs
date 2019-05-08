using System;
using NHibernate.Search.Attributes;

namespace Xilion.Framework.Domain
{
    /// <summary>
    /// An entity that tracks who created/updated it and when that happened.
    /// </summary>
    public abstract class TrackableEntity : Entity, ITrackable
    {
        #region ITrackable Members

        /// <summary>
        /// Gets or sets the name of the Users that created this entity.
        /// </summary>
        [Field(Name = "createdby")]
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time this entity was created.
        /// </summary>
        [Field(Name = "createdon")]
        public virtual DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the Users that updated this entity.
        /// </summary>
        [Field(Name = "updatedby")]
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time this entity was last updated.
        /// </summary>
        [Field(Name = "updatedon")]
        public virtual DateTime UpdatedOn { get; set; }

        #endregion
    }
}