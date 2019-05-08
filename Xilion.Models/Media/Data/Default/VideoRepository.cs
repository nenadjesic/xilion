using System.Linq;
using Xilion.Models.Media.Video;
using Xilion.Framework.Data;

namespace Xilion.Models.Media.Data.Default
{
    public class VideoRepository : MediaItemRepository<VideoItem>, IVideoRepository
    {
        public VideoRepository(ISessionBuilder sessionBuilder)
            : base(sessionBuilder)
        {
        }

        #region IVideoRepository Members

        public override IQueryable<VideoItem> GetAll()
        {
            return base.GetAll().Where(x => x.Library.LibraryScope == LibraryScope.Users);
        }

        #endregion
    }
}