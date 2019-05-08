using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Xilion.Framework.IO
{
    public class ArchiveHelper
    {
        public static IList<string> Extract(string zipPath, string outputFolder, string password, bool deleteAfter)
        {
            IList<string> files = new List<string>();
            // ensure directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            using (var zip = ZipFile.Read(zipPath))
            {
                zip.Password = password;
                // here, we extract every entry, but we could extract conditionally
                // based on entry name, size, date, checkbox status, etc.  
                foreach (var e in zip)
                {
                    e.Extract(outputFolder, ExtractExistingFileAction.OverwriteSilently);
                    files.Add(e.FileName);
                }
            }
            return files;
        }
    }
}