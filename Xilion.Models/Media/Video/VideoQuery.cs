using System;
using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Models.Media.Images;
using Xilion.Framework.Data;
using Xilion.Framework.Extensions;

namespace Xilion.Models.Media.Video
{
    public class VideoQuery : MetaDataQuery<ImageItem>
    {
        public VideoQuery(ICmsContext cmsContext)
            : base(cmsContext)
        {
            AddProperty(Properties.LibraryID);
        }

        public static VideoQuery Default
        {
            get
            {
                return new VideoQuery(CmsContext.Current)
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