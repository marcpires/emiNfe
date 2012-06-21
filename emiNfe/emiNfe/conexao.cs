using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Text;

namespace criarNfeXML
{
    class conexao
    {
        public string conn() {
            string str_con;
            string user;
            string senha;
            string server;
            string banco;

            XmlDocument xml = new XmlDocument();
            //string str_xml = "C:\\Users\\Administrador\\Documents\\Visual Studio 2008\\Projects\\criarNfeXML\\criarNfeXML\\conf\\confdb.xml";
            string str_xml = "C:\\Nfe\\conf\\confdb.xml";
            if (File.Exists(str_xml))
            {

                xml.Load(str_xml);
                user = xml["ConfDB"]["user"].InnerText;
                senha = xml["ConfDB"]["senha"].InnerText;
                server = xml["ConfDB"]["server"].InnerText;
                banco = xml["ConfDB"]["banco"].InnerText;
                str_con = "user id="+user.Trim()+";" + "password="+senha.Trim()+";server="+server.Trim()+";" + "Trusted_Connection=yes;" + "database="+banco.Trim()+"; " + "connection timeout=30";
            }
            else {
                str_con = "user id=;" + "password=;server="+System.Environment.MachineName+";" + "Trusted_Connection=yes;" + "database=nonexistent336655; " + "connection timeout=30";
            }
            string con = str_con;
            return con;
        }

        public string conn2()
        {
            string str_con;
            string user;
            string senha;
            string server;
            string banco;

            XmlDocument xml = new XmlDocument();
//            string str_xml = "C:\\Users\\Administrador\\Documents\\Visual Studio 2008\\Projects\\criarNfeXML\\criarNfeXML\\conf\\confdb.xml";
            string str_xml = "C:\\Nfe\\conf\\confdb.xml";
            if (File.Exists(str_xml))
            {

                xml.Load(str_xml);
                user = xml["ConfDB"]["user"].InnerText;
                senha = xml["ConfDB"]["senha"].InnerText;
                server = xml["ConfDB"]["server"].InnerText;
                banco = xml["ConfDB"]["banco"].InnerText;
                str_con = "user id=" + user.Trim() + ";" + "password=" + senha.Trim() + ";server=" + server.Trim() + ";" + "Trusted_Connection=yes;" + "database=" + banco.Trim() + "; " + "connection timeout=30";
            }
            else
            {
                str_con = "user id=;" + "password=;server=" + System.Environment.MachineName + ";" + "Trusted_Connection=yes;" + "database=nonexistent336655; " + "connection timeout=30";
            }
            string con = str_con;
            return con;
        }


        public string conex() {
            string resposta = null;
            
            try
            {
                 string con = conn2();
                 SqlConnection nova_con = new SqlConnection(con);
                 nova_con.Open();

                return "1";

            }
            catch (Exception z)
            {

                resposta = (z.ToString() + "erro");
                return "0";

            }
            
        }
        public string fechaConex() {
            string resposta = null;
            try
            {
                 string con = conn();
                SqlConnection nova_con = new SqlConnection(con);
                nova_con.Close();
                return "1";
            }
            catch (Exception t) {
                resposta = (t.ToString() + "erro");
                return "0 - "+resposta;
                
            }
        }
    }
}
