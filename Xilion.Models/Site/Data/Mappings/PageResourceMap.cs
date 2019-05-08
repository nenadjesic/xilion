using FluentNHibernate.Mapping;
using Xilion.Models.Site.Data.Mappings.Conventions;

namespace Xilion.Models.Site.Data.Mappings
{
    public class PageResourceMap : ClassMap<PageResource>
    {
        public PageResourceMap()
        {
            Id(x => x.Id);
            Map(x => x.ResourceData)
                .Column("ResourceData")
                .CustomType<DynamicDataType>()
                .Length(4001);
            Map(x => x.ResourceType);
            Map(x => x.Scope);
        }
    }
}