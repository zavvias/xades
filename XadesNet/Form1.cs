﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using XadesNetLib.xmlDsig;
using XadesNetLib.certificates;

namespace XadesNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFirmar_Click(object sender, EventArgs e)
        {

            var documentoXml = new XmlDocument { PreserveWhitespace = false };
            var inputPath = txtFileToSign.Text;
            documentoXml.Load(inputPath);
            var outputPath = txtOutputFile.Text;
            XmlDsig.SignDocument(new XmlDsigSignParameters
                                     {
                                         CertificadoDeFirma = (X509Certificate2)cmbSignCertificate.SelectedItem,
                                         FormatoDeFirma = (XmlDsigSignatureFormat)cmbSignatureFormat.SelectedItem,
                                         IncluirCertificadoEnFirma = true,
                                         PathSalida = outputPath,
                                         XmlDeEntrada = documentoXml
                                     });
            XmlDsig.ValidateDocument(new XmlDsigValidationParameters
                                         {
                                             PathDocumento = outputPath,
                                             ValidarTambienElCertificado = true
                                         });
            MessageBox.Show(@"Signature created and validated successfully :)");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var certificados = Certificates.GetCertificatesFrom(CertificateStore.My);
            cmbSignCertificate.DisplayMember = "Subject";
            cmbSignCertificate.DataSource = certificados;
            var formats = new List<XmlDsigSignatureFormat>
                              {
                                  XmlDsigSignatureFormat.Detached,
                                  XmlDsigSignatureFormat.Enveloped,
                                  XmlDsigSignatureFormat.Enveloping
                              };
            cmbSignatureFormat.DataSource = formats;
        }

        private void btnBrowseFileToSign_Click(object sender, EventArgs e)
        {
            openDialog.ShowDialog();
            txtFileToSign.Text = openDialog.FileName;
        }

        private void btnBrowseOutputFile_Click(object sender, EventArgs e)
        {
            saveDialog.ShowDialog();
            txtOutputFile.Text = saveDialog.FileName;
        }
    }
}