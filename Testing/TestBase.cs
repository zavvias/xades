﻿using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Testing
{
    public class TestBase
    {
        protected static string BasePath
        {
            get { return Environment.CurrentDirectory; }
        }

        protected static string BaseFilesPath
        {
            get { return Environment.CurrentDirectory + @"\TestFiles"; }
        }

        protected readonly string _testCertificatePath = BasePath + @"\cert\xadesnettest.p12";

        protected string GetDocumentFromFile(string resultPath)
        {
            if (File.Exists(resultPath))
            {
                return File.ReadAllText(resultPath);
            }
            return string.Empty;
        }

        protected string GetXmlStringFromFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc.OuterXml;
        }

        protected X509Certificate2 GetTestCertificate()
        {
            return new X509Certificate2(_testCertificatePath, "xadesnet");
        }

    }
}
