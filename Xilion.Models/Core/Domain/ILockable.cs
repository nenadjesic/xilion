using System;

namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// Represents an entity that can be locked. Locked entities are unavailable for editing to other Users.
    /// </summary>
    public interface ILockable
    {
        /// <summary>
        /// Gets or sets the name of the Users that has locked this entity.
        /// </summary>
        string LockedBy { get; set; }

        /// <summary>
        /// Gets or sets the date entity is locked.
        /// </summary>
        DateTime? LockedOn { get; set; }
    }
}