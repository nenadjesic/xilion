using Xilion.Models.Core.Data.Mappings;
using Xilion.Models.Messages.Domain;
using Xilion.Framework.Data.Mappings.Conventions;

namespace Xilion.Models.Messages.Data.Mapping
{
    public class MessageMap : CmsEntityMap<Message>
    {
        public MessageMap()
        {
            Map(x => x.Content);
            References(x => x.Sender);
            References(x => x.Conversation).Cascade.SaveUpdate();

            HasMany(x => x.Attachments)
                .Table(TableNameConvention.Prefix + "Attachment")
                .KeyColumn("MessageID")
                .Cascade.All();
        }
    }
}
