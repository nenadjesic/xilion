using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Site.Data.Default
{
    public class PageTemplateRepository : Repository<PageTemplate>, IPageTemplateRepository
    {
        public PageTemplateRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}