using Xilion.Models.Notifications.Domain;

using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;
using System.Linq;

namespace Xilion.Models.Notifications.Data.Default
{
    public class NotificationStateRepository : Repository<NotificationState>, INotificationStateRepository
    {
        public NotificationStateRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }

        #region Implementation of INotificationStateRepository

        /// <summary>
        /// Set notification as read or unread setting its parameter IsRead to true or false.
        /// </summary>
        /// <param name="Users">Users object.</param>
        /// <param name="notification">Notification object.</param>
        /// <param name="read"></param>
        public void SetRead(Users user, Notification notification, bool read)
        {
            NotificationState state = Query().SingleOrDefault(
                x => x.User.Id == user.Id && x.Notification.Id == notification.Id)
                                      ?? new NotificationState
                                             {
                                                 Notification = notification,
                                                 User = user
                                             };

            state.IsRead = read;
            Save(state);
        }

        #endregion
    }
}