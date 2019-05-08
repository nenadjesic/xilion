using Xilion.Models.Core.Data.Mappings;
using Xilion.Models.Notifications.Data.Mapping.Conventions;
using Xilion.Models.Notifications.Domain;
using Xilion.Framework.Data.Mappings.Conventions;

namespace Xilion.Models.Notifications.Data.Mapping
{
    public class NotificationMap : CmsEntityMap<Notification>
    {
        public NotificationMap()
        {
           
            Map(x => x.Data)
                .Column("Data")
                //.CustomType<NotificationDataType<Notification>>()
                .Length(4001);

            Map(x => x.Definition).Column("DefinitionName")
               .CustomType(typeof(NotificationDefinitionType));


            References(x => x.Sender);

            HasMany(x => x.States)
                .Table(TableNameConvention.Prefix + "NotificationState")
                .KeyColumn("NotificationID")
                .Cascade.All();
        }
    }
}