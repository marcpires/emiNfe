using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;

namespace criarNfeXML
{
    class criaNFe_entrada
    {

        public conexao cs = new conexao();
        public geraLote lote = new geraLote();

        //funcao para adequar a forma que valores sao exibidos - no padrao da Nfe
        public string valores(int tamanho, int decimais, string valor)
        {
            string value = null;
            if (valor.IndexOf(",") > 0)
            {
                valor = valor.Replace(",", ".");
            }
            else
            {
                valor = valor + ".";
            }

            string[] valor_split = valor.Split('.');
            string valor_esq = valor_split[0];
            string valor_dir = valor_split[1];

            if (decimais == 0)
            {
                value = valor_esq + "." + valor_dir;
                while (value.Length < tamanho)
                {
                    value = value + "0";
                }

            }
            else
            {

                while (valor_dir.Length < decimais)
                {
                    valor_dir = valor_dir + "0";
                }

                if (valor_dir.Length > decimais)
                {
                    valor_dir = valor_dir.Substring(0, decimais);
                }

                if (valor_esq.Length > (tamanho - decimais) - 1)
                {
                    valor_esq = valor_esq.Substring(0, ((tamanho - decimais) - 1));
                }

                value = valor_esq + "." + valor_dir;
            }
            return value;

        }
        // fim da funcao valores

        public string criaNF(string venda)
        {

            string retorno = null;
            string testa_con = cs.conex();
            string tempo_ano = DateTime.Now.Year.ToString();
            string tempo_mes = DateTime.Now.Month.ToString();
            if (Convert.ToInt32(tempo_mes) < 10)
            {
                tempo_mes = "0" + tempo_mes;
            }
            string tempo_dia = DateTime.Now.Day.ToString();
            if (Convert.ToInt32(tempo_dia) < 10)
            {
                tempo_dia = "0" + tempo_dia;
            }
            string tempo = tempo_ano + "-" + tempo_mes + "-" + tempo_dia;
            string tempo_chave = tempo_ano.Substring(2, 2) + tempo_mes;

            SqlDataReader dados_nf = null;
            SqlDataReader dados_emit = null;
            SqlDataReader dados_itens = null;
            SqlDataReader update_nf = null;
            SqlDataReader update_nota = null;
            string xProd;
            string pPis = null;
            string pCofins = null;
            SqlDataReader aliqs_read = null;
            //SqlDataReader cidcli_read = null;
            //SqlDataReader cidemit_read = null;

            int nItem = 0;

            string con = cs.conn2();
            SqlConnection nova_con = new SqlConnection(con);
            nova_con.Open();
            using (nova_con)
            {


                // definição dos data readers


                //SqlCommand comando_nf = new SqlCommand("select a.*,b.cliente as cli_nome,b.endereco as cli_end,b.numero as cli_numero, b.bairro as cli_bairro, b.cidade as cli_cidade,b.estado as cli_estado,b.cep as cli_cep,b.inscest as cli_ie from nfe_entrada a inner join usuarios b on a.cnpj_dest = b.cgc where a.documento='" + venda + "'", nova_con);
                SqlCommand comando_nf = new SqlCommand("SELECT a.*, b.CLIENTE AS cli_nome, b.ENDERECO AS cli_end, b.NUMERO AS cli_numero, b.BAIRRO AS cli_bairro, b.CIDADE AS cli_cidade, b.ESTADO AS cli_estado, b.CEP AS cli_cep, b.INSCEST AS cli_ie, c.cod_municipio AS cod_mun FROM nfe_entrada a INNER JOIN usuarios b ON a.cnpj_dest = b.CGC INNER JOIN cod_muni_ibge c ON b.CIDADE = c.nm_municipio where a.documento='" + venda + "'", nova_con);
                dados_nf = comando_nf.ExecuteReader(); // data reader para consultar informações da nota e do destinatario da nota
                dados_nf.Read();

                string xNome = dados_nf["fornec"].ToString().Trim();
                string natOp = dados_nf["natOp"].ToString().Trim();
                string xMun = dados_nf["cidade"].ToString().Trim();
                string xLgr = dados_nf["endereco"].ToString().Trim();
                string nro = dados_nf["numero"].ToString().Trim();
                string xBairro = dados_nf["bairro"].ToString().Trim();
                string UF = dados_nf["UF"].ToString().Trim();
                string CEP = dados_nf["CEP"].ToString().Trim();
                string IE = dados_nf["IE"].ToString().Trim();
                string CPF_CNPJ = dados_nf["cnpj_dest"].ToString().Trim();
                string xLgr_dest = dados_nf["cli_end"].ToString().Trim();
                string nro_dest = dados_nf["cli_numero"].ToString().Trim();
                string xBairro_dest = dados_nf["cli_bairro"].ToString().Trim();
                string xMun_dest = dados_nf["cli_cidade"].ToString().Trim();
                string UF_dest = dados_nf["cli_estado"].ToString().Trim();
                string CEP_dest = dados_nf["cli_cep"].ToString().Trim();
                string vBc = dados_nf["bc_icms"].ToString().Trim();
                string vICMS = dados_nf["vl_icms"].ToString().Trim();
                string vFrete = dados_nf["vl_frete"].ToString().Trim();
                string vSeg = dados_nf["vl_seguro"].ToString().Trim();
                string vOutro = dados_nf["vl_outras"].ToString().Trim();
                string vNf = dados_nf["vl_total_nfe"].ToString().Trim();
                string vProd_totais = dados_nf["vl_total_prod"].ToString().Trim();
                string IE_dest = dados_nf["cli_ie"].ToString().Trim();
                string chave_CNPJ = dados_nf["cnpj_fornec"].ToString().Trim();
                string xNome_dest = dados_nf["cli_nome"].ToString().Trim();
                string cCid_cli = dados_nf["cod_mun"].ToString().Trim();
                string cCid_emit = dados_nf["cod_cidade"].ToString().Trim();
                string vPis_totais = dados_nf["vl_pis"].ToString().Trim();
                string vCofins_totais = dados_nf["vl_cofins"].ToString().Trim();
                dados_nf.Close();

                /*SqlCommand c_emit = new SqlCommand("select cod_municipio from cod_muni_ibge where nm_municipio like '" + xMun + "%' and uf='"+UF+"'", nova_con);
                cidemit_read = c_emit.ExecuteReader();
                cidemit_read.Read();
                MessageBox.Show(xMun);
                cCid_emit = cidemit_read["cod_municipio"].ToString();
                cidemit_read.Close();

                SqlCommand c_cli = new SqlCommand("select cod_municipio from cod_muni_ibge where nm_municipio like '" + xMun_dest + "%' and uf='" + UF_dest + "'", nova_con);
                cidcli_read = c_cli.ExecuteReader();
                cidcli_read.Read();
                cCid_cli = cidcli_read["cod_municipio"].ToString();
                cidcli_read.Close();
                */

                SqlCommand aliqs = new SqlCommand("select * from val_pis_cofins", nova_con);
                aliqs_read = aliqs.ExecuteReader();
                aliqs_read.Read();
                pPis = aliqs_read["pis"].ToString().Trim();
                pCofins = aliqs_read["cofins"].ToString().Trim();
                aliqs_read.Close();

                string chave_UF;
                string chave_AAMM;
                string chave_MOD;
                string chave_SERIE;
                string chave_NRO;
                string chave_COD;
                string chave_TIPO;
                string mod = "55";
                int chave_NRO_int;
                int chave_COD_int;
                int chave_lote_int;


                SqlCommand comando_emit = new SqlCommand("select * from Nfe where cnpj='" + chave_CNPJ + "'", nova_con);
                dados_emit = comando_emit.ExecuteReader(); // data reader para consultar informações sobre o emissor da nota fiscal, numero de nota, lote, serie
                dados_emit.Read();
                chave_NRO_int = Convert.ToInt32(dados_emit["nNf"].ToString());
                chave_COD_int = Convert.ToInt32(dados_emit["cNf"].ToString());
                chave_lote_int = Convert.ToInt32(dados_emit["lote"].ToString());
                chave_UF = dados_emit["cUF"].ToString().Trim();
                chave_SERIE = dados_emit["serie"].ToString().Trim();
                chave_TIPO = dados_emit["serie"].ToString().Trim();
                dados_emit.Close();
                //fim dos data readers

                //criar chave de acesso com dv calculado

                chave_NRO_int = chave_NRO_int + 1;
                chave_NRO = chave_NRO_int.ToString();
                while (chave_NRO.Length < 9)
                {
                    chave_NRO = "0" + chave_NRO;
                }


                chave_COD_int = chave_COD_int + 1;
                chave_COD = chave_COD_int.ToString();
                while (chave_COD.Length < 8)
                {
                    chave_COD = "0" + chave_COD;
                }


                chave_lote_int += 1;
                string chave_lote = chave_lote_int.ToString();


                chave_AAMM = tempo_chave;
                chave_MOD = mod;
                chave_UF = chave_UF.Trim();
                chave_AAMM = chave_AAMM.Trim();
                chave_CNPJ = chave_CNPJ.Trim();
                chave_MOD = chave_MOD.Trim();
                chave_SERIE = chave_SERIE.Trim();
                chave_NRO = chave_NRO.Trim();
                chave_TIPO = chave_TIPO.Trim();
                chave_COD = chave_COD.Trim();
                int tipo = Convert.ToInt32(chave_TIPO);
                int serie = Convert.ToInt32(chave_SERIE);
                chave_TIPO = tipo.ToString();

                string chave_acesso = chave_UF + chave_AAMM + chave_CNPJ + chave_MOD + chave_SERIE + chave_NRO + chave_TIPO + chave_COD;



                char[] arr_chave = chave_acesso.ToCharArray();
                Array.Reverse(arr_chave);

                int inicio = 2;
                int fim = 9;
                int mult = 0;
                int total_soma = 0;
                int soma = 0;
                int modulo;
                int dv;
                string str_mult = "";

                foreach (char digito in arr_chave)
                {
                    if (mult == 0 || mult > fim)
                    {
                        mult = inicio;
                    }
                    string res_ = digito.ToString();
                    int hh = Convert.ToInt32(res_);
                    soma = hh * mult;
                    total_soma = total_soma + soma;
                    str_mult = str_mult + mult.ToString();
                    mult++;
                }

                modulo = total_soma % 11;
                dv = 11 - modulo;

                if (modulo == 0 || modulo == 1)
                {
                    dv = 0;
                }


                chave_acesso = chave_acesso + dv.ToString();

                //fim da chave de acesso

                // criação do xml
                //string caminho = "C:\\inetpub\\wwwroot\\nota_xml\\" + venda + ".xml";
                string caminho = "C:\\Nfe\\nota_xml\\" + venda + ".xml";
                XmlTextWriter writer = new XmlTextWriter(caminho, Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.WriteStartElement("enviNFe");//tag enviNFe
                writer.WriteAttributeString("versao", "2.00");
                writer.WriteAttributeString("xmlns", "http://www.portalfiscal.inf.br/nfe");
                writer.WriteElementString("idLote", chave_lote);//tag idLote
                writer.WriteStartElement("NFe");//tag NFe
                writer.WriteStartElement("infNFe");//tag infNfe
                writer.WriteAttributeString("Id", "NFe" + chave_acesso);
                writer.WriteAttributeString("versao", "2.00");
                writer.WriteStartElement("ide");
                writer.WriteElementString("cUF", chave_UF);
                writer.WriteElementString("cNF", chave_COD);
                writer.WriteElementString("natOp", natOp);
                writer.WriteElementString("indPag", "0");
                writer.WriteElementString("mod", mod);
                writer.WriteElementString("serie", serie.ToString());
                writer.WriteElementString("nNF", chave_NRO_int.ToString());
                writer.WriteElementString("dEmi", tempo);
                writer.WriteElementString("tpNF", chave_TIPO);

                writer.WriteElementString("cMunFG", cCid_emit);
                writer.WriteElementString("tpImp", "1");
                writer.WriteElementString("tpEmis", "1");
                writer.WriteElementString("cDV", dv.ToString());
                writer.WriteElementString("tpAmb", "2");
                writer.WriteElementString("finNFe", "1");
                writer.WriteElementString("procEmi", "3");
                writer.WriteElementString("verProc", "2.1.4");
                writer.WriteEndElement();// fim ide
                //inicio dos dados de quem emite a nota fiscal
                writer.WriteStartElement("emit"); //tag emit
                writer.WriteElementString("CNPJ", chave_CNPJ);
                writer.WriteElementString("xNome", xNome.Trim());
                writer.WriteStartElement("enderEmit");//tag enderEmit
                writer.WriteElementString("xLgr", xLgr.Trim());
                writer.WriteElementString("nro", nro.Trim());
                writer.WriteElementString("xBairro", xBairro.Trim());
                writer.WriteElementString("cMun", cCid_emit.ToString().Trim());

                writer.WriteElementString("xMun", xMun.Trim());
                writer.WriteElementString("UF", UF.Trim());
                writer.WriteElementString("CEP", CEP.Trim());
                writer.WriteElementString("cPais", "1058");
                writer.WriteElementString("xPais", "BRASIL");
                writer.WriteEndElement();//fim enderEmit
                writer.WriteElementString("IE", IE.Trim());
                writer.WriteElementString("CRT", "3");
                writer.WriteEndElement();// fim emit
                //fim dos dados de quem emite a nota fiscal
                //inicio dos dados do cliente para quem se esta emitindo a nota fiscal
                writer.WriteStartElement("dest"); //tag dest
                writer.WriteElementString("CPF", CPF_CNPJ.Trim());
                writer.WriteElementString("xNome", "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL");
                writer.WriteStartElement("enderDest"); //tag enderDest
                writer.WriteElementString("xLgr", xLgr_dest.Trim());
                writer.WriteElementString("nro", nro_dest.Trim());
                writer.WriteElementString("xBairro", xBairro_dest);

                writer.WriteElementString("cMun", cCid_cli.ToString().Trim());
                writer.WriteElementString("xMun", xMun_dest.Trim());
                writer.WriteElementString("UF", UF_dest.Trim());
                writer.WriteElementString("CEP", CEP_dest.Trim());
                writer.WriteElementString("cPais", "1058");
                writer.WriteElementString("xPais", "BRASIL");
                writer.WriteEndElement();//fim enderDest
                if (CPF_CNPJ.Trim().Length < 14)
                {
                    writer.WriteElementString("IE", "ISENTO");
                }
                else
                {
                    writer.WriteElementString("IE", IE_dest.Trim());

                }
                writer.WriteEndElement(); //fim dest
                //fim dos dados do cliente
                SqlCommand comando_itens = new SqlCommand("select * from nfe_itens_entrada where documento='" + venda + "'", nova_con);
                dados_itens = comando_itens.ExecuteReader(); // data reader para consultar os itens da compra

                while (dados_itens.Read()) //enquanto houver produto
                {
                    xProd = dados_itens["descricao"].ToString().Trim();
                    if (xProd.Length > 120)
                    {
                        xProd = xProd.Substring(0, 120);
                    }
                    string qCom = dados_itens["qtde"].ToString() + ".";
                    while (qCom.Length < 6)
                    {
                        qCom = qCom + "0";
                    }
                    string vUnCom = valores(21, 2, dados_itens["vl_unitario"].ToString());
                    string vProd = valores(15, 2, dados_itens["vl_total"].ToString());
                    string qTrib = valores(15, 2, dados_itens["qtde"].ToString());
                    string vUnTrib = valores(21, 2, dados_itens["vl_unitario"].ToString());
                    string origem = dados_itens["origem"].ToString().Trim();
                    string vBCST = valores(15, 2, dados_itens["bc_prod_icms_st"].ToString());
                    string vICMSST = valores(15, 2, dados_itens["vl_prod_icms_st"].ToString());
                    string cst_pis = dados_itens["cst_pis"].ToString().Trim();
                    string cst_cofins = dados_itens["cst_cofins"].ToString().Trim();
                    string vPis = dados_itens["vl_pis"].ToString().Trim();
                    string vCofins = dados_itens["vl_cofins"].ToString().Trim();

                    int cst_int = Convert.ToInt32(dados_itens["cst"].ToString().Trim());
                    string cst = cst_int.ToString().Trim();
                    if (cst_int < 10)
                    {
                        cst = "0" + cst;
                    }


                    nItem += 1;
                    writer.WriteStartElement("det"); //tag det
                    writer.WriteAttributeString("nItem", nItem.ToString());
                    writer.WriteStartElement("prod"); //tag prod
                    writer.WriteElementString("cProd", dados_itens["coditem"].ToString().Trim());
                    writer.WriteStartElement("cEAN"); // tag cEAN
                    writer.WriteEndElement();//fim cEAN
                    writer.WriteElementString("xProd", xProd);
                    writer.WriteElementString("NCM", dados_itens["ncm"].ToString().Trim());
                    writer.WriteElementString("CFOP", dados_itens["cfop"].ToString().Trim());
                    writer.WriteElementString("uCom", dados_itens["unidade"].ToString().Trim());
                    writer.WriteElementString("qCom", qCom);
                    writer.WriteElementString("vUnCom", vUnCom);
                    writer.WriteElementString("vProd", vProd);
                    writer.WriteStartElement("cEANTrib"); // tag cEANTrib
                    writer.WriteEndElement();//fim cEANTrib
                    writer.WriteElementString("uTrib", dados_itens["unidade"].ToString().Trim());
                    writer.WriteElementString("qTrib", qTrib);
                    writer.WriteElementString("vUnTrib", vUnTrib);
                    writer.WriteElementString("indTot", "1");
                    writer.WriteEndElement(); //fim prod
                    writer.WriteStartElement("imposto"); //tag imposto
                    writer.WriteStartElement("ICMS");//tag ICMS
                    //tags novas
                    writer.WriteStartElement("ICMS60"); //tag ICMSSN101
                    writer.WriteElementString("orig", origem);
                    writer.WriteElementString("CST", cst);
                    writer.WriteElementString("vBCSTRet", "0.00");
                    writer.WriteElementString("vICMSSTRet", "0.00");
                    //writer.WriteElementString("vBCST", vBCST);
                    //writer.WriteElementString("vICMSST", vICMSST);
                    writer.WriteEndElement(); // fim ICMS60

                    //fim tags novas
                    //tags mandadas anteriormente
                    //writer.WriteStartElement("ICMSSN101"); //tag ICMSSN101
                    //writer.WriteElementString("orig", "0");
                    //writer.WriteElementString("CSOSN", "101");
                    //writer.WriteElementString("pCredSN", "0.00");
                    //writer.WriteElementString("vCredICMSSN", "0.00");
                    //writer.WriteEndElement();// fim ICMSSN101
                    //fim das tags mandadas anteriomente
                    writer.WriteEndElement(); //fim ICMS
                    //tags novas
                    writer.WriteStartElement("IPI"); //tag IPI
                    writer.WriteElementString("cEnq", "999");
                    writer.WriteStartElement("IPINT"); //tag IPINT
                    writer.WriteElementString("CST", "52");
                    writer.WriteEndElement();//fim IPINT
                    writer.WriteEndElement();//fim IPI 
                    //fim das tags novas
                    //tags novas
                    writer.WriteStartElement("PIS"); //tag PIS
                    if (cst_pis == "08")
                    {
                        writer.WriteStartElement("PISNT"); //tag PISNT
                        writer.WriteElementString("CST", cst_pis);
                        writer.WriteEndElement(); // fim tag PISNT   
                    }
                    if (cst_pis == "01")
                    {
                        writer.WriteStartElement("PISAliq"); //tag PISAliq
                        writer.WriteElementString("CST", cst_pis);
                        writer.WriteElementString("vBC", vProd);
                        writer.WriteElementString("pPIS", valores(5, 2, pPis.ToString()));
                        writer.WriteElementString("vPIS", valores(15, 2, vPis));
                        writer.WriteEndElement();// dim PISAliq
                    }
                    // fim das tags novas
                    //tags antigas
                    //writer.WriteStartElement("PISNT"); //tag PISNT
                    //writer.WriteStartElement("PISAliq"); //tag PISAliq
                    //writer.WriteElementString("CST", "06");
                    //writer.WriteElementString("vBC", vProd);
                    //writer.WriteElementString("pPIS", valores(5,2,pPis.ToString()));
                    //writer.WriteElementString("vPIS", valores(15,2,(Convert.ToDouble(vProd)/pPis).ToString()));
                    //writer.WriteEndElement();// dim PISAliq
                    //writer.WriteEndElement();// dim PISNT
                    //fim tags antigas
                    writer.WriteEndElement();// fim PIS
                    //writer.WriteStartElement("PISST");// tag PISST
                    //writer.WriteElementString("vBC", vProd);
                    //writer.WriteElementString("pPIS", "0.01");
                    //writer.WriteElementString("vPIS", "0.01");
                    //writer.WriteEndElement(); //fim PISST
                    writer.WriteStartElement("COFINS"); //tag COFINS
                    if (cst_cofins == "08")
                    {
                        writer.WriteStartElement("COFINSNT");
                        writer.WriteElementString("CST", cst_cofins);
                        writer.WriteEndElement(); // fim COFINSNT
                    }
                    if (cst_cofins == "01")
                    {
                        writer.WriteStartElement("COFINSAliq"); // tag COFINSAliq
                        writer.WriteElementString("CST", cst_cofins);
                        writer.WriteElementString("vBC", vProd);
                        writer.WriteElementString("pCOFINS", valores(5, 2, pCofins.ToString()));
                        writer.WriteElementString("vCOFINS", valores(15, 2, vCofins.ToString()));
                        writer.WriteEndElement(); // fim COFINSAliq
                    }
                    //tags antigas
                    //writer.WriteStartElement("COFINSNT");
                    //writer.WriteStartElement("COFINSAliq"); // tag COFINSAliq
                    //writer.WriteElementString("CST", "06");
                    //writer.WriteElementString("vBC",vProd);
                    //writer.WriteElementString("pCOFINS", valores(5,2,pCofins.ToString()));
                    //writer.WriteElementString("vCOFINS", valores(15, 2, (Convert.ToDouble(vProd) / pCofins).ToString()));
                    //writer.WriteEndElement(); // fim COFINSAliq
                    //writer.WriteEndElement(); // fim COFINSNT
                    //fim das tags antigas
                    writer.WriteEndElement(); //fiM COFINS
                    //writer.WriteStartElement("COFINSST"); // tag COFINSST
                    //writer.WriteElementString("vBC", vProd);
                    //writer.WriteElementString("pCOFINS", "0.01");
                    //writer.WriteElementString("vCOFINS", "0.01");
                    //writer.WriteEndElement(); //fim COFINSST
                    writer.WriteEndElement(); // fim imposto
                    writer.WriteEndElement();//fim det
                }//fim do while para produtos
                dados_itens.Close();
                writer.WriteStartElement("total"); //tag total
                writer.WriteStartElement("ICMSTot"); //tag ICMSTot
                writer.WriteElementString("vBC", valores(5, 2, vBc));
                writer.WriteElementString("vICMS", valores(5, 2, vICMS));
                writer.WriteElementString("vBCST", "0.00");
                writer.WriteElementString("vST", "0.00");
                writer.WriteElementString("vProd", valores(15, 2, vProd_totais));
                writer.WriteElementString("vFrete", valores(5, 2, vFrete));
                writer.WriteElementString("vSeg", valores(5, 2, vSeg));
                writer.WriteElementString("vDesc", "0.00");
                writer.WriteElementString("vII", "0.00");
                writer.WriteElementString("vIPI", "0.00");
                //writer.WriteElementString("vPIS", valores(5,2,dados_nf["vl_pis"].ToString()));
                //writer.WriteElementString("vCOFINS", valores(5, 2, dados_nf["vl_cofins"].ToString()));
                writer.WriteElementString("vPIS", valores(15, 2, vPis_totais));
                writer.WriteElementString("vCOFINS", valores(15, 2, vCofins_totais));
                writer.WriteElementString("vOutro", valores(5, 2, vOutro));
                writer.WriteElementString("vNF", valores(15, 2, vNf));
                writer.WriteEndElement(); //fim ICMSTot
                writer.WriteEndElement(); //fim total
                writer.WriteStartElement("transp"); //tag transf
                writer.WriteElementString("modFrete", "0");
                writer.WriteEndElement();//fim transf
                writer.WriteEndElement();// fim infNFe
                writer.WriteEndElement(); //fim NFe
                writer.WriteEndElement();//fim enviNFe
                writer.WriteEndDocument();
                writer.Close();


                SqlCommand up_nfe = new SqlCommand("update Nfe set nNf='" + chave_NRO_int.ToString() + "',cNf = '" + chave_COD_int.ToString() + "', lote='" + chave_lote_int.ToString() + "' where cnpj='" + chave_CNPJ + "'", nova_con);
                update_nf = up_nfe.ExecuteReader(); // data reader para consultar os itens da compra
                update_nf.Close();

                SqlCommand up_nota = new SqlCommand("update nfe_entrada set xml='S',nf = '" + chave_NRO_int.ToString() + "', chave='" + chave_acesso + "',dtEmiss=getdate(),lote='" + chave_lote_int.ToString() + "' where documento='" + venda + "'", nova_con);
                update_nota = up_nota.ExecuteReader(); // data reader para consultar os itens da compra
                update_nota.Close();

                retorno = "1";

                cs.fechaConex();
                return retorno;
            }
        }
    }
}