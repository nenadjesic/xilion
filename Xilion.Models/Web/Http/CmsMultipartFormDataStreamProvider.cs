using System.Net.Http;
using System.Net.Http.Headers;

namespace Xilion.Models.Web.Http
{
    public class CmsMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CmsMultipartFormDataStreamProvider(string path)
            : base(path)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName)
                           ? headers.ContentDisposition.FileName
                           : "Untitled";
            return name.Replace("\"", string.Empty);
        }
    }
}