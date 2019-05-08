using Xilion.Models.Core.Data.Repositories.Default;
using Xilion.Framework.Data;

namespace Xilion.Models.GenericContent.Data.Default
{
    public class GenericContentRepository : MetaDataEntityRepository<GenericContent>, IGenericContentRepository
    {
        public GenericContentRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}