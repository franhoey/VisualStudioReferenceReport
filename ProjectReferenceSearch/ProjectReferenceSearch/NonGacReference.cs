using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace ProjectReferenceSearch
{
    public class NonGacReference
    {
        public static readonly Regex AssemblyNameRegex = new Regex("^[^,]*", RegexOptions.Compiled);
        public static readonly Regex FileRegex = new Regex(@"(?<=\\)[^\\]*$", RegexOptions.Compiled);
        public static readonly Regex NugetRegex = new Regex(@"\\NuGet\\", RegexOptions.Compiled);

        public string AssemblyName { get; }
        public string File { get; }
        public bool IsNuget { get; }

        public NonGacReference(XPathNavigator node, string nameSpaceUri)
        {
            var namespaceManager = new XmlNamespaceManager(node.NameTable);
            namespaceManager.AddNamespace("n", nameSpaceUri);

            AssemblyName = GetValue(node.SelectSingleNode("@Include")?.Value, AssemblyNameRegex);
            File = GetValue(node.SelectSingleNode("n:HintPath", namespaceManager)?.Value, FileRegex);
            IsNuget = Exists(node.SelectSingleNode("n:HintPath", namespaceManager)?.Value, NugetRegex);
        }

        private string GetValue(string nodeValue, Regex parser)
        {
            if (string.IsNullOrEmpty(nodeValue))
                return null;

            var result = parser.Match(nodeValue);
            return result.Success ? result.Value : null;
        }

        private bool Exists(string nodeValue, Regex parser)
        {
            if (string.IsNullOrEmpty(nodeValue))
                return false;

            var result = parser.Match(nodeValue);
            return result.Success ;
        }

        public override string ToString()
        {
            return AssemblyName;
        }
    }
}