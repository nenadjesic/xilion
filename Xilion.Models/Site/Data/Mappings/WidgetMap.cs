using FluentNHibernate.Mapping;
using Xilion.Models.Site.Data.Mappings.Conventions;

namespace Xilion.Models.Site.Data.Mappings
{
    public class WidgetMap : ClassMap<Widget>
    {
        public WidgetMap()
        {
            Id(x => x.Id);
            References(x => x.Page).Column("PageID").Nullable();
            References(x => x.Template).Column("TemplateID").Nullable();
            Map(x => x.Section);
            Map(x => x.Title);
            Map(x => x.WidgetData)
                .Column("WidgetData")
                .CustomType<WidgetDataType>()
                .Length(4001);
        }
    }
}