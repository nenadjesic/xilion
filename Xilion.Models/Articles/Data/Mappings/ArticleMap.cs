using Xilion.Models.Core.Data.Mappings;
using Xilion.Framework.Data.Mappings.Conventions;

namespace Xilion.Models.Articles.Data.Mappings
{
    public class ArticleMap : CmsEntityMap<Article>
    {
        public ArticleMap()
        {
            Map(x => x.ExternalLink).Nullable();
            Map(x => x.Title).Nullable();
            Map(x => x.Content).Nullable();
            Map(x => x.Summary).Nullable();
            References(x => x.Thumb)
                .ForeignKey("FK_Article_ImageItem")
                .Nullable()
                .Cascade.SaveUpdate()
                .Column("ThumbID");
            Map(x => x.Source).Nullable().Length(1000);
            Map(x => x.Author);
            References(x => x.Library)
                .ForeignKey("FK_Article_LibraryDocument")
                .Nullable()
                .Cascade.SaveUpdate()
                .Column("LibraryID");
            References(x => x.Document)
                .ForeignKey("FK_Article_DocumentItem")
                .Nullable()
                .Cascade.SaveUpdate()
                .Column("DocumentID");
            References(x => x.LibraryDocument)
                .ForeignKey("FK_Article_Library")
                .Nullable()
                .Cascade.SaveUpdate()
                .Column("LibraryDocumentID");
            References(x => x.Karta)
               .ForeignKey("FK_Article_Karta")
               .Nullable()
               .Cascade.SaveUpdate()
               .Column("KartaID");
            HasManyToMany(x => x.Labels).Cascade.All();

        }
    }
}