using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xilion.Models.Core;
using Xilion.Models.Media.Data;
using Xilion.Models.Media.Extensions;
using Xilion.Models.Media.Video;

namespace Xilion.Models.Media.Core
{
    public class VideoService : MediaService<VideoItem>
    {
        private readonly IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository) : base(videoRepository)
        {
            _videoRepository = videoRepository;
        }

        /// <summary>
        /// Gets settings for VideoApplication.
        /// </summary>
        public VideoSettings Settings
        {
            get { return (VideoSettings) CmsContext.Current.GetApplication<VideoApplication>().GetSettings(); }
        }

        /// <summary>
        ///   Gets all images by their library.
        /// </summary>
        public IList<VideoItem> GetForLibrary(long id)
        {
            return _videoRepository.Query().Where(x => x.Library.Id == id).ToList();
        }

        /// <summary>
        /// Delete video file from file system.
        /// </summary>
        /// <param name="videoItem">VideoItem reference.</param>
        public void DeleteFile(VideoItem videoItem)
        {
            string path = String.Format("{0}/{1}", videoItem.Library.Directory(),
                                        videoItem.FilePath);
            File.Delete(path);
        }

        /// <summary>
        /// Delete VideoItem from persitance.
        /// </summary>
        /// <param name="videoItem">VideoItem reference.</param>
        /// <param name="permanently">Indicates whether the file will be deleted from file system.</param>
        public void Delete(VideoItem videoItem, bool permanently)
        {
            if (permanently)
                DeleteFile(videoItem);

            base.Delete(videoItem);
        }

        /// <summary>
        /// Reordering videos in library.
        /// </summary>
        /// <param name="memberIds">sorted member ids.</param>
        public void Organize(long[] memberIds)
        {
            int index = 0;

            foreach (long memberId in memberIds)
            {
                VideoItem video = GetById(memberId);
                if (video != null)
                {
                    video.Ordinal = index;
                    index++;
                    Save(video);
                }
            }
        }
    }
}