﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using NUnit.Framework;

using XadesNetLib.XmlDsig;

namespace Testing
{
    [TestFixture]
    public class XmlDSigSignTest : TestBase
    {
        private string _filesBasePath = BaseFilesPath + @"\xmldsig\";
        private string _inputPath;
        public XmlDSigSignTest()
        {
            _inputPath = _filesBasePath + "Input.xml";
        }

        [Test]
        public void TestEnvelopedSign()
        {
            X509Certificate2 certificate = GetTestCertificate();
            XmlDocument document = XmlDsigHelper.Sign(_inputPath).Using(certificate).Enveloped().IncludingCertificateInSignature().SignAndGetXml();
            String expected = GetXmlStringFromFile(_filesBasePath + "OutputEnveloped.xml");

            Assert.AreEqual(expected, document.OuterXml);
        }

        [Test]
        public void TestEnvelopingSign()
        {
            X509Certificate2 certificate = GetTestCertificate();
            XmlDocument document = XmlDsigHelper.Sign(_inputPath).Using(certificate).Enveloping().IncludingCertificateInSignature().SignAndGetXml();
           
            String expected = GetXmlStringFromFile(_filesBasePath + "OutputEnveloping.xml");

            Assert.AreEqual(expected, document.OuterXml);
        }
    }
}
