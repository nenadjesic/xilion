using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Framework.Data;
using Xilion.Framework.Queries;

namespace Xilion.Models.Classifications.Core
{
    public class LabelQuery : MetaDataQuery<Label>
    {
        private readonly ICmsContext _cmsContext;

        public LabelQuery(ICmsContext cmsContext)
            : base(cmsContext)
        {
            _cmsContext = cmsContext;
            Sorting = new SortingInfo("Ordinal", SortOrder.Ascending);
            Paging = new PagerInfo(1, 50);
            AddProperty(Properties.Classification);
            GetProperty(Properties.Name).SetOperator(QueryOperator.StartsWith);
        }

        public static LabelQuery Default
        {
            get { return new LabelQuery(CmsContext.Current); }
        }

        public string Name
        {
            get { return GetValue<string>(Properties.Name); }
            set { SetValue(Properties.Name, value); }
        }

        public string Classification
        {
            get { return GetValue<string>(Properties.Classification); }
            set { SetValue(Properties.Classification, value); }
        }

        #region Nested type: Properties

        private static class Properties
        {
            public const string Name = "MetaData.Name";
            public const string Classification = "Classification";
        }

        #endregion
    }
}