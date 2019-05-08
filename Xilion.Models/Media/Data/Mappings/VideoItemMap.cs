using Xilion.Models.Media.Video;

namespace Xilion.Models.Media.Data.Mappings
{
    public class VideoItemMap : MediaItemMap<VideoItem>
    {
        public VideoItemMap()
        {
            Map(x => x.Duration);
        }
    }
}