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
    class consLote
    {
        public string consultaLote(string documento, string recibo, string tipo) {

            procNfe proc_Nfe = new procNfe();
            SqlDataReader update_cons;
            string cStat;
            string xMotivo;
            string cStat_mae;
            string nProt = null;
            XmlNode resposta;
            string v_XMLConteudo;
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
                       
                        emiNfe.br.gov.sp.fazenda.nfe.homologacao1.NfeRetRecepcao2 obj = new emiNfe.br.gov.sp.fazenda.nfe.homologacao1.NfeRetRecepcao2();
                        emiNfe.br.gov.sp.fazenda.nfe.homologacao1.nfeCabecMsg cabec = new emiNfe.br.gov.sp.fazenda.nfe.homologacao1.nfeCabecMsg();
                        //conteudo do xml dentro de uma variavel
                        v_XMLConteudo = "<?xml version=\"1.0\" encoding=\"utf-8\"?><consReciNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\"><tpAmb>2</tpAmb><nRec>" + recibo + "</nRec></consReciNFe>";

                        XmlDocument myXMLDoc = new XmlDocument();
                        myXMLDoc.PreserveWhitespace = true;
                        myXMLDoc.LoadXml(v_XMLConteudo);

                        XmlNode xmlDados = null;
                        xmlDados = myXMLDoc.DocumentElement;

                        cabec.cUF = "35";
                        cabec.versaoDados = "2.00";
                        obj.nfeCabecMsgValue = cabec;

                        obj.ClientCertificates.Add(chave);
                        resposta = obj.nfeRetRecepcao2(xmlDados);

                        XmlDocument erro = new XmlDocument();
                        erro.PreserveWhitespace = true;
                        erro.LoadXml(resposta.OuterXml.ToString());
                        using(XmlTextWriter w = new XmlTextWriter("C:\\"+recibo+".xml",new UTF8Encoding(false))){
                            erro.WriteTo(w);
                            w.Close();
                        }
                        cStat_mae = resposta["cStat"].InnerText.Trim();


                        switch (cStat_mae)
                        {
                            case "641":
                                {
                                    cStat = resposta["cStat"].InnerText;
                                    xMotivo = resposta["xMotivo"].InnerText;   
                                } break;
                            case "225":
                                {
                                    cStat = resposta["cStat"].InnerText;
                                    xMotivo = resposta["xMotivo"].InnerText;
                                } break;
                            case "104":
                                {
                                    cStat = resposta["protNFe"]["infProt"]["cStat"].InnerText;
                                    xMotivo = resposta["protNFe"]["infProt"]["xMotivo"].InnerText;
                                    if (cStat == "100")
                                    {
                                        nProt = resposta["protNFe"]["infProt"]["nProt"].InnerText;
                                    }
                                    //!= "204" && cStat != "245"
                                    
                                    proc_Nfe.proc_Nfe(documento,resposta);
                                } break;
                            default:
                                {
                                    cStat = resposta["cStat"].InnerText;
                                    xMotivo = resposta["xMotivo"].InnerText;
                                    //cStat = resposta["protNFe"]["infProt"]["cStat"].InnerText;
                                    //xMotivo = resposta["protNFe"]["infProt"]["xMotivo"].InnerText;
                                } break;

                        }

                        SqlCommand update_consulta = null;
                        if (tipo == "saida")
                        {
                            if (cStat_mae == "641")
                            {
                                update_consulta = new SqlCommand("update nfe_saida set cStat = '" + cStat + "', xMotivo = '" + xMotivo + "',nProt='" + nProt + "' where documento = '" + documento + "'", nova_con);
                            }
                            else
                            {
                                update_consulta = new SqlCommand("update nfe_saida set cStat = '" + cStat + "', xMotivo = '" + xMotivo + "',nProt='" + nProt + "' where recibo = '" + recibo + "'", nova_con);
                            }
                        }
                        else if (tipo == "entrada") {
                            update_consulta = new SqlCommand("update nfe_entrada set cStat = '" + cStat + "', xMotivo = '" + xMotivo + "',nProt='" + nProt + "' where recibo = '" + recibo + "'", nova_con);
                        }
                        update_cons = update_consulta.ExecuteReader();

                        update_cons.Close();
                        nova_con.Close();
                        cs.fechaConex();

                        return recibo + " - " + xMotivo;
                    }
                }
                else {
                    return "0";
                }

            }
            catch (Exception e) {
                string erro = e.ToString();
                //return "0"+erro;
                return "0 - Falha na consulta. Tentando novamente, em instantes...";
            
            } 
        }
    }
}
