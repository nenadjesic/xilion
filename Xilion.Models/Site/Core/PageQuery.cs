using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Framework.Data;
using Xilion.Framework.Queries;

namespace Xilion.Models.Site.Core
{
    public class PageQuery : WorkflowQuery<Page>
    {
        public PageQuery(ICmsContext cmsContext) : base(cmsContext)
        {
            Sorting = new SortingInfo("Title", SortOrder.Descending);
            Paging = new PagerInfo(1, 20);
            AddProperty(Properties.PageType);
            AddProperty(Properties.AllowAnonymous);
            AddProperty(Properties.RequireSSL);
            AddProperty(Properties.Navigable);
            GetProperty(Properties.Title).SetOperator(QueryOperator.StartsWith);
            GetProperty(Properties.MenuName).SetOperator(QueryOperator.StartsWith);
        }

        public static PageQuery Default
        {
            get { return new PageQuery(CmsContext.Current); }
        }

        public string PageType
        {
            get { return GetValue<string>(Properties.PageType); }
            set { SetValue(Properties.PageType, value); }
        }

        public string Title
        {
            get { return GetValue<string>(Properties.Title); }
            set { SetValue(Properties.Title, value); }
        }

        public string MenuName
        {
            get { return GetValue<string>(Properties.MenuName); }
            set { SetValue(Properties.MenuName, value); }
        }

        public string Description
        {
            get { return GetValue<string>(Properties.Description); }
            set { SetValue(Properties.Description, value); }
        }

        public bool Navigable
        {
            get { return GetValue<bool>(Properties.Navigable); }
            set { SetValue(Properties.Navigable, value); }
        }

        public bool AllowAnonymous
        {
            get { return GetValue<bool>(Properties.AllowAnonymous); }
            set { SetValue(Properties.AllowAnonymous, value); }
        }

        public bool RequireSSL
        {
            get { return GetValue<bool>(Properties.RequireSSL); }
            set { SetValue(Properties.RequireSSL, value); }
        }

        #region Nested type: Properties

        private static class Properties
        {
            public const string Title = "MetaData.Title";
            public const string Description = "MetaData.Description";
            public const string MenuName = "MetaData.MenuName";
            public const string PageType = "PageType";
            public const string Navigable = "Navigable";
            public const string AllowAnonymous = "AllowAnonymous";
            public const string RequireSSL = "RequireSSL";
        }

        #endregion
    }
}