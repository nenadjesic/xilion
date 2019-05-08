using Xilion.Models.Media.Images;

namespace Xilion.Models.Media.Data.Mappings
{
    public class ImageItemMap : MediaItemMap<ImageItem>
    {
        public ImageItemMap()
        {
            Map(x => x.Width);
            Map(x => x.Height);
            
        }
    }
}