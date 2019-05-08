using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Xilion.Models.Core;
using Xilion.Models.Media.Data;
using Xilion.Models.Media.Extensions;
using Xilion.Models.Media.Images;

namespace Xilion.Models.Media.Core
{
    public class ImageService : MediaService<ImageItem>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IHostingEnvironment _hosting;
        public ImageService(IImageRepository imageRepository, IHostingEnvironment hosting) : base(imageRepository)
        {
            _imageRepository = imageRepository;
            _hosting = hosting;
        }

        /// <summary>
        /// Gets settings for ImageApplication.
        /// </summary>
        public ImageSettings Settings
        {
            get { return (ImageSettings) CmsContext.Current.GetApplication<ImageApplication>().GetSettings(); }
        }


        /// <summary>
        ///   Gets all images by their library.
        /// </summary>
        public IList<ImageItem> GetForLibrary(long id)
        {
            return _imageRepository.Query().Where(x => x.Library.Id == id).ToList();
        }

        public void Resize(ImageItem imageItem)
        {
            Resize(imageItem, null, null);
        }

        public void Resize(ImageItem imageItem, int? width)
        {
            Resize(imageItem, width, null);
        }

        public Image OriginalImage(ImageItem imageItem)
        {
            return Image.FromFile(imageItem.FilePath());
        }

        /// <summary>
        /// Delete image file from file system and clear cache.
        /// </summary>
        /// <param name="imageEntry">ImageItem reference.</param>
        public void DeleteFile(ImageItem imageEntry)
        {
            var path = String.Format("{0}/{1}", imageEntry.Library.Directory(),
                                           imageEntry.FilePath);
            File.Delete(path);
            ClearCache(imageEntry);
        }

        /// <summary>
        /// Delete ImageItem from persitance.
        /// </summary>
        /// <param name="imageEntry">ImageItem reference.</param>
        /// <param name="permanently">Indicates whether the file will be deleted from file system.</param>
        public void Delete(ImageItem imageEntry, bool permanently)
        {
            base.Delete(imageEntry);

            if (permanently)
                DeleteFile(imageEntry);
        }

        public void Resize(ImageItem item, int? width, int? height)
        {
            string sourcePath = item.FilePath();

            if (item.Width == 0 || item.Height == 0)
            {
                Image image = Image.FromFile(sourcePath);
                item.Width = image.Width;
                item.Height = image.Height;
                Save(item);
                image.Dispose();
            }

            Size size = ImageCache.CalculateImageDimenzions(item.Width, item.Height,
                                                            width.HasValue ? width.Value : 0,
                                                            height.HasValue ? height.Value : 0);

            string cachePath = GetCachedImagePath(item, size);

            // resize and  cache image
            if (!File.Exists(cachePath))
                ImageCache.Resize(sourcePath, size, cachePath);
        }

        public string GetCachedImagePath(ImageItem imageItem, Size size)
        {
            string cacheFileName = String.Format("{0}_{1}_{2}{3}",
                                                 imageItem.Id, size.Width, size.Height,
                                                 Path.GetExtension(imageItem.FilePath));

            return Path.Combine(_hosting.WebRootPath + "~/_cache/" + cacheFileName);
        }

        public string GetCachedImageUrl(ImageItem item, Size size)
        {
            string cacheFileName = String.Format("{0}_{1}_{2}{3}",
                                                 item.Id, size.Width, size.Height,
                                                 Path.GetExtension(item.FilePath));

            return String.Concat(Settings.CachePath.TrimEnd('/'), "/", cacheFileName);
        }

        public string GetCachedImagePath(ImageItem item, int width)
        {
            Size size = ImageCache.CalculateImageDimenzions(item.Width, item.Height, width, 0);
            string cachePath = GetCachedImagePath(item, size);
            if (!File.Exists(cachePath))
                Resize(item, size.Width);

            return GetCachedImagePath(item, size);
        }

        public void ClearCache(ImageItem imageItem)
        {
            string directory = imageItem.Library.Directory();
            string cfgCachePath = Settings.CachePath;
            string[] cachedFiles = Directory.GetFiles(_hosting.WebRootPath + cfgCachePath + imageItem.Id + "*");

            foreach (string cached in cachedFiles)
            {
                File.Delete(Path.Combine(directory, cached));
            }
        }



        /// <summary>
        ///   Gets all images by their library.
        /// </summary>
        public  IQueryable<ImageItem> GetAllImagesByType(string type)
        {
            return _imageRepository.Query().Where(x => x.Library.Type.Name == type);
        }

        /// <summary>
        /// Reordering images in library.
        /// </summary>
        /// <param name="memberIds">sorted member ids.</param>
        public void Organize(long[] memberIds)
        {
            var index = 0;
            
            foreach (long memberId in memberIds)
            {
                var image = GetById(memberId);
                if (image != null)
                {
                    image.Ordinal = index;
                    index++;
                    Save(image);
                }
            }
        }
    }
}