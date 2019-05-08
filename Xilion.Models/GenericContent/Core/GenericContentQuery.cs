using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Framework.Data;
using Xilion.Framework.Extensions;
using System;

namespace Xilion.Models.GenericContent.Core
{
    public class GenericContentQuery : MetaDataQuery<GenericContent>
    {
        public GenericContentQuery(ICmsContext cmsContext) : base(cmsContext)
        {
            AddProperty(Properties.PageID);
            Sorting = new SortingInfo("Title", SortOrder.Descending);
            Paging = new PagerInfo(1, 20);
        }

        public static GenericContentQuery Default
        {
            get { return new GenericContentQuery(CmsContext.Current); }
        }

        public string Title
        {
            get { return GetValue<string>(Properties.Title); }
            set { SetValue(Properties.Title, value); }
        }


        public long? PageId
        {
            get
            {
                var value = GetValue<string>(Properties.PageID);
                return value.Islong() ? long.Parse(value) : (long?)null;
            }
        }
        #region Nested type: Properties

        private static class Properties
        {
            public const string Title = "MetaData.Title";
            public const string PageID = "Page";
        }

        #endregion
    }
}