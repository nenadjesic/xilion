using System;

namespace Xilion.Framework.Domain
{
    /// <summary>
    /// Represents an entity that tracks who created/updated it and when that happened.
    /// </summary>
    public interface ITrackable
    {
        /// <summary>
        /// Gets or sets the name of the Users that created this entity.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time this entity was created.
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the Users that updated this entity.
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time this entity was last updated.
        /// </summary>
        DateTime UpdatedOn { get; set; }
    }
}