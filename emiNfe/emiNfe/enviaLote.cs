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
    class enviaLote
    {
        public string envia_Lote(string venda, string tipo) {
            string rec;
            XmlNode no_resp;
            string v_XMLConteudo;
            
            SqlDataReader update_enviado = null;
            conexao cs = new conexao();
            string teste_conexao = cs.conex();
            selChave selchave = new selChave();
            X509Certificate2 chave = null;
            try
            {
                if (teste_conexao == "1")
                {
                     string con = cs.conn2();
                     SqlConnection nova_con = new SqlConnection(con);
                    nova_con.Open();
                    using (nova_con)
                    {
                        chave = selchave.selecionaChave();


                        
                        emiNfe.br.gov.sp.fazenda.nfe.homologacao.NfeRecepcao2 obj = new emiNfe.br.gov.sp.fazenda.nfe.homologacao.NfeRecepcao2();
                        
                        emiNfe.br.gov.sp.fazenda.nfe.homologacao.nfeCabecMsg cabec = new emiNfe.br.gov.sp.fazenda.nfe.homologacao.nfeCabecMsg();

                        //conteudo do xml dentro de uma variavel

                        XmlDocument myXMLDoc1 = new XmlDocument();
                        myXMLDoc1.PreserveWhitespace = true;
                        //myXMLDoc1.Load("C:\\inetpub\\wwwroot\\nota_xml_assinado\\" + venda.Trim() + ".xml");
                        myXMLDoc1.Load("C:\\Nfe\\nota_xml_assinado\\" + venda.Trim() + ".xml");

                        v_XMLConteudo = myXMLDoc1.OuterXml;

                        //Response.Write(v_XMLConteudo);

                        XmlDocument myXMLDoc = new XmlDocument();

                        myXMLDoc.PreserveWhitespace = true;

                        myXMLDoc.LoadXml(v_XMLConteudo);


                        XmlNode xmlDados = null;

                        xmlDados = myXMLDoc.DocumentElement;

                        cabec.cUF = "35";

                        cabec.versaoDados = "2.00";

                        obj.nfeCabecMsgValue = cabec;

                        obj.ClientCertificates.Add(chave);

                        //myXMLDoc.Save("C:\\inetpub\\wwwroot\\envLoteNfe\\teste82048440.xml");
                        no_resp = obj.nfeRecepcaoLote2(xmlDados);
                        rec = no_resp["infRec"]["nRec"].InnerText;

                        SqlCommand update_envio = null;
                        if (tipo == "saida")
                        {
                            update_envio = new SqlCommand("update nfe_saida set enviado='S',recibo='" + rec + "' where documento='" + venda + "'", nova_con);
                        }
                        else if (tipo == "entrada") {
                            update_envio = new SqlCommand("update nfe_entrada set enviado='S',recibo='" + rec + "' where documento='" + venda + "'", nova_con);
                        }
                        update_enviado = update_envio.ExecuteReader();
                        nova_con.Close();
                        cs.fechaConex();
                        return "1";
                    }

                }
                else {
                    return "0";
                }
            }
            catch(Exception e) {
                string erro = e.ToString();
                return "0";
            }
            
        
        }
    }
}
