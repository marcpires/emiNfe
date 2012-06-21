using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace criarNfeXML
{
    public partial class confBD : Form
    {
        //string caminho = "C:\\Users\\Administrador\\Documents\\Visual Studio 2008\\Projects\\criarNfeXML\\criarNfeXML\\conf\\confdb.xml";
        string caminho = "C:\\Nfe\\conf\\confdb.xml";
        
        public confBD()
        {
            
            InitializeComponent();
            string user;
            string senha;
            string server;
            string banco;
            
            XmlDocument xml = new XmlDocument();
            //if (File.Exists("C:\\Users\\Administrador\\Documents\\Visual Studio 2008\\Projects\\criarNfeXML\\criarNfeXML\\conf\\confdb.xml"))
            if (File.Exists("C:\\Nfe\\conf\\confdb.xml"))
            {
                
                xml.Load(caminho);                
                user = xml["ConfDB"]["user"].InnerText;
                senha = xml["ConfDB"]["senha"].InnerText;
                server = System.Environment.MachineName;
                banco = xml["ConfDB"]["banco"].InnerText;
                txtUser.Text = user;
                txtSenha.Text = senha;
                
                txtBanco.Text = banco;
            }
            
            
        }

        private void btn_sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_salva_Click(object sender, EventArgs e)
        {
            if (txtBanco.Text.Trim().Length > 0 && txtUser.Text.Trim().Length > 0 && txtSenha.Text.Trim().Length > 0)
            {
                XmlTextWriter writer = new XmlTextWriter(caminho, Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.WriteStartElement("ConfDB");//tag enviNFe
                writer.WriteElementString("user", txtUser.Text);
                writer.WriteElementString("senha", txtSenha.Text);
                writer.WriteElementString("server", System.Environment.MachineName);
                writer.WriteElementString("banco", txtBanco.Text);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                this.Close();
            }
            else {
                MessageBox.Show("Atenção. Todos os itens devem estar preenchidos.");
            }
        }
    }
}
