using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Site.Data.Default
{
    public class SiteInfoRepository : Repository<SiteInfo>, ISiteInfoRepository
    {
        public SiteInfoRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }
    }
}