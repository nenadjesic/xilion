using Xilion.Models.Core.Data.Mappings;
using Xilion.Framework.Data.Mappings.Conventions;

namespace Xilion.Models.Site.Data.Mappings
{
    public class PageTemplateMap : CmsEntityMap<PageTemplate>
    {
        public PageTemplateMap()
        {
            Map(x => x.Content).Length(4001);

            HasMany(x => x.Pages).KeyColumn("TemplateID");
            HasMany(x => x.Widgets)
                .KeyColumn("TemplateID");

            HasManyToMany(x => x.Resources)
               .Table(TableNameConvention.Prefix + "TemplateResources")
               .ParentKeyColumn("TemplateID")
               .ForeignKeyConstraintNames("FK_TemplateResource_Resource", null)
                //.Cascade.AllDeleteOrphan()
               .Cache.ReadWrite();
        }
    }
}