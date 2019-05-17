using System;
using System.Collections.Generic;
using Xilion.Framework.Domain;
using Xilion.Models.Classifications;

namespace Xilion.Models.Events
{
    /// <summary>
    ///   Represents a subscriber data.
    /// </summary>

    public class EventTeam : TrackableEntity, ILabeled 
    {
        private IList<Xilion.Models.Classifications.Label> _labels = new List<Xilion.Models.Classifications.Label>();



        /// <summary>
        ///   Gets or sets the activity that the Users is subscribed to.
        /// </summary>

        public virtual Event Event { get; set; }

        /// <summary>
        ///   Gets or sets the date and time this subscriber is approved, or null if it's not yet approved.
        /// </summary>
        public virtual DateTime? ApprovedOn { get; set; }

        /// <summary>
        ///   Gets or set the date and time this subscriber is suspended, or null if it's not suspended.
        /// </summary>
        public virtual DateTime? SuspendedOn { get; set; }

        /// <summary>
        ///   Gets or sets the date and time ths subscription for this subscriber starts, or now if it starts immidietaly.
        /// </summary>
        public virtual DateTime? StartsOn { get; set; }

        /// <summary>
        ///   Gets or sets the date and time ths subscription for this subscriber expires, or null if it won't expire.
        /// </summary>
        public virtual DateTime? ExpiresOn { get; set; }

        /// <summary>
        ///   Gets or sets the value indicating whether the subscription for this subsriber is initiated by an invitation.
        /// </summary>
        public virtual bool IsInvited { get; set; }

        /// <summary>
        ///   Gets or sets the subscriber status.
        /// </summary>

        public virtual EventSubscriptionStatus EventSubscriptionStatus { get; set; }

        /// <summary>
        /// Gets or sets labels.
        /// </summary>
        public virtual IList<Classifications.Label> Labels
        {
            get { return _labels; }
            set { _labels = value; }
        }
    }
}