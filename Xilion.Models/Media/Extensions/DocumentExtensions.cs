using System.IO;
using Xilion.Models.Media.Documents;

namespace Xilion.Models.Media.Extensions
{
    public static class DocumentExtensions
    {
        public static string FilePath(this DocumentItem document)
        {
            return Path.Combine(document.Library.Directory(), document.FileName);
        }
    }
}