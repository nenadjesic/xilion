using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Filters.Data.Default
{
    public class FilterRepository : Repository<Filter>, IFilterRepository
    {
        public FilterRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}