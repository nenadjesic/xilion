using System;
using System.Collections.Generic;
using System.IO;
using System.IO;
using System.Linq;

namespace Xilion.Framework.Extensions
{
    public static class FileSystemExtensions
    {
        /// <summary>
        /// Copy the given directory tree to the given destination.
        /// </summary>
        /// <param name="source">Directory to copy.</param>
        /// <param name="destination">Destination path.</param>
        public static void CopyDirectoryTo(this DirectoryInfo source, string destination)
        {
            Guard.IsNotNull(source, "source");
            Guard.IsNotNull(destination, "destination");
            if (!source.Exists)
                throw new ArgumentException("Source directory does not exists.", "source");

            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            foreach (string filePath in Directory.GetFiles(source.FullName))
            {
                string fileName = Path.GetFileName(filePath);
                if (fileName != null)
                    File.Copy(filePath, Path.Combine(destination, fileName), true);
            }

            foreach (DirectoryInfo subDirectory in source.GetDirectories())
            {
                subDirectory.CopyDirectoryTo(Path.Combine(destination, subDirectory.Name));
            }
        }

        /// <summary>
        /// Gets the directory size for the given directory with all it's sub directories.
        /// </summary>
        /// <param name="directory">Directory to calculate the size for.</param>
        /// <returns>A directory size.</returns>
        public static long GetDirectorySize(this DirectoryInfo directory)
        {
            return directory == null || !directory.Exists
                       ? 0
                       : directory.GetFiles().Sum(file => file.Length) +
                         directory.GetDirectories().Sum(subDirectory => GetDirectorySize(subDirectory));
        }

        /// <summary>
        /// Returns unique name of a file, i.e. dab-1.jpg if dab.jpg exists in the given folder.
        /// </summary>
        /// <param name="path">Path to the file in the file system.</param>
        /// <returns>New file name if the given one exists, or given path if it doesn't exist.</returns>
        public static string GetUniqueFileName(this string path)
        {
            Guard.IsNotNull(path, "path");
            Guard.IsNotNullOrEmpty(path, "path");

            string directory = Path.GetDirectoryName(path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);

            int offset = 1;
            while (File.Exists(path))
            {
                string uniqueFileName = String.Format("{0}-{1}", fileName, offset);
                path = String.Format("{0}\\{1}{2}", directory, uniqueFileName, extension);
                offset++;
            }

            return path;
        }

        /// <summary>
        /// Returns file infoes from a given directory and extensions.
        /// </summary>
        /// <param name="directory"><see cref="Directory"/></param>
        /// <param name="extensions">Extensions search pattern</param>
        /// <returns>List of <see cref="FileInfo"/>s.</returns>
        public static IList<FileInfo> GetDirectoryFiles(this DirectoryInfo directory, string extensions)
        {
            Guard.IsNotNull(directory, "directory");
            Guard.IsNotNullOrEmpty(extensions, "extensions");

            var files = new List<FileInfo>();
            string[] extensionList = extensions.Split(';');

            foreach (string extension in extensionList)
            {
                files.AddRange(directory.GetFiles(extension));
            }

            return files;
        }
    }
}