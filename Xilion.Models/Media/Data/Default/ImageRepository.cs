using System.Linq;
using Xilion.Models.Media.Images;
using Xilion.Framework.Data;
using System.Collections.Generic;

namespace Xilion.Models.Media.Data.Default
{
    public class ImageRepository : MediaItemRepository<ImageItem>, IImageRepository
    {
        public ImageRepository(ISessionBuilder sessionBuilder)
            : base(sessionBuilder)
        {
        }

        #region IImageRepository Members

        public override IQueryable<ImageItem> GetAll()
        {
            return base.GetAll().Where(x => x.Library.LibraryScope == LibraryScope.Users);
        }

        #endregion
    }
}