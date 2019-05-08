using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Xilion.Models.Media.Core
{
    public class ImageCache
    {
        public static void Resize(string imagePath, Size size, string cachePath)
        {
            using (var fromFile = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                using (Image image = Image.FromStream(fromFile))
                {
                    using (var bitmap = new Bitmap(image, size.Width, size.Height))
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.Clear(Color.Transparent);
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            var rectangle = new Rectangle(0, 0, size.Width, size.Height);
                            graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                            ImageCodecInfo codec =
                                ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == image.RawFormat.Guid);
                            var codecParams = new EncoderParameters(1);
                            codecParams.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                            string dir = Path.GetDirectoryName(cachePath);
                            //TODO: Put this part to less expensive place
                            if (!String.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                                Directory.CreateDirectory(dir);
                            bitmap.Save(cachePath, codec, codecParams);
                        }
                    }
                }
            }
        }

        public static Size CalculateImageDimenzions(int sourceWidth, int sourceHeight, int width, int height)
        {
            float percent = 1;
            if (width == 0 && height > 0)
                percent = (height/(float) sourceHeight);
            if (width > 0 && height == 0)
                percent = (width/(float) sourceWidth);
            if (width > 0 && height > 0)
            {
                float widthPercent = (width/(float) sourceWidth);
                float heightPercent = (height/(float) sourceHeight);
                percent = widthPercent < heightPercent ? widthPercent : heightPercent;
            }

            var resizedWidth = (int) Math.Round(sourceWidth*percent);
            var resizedHeight = (int) Math.Round(sourceHeight*percent);

            if (sourceWidth < resizedWidth && sourceHeight < resizedHeight)
                return new Size(sourceWidth, sourceHeight);

            return new Size(resizedWidth, resizedHeight);
        }
    }
}