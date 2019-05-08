using System.IO;
using Xilion.Models.Media.Images;

namespace Xilion.Models.Media.Extensions
{
    public static class ImageExtensions
    {
        public static string FilePath(this ImageItem image)
        {
            if (image.Library == null)
                return image.FileName;

            return Path.Combine(image.Library.Directory(), image.FileName);
        }

    }
}