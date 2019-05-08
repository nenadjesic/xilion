using Xilion.Models.Core;
using Xilion.Framework.Data;
using Xilion.Framework.Queries;
using System.Globalization;

namespace Xilion.Models.Media.Documents
{
    public class DocumentQuery : MediaItemQuery
    {
        private readonly ICmsContext _cmsContext;

        public DocumentQuery(ICmsContext cmsContext)
            : base(cmsContext)
        {
            _cmsContext = cmsContext;
            Sorting = new SortingInfo("Ordinal", SortOrder.Ascending);
            Paging = new PagerInfo(1, Settings.General.PageSize);
            SetQ(Properties.Title).SetIsLocalized(true);
            AddProperty(Properties.Extension);
            AddProperty(Properties.FileName).SetOperator(QueryOperator.StartsWith);
            AddProperty(Properties.Category);
            
        }

        private DocumentsSettings Settings
        {
            get { return (DocumentsSettings)_cmsContext.GetApplication<DocumentsApplication>().GetSettings(); }
        }

        public static DocumentQuery Default
        {
            get { return new DocumentQuery(CmsContext.Current); }
        }

        public string Title
        {
            get { return GetValue<string>(Properties.Title); }
            set { SetValue(Properties.Title, value); }
        }

        public string FileName
        {
            get { return GetValue<string>(Properties.FileName); }
            set { SetValue(Properties.FileName, value); }
        }
        


        public string Extension
        {
            get { return GetValue<string>(Properties.Extension); }
            set { SetValue(Properties.Extension, value); }
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
            public const string FileName = "filename";
            public const string Extension = "extension";
            public const string Category = "label_alias";
        }
        #endregion
    }
}