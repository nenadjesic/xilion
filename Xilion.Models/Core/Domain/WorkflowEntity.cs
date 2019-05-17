using System;


namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// An <see cref="AliasedEntity"/> with the workflow that can be published and expired.
    /// </summary>
    public abstract class WorkflowEntity : AliasedEntity, IHaveWorkflow
    {
        private DateTime _publishedOn = DateTime.Now;
        private WorkflowStatus _status = WorkflowStatus.Draft;

        #region IHaveWorkflow Members

        /// <summary>
        /// Gets or sets the date and time this entity is published.
        /// </summary>
        public virtual DateTime PublishedOn
        {
            get { return _publishedOn; }
            set { _publishedOn = value; }
        }

        /// <summary>
        /// Gets or sets the date and time this entity is expired, or will be expired. If it's null, entity will never 
        /// expire.
        /// </summary>

        public virtual DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// Gets or sets the workflow status of this entity.
        /// </summary>

        public virtual WorkflowStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion
    }
}