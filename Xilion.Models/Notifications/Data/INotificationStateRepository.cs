using Xilion.Models.Notifications.Domain;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Notifications.Data
{
    public interface INotificationStateRepository : IRepository<NotificationState>
    {
        void SetRead(Users Users, Notification notification, bool read);
    }
}