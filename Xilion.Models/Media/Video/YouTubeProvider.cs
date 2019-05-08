using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web;
using Google.GData.Client;
using Google.GData.YouTube;
using Google.YouTube;


namespace Xilion.Models.Media.Video
{
    public class YouTubeProvider
    {
        private const string DeveloperKey =
            "KEY-KEY-KEY";

        private const string UsersName = "nenad.jesicepi@gmail.com";
        private const string UsersPass = "1234rewq";

        public IEnumerable<VideoItem> Search(YouTubeQuery query)
        {
            var q = query;
            var youTubeQuery = new YouTubeQuery("https://gdata.youtube.com/feeds/api/videos") { Query = q.Query };
            return GetVideos(youTubeQuery);
        }

        /// <summary>
        ///   Return list of videos
        /// </summary>
        /// <param name="q"> </param>
        /// <returns> </returns>
        private static IEnumerable<VideoItem> GetVideos(YouTubeQuery q)
        {
            Google.YouTube.YouTubeRequest request = GetRequest();

            IList<VideoItem> videos = new List<VideoItem>();

            try
            {

                var feed = request.Get<Google.YouTube.Video>(q);
                foreach (Google.YouTube.Video item in feed.Entries)
                {
                    var video = new VideoItem
                    {
                        Extension = "youtube",
                        Duration = Int32.Parse(item.Media.Duration.Seconds),
                        FileName = item.VideoId,
                        CreatedBy = item.Author,
                        Title = item.Title
                    };
                    videos.Add(video);
                }
            }
            catch (GDataRequestException gdre)
            {
                Debug.WriteLine(gdre.Message);
                var response = (HttpWebResponse)gdre.Response;
            }

            return videos;
        }

        /// <summary>
        /// Autorize Users
        /// </summary>
        /// <returns></returns>
        private static YouTubeRequest GetRequest()
        {
            YouTubeRequest request = null;//HttpContext.Current.Session["YouTubeRequest"] as YouTubeRequest;
            if (request == null)
            {
                var settings = new YouTubeRequestSettings("LearningCubes", DeveloperKey, UsersName, UsersPass);

                request = new YouTubeRequest(settings);
                //var session = HttpContext.Current.Session;
               // HttpContext.Current.Session["YouTubeRequest"] = request;
            }
            return request;
        }
    }
}
