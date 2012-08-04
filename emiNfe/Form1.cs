using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;




namespace criarNfeXML
{
    public partial class Form1 : Form
    {
        string diamesano = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
        string print;
        SqlDataReader reader_lote = null;
        SqlDataReader reader_venda = null;
        SqlDataReader reader_assina = null;
        SqlDataReader reader_envia = null;
        SqlDataReader reader_alerta = null;
        SqlDataReader reader_cancela = null;
        Timer timer_con = new Timer();
        Timer timer_venda = new Timer();
        Timer timer_assina = new Timer();
        Timer timer_envia = new Timer();
        Timer timer_consLote = new Timer();
        Timer timer_alerta = new Timer();
        Timer timer_cert = new Timer();
        Timer timer_entrada = new Timer();
        Timer timer_consLoteEmProc_entrada = new Timer();
        Timer timer_consLoteEmProc_saida = new Timer();

        conexao cs = new conexao();
        criaNFe nova = new criaNFe();
        geraLote lote = new geraLote();
        enviaLote env = new enviaLote();
        consLote cons_lote = new consLote();
        cancelaNFe cancela_nfe = new cancelaNFe();
        string testa_conexao = null;


        public Form1()
        {
            InitializeComponent();
            txt.ScrollBars = ScrollBars.Vertical;
            txt2.ScrollBars = ScrollBars.Vertical;
            txt_alerta.ScrollBars = ScrollBars.Vertical;
            timer_venda.Interval = 9000;
            timer_venda.Enabled = true;
            timer_alerta.Interval = 1000;
            timer_alerta.Enabled = true;
            timer_con.Interval = 7000;
            timer_con.Enabled = true;
            timer_cert.Enabled = true;
            timer_cert.Interval = 5000;
            timer_entrada.Enabled = true;
            timer_entrada.Interval = 7000;
            timer_consLoteEmProc_entrada.Enabled = true;
            timer_consLoteEmProc_entrada.Interval = 100000;
            timer_consLoteEmProc_saida.Enabled = true;
            timer_consLoteEmProc_saida.Interval = 100000;


        }

        public string resp(string resposta)
        {
            string re = null;
            if (resposta == "1")
            {
                re = "OK";
            }
            else if (resposta == "0")
            {
                re = "FALHA";
            }
            return re;
        }


        //public void teste_cert(object sender, EventArgs e) {
        public void teste_cert()
        {
            selChave chave = new selChave();
            X509Certificate2 cha = new X509Certificate2();
            cha = chave.selecionaChave();
            geraLog log = new geraLog();

            if (cha == null)
            {
                txt2.Text = txt2.Text + DateTime.Now + " - Configuração de certificado digital incorreta ou inexistente. Verifique configuração.\r\n";
                print = DateTime.Now + " - Configuração de certificado digital incorreta ou inexistente. Verifique configuração.\r\n";
                log.gera_Log(diamesano, print);


            }
            else
            {
                txt2.Text = txt2.Text + DateTime.Now + " - CERT. DIGITAL OK \r\n";
                print = DateTime.Now + " - CERT. DIGITAL OK \r\n";
                log.gera_Log(diamesano, print);
            }

        }



        //public void teste_conexao(object sender, EventArgs e)
        public void teste_conexao()
        {
            string ss = cs.conn();
            geraLog log = new geraLog();
            using (SqlConnection c = new SqlConnection(ss))
            {
                try
                {
                    c.Open();
                    string mens = "1";
                    txt2.Text = txt2.Text + DateTime.Now + testa_conexao + " - CONEX. " + resp(mens) + "\r\n";
                    print = DateTime.Now + testa_conexao + " - CONEX. " + resp(mens) + "\r\n";
                    log.gera_Log(diamesano, print);
                    c.Close();

                }
                catch
                {
                    string mens = "0";
                    txt2.Text = txt2.Text + DateTime.Now + testa_conexao + " - CONEX. " + resp(mens) + "\r\n";
                    print = DateTime.Now + testa_conexao + " - CONEX. " + resp(mens) + "\r\n";
                    log.gera_Log(diamesano, print);
                }
            }
        }

        public void falha(string documento, SqlConnection con, string tipo, string operacao)
        {
            SqlCommand up = null;
            SqlDataReader reader = null;
            conexao conec = new conexao();
            string conn = conec.conex();
            if (conn == "1")
            {
                string conexao = conec.conn2();
                SqlConnection connection = new SqlConnection(conexao);
                connection.Open();
                using (connection)
                {
                    if (tipo == "saida")
                    {
                        if (operacao == "xml")
                        {
                            up = new SqlCommand("update nfe_saida set cStat='0',xMotivo='FALHA XML',xml='0' where documento='" + documento + "'", connection);
                            reader = up.ExecuteReader();

                        }
                        if (operacao == "assinatura")
                        {
                            up = new SqlCommand("update nfe_saida set cStat='0',xMotivo='FALHA ASSINATURA',assinado='0' where documento='" + documento + "'", connection);
                            reader = up.ExecuteReader();
                            //reader.Close();
                        }
                        if (operacao == "envio")
                        {
                            up = new SqlCommand("update nfe_saida set cStat='0',xMotivo='FALHA ENVIO',enviado='0' where documento='" + documento + "'", connection);
                            reader = up.ExecuteReader();
                            //reader.Close();
                        }
                    }
                    else if (tipo == "entrada")
                    {
                        if (operacao == "xml")
                        {
                            up = new SqlCommand("update nfe_entrada set cStat='0',xMotivo='FALHA XML',xml='0' where documento='" + documento + "'", connection);
                            reader = up.ExecuteReader();
                            //reader.Close();
                        }
                        if (operacao == "assinatura")
                        {
                            up = new SqlCommand("update nfe_entrada set cStat='0',xMotivo='FALHA ASSINATURA',assinado='0' where documento='" + documento + "'", connection);
                            reader = up.ExecuteReader();
                            //reader.Close();
                        }
                        if (operacao == "envio")
                        {
                            up = new SqlCommand("update nfe_entrada set cStat='0',xMotivo='FALHA ENVIO',enviado='0' where documento='" + documento + "'", connection);
                            reader = up.ExecuteReader();
                            //reader.Close();
                        }
                    }
                    reader.Close();
                }
            }
        }
        public void alerta(object sender, EventArgs e)
        {
            conexao cs = new conexao();
            string teste = cs.conex();
            geraLog log = new geraLog();

            if (teste == "1")
            {
                string alerta = null;
                string str_con = cs.conn2();
                SqlConnection con = new SqlConnection(str_con);
                con.Open();
                using (con)
                {

                    SqlCommand c_alerta = new SqlCommand("select * from nfe_saida with(nolock) where cStat<>'100' and cStat <> '110'", con);
                    reader_alerta = c_alerta.ExecuteReader();
                    while (reader_alerta.Read())
                    {
                        alerta = alerta + "Alerta - documento: " + reader_alerta["documento"].ToString() + " - " + reader_alerta["cStat"].ToString() + " - " + reader_alerta["xMotivo"].ToString() + "\r\n";
                        print = "Alerta - documento: " + reader_alerta["documento"].ToString() + " - " + reader_alerta["cStat"].ToString() + " - " + reader_alerta["xMotivo"].ToString() + "\r\n";
                        log.gera_Log(diamesano, print);
                    }
                    txt_alerta.Text = alerta;

                    reader_alerta.Close();
                    con.Close();
                }
            }
        }





        public void consVenda(object sender, EventArgs e)
        {
            string doc = null;
            string tipo = "saida";
            geraLog log = new geraLog();
            testa_conexao = cs.conex();
            if (testa_conexao == "1")
            {
                string con = cs.conn();
                SqlConnection nova_con = new SqlConnection(con);
                nova_con.Open();
                using (nova_con)
                {
                    SqlCommand c_venda = new SqlCommand("select top 1 nfe_saida.*, nfe_itens_saida.documento as doc_itens from nfe_saida with(nolock) left outer join nfe_itens_saida with(nolock) on nfe_saida.documento = nfe_itens_saida.documento where (nfe_saida.chave = '' or nfe_saida.chave is null) and (nfe_saida.nf='' or nfe_saida.nf is null) and (nfe_saida.xml='' or nfe_saida.xml is null) and (nfe_itens_saida.documento is not null)", nova_con);
                    reader_venda = c_venda.ExecuteReader();
                    while (reader_venda.Read())
                    {

                        string resposta = nova.criaNF(reader_venda["documento"].ToString(), tipo);
                        txt.Text = txt.Text + DateTime.Now + " - XML - " + reader_venda["documento"].ToString() + " - " + resp(resposta) + "\r\n";
                        print = DateTime.Now + " - XML - " + reader_venda["documento"].ToString() + " - " + resposta + "\r\n";
                        log.gera_Log(diamesano, print);
                        if (resp(resposta) == "FALHA") {
                            doc = reader_venda["documento"].ToString();
                            falha(doc, nova_con, "saida", "xml");
                            doc = null;
                        }
                        print = null;
                    }
                    reader_venda.Close();
                    SqlCommand c_assina = new SqlCommand("select  top 1 nfe_saida.*, nfe_itens_saida.documento as doc_itens from nfe_saida with(nolock) left outer join nfe_itens_saida with(nolock) on nfe_saida.documento = nfe_itens_saida.documento where (nfe_saida.chave <> '' and nfe_saida.chave is not null) and (nfe_saida.nf<>'') and (nfe_saida.xml='S') and (nfe_saida.assinado is null or nfe_saida.assinado = '') and (nfe_itens_saida.documento is not null)", nova_con);
                    reader_assina = c_assina.ExecuteReader();
                    while (reader_assina.Read())
                    {
                        string resposta_assina = lote.gera_Lote(reader_assina["documento"].ToString(), reader_assina["chave"].ToString(), tipo);
                        print = DateTime.Now + " - ASSINA - " + reader_assina["documento"].ToString() + " - " + resposta_assina + "\r\n";
                        txt.Text = txt.Text + DateTime.Now + " - ASSINA - " + reader_assina["documento"].ToString() + " - " + resp(resposta_assina) + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;
                        if (resp(resposta_assina) == "FALHA")
                        {
                            doc = reader_assina["documento"].ToString();
                            falha(doc, nova_con, "saida", "assinatura");
                            doc = null;
                        }
                    }
                    reader_assina.Close();

                    SqlCommand c_envia = new SqlCommand("select top 1 nfe_saida.*,nfe_itens_saida.documento as doc_itens from nfe_saida with(nolock) left outer join nfe_itens_saida with(nolock) on nfe_saida.documento = nfe_itens_saida.documento where (nfe_saida.chave <> '' and nfe_saida.chave is not null) and (nfe_saida.nf<>'') and (nfe_saida.xml='S') and (nfe_saida.assinado = 'S') and (nfe_saida.enviado is null or nfe_saida.enviado = '') and (nfe_itens_saida.documento is not null)", nova_con);
                    reader_envia = c_envia.ExecuteReader();
                    while (reader_envia.Read())
                    {
                        string resposta_envia = env.envia_Lote(reader_envia["documento"].ToString().Trim(), tipo);
                        txt.Text = txt.Text + DateTime.Now + " - ENVIA - " + reader_envia["documento"].ToString().Trim() + " - " + resp(resposta_envia) + "\r\n";
                        print = DateTime.Now + " - ENVIA - " + reader_envia["documento"].ToString().Trim() + " - " + resposta_envia + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;
                        if (resp(resposta_envia) == "FALHA")
                        {
                            doc = reader_envia["documento"].ToString();
                            falha(doc, nova_con, "saida", "envio");
                            doc = null;
                        }

                    }
                    reader_envia.Close();
                    SqlCommand c_lote = new SqlCommand("select top 1 nfe_saida.*,nfe_itens_saida.documento as doc_itens from nfe_saida with(nolock) left outer join nfe_itens_saida with(nolock) on nfe_saida.documento = nfe_itens_saida.documento where (nfe_saida.chave <> '' and nfe_saida.chave is not null) and (nfe_saida.nf<>'') and (nfe_saida.xml='S') and (nfe_saida.assinado = 'S') and (nfe_saida.enviado = 'S') and (nfe_saida.recibo is not null or nfe_saida.recibo <> '') and (nfe_saida.cStat = '' or nfe_saida.cStat is null) and (nfe_saida.xMotivo = '' or nfe_saida.xMotivo is null) and (nfe_itens_saida.documento is not null)", nova_con);
                    reader_lote = c_lote.ExecuteReader();
                    while (reader_lote.Read())
                    {
                        string resposta_lote = cons_lote.consultaLote(reader_lote["documento"].ToString().Trim(), reader_lote["recibo"].ToString().Trim(), tipo);
                        print = DateTime.Now + " - CONSULTA LOTE - " + resposta_lote + "\r\n";
                        txt.Text = txt.Text + DateTime.Now + " - CONSULTA LOTE - " + resposta_lote + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;

                    }
                    reader_lote.Close();

                    SqlCommand c_cancela = new SqlCommand("select top 1 nfe_saida.*, nfe_itens_saida.documento from nfe_saida with(nolock) left outer join nfe_itens_saida with(nolock) on nfe_saida.documento = nfe_itens_saida.documento where (nfe_saida.chave <> '' and nfe_saida.chave is not null) and (nfe_saida.nf<>'') and (nfe_saida.xml='S') and (nfe_saida.assinado = 'S') and (nfe_saida.enviado = 'S') and (nfe_saida.recibo is not null or nfe_saida.recibo <> '') and (nfe_saida.cStat <> '' or nfe_saida.cStat is not null) and (nfe_saida.xMotivo <> '' or nfe_saida.xMotivo is not null) and (nfe_saida.cancelado = 'aguardando') and (nfe_saida.dt_cancela = '' or nfe_saida.dt_cancela is null) and (nfe_itens_saida.documento is not null)", nova_con);
                    reader_cancela = c_cancela.ExecuteReader();
                    while (reader_cancela.Read())
                    {
                        string resposta_cancela = cancela_nfe.cancela_NFe(reader_cancela["documento"].ToString(), tipo);
                        print = DateTime.Now + " - CANCELA NFE - " + resposta_cancela + "\r\n";
                        txt.Text = txt.Text + DateTime.Now + " - CANCELA NFE - " + resposta_cancela + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;

                    }
                    reader_cancela.Close();

                    cs.fechaConex();
                    nova_con.Close();

                }

            }
            else
            {
                print = DateTime.Now + " - Conexao com o banco de dados malsucedida. Verifique configuração.";
                txt.Text = print;
                log.gera_Log(diamesano, print);
                print = null;
            }
        }

        public void consLoteEmProc_saida(object sender, EventArgs e)
        {

            string tipo = "saida";
            geraLog log = new geraLog();
            testa_conexao = cs.conex();
            if (testa_conexao == "1")
            {
                string con = cs.conn();
                SqlConnection nova_con = new SqlConnection(con);
                nova_con.Open();
                using (nova_con)
                {
                    SqlCommand c_lote = new SqlCommand("select top 1 nfe_saida.*,nfe_itens_saida.documento as doc_itens from nfe_saida with(nolock) left outer join nfe_itens_saida with(nolock) on nfe_saida.documento = nfe_itens_saida.documento where (nfe_saida.chave <> '' and nfe_saida.chave is not null) and (nfe_saida.nf<>'') and (nfe_saida.xml='S') and (nfe_saida.assinado = 'S') and (nfe_saida.enviado = 'S') and (nfe_saida.recibo is not null or nfe_saida.recibo <> '') and (nfe_saida.cStat = '105') and (nfe_itens_saida.documento is not null)", nova_con);
                    reader_lote = c_lote.ExecuteReader();
                    while (reader_lote.Read())
                    {
                        string resposta_lote = cons_lote.consultaLote(reader_lote["documento"].ToString().Trim(), reader_lote["recibo"].ToString().Trim(), tipo);
                        print = DateTime.Now + " - CONSULTA LOTE - " + resposta_lote + "\r\n";
                        txt.Text = txt.Text + DateTime.Now + " - CONSULTA LOTE - " + resposta_lote + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;

                    }
                    reader_lote.Close();
                }
            }
        }



        //entrada
        public void consEntrada(object sender, EventArgs e)
        {
            string doc = null;
            string tipo = "entrada";
            geraLog log = new geraLog();
            testa_conexao = cs.conex();
            if (testa_conexao == "1")
            {
                string con = cs.conn();
                SqlConnection nova_con = new SqlConnection(con);
                nova_con.Open();
                using (nova_con)
                {
                    SqlCommand c_venda = new SqlCommand("select top 1 nfe_entrada.*, nfe_itens_entrada.documento as doc_itens from nfe_entrada with(nolock) left outer join nfe_itens_entrada with(nolock) on nfe_entrada.documento = nfe_itens_entrada.documento where (nfe_entrada.chave = '' or nfe_entrada.chave is null) and (nfe_entrada.nf='' or nfe_entrada.nf is null) and (nfe_entrada.xml='' or nfe_entrada.xml is null) and (nfe_itens_entrada.documento is not null)", nova_con);
                    reader_venda = c_venda.ExecuteReader();
                    while (reader_venda.Read())
                    {

                        string resposta = nova.criaNF(reader_venda["documento"].ToString(), tipo);
                        txt.Text = txt.Text + DateTime.Now + " - XML - " + reader_venda["documento"].ToString() + " - " + resp(resposta) + "\r\n";
                        print = DateTime.Now + " - XML - " + reader_venda["documento"].ToString() + " - " + resposta + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;
                        if (resp(resposta) == "FALHA")
                        {
                            doc = reader_venda["documento"].ToString();
                            falha(doc, nova_con, "entrada", "xml");
                            doc = null;
                        }
                    }
                    reader_venda.Close();
                    SqlCommand c_assina = new SqlCommand("select  top 1 nfe_entrada.*, nfe_itens_entrada.documento as doc_itens from nfe_entrada with(nolock) left outer join nfe_itens_entrada with(nolock) on nfe_entrada.documento = nfe_itens_entrada.documento where (nfe_entrada.chave <> '' and nfe_entrada.chave is not null) and (nfe_entrada.nf<>'') and (nfe_entrada.xml='S') and (nfe_entrada.assinado is null or nfe_entrada.assinado = '') and (nfe_itens_entrada.documento is not null)", nova_con);
                    reader_assina = c_assina.ExecuteReader();
                    while (reader_assina.Read())
                    {
                        string resposta_assina = lote.gera_Lote(reader_assina["documento"].ToString(), reader_assina["chave"].ToString(), tipo);
                        print = DateTime.Now + " - ASSINA - " + reader_assina["documento"].ToString() + " - " + resposta_assina + "\r\n";
                        txt.Text = txt.Text + DateTime.Now + " - ASSINA - " + reader_assina["documento"].ToString() + " - " + resp(resposta_assina) + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;
                        if (resp(resposta_assina) == "FALHA")
                        {
                            doc = reader_assina["documento"].ToString();
                            falha(doc, nova_con, "entrada", "assinatura");
                            doc = null;
                        }
                    }
                    reader_assina.Close();

                    SqlCommand c_envia = new SqlCommand("select top 1 nfe_entrada.*,nfe_itens_entrada.documento as doc_itens from nfe_entrada with(nolock) left outer join nfe_itens_entrada with(nolock) on nfe_entrada.documento = nfe_itens_entrada.documento where (nfe_entrada.chave <> '' and nfe_entrada.chave is not null) and (nfe_entrada.nf<>'') and (nfe_entrada.xml='S') and (nfe_entrada.assinado = 'S') and (nfe_entrada.enviado is null or nfe_entrada.enviado = '') and (nfe_itens_entrada.documento is not null)", nova_con);
                    reader_envia = c_envia.ExecuteReader();
                    while (reader_envia.Read())
                    {
                        string resposta_envia = env.envia_Lote(reader_envia["documento"].ToString().Trim(), tipo);
                        txt.Text = txt.Text + DateTime.Now + " - ENVIA - " + reader_envia["documento"].ToString().Trim() + " - " + resp(resposta_envia) + "\r\n";
                        print = DateTime.Now + " - ENVIA - " + reader_envia["documento"].ToString().Trim() + " - " + resposta_envia + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;
                        if (resp(resposta_envia) == "FALHA")
                        {
                            doc = reader_envia["documento"].ToString();
                            falha(doc, nova_con, "entrada", "envio");
                            doc = null;
                        }

                    }
                    reader_envia.Close();
                    SqlCommand c_lote = new SqlCommand("select top 1 nfe_entrada.*,nfe_itens_entrada.documento as doc_itens from nfe_entrada with(nolock) left outer join nfe_itens_entrada with(nolock) on nfe_entrada.documento = nfe_itens_entrada.documento where (nfe_entrada.chave <> '' and nfe_entrada.chave is not null) and (nfe_entrada.nf<>'') and (nfe_entrada.xml='S') and (nfe_entrada.assinado = 'S') and (nfe_entrada.enviado = 'S') and (nfe_entrada.recibo is not null or nfe_entrada.recibo <> '') and (nfe_entrada.cStat = '' or nfe_entrada.cStat is null) and (nfe_entrada.xMotivo = '' or nfe_entrada.xMotivo is null) and (nfe_itens_entrada.documento is not null)", nova_con);
                    reader_lote = c_lote.ExecuteReader();
                    while (reader_lote.Read())
                    {
                        string resposta_lote = cons_lote.consultaLote(reader_lote["documento"].ToString().Trim(), reader_lote["recibo"].ToString().Trim(), tipo);
                        print = DateTime.Now + " - CONSULTA LOTE - " + resposta_lote + "\r\n";
                        txt.Text = txt.Text + DateTime.Now + " - CONSULTA LOTE - " + resposta_lote + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;

                    }
                    reader_lote.Close();

                    SqlCommand c_cancela = new SqlCommand("select top 1 nfe_entrada.*, nfe_itens_entrada.documento from nfe_entrada with(nolock) left outer join nfe_itens_entrada with(nolock) on nfe_entrada.documento = nfe_itens_entrada.documento where (nfe_entrada.chave <> '' and nfe_entrada.chave is not null) and (nfe_entrada.nf<>'') and (nfe_entrada.xml='S') and (nfe_entrada.assinado = 'S') and (nfe_entrada.enviado = 'S') and (nfe_entrada.recibo is not null or nfe_entrada.recibo <> '') and (nfe_entrada.cStat <> '' or nfe_entrada.cStat is not null) and (nfe_entrada.xMotivo <> '' or nfe_entrada.xMotivo is not null) and (nfe_entrada.cancelado = 'aguardando') and (nfe_entrada.dt_cancela = '' or nfe_entrada.dt_cancela is null) and (nfe_itens_entrada.documento is not null)", nova_con);
                    reader_cancela = c_cancela.ExecuteReader();
                    while (reader_cancela.Read())
                    {
                        string resposta_cancela = cancela_nfe.cancela_NFe(reader_cancela["documento"].ToString(), tipo);
                        print = DateTime.Now + " - CANCELA NFE - " + resposta_cancela + "\r\n";
                        txt.Text = txt.Text + DateTime.Now + " - CANCELA NFE - " + resposta_cancela + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;

                    }
                    reader_cancela.Close();

                    cs.fechaConex();
                    nova_con.Close();

                }

            }
            else
            {
                print = DateTime.Now + " - Conexao com o banco de dados malsucedida. Verifique configuração.";
                txt.Text = print;
                log.gera_Log(diamesano, print);
                print = null;
            }
        }

        public void consLoteEmProc_entrada(object sender, EventArgs e)
        {

            string tipo = "entrada";
            geraLog log = new geraLog();
            testa_conexao = cs.conex();
            if (testa_conexao == "1")
            {
                string con = cs.conn();
                SqlConnection nova_con = new SqlConnection(con);
                nova_con.Open();
                using (nova_con)
                {
                    SqlCommand c_lote = new SqlCommand("select top 1 nfe_entrada.*,nfe_itens_entrada.documento as doc_itens from nfe_entrada with(nolock) left outer join nfe_itens_entrada with(nolock) on nfe_entrada.documento = nfe_itens_entrada.documento where (nfe_entrada.chave <> '' and nfe_entrada.chave is not null) and (nfe_entrada.nf<>'') and (nfe_entrada.xml='S') and (nfe_entrada.assinado = 'S') and (nfe_entrada.enviado = 'S') and (nfe_entrada.recibo is not null or nfe_entrada.recibo <> '') and (nfe_entrada.cStat = '105')  and (nfe_itens_entrada.documento is not null)", nova_con);
                    reader_lote = c_lote.ExecuteReader();
                    while (reader_lote.Read())
                    {
                        string resposta_lote = cons_lote.consultaLote(reader_lote["documento"].ToString().Trim(), reader_lote["recibo"].ToString().Trim(), tipo);
                        print = DateTime.Now + " - CONSULTA LOTE - " + resposta_lote + "\r\n";
                        txt.Text = txt.Text + DateTime.Now + " - CONSULTA LOTE - " + resposta_lote + "\r\n";
                        log.gera_Log(diamesano, print);
                        print = null;

                    }
                    reader_lote.Close();
                }
            }
        }

        //fim entrada
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txt.SelectionStart = txt.Text.Length;
            txt.ScrollToCaret();
            txt.Refresh();
        }

        private void btn_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void timerConexao()
        {
            //timer_con.Tick += new EventHandler(teste_conexao);
            //timer_con.Start();
        }

        public void timerCert()
        {
            //timer_cert.Tick += new EventHandler(teste_cert);
            //timer_cert.Start();
        }

        public void timerAlerta()
        {
            timer_alerta.Tick += new EventHandler(alerta);
            timer_alerta.Start();
        }

        public void timerAlerta_stop()
        {
            timer_alerta.Stop();
        }

        public void timerCert_stop()
        {
            timer_cert.Stop();
        }

        public void timerConexao_stop()
        {
            timer_con.Stop();
        }

        private void txt2_TextChanged(object sender, EventArgs e)
        {
            txt2.SelectionStart = txt2.Text.Length;
            txt2.ScrollToCaret();
            txt2.Refresh();
        }

        public void timerNfe()
        {
            timer_venda.Tick += new EventHandler(consVenda);
            timer_venda.Start();

        }

        public void timerEntrada()
        {
            timer_entrada.Tick += new EventHandler(consEntrada);
            timer_entrada.Start();
        }

        public void timerNfe_stop()
        {
            timer_venda.Stop();
            timer_assina.Stop();
            timer_envia.Stop();
            timer_consLote.Stop();
            timer_entrada.Stop();
            timer_consLoteEmProc_saida.Stop();
            timer_consLoteEmProc_entrada.Stop();
        }

        public void consNfeEmProc() {
            timer_consLoteEmProc_saida.Tick += new EventHandler(consLoteEmProc_saida);
            timer_consLoteEmProc_saida.Start();
            timer_consLoteEmProc_entrada.Tick += new EventHandler(consLoteEmProc_entrada);
            timer_consLoteEmProc_entrada.Start();
        }

        public void consNfeEmProc_stop() {
            timer_consLoteEmProc_entrada.Stop();
            timer_consLoteEmProc_saida.Stop();
        }

        private void txt_alerta_TextChanged(object sender, EventArgs e)
        {
            txt_alerta.SelectionStart = txt_alerta.Text.Length;
            txt_alerta.ScrollToCaret();
            txt_alerta.Refresh();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt.Text = "";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_alerta.Text = "";
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt2.Text = "";
        }

    }
}
