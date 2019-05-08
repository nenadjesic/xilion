using System.IO;

namespace Xilion.Models.Content
{
    public class ContentContext
    {
        public string Name { get; set; }
        public FileStream Stream { get; set; } 
    }
}