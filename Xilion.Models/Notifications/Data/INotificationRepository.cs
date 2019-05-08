using Xilion.Models.Notifications.Domain;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Notifications.Data
{
    public interface INotificationRepository : IRepository<Notification>
    {
    }
}
