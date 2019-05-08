using Xilion.Models.Messages.Domain;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Messages.Data.Default
{
    public class ConversationRepository : Repository<Conversation>, IConversationRepository
    {
        public ConversationRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}
