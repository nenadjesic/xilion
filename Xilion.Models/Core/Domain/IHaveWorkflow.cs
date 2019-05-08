using System;

namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// Represents an entity with a workflow that can be published and expired.
    /// </summary>
    public interface IHaveWorkflow
    {
        /// <summary>
        /// Gets or sets the date and time this entity is published.
        /// </summary>
        DateTime PublishedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time this entity is expired, or will be expired. If it's null, entity will never 
        /// expire.
        /// </summary>
        DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// Gets or sets the workflow status of this entity.
        /// </summary>
        WorkflowStatus Status { get; set; }
    }
}