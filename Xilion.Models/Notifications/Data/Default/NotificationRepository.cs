using Xilion.Models.Notifications.Domain;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Notifications.Data.Default
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}
