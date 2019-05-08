using Xilion.Models.Notifications.Domain;

namespace Xilion.Models.Notifications.Definitions
{
    public class NotificationModel<T> where T : INotificationDefinition
    {
        public T Definition { get; set; }

        public NotificationState State { get; set; }

        public Users Users
        {
            get { return State != null ? State.User : null; }
        }

        /// <summary>
        /// Get sender of notification
        /// </summary>
        public Users Sender
        {
            get { return State != null && State.Notification != null ? State.Notification.Sender : null; }
        }
    }
}
