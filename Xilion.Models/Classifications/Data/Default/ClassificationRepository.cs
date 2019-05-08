using Xilion.Models.Core.Data.Repositories.Default;
using Xilion.Framework.Data;

namespace Xilion.Models.Classifications.Data.Default
{
    public class ClassificationRepository : AliasedEntityRepository<Classification>, IClassificationRepository
    {
        public ClassificationRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}