using Xilion.Models.Core.Data.Mappings;
using Xilion.Models.Notifications.Domain;

namespace Xilion.Models.Notifications.Data.Mapping
{
    public class NotificationStateMap : CmsEntityMap<NotificationState>
    {
        public NotificationStateMap()
        {
            Map(x => x.IsRead);
            Map(x => x.ChangeDate);

            References(x => x.Notification);
            References(x => x.User);
        }
    }
}