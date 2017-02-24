using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace ProjectReferenceSearch
{
    public class ProjectFile
    {
        private const string Namespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        public string FilePath { get; set; }
        public string FileContents { get; set; }

        public string FileName
            => Path.GetFileName(FilePath); 

        public XPathDocument GetXml()
        {
            using (var reader = new StringReader(FileContents))
            {
                return new XPathDocument(
                    new XmlTextReader(reader));
            }
        }

        public IEnumerable<NonGacReference> GetNonGacReferences()
        {
            var doc = GetXml();
            var navigator = doc.CreateNavigator();

            var namespaceManager = new XmlNamespaceManager(navigator.NameTable);
            namespaceManager.AddNamespace("n", Namespace);

            var nodes = navigator.Select("//n:Reference[n:HintPath]", namespaceManager);
            foreach (XPathNavigator node in nodes)
            {
                yield return new NonGacReference(node, Namespace);
            }
        }

        
    }
}