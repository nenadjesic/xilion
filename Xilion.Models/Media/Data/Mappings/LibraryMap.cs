using Xilion.Models.Core.Data.Mappings;

namespace Xilion.Models.Media.Data.Mappings
{
    public class LibraryMap : CmsEntityMap<Library>
    {
        public LibraryMap()
        {
            Map(x => x.LibraryScope);
            Map(x => x.ApplicationName);
            Map(x => x.Type);
            References(x => x.LibraryType).Nullable();
            Map(x => x.Status).Nullable();
            Map(x => x.ArDatum).Nullable();
        }
    }
}