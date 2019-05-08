using Xilion.Models.Core.Data.Mappings;

namespace Xilion.Models.GenericContent.Data.Mappings
{
    public class GenericContentMap : CmsEntityMap<GenericContent>
    {
        public GenericContentMap()
        {
            References(x => x.Page);
        }
    }
}