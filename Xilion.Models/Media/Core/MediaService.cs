using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Web;
using Xilion.Models.Core;
using Xilion.Models.Core.Services;
using Xilion.Models.Media.Data;

namespace Xilion.Models.Media.Core
{
    public class MediaService
    {
        private static IHostingEnvironment _hosting;
        protected static MediaSettings Settings
        {
            get { return CmsContext.Current.GetSettings<MediaSettings, MediaApplication>(); }
        }

        public static string GetCollectionPath(Library library)
        {
            return GetCollectionPath(library.Id);
        }

       

        public static string GetCollectionPath(long collectionId)
        {
            //TODO read path from settings
            string configRoot = "~/_content/_media/";
            string root = configRoot.StartsWith("~/") ? _hosting.WebRootPath : configRoot;
            string mediaCollectionRoot = String.Concat(root.TrimEnd('/').TrimEnd('\\'), "\\", collectionId);

            if (!Directory.Exists(mediaCollectionRoot))
                Directory.CreateDirectory(mediaCollectionRoot);

            return mediaCollectionRoot;
        }
    }

    public abstract class MediaService<T> : CmsService<T> where T : MediaItem
    {
        private static IMediaItemRepository<T> _mediaItemRepository;

        protected MediaService(IMediaItemRepository<T> mediaItemRepository)
            : base(mediaItemRepository)
        {
            _mediaItemRepository = mediaItemRepository;
        }
    }
}