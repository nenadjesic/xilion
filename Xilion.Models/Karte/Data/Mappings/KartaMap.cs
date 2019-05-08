using Xilion.Models.Core.Data.Mappings;
using Xilion.Models.Karte;

namespace Xilion.Models.Karte.Data.Mappings
{
    public class KartaMap : CmsEntityMap<Karta>
    {
        public KartaMap()
        {
            Map(x => x.Latitude);
            Map(x => x.Longitude);
            References(x => x.Avatar).Nullable();
            HasManyToMany(x => x.Labels).Cascade.All();
        }
    }
}