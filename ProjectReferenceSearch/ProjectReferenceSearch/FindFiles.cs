using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectReferenceSearch
{
    public static class FindFiles
    {
        private static string[] _ignoreFolders = { "node_modules" };
        private static Regex _folderName = new Regex(@"(?<=\\)[^\\]*$", RegexOptions.Compiled);

        public static IEnumerable<ProjectFile> Search(string folderPath, string extensionFilter)
        {
            var files = Directory.GetFiles(folderPath, extensionFilter);
            foreach (var file in files)
                yield return GetFile(file);

            var subDirectories = Directory.GetDirectories(folderPath);
            foreach (var subDirectory in subDirectories)
            {
                if(IsIgnoreFolder(subDirectory))
                    continue;

                foreach (var file in Search(subDirectory, extensionFilter))
                {
                    yield return file;
                }
            }
        }

        private static bool IsIgnoreFolder(string folderPath)
        {
            var folderNameMatch = _folderName.Match(folderPath);
            if (!folderNameMatch.Success)
                return false;

            return _ignoreFolders.Contains(folderNameMatch.Value);
        }

        private static ProjectFile GetFile(string path)
        {
            return new ProjectFile()
            {
                FilePath = path,
                FileContents = File.ReadAllText(path)
            };
        }
    }
}