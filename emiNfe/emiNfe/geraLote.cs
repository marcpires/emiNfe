using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Data.SqlClient;

namespace criarNfeXML
{
    class geraLote
    {
        public string gera_Lote(string venda,string chave_acesso,string tipo) {
            //string erro;
            conexao cs = new conexao();
            SqlDataReader update_assinado = null;
            selChave sel_chave = new selChave();
            X509Certificate2 chave = null;

            try
            {
                chave = sel_chave.selecionaChave();
                string teste_conex = cs.conex();
                if (teste_conex == "1")
                {
                    string con = cs.conn2();
                    SqlConnection nova_con = new SqlConnection(con);
                    nova_con.Open();
                    using (nova_con)
                    {
                        
                        XmlDocument myXMLDoc1 = new XmlDocument();
                        XmlDocument myXMLDoc = new XmlDocument();

                        myXMLDoc1.PreserveWhitespace = false;

                        //myXMLDoc1.Load("C:\\inetpub\\wwwroot\\nota_xml\\" + venda + ".xml");
                        myXMLDoc1.Load("C:\\Nfe\\nota_xml\\" + venda + ".xml");

                        //tentando assinar somente o xml da nota
                        SignedXml sign = new SignedXml(myXMLDoc1);
                        try
                        {
                            sign.SigningKey = chave.PrivateKey;
                            Reference reference = new Reference();
                            reference.Uri = "#NFe" + chave_acesso.Trim();
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
                            XmlNodeList nodeList = myXMLDoc1.GetElementsByTagName("NFe");
                            foreach (XmlNode node in nodeList)
                            {
                                node.AppendChild(myXMLDoc1.ImportNode(xmlDigitalSignature, true));
                            }


                            // Save the signed XML document to a file specified
                            // using the passed string.
                            //using (XmlTextWriter xmltw = new XmlTextWriter("C:\\inetpub\\wwwroot\\nota_xml_assinado\\" + venda + ".xml", new UTF8Encoding(false)))
                            using (XmlTextWriter xmltw = new XmlTextWriter("C:\\Nfe\\nota_xml_assinado\\" + venda + ".xml", new UTF8Encoding(false)))
                            {
                                myXMLDoc1.WriteTo(xmltw);
                                xmltw.Close();
                            }

                            //fim da assinatura da nota
                            SqlCommand up_assinado = null;
                            if (tipo == "saida")
                            {
                                up_assinado = new SqlCommand("update nfe_saida set assinado='S' where documento='" + venda + "'", nova_con);
                            }
                            else if (tipo == "entrada") {
                                up_assinado = new SqlCommand("update nfe_entrada set assinado='S' where documento='" + venda + "'", nova_con);
                            }
                            update_assinado = up_assinado.ExecuteReader();
                            nova_con.Close();
                            cs.fechaConex();
                            return ("1");
                        }
                        catch  {
                            return "0";
                        }
                    }
                }
                else {
                    return "0";
                }


            }
            catch  {
                //erro = e.ToString();
                //return (erro);
                return "0";
                
            }
                
        }
    }
}
