using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Icarus.Utilities
{
    /// <summary>
    /// Provides methods for interacting with the file system.
    /// </summary>
    public static class FileUtilities
    {
        /// <summary>
        /// Recursively searches down a directory tree (pre-order) looking for all files that match a search pattern.
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="searchPattern"></param>
        /// <param name="includeTopLevel"></param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> EnumerateFilesRecursively(this DirectoryInfo directoryInfo, string searchPattern = "*", bool includeTopLevel = true)
        {
            if (includeTopLevel)
                foreach (var fileInfos in directoryInfo.EnumerateFiles(searchPattern))
                    yield return fileInfos;

            foreach (var nextLevel in directoryInfo.EnumerateDirectories())
            foreach (var fileInfo in nextLevel.EnumerateFilesRecursively(searchPattern))
                yield return fileInfo;
        }

        /// <summary>
        /// Retrieves the size of all files in a directory, recursing through all children.
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static long GetSize(this DirectoryInfo directoryInfo, string searchPattern = "*")
        {
            return directoryInfo.EnumerateFilesRecursively(searchPattern).Sum(x => x.Length);
        }
        
    }
}