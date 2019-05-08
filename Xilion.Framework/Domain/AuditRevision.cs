using System;
using NHibernate.Envers.Configuration.Attributes;

namespace Xilion.Framework.Domain
{
    /// <summary>
    /// Represents a revision entity used to save information about the audit revision. It's used internally by
    /// NHibernate.Envers to track revisions.
    /// </summary>
    [RevisionEntity]
    public class AuditRevision
    {
        /// <summary>
        /// Gets or sets the revision id.
        /// </summary>
        [RevisionNumber]
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the revision date.
        /// </summary>
        [RevisionTimestamp]
        public virtual DateTime RevisionDate { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current 
        /// <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; 
        /// otherwise, false.
        /// </returns>
        /// <param name="other">The <see cref="T:System.Object"/> to compare with the current 
        /// <see cref="T:System.Object"/>.</param>
        public override bool Equals(object other)
        {
            if (this == other) return true;

            var auditRevision = other as AuditRevision;
            if (auditRevision == null) return false;

            if (Id != auditRevision.Id) return false;

            return RevisionDate == auditRevision.RevisionDate;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return (int) (31 * Id + (RevisionDate.Ticks ^ (long) ((ulong) RevisionDate.Ticks >> 32)));
        }
    }
}