using Xilion.Models.Core.Data.Repositories.Default;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Site.Data.Default
{
    public class PageRepository : Repository<Page>, IPageRepository
    {
        public PageRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}