using Xilion.Models.Notifications.Domain;

namespace Xilion.Models.Notifications.Definitions
{
    public interface INotificationDefinition
    {
        NotificationData Data { get; set; }
        NotificationState State { get; set; }
    }
}
