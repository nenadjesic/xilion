using Xilion.Models.Notifications.Domain;
using Xilion.Models.Notifications.Models;


namespace Xilion.Models.Notifications.Definitions
{
    public abstract class NotificationDefinition : INotificationDefinition
    {
        private NotificationState _notificationState;

        protected NotificationDefinition()
        {
            Data = new NotificationData();
        }

        /// <summary>
        /// Gets or sets notification state
        /// </summary>
        public NotificationState State {
            get { return _notificationState; }
            set { _notificationState = value; }
        }

        /// <summary>
        /// Gets or sets Users who initiates the notification
        /// </summary>
        public UsersBasic Users
        {
            get { return State != null ?AutoMapper.Mapper.Map<Users, UsersBasic>(State.User) : null; }
        }

        /// <summary>
        /// Get sender of notification
        /// </summary>
        public UsersBasic Sender
        {
            get { return State != null && State.Notification != null ? AutoMapper.Mapper.Map<Users, UsersBasic>(State.Notification.Sender) : null; }
        }

        #region INotificationDefinition Members

        public NotificationData Data { get; set; }

        #endregion
    }
}