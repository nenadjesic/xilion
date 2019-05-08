using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Framework.Data;
using System.Globalization;

namespace Xilion.Models.Classifications.Core
{
    public class ClassificationQuery : MetaDataQuery<Classification>
    {
        private readonly ICmsContext _cmsContext;

        public ClassificationQuery(ICmsContext cmsContext) :base(cmsContext)
        {
            _cmsContext = cmsContext;
            Sorting = new SortingInfo("Name", SortOrder.Descending);
            Sorting = new SortingInfo("Alias", SortOrder.Descending);
            Paging = new PagerInfo(1, 20);
            AddProperty(Properties.ClassificationType);
        }

        public static ClassificationQuery Default
        {
            get { return new ClassificationQuery(CmsContext.Current); }
        }

        public string Name
        {
            get { return GetValue<string>(Properties.Name); }
            set { SetValue(Properties.Name, value); }
        }
        public string Alias
        {
            get { return GetValue<string>(Properties.Alias); }
            set { SetValue(Properties.Alias, value); }
        }
        public string ClassificationType
        {
            get { return GetValue<string>(Properties.ClassificationType); }
            set { SetValue(Properties.ClassificationType, value); }
        }

        #region Nested type: Properties

        private static class Properties
        {
            public const string Name = "MetaData.Name";
            public const string Alias = "Alias";
            public const string ClassificationType = "ClassificationType";
         
        }

        #endregion
    }
}