using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Framework.Data;

namespace Xilion.Models.Articles.Core
{
    public class ArticleQuery : WorkflowQuery<Article>
    {
        private readonly ICmsContext _cmsContext;

        public ArticleQuery(ICmsContext cmsContext) : base(cmsContext)
        {
            _cmsContext = cmsContext;
            Sorting = new SortingInfo("CreatedOn", SortOrder.Descending);
            Paging = new PagerInfo(1, Settings.General.PageSize);
            //Paging = new PagerInfo(1, Settings.General.PageSize);
            SetQ(Properties.Title).SetIsLocalized(true);
            SetQ(Properties.Summary).SetIsLocalized(true);
            SetQ(Properties.Content).SetIsLocalized(true);
            AddProperty(Properties.Category);
        }

        private ArticleSettings Settings
        {
            get { return (ArticleSettings) _cmsContext.GetApplication<ArticleApplication>().GetSettings(); }
        }

        public static ArticleQuery Default
        {
            get { return new ArticleQuery(CmsContext.Current); }
        }

        public string Title
        {
            get { return GetValue<string>(Properties.Title); }
            set { SetValue(Properties.Title, value); }
        }

        public string Summary
        {
            get { return GetValue<string>(Properties.Summary); }
            set { SetValue(Properties.Summary, value); }
        }

        public string Content
        {
            get { return GetValue<string>(Properties.Content); }
            set { SetValue(Properties.Content, value); }
        }

        public string Category
        {
            get { return GetValue<string>(Properties.Category); }
            set { SetValue(Properties.Category, value); }
        }

     
        #region Nested type: Properties

        private static class Properties
        {
            public const string Title = "MetaData.Title";
            public const string Summary = "MetaData.Summary";
            public const string Content = "MetaData.Content";
            public const string Labels = "Labels";
            public const string Category = "label_alias";

        }

        #endregion
    }
}