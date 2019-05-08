using Xilion.Models.Core.Data.Repositories.Default;
using Xilion.Framework.Data;

namespace Xilion.Models.Articles.Data.Default
{
    public class ArticleRepository : MetaDataEntityRepository<Article>, IArticleRepository
    {
        public ArticleRepository(ISessionBuilder sessionBuilder)
            : base(sessionBuilder)
        {
        }
    }
}