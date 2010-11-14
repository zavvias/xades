﻿using System.Security.Cryptography.Xml;
using System.Xml;
using XadesNetLib.Exceptions;

namespace XadesNetLib.XmlDsig.Signing.Signers
{
    public class EnvelopedSigner : Signer
    {
        protected override void CreateAndAddReferenceTo(SignedXml signedXml, XmlDocument document, string inputPath, string xpathToNodeToSign)
        {
            if (signedXml == null)
            {
                throw new InvalidParameterException("Signed Xml cannot be null");
            }

            var signatureReference = new Reference { Uri = xpathToNodeToSign};
            signatureReference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(signatureReference);
        }
        protected override XmlDocument BuildFinalSignedXmlDocument(XmlDocument inputXml, XmlElement signatureXml)
        {
            var raiz = inputXml.DocumentElement;
            if (raiz != null) raiz.AppendChild(inputXml.ImportNode(signatureXml, true));
            return inputXml;
        }
    }
}