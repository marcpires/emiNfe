using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Reflection;


namespace criarNfeXML
{
    class geraLog
    {
        public void gera_Log(string data, string log)
        {
            //string caminho = "C:\\Nfe\\log\\" + data + ".txt";
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\Nfe\\log\\" + data + ".txt";
            if (File.Exists(caminho))
            {
                StreamWriter arquivo = File.AppendText(caminho);
                arquivo.WriteLine(log);
                arquivo.Close();

            }
            else
            {
                System.IO.StreamWriter arquivo = new System.IO.StreamWriter(caminho);
                arquivo.WriteLine(log);
                arquivo.Close();
            }
        }


        }
    }


