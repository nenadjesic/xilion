using Xilion.Models.Core.Data.Mappings;
using Xilion.Models.Messages.Domain;

namespace Xilion.Models.Messages.Data.Mapping
{
    public class ConversationMemberMap : CmsEntityMap<ConversationMember>
    {
        public ConversationMemberMap()
        {
            References(x => x.Conversation);
            References(x => x.Users);
            Map(x => x.IsLeaved);
        }
    }
}
