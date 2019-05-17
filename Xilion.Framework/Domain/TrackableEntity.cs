using System;


namespace Xilion.Framework.Domain
{
    /// <summary>
    /// An entity that tracks who created/updated it and when that happened.
    /// </summary>
    public abstract class TrackableEntity : Entity, ITrackable
    {
        #region ITrackable Members

        public virtual string CreatedBy { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual string UpdatedBy { get; set; }

        public virtual DateTime UpdatedOn { get; set; }

        #endregion
    }
}