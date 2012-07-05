using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;

namespace criarNfeXML
{
    public partial class confCert : Form
    {
        //string caminho = "C:\\Nfe\\conf\\confcert.xml";
        string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        public confCert()
        {
            InitializeComponent();
            string cnpj;
            string serie;
            
            
            XmlDocument xml = new XmlDocument();
            caminho = caminho + "\\Nfe\\conf\\confcert.xml";
            //if (File.Exists("C:\\Users\\Administrador\\Documents\\Visual Studio 2008\\Projects\\criarNfeXML\\criarNfeXML\\conf\\confdb.xml"))
            if (File.Exists(caminho))
            {
                
                xml.Load(caminho);                
                cnpj = xml["ConfCert"]["cnpj"].InnerText;
                serie = xml["ConfCert"]["serie"].InnerText;
                
                txt_cnpj.Text = cnpj;
                txt_serie.Text = serie;
                
                
            }
            
            
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (txt_serie.Text.Trim().Length > 0 && txt_cnpj.Text.Trim().Length > 0)
            {
                XmlTextWriter writer = new XmlTextWriter(caminho, Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.WriteStartElement("ConfCert");//tag enviNFe
                writer.WriteElementString("cnpj", txt_cnpj.Text);
                writer.WriteElementString("serie", txt_serie.Text);
                writer.WriteStartElement("SignCert");
                writer.WriteAttributeString("id", "Cert1");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Atenção. Todos os itens devem estar preenchidos.");
            }
        }
    }
}
