using Xilion.Models.Messages.Domain;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Messages.Data.Default
{
    public class ConversationMemberRepository : Repository<ConversationMember>, IConversationMemberRepository
    {
        public ConversationMemberRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}
