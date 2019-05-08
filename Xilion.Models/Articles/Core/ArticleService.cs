using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Core;
using Xilion.Models.Core.Services;
using Xilion.Models.Articles.Data;

namespace Xilion.Models.Articles.Core
{
    public class ArticleService : CmsService<Article>
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository) : base(articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public ArticleSettings Settings
        {
            get { return (ArticleSettings)CmsContext.Current.GetApplication<ArticleApplication>().GetSettings(); }

        }

        /// <summary>
        ///   Gets list of all articles.
        /// </summary>
        public IList<Article> GetArticles()
        {
            return _articleRepository.GetAll().ToList();
        }

        public override void Save(Article entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Alias))
                entity.Alias = GenerateAlias(entity.Title);
            base.Save(entity);
        }
    }
}