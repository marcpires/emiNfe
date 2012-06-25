using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.IO;
using System.Reflection;


namespace criarNfeXML
{
    public partial class form_pai : Form
    {
        Form1 f1 = new Form1();

        
        public form_pai()
        {
            InitializeComponent();
            f1.MdiParent = this;
            pararToolStripMenuItem.Enabled = false;
            emitirNFeToolStripMenuItem.Enabled = false;

        }

        private void form_pai_Load(object sender, EventArgs e)
        {
            f1.Show();
        }

        private void iniciarTesteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            f1.timerConexao();
            
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pararTesteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f1.timerConexao_stop();
        }

        private void pararToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f1.timerNfe_stop();
            f1.timerAlerta_stop();
        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            selChave chave = new selChave();
            X509Certificate2 key = chave.selecionaChave();
            XmlDocument doc = new XmlDocument();
            //doc.Load("C:\\Nfe\\conf\\confcert.xml");
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\Nfe\\conf\\confcert.xml";
            doc.Load(caminho);
            SignedXml xml = new SignedXml(doc);
            xml.SigningKey = key.PrivateKey;
            try
            {
                Reference reference = new Reference();
                reference.Uri = "#Cert1";
                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                reference.AddTransform(env);
                reference.AddTransform(c14);
                KeyInfo keyinfo = new KeyInfo();
                keyinfo.AddClause(new KeyInfoX509Data(key));
                xml.KeyInfo = keyinfo;
                xml.AddReference(reference);
                xml.ComputeSignature();
                XmlElement xmlDigitalSignature = xml.GetXml();
                emitirNFeToolStripMenuItem.Enabled = true;
                pararToolStripMenuItem.Enabled = true;

            }
            catch {
                MessageBox.Show("Insira chave do certificado digital.");
                emitirNFeToolStripMenuItem.Enabled = false;
                pararToolStripMenuItem.Enabled = false;
                return;
            }

            
            

        }

        private void nFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void confConexaoBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            confBD formConfBd = new confBD();
            formConfBd.MdiParent = this;
            formConfBd.Show();
        }

        private void confCertDigitalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            confCert formConfCert = new confCert();
            formConfCert.MdiParent = this;
            formConfCert.Show();
        }

        private void iniciarTesteDeCertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f1.timerCert();
        }

        private void pararTesteDeCertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f1.timerCert_stop();
        }

        public void fechar() {
            Application.Exit();
        }

        private void emitirNFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f1.timerEntrada();
            f1.timerNfe();
            f1.timerAlerta();
        }
    }
}
