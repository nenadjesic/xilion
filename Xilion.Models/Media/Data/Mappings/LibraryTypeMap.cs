using Xilion.Models.Core.Data.Mappings;

namespace Xilion.Models.Media.Data.Mappings
{
    public class LibraryTypeMap : CmsEntityMap<LibraryType>
    {
        public LibraryTypeMap()
        {
            References(x => x.Owner);
            HasMany(x => x.Library);
        }
    }
}