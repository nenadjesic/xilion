using System.Linq;
using Xilion.Models.Site.Data;

namespace Xilion.Models.Site.Core
{
    public class SiteService
    {
        private readonly ISiteInfoRepository _siteInfoRepository;

        public SiteService(ISiteInfoRepository siteInfoRepository)
        {
            _siteInfoRepository = siteInfoRepository;
        }

        // TODO: Create system to read by domain or subdomain and creates site with wizard
        public SiteInfo GetCurrent()
        {
            const string alias = "/";
            SiteInfo site = _siteInfoRepository.Query().SingleOrDefault(x => x.Alias.ToLower() == alias.ToLower());
            if (site == null)
            {
                site = new SiteInfo
                           {
                               Alias = alias,
                               Title = alias
                           };
                _siteInfoRepository.Save(site);
            }
            return site;
        }

        public void SetRoot(Page page)
        {

            SetRoot(page, null);
        }

        public void SetRoot(Page page, SiteInfo siteInfo)
        {
            if (siteInfo == null)
                siteInfo = GetCurrent();

            siteInfo.Root = page;
            _siteInfoRepository.Save(siteInfo);
        }

        public void Save(SiteInfo siteInfo)
        {
            _siteInfoRepository.Save(siteInfo);
        }
    }
}