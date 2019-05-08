using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Notifications.Data;
using Xilion.Models.Notifications.Definitions;
using Xilion.Models.Notifications.Domain;
using Remotion.Linq;
using System.Web.Mvc;

namespace Xilion.Models.Notifications
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationStateRepository _notificationStateRepository;

        public NotificationService(INotificationRepository notificationRepository,
                                   INotificationStateRepository notificationStateRepository)
        {
            _notificationRepository = notificationRepository;
            _notificationStateRepository = notificationStateRepository;
        }

        /// <summary>
        /// Count all notifications for Users.
        /// </summary>
        /// <param name="Users">Users object.</param>
        /// <returns>Number of all notifications.</returns>
        public int Count(Users Users)
        {
            return GetNotificationQuery(Users, false).Count();
        }

        /// <summary>
        /// Count notification for Users.
        /// </summary>
        /// <param name="Users">Users object.</param>
        /// <param name="ignoreRead">Ignore read notfication or not.</param>
        /// <returns>Number of notifications.</returns>
        public int Count(Users Users, bool ignoreRead)
        {
            return GetNotificationQuery(Users, ignoreRead).Count();
        }

        /// <summary>
        /// Get notifications for Users.
        /// </summary>
        /// <param name="recipient">Users object.</param>
        /// <param name="context">ControllerContext object.</param>
        /// <param name="ignoreRead">If</param>
        /// <returns>List of notifications</returns>
        public IList<Notification> GetNotifications(Users recipient, ControllerContext context, bool ignoreRead)
        {
            var notifications =  GetNotificationQuery(recipient, ignoreRead).ToList();

            foreach (var notification in notifications)
                notification.Content = NotificationTemplate.ExecuteTemplate(context, notification.Definition,
                                                                            notification.States.SingleOrDefault(
                                                                                x => x.User == recipient));

            return notifications;
        }

        /// <summary>
        /// Generate NHibernet linq query for getting all notification for Users.
        /// </summary>
        /// <param name="Users">Users object.</param>
        /// <returns>NHibernate linq query.</returns>
        public IQueryable<Notification> GetNotificationQuery(Users Users)
        {
            return GetNotificationQuery(Users, false);
        } 

        /// <summary>
        /// Generate NHibernet linq query for getting all or unread notification defined by ignoreRead param for Users. 
        /// </summary>
        /// <param name="Users">Users object.</param>
        /// <param name="ignoreRead">Ignore read notfication or not.</param>
        /// <returns>NHibernate linq query.</returns>
        private IQueryable<Notification> GetNotificationQuery(Users Users, bool ignoreRead)
        {
            if(Users == null)
                throw new ArgumentNullException("Users");

            IQueryable<Notification> notifications = null;
            if (ignoreRead)
                notifications =
                    _notificationRepository.Query().Where(x => x.States.Any(y => y.User == Users && !y.IsRead));

            if (notifications == null)
                notifications = _notificationRepository.Query();

            notifications = notifications.Where(x => x.States.Any(y => y.User == Users)).OrderBy(x => x.CreatedOn);

            return notifications;
        }

        /// <summary>
        /// Send notification to all recipients.
        /// </summary>
        /// <param name="sender">Users object.</param>
        /// <param name="definition">INotificationDefinition object.</param>
        /// <param name="recipients">List of recipients for notification.</param>
        public void Notify(Users sender, INotificationDefinition definition, IList<Users> recipients)
        {
            if (sender == null)
                throw new ArgumentNullException("sender");

            if (definition == null)
                throw new ArgumentNullException("definition");

            var notification = new Notification
                                   {
                                       Data = definition.Data,
                                       Definition = definition,
                                       Sender = sender
                                   };

            Notify(notification, recipients);
        }

        /// <summary>
        /// Send notification to all recipients
        /// </summary>
        /// <param name="notification">Notification object.</param>
        /// <param name="recipients">List of recipients for notification.</param>
        public void Notify(Notification notification, IList<Users> recipients)
        {
            if (notification == null)
                throw new ArgumentNullException("notification");

            if (recipients == null || !recipients.Any())
                throw new ArgumentNullException("recipients");

            foreach (Users recipient in recipients)
            {
                var state = new NotificationState
                                {
                                    ChangeDate = DateTime.Now,
                                    IsRead = false,
                                    User = recipient,
                                    Notification = notification
                                };

                notification.States.Add(state);
            }

            _notificationRepository.Save(notification);
        }

        /// <summary>
        /// Set notification as read setting its parameter IsRead to true.
        /// </summary>
        /// <param name="Users">Users object.</param>
        /// <param name="notification">Notification object.</param>
        public void SetRead(Users Users, Notification notification)
        {
            if(Users == null)
                throw new ArgumentNullException("Users");

            if(notification == null)
                throw new ArgumentNullException("notification");

            _notificationStateRepository.SetRead(Users, notification, true);
        }

        /// <summary>
        /// Set notifications as read setting its parameter IsRead to true.
        /// </summary>
        /// <param name="Users">Users object.</param>
        /// <param name="notifications">List of notifications.</param>
        public void SetRead(Users Users, IList<Notification> notifications)
        {
            foreach (Notification notification in notifications)
                _notificationStateRepository.SetRead(Users, notification, true);
        }
    }
}