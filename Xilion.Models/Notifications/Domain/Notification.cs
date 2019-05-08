using System.Collections.Generic;
using Xilion.Models.Notifications.Definitions;
using Users = Xilion.Models.Users;
using Xilion.Framework.Domain;

namespace Xilion.Models.Notifications.Domain
{
    public class Notification : TrackableEntity
    {
        private IList<NotificationState> _states = new List<NotificationState>();
        private INotificationDefinition _definition;
        /// <summary>
        /// Gets or sets notifications initiator.
        /// </summary>
        public virtual Users Sender { get; set; }

        /// <summary>
        /// Gets or sets notification state for Users.
        /// </summary>
        public virtual IList<NotificationState> States
        {
            get { return _states; }
            protected set { _states = value; }
        }

        /// <summary>
        /// Gets or sets notification content.
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// Gets or sets notification data.
        /// </summary>
        public virtual NotificationData Data { get; set; }

        public virtual INotificationDefinition Definition
        {
            get { return _definition; }
            set
            {
                if (value != null)
                    value.Data = Data;

                _definition = value;
            }
        }
    }
}