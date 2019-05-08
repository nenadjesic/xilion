using Xilion.Framework;

namespace Xilion.Models.Media
{
    public class MediaType : Enumeration
    {
        public static readonly MediaType Audio = new MediaType(1, "Audio");
        public static readonly MediaType Video = new MediaType(2, "Video");
        public static readonly MediaType Image = new MediaType(3, "Image");
        public static readonly MediaType Document = new MediaType(4, "Document");
        public static readonly MediaType eImage = new MediaType(5, "eImage");

        public MediaType(int value, string displayName) : base(value, displayName)
        {
        }
    }
}