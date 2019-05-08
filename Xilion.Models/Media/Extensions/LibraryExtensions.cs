using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Web;
using Xilion.Models.Core;
using Xilion.Models.Media.Documents;

namespace Xilion.Models.Media.Extensions
{
    public static class LibraryExtensions
    {
        private static readonly DocumentsSettings Settings =
            CmsContext.Current.GetSettings<DocumentsSettings, DocumentsApplication>();
        private static readonly IHostingEnvironment _hosting;
        public static string Directory(this Library library)
        {
            return library.Directory(true);
        }

        public static string Directory(this Library library, bool ensureExists)
        {
            //TODO set value from settings
            string root = "~/_content/_media/".StartsWith("~/")
                              ? _hosting.WebRootPath + "~/_content/_media/"
                              : "~/_content/_media/";

            root = root.TrimEnd('/').TrimEnd('\\');

            string directory = Path.Combine(root, library.Id.ToString());

            if (!System.IO.Directory.Exists(directory) && ensureExists)
                System.IO.Directory.CreateDirectory(directory);

            return directory;
        }
    }
}