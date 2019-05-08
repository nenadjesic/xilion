using System;
using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Framework.Data;
using Xilion.Framework.Extensions;

namespace Xilion.Models.Media.Images
{
    public class ImageQuery : MetaDataQuery<ImageItem>
    {
        public ImageQuery(ICmsContext cmsContext) : base(cmsContext)
        {
            AddProperty(Properties.LibraryID);
        }

        public static ImageQuery Default
        {
            get
            {
                return new ImageQuery(CmsContext.Current)
                           {
                               Sorting = new SortingInfo("Ordinal", SortOrder.Ascending),
                               Paging = new PagerInfo(1, 30)
                           };
            }
        }

        public long? LibraryID
        {
            get
            {
                var value = GetValue<string>(Properties.LibraryID);
                return value.Islong() ? long.Parse(value) : (long?) null;
            }
            set { SetValue(Properties.LibraryID, value == null ? string.Empty : value.Value.ToString()); }
        }

        #region Nested type: Properties

        public static class Properties
        {
            public const string LibraryID = "library";
        }

        #endregion
    }
}