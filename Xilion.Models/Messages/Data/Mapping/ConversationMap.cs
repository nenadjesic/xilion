using Xilion.Models.Core.Data.Mappings;
using Xilion.Models.Messages.Domain;
using Xilion.Framework.Data.Mappings.Conventions;

namespace Xilion.Models.Messages.Data.Mapping
{
    public class ConversationMap : CmsEntityMap<Conversation>
    {
        public ConversationMap()
        {
            HasMany(x => x.Members)
                .Table(TableNameConvention.Prefix + "ConversationMember")
                .KeyColumn("ConversationID")
                .Cascade.All();

            HasMany(x => x.Messages)
                .Table(TableNameConvention.Prefix+ "Message")
                    .KeyColumn("ConversationID")
                    .Cascade.All();
        }
    }
}
