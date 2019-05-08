using Xilion.Models.Messages.Domain;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Messages.Data.Default
{
    public class AttachmentRepository : Repository<Attachment>
    {
        public AttachmentRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}
