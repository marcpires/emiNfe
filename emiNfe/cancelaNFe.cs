using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;

namespace criarNfeXML
{
    class cancelaNFe
    {
        public string cancela_NFe(string venda, string tipo) {
            SqlDataReader reader_nf = null;
            string retorno;
            conexao cs = new conexao();
            selChave key = new selChave();
            X509Certificate2 chave;

            try
            {
                chave = key.selecionaChave();
                string test_con = cs.conex();
                if (test_con == "1")
                {
                    string con = cs.conn2();
                    SqlConnection nova_con = new SqlConnection(con);
                    nova_con.Open();
                    using (nova_con)
                    {
                        emiNfe.br.gov.sp.fazenda.nfe.homologacao2.NfeCancelamento2 obj = new emiNfe.br.gov.sp.fazenda.nfe.homologacao2.NfeCancelamento2();
                        emiNfe.br.gov.sp.fazenda.nfe.homologacao2.nfeCabecMsg cabec = new emiNfe.br.gov.sp.fazenda.nfe.homologacao2.nfeCabecMsg();
                        SqlCommand comando = null;
                        if (tipo == "saida")
                        {
                            comando = new SqlCommand("select * from nfe_saida where cancelado = 'aguardando' and dt_cancela is null", nova_con);
                        }else if(tipo == "entrada"){
                            comando = new SqlCommand("select * from nfe_entrada where cancelado = 'aguardando' and dt_cancela is null", nova_con);
                        }
                        reader_nf = comando.ExecuteReader();
                        reader_nf.Read();

                        string chave_nfe = reader_nf["chave"].ToString();
                        string recibo = reader_nf["recibo"].ToString();

                        reader_nf.Close();

                        //string caminho = "C:\\" + venda + ".xml";
                        string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        caminho = caminho + "\\Nfe\\cancNfe\\" + venda + ".xml";

                        XmlTextWriter writer = new XmlTextWriter(caminho, Encoding.UTF8);
                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("cancNFe");
                        writer.WriteAttributeString("versao", "2.00");
                        writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe");
                        writer.WriteStartElement("infCanc");
                        writer.WriteAttributeString("Id", "ID" + chave_nfe);
                        writer.WriteElementString("tpAmb", "2");
                        writer.WriteElementString("xServ", "CANCELAR");
                        writer.WriteElementString("chNFe", chave_nfe);
                        writer.WriteElementString("nProt", recibo);
                        writer.WriteElementString("xJust", "Cancelamento de nfe");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();



                        XmlDocument myXMLDoc1 = new XmlDocument();
                        XmlDocument myXMLDoc = new XmlDocument();

                        myXMLDoc1.PreserveWhitespace = false;

                        //myXMLDoc1.Load("C:\\inetpub\\wwwroot\\nota_xml\\" + venda + ".xml");
                        //myXMLDoc1.Load("C:\\" + venda + ".xml");
                        
                        myXMLDoc1.Load(caminho);

                        //tentando assinar somente o xml da nota
                        SignedXml sign = new SignedXml(myXMLDoc1);
                        sign.SigningKey = chave.PrivateKey;
                        Reference reference = new Reference();
                        reference.Uri = "#ID" + chave_nfe.Trim();
                        XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                        XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                        reference.AddTransform(env);
                        reference.AddTransform(c14);
                        KeyInfo keyinfo = new KeyInfo();
                        keyinfo.AddClause(new KeyInfoX509Data(chave));
                        sign.KeyInfo = keyinfo;

                        sign.AddReference(reference);
                        sign.ComputeSignature();
                        XmlElement xmlDigitalSignature = sign.GetXml();


                        //foreach para assinar a nfe dentro
                        XmlNodeList nodeList = myXMLDoc1.GetElementsByTagName("cancNFe");
                        foreach (XmlNode node in nodeList)
                        {
                            node.AppendChild(myXMLDoc1.ImportNode(xmlDigitalSignature, true));
                        }


                        // Save the signed XML document to a file specified
                        // using the passed string.
                        //using (XmlTextWriter xmltw = new XmlTextWriter("C:\\inetpub\\wwwroot\\nota_xml_assinado\\" + venda + ".xml", new UTF8Encoding(false)))
                        using (XmlTextWriter xmltw = new XmlTextWriter(caminho, new UTF8Encoding(false)))
                        {
                            myXMLDoc1.WriteTo(xmltw);
                            xmltw.Close();
                        }




                        retorno = "1";

                        cs.fechaConex();
                        return retorno;



                    }
                }

                else {
                    retorno = "erro";
                    cs.fechaConex();
                    return retorno;
                }
            }
            catch (Exception e) {
                string erro = e.ToString();
                cs.fechaConex();
                return erro;
            }

        }

    }
}
