using Xilion.Models.Core.Data.Mappings;

namespace Xilion.Models.Media.Data.Mappings
{
    public class MediaItemMap<T> : CmsEntityMap<T> where T : MediaItem
    {
        public MediaItemMap()
        {
            Map(x => x.Extension);
            Map(x => x.FileName);
            Map(x => x.Ordinal);
            References(x => x.Library);
            HasManyToMany(x => x.Labels).Cascade.All();
        }
    }
}