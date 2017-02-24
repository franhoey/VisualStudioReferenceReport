using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectReferenceSearch
{
    public static class FindFiles
    {
        public static IEnumerable<ProjectFile> Search(string folderPath, string extensionFilter)
        {
            var files = Directory.GetFiles(folderPath, extensionFilter);
            foreach (var file in files)
                yield return GetFile(file);

            var subDirectories = Directory.GetDirectories(folderPath);
            foreach (var subDirectory in subDirectories)
            {
                foreach (var file in Search(subDirectory, extensionFilter))
                {
                    yield return file;
                }
            }
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