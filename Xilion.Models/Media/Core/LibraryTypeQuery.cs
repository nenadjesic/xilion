using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Models.Media;
using Xilion.Framework.Data;
using Xilion.Framework.Queries;

namespace Xilion.Models.Media.Core
{
    public class LibraryTypeQuery : WorkflowQuery<LibraryType>
    {
        private readonly ICmsContext _cmsContext;

        public LibraryTypeQuery(ICmsContext cmsContext)
            : base(cmsContext)
        {
            _cmsContext = cmsContext;
            Sorting = new SortingInfo("Title", SortOrder.Descending);
            Paging = new PagerInfo(1, 20);
            SetQ(Properties.Title).SetIsLocalized(true);

        }

        public static LibraryTypeQuery Default
        {
            get { return new LibraryTypeQuery(CmsContext.Current); }
        }

        public string Title
        {
            get { return GetValue<string>(Properties.Title); }
            set { SetValue(Properties.Title, value); }
        }

    

        #region Nested type: Properties

        private static class Properties
        {
            public const string Title = "MetaData.Title";
        }

        #endregion
    }
}