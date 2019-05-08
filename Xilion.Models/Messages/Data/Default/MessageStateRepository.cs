using Xilion.Models.Messages.Domain;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Messages.Data.Default
{
    public class MessageStateRepository : Repository<MessageState>, IMessageStateRepository
    {
        public MessageStateRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}
