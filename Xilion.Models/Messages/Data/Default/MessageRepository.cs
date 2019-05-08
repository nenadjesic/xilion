using Xilion.Models.Messages.Domain;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Messages.Data.Default
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}