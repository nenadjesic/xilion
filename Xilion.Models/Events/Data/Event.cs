using System;
using System.Collections.Generic;
using NHibernate.Search.Attributes;
using Xilion.Models.Classifications;
using Xilion.Models.Core.Data;
using Xilion.Models.Core;
using Xilion.Framework.Domain;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Events
{
    /// <summary>
    /// Represents Event object.
    /// </summary>
    [Indexed]
    public class Event : MetaDataEntity, IHaveWorkflow, IAliased
    {
        private IList<EventTeam> _teams = new List<EventTeam>();
        private IList<Label> _labels = new List<Label>();

        /// <summary>
        /// Gets or sets event starting date and time.
        /// </summary>
        [Field(Name = "startson")]
        public virtual DateTime StartsOn { get; set; }

        /// <summary>
        /// Gets or sets event ending date and time.
        /// </summary>
        [Field(Name = "endson")]
        public virtual DateTime EndsOn { get; set; }

        /// <summary>
        /// Gets value that indicates if event is all day event.
        /// </summary>
        public virtual bool AllDayEvent { get; set; }

        /// <summary>
        /// Gets or sets event location.
        /// </summary>
        [Field(Name = "location")]
        public virtual string Location { get; set; }

        /// <summary>
        /// Gets or sets event <see cref="IHaveEvent " /> parent.
        /// </summary>
        [Field(Name = "parent")]
        [FieldBridge(typeof (longFieldBridge))]
        public virtual IHaveEvent Parent { get; set; }

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
        ///   Gets or sets the event external link.
        /// </summary>
        public virtual string ExternalLink { get; set; }

        /// <summary>
        ///   Gets or sets the event live session.
        /// </summary>
        public virtual string SessionId { get; set; }

        /// <summary>
        ///   Gets or sets the session token for event live session.
        /// </summary>
        public virtual string SessionToken { get; set; }

        /// <summary>
        ///   Gets or sets event Users
        /// </summary>
        public virtual IList<EventTeam> EventTeams
        {
            get { return _teams; }
            set { _teams = value; }
        }
        /// <summary>
        /// Gets or sets labels.
        /// </summary>
        public virtual IList<Label> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }

        #region IHaveWorkflow Members

        /// <summary>
        /// Gets or sets date and time when event is published.
        /// </summary>
        public virtual DateTime PublishedOn { get; set; }

        /// <summary>
        /// Gets or sets date and time when event expires.
        /// </summary>
        public virtual DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// Gets or sets event status.
        /// </summary>
        public virtual WorkflowStatus Status { get; set; }

        #endregion

        #region IAliased Members

        /// <summary>
        /// Gets or sets event alias.
        /// </summary>
        public virtual string Alias { get; set; }

        #endregion

    }
}