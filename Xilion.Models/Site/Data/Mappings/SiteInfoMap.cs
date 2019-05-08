using Xilion.Models.Core.Data.Mappings;

namespace Xilion.Models.Site.Data.Mappings
{
    public class SiteInfoMap : CmsEntityMap<SiteInfo>
    {
        public SiteInfoMap()
        {
            References(x => x.Root).Nullable();
            Map(x => x.Title);
        }
    }
}