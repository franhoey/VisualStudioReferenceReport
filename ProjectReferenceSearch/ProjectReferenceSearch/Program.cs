using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectReferenceSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var progArgs = new ProgramArguments(args);

            var files = FindFiles.Search(progArgs.SearchPath, "*.csproj")
                .ToList();
            files.AddRange(FindFiles.Search(progArgs.SearchPath, "*.vbproj"));

            SaveCsv.SaveToCsv(files);
        }
    }
}
