using System;
using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Models.Karte;
using Xilion.Framework.Data;
using Xilion.Framework.Extensions;

namespace Xilion.Models.Karte.Core
{
    public class KartaQuery : MetaDataQuery<Karta>
    {
        private readonly ICmsContext _cmsContext;

        public KartaQuery(ICmsContext cmsContext) : base(cmsContext)
        {
            _cmsContext = cmsContext;
            Sorting = new SortingInfo("Title", SortOrder.Descending);
            SetQ(Properties.Title).SetIsLocalized(true);
            SetQ(Properties.Summary).SetIsLocalized(true);
           AddProperty(Properties.Category);
            Paging = new PagerInfo(1, 20);
        
        }

        private KartaSettings Settings
        {
            get { return (KartaSettings)_cmsContext.GetApplication<KartaApplication>().GetSettings(); }
        }

        public static KartaQuery Default
        {
            get { return new KartaQuery(CmsContext.Current); }
        }

        public string Title
        {
            get { return GetValue<string>(Properties.Title); }
            set { SetValue(Properties.Title,value); }
        }

        public string Summary
        {
            get { return GetValue<string>(Properties.Summary); }
            set { SetValue(Properties.Summary, value); }
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
            public const string Labels = "Labels";
            public const string Category = "label_alias";
        }

        #endregion
    }
}