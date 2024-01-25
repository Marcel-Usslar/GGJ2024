using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Packages.Utility.LinkXmlExtractor
{
    public class PackagesLinkXmlExtractor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public string TemporaryFolder
        {
            get { return string.Format("{0}/Temporary/", Application.dataPath); }
        }

        public string LinkFilePath
        {
            get { return  string.Format("{0}link.xml", TemporaryFolder); }
        }

        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild(BuildReport report)
        {
            CreateMergedLinkFromPackages();
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            if(File.Exists(LinkFilePath))
                File.Delete(LinkFilePath);
            if(!Directory.GetFiles(TemporaryFolder, "*").Any())
                Directory.Delete(TemporaryFolder);
        }

        private void CreateMergedLinkFromPackages()
        {
            var request = Client.List(false, true);
            do { } while (!request.IsCompleted);
            if (request.Status == StatusCode.Success)
            {
                List<string> xmlPathList = new List<string>();
                foreach (var package in request.Result)
                {
                    var path = package.resolvedPath;
                    xmlPathList.AddRange(Directory.GetFiles(path, "link.xml", SearchOption.AllDirectories).ToList());
                }

                if (xmlPathList.Count <= 0)
                    return;

                var xmlList = xmlPathList.Select(XDocument.Load).ToArray();
            
                var combinedXml = xmlList.First();
                foreach (var xDocument in xmlList.Where(xml => xml != combinedXml))
                {
                    combinedXml.Root.Add(xDocument.Root.Elements());
                }

                if (!Directory.Exists(TemporaryFolder))
                    Directory.CreateDirectory(TemporaryFolder);
                combinedXml.Save(LinkFilePath);
            }
            else if (request.Status >= StatusCode.Failure)
            {
                Debug.LogError(request.Error.message);
            }
        }
    }
}
