using Xilion.Models.Core.Data.Repositories.Default;
using Xilion.Framework.Data;

namespace Xilion.Models.Classifications.Data.Default
{
    public class LabelRepository : AliasedEntityRepository<Label>, ILabelRepository
    {
        public LabelRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}