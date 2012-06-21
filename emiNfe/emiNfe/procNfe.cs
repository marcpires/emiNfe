using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace criarNfeXML
{
    class procNfe
    {

        public void proc_Nfe(string venda,XmlNode node)
        {
            //XmlTextWriter wr = new XmlTextWriter("C:\\inetpub\\wwwroot\\procNfe\\" + venda + ".xml", Encoding.UTF8);
            XmlTextWriter wr = new XmlTextWriter("C:\\Nfe\\procNfe\\" + venda + ".xml", Encoding.UTF8);
            XmlNode no = null;
            XmlNode no_ = null;
            XmlDocument xml = new XmlDocument();
            XmlDocument xml_ = new XmlDocument();
            
            xml_.PreserveWhitespace = true;
            xml_.LoadXml(node.OuterXml.ToString());
            //xml.Load("C:\\inetpub\\wwwroot\\nota_xml_assinado\\" + venda + ".xml");
            xml.Load("C:\\Nfe\\nota_xml_assinado\\" + venda + ".xml");
            XmlNodeList lista = xml.GetElementsByTagName("NFe");
            XmlNodeList lista2 = xml_.GetElementsByTagName("protNFe");
            foreach (XmlNode x in lista)
            {
                no = x;
            }
            foreach (XmlNode y in lista2)
            {
                no_ = y;
            }

            wr.WriteStartDocument(true);
            wr.WriteStartElement("nfeProc");
            wr.WriteAttributeString("versao", "2.00");
            wr.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe");
            wr.WriteRaw(no.OuterXml.ToString());
            wr.WriteRaw(no_.OuterXml.ToString());
            wr.WriteEndElement();
            wr.WriteEndDocument();
            wr.Close();
            
        }
    }
}
