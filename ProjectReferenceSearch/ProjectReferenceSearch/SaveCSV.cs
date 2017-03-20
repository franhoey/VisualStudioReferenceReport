using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectReferenceSearch
{
    public static class SaveCsv
    {
        public static void SaveToCsv(IEnumerable<ProjectFile> projects)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("\"Project\",\"Assembly\",\"Dll\",\"Version\",\"Nuget\"");
            foreach (var project in projects)
            {
                foreach (var reference in project.GetNonGacReferences())
                {
                    csvBuilder.AppendLine($"\"{project.FileName}\",\"{reference.AssemblyName}\",\"{reference.File}\",\"{reference.Version}\",\"{reference.IsNuget}\"");
                }
            }

            File.WriteAllText("references.csv", csvBuilder.ToString());
        }
    }
}