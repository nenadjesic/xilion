using System;
using Xilion.Framework.Domain;

namespace Xilion.Models.Notifications.Domain
{
    /// <summary>
    /// Represent notification state.
    /// </summary>
    public class NotificationState : Entity
    {
        /// <summary>
        /// Gets or sets notification.
        /// </summary>
        public virtual Notification Notification { get; set; }

        /// <summary>
        /// Gets or sets reciver for notification.
        /// </summary>
        public virtual Users User { get; set; }

        /// <summary>
        /// Gets or sets is notification read (true or false).
        /// </summary>
        public virtual bool IsRead { get; set; }

        /// <summary>
        /// Gets or sets last change date.
        /// </summary>
        public virtual DateTime ChangeDate { get; set; }
    }
}