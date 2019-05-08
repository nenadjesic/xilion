using Xilion.Models.Core.Data.Mappings;
using Xilion.Framework.Data.Mappings.Conventions;
using Xilion.Framework.Data.Types;

namespace Xilion.Models.Site.Data.Mappings
{
    public class PageMap : CmsEntityMap<Page>
    {
        public PageMap()
        {
            Map(x => x.PageType).CustomType(typeof (EnumerationType<PageType>));
            Map(x => x.ExternalUrl).Nullable();
            Map(x => x.Navigable);
            Map(x => x.AllowAnonymous);
            Map(x => x.RequireSSL);
            //Map(x => x.Status);

            References(x => x.SiteInfo);
            References(x => x.InternalRedirect).ForeignKey("FK_Page_InternalRedirect")
                .Nullable()
                .Cascade.SaveUpdate()
                .Column("InternalRedirectID");

            References(x => x.Template)
                .ForeignKey("TemplateID")
                .ForeignKey("FK_SitePage_Template")
                .Nullable();

            References(x => x.Parent)
                .ForeignKey("FK_Page_Parent")
                .Column("ParentID")
                .Nullable();

            HasMany(x => x.Children)
                .KeyColumn("ParentID");

            HasMany(x => x.Widgets)
                .KeyColumn("PageID");

            HasManyToMany(x => x.Resources)
                .Table(TableNameConvention.Prefix + "PageResources")
                .ParentKeyColumn("PageID")
                .ForeignKeyConstraintNames("FK_PageResource_Resource", null)
                .Cascade.SaveUpdate()
                //.Cascade.AllDeleteOrphan()
                .Cache.ReadWrite();
        }
    }
}