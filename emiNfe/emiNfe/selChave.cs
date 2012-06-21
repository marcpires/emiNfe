using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.IO;

namespace criarNfeXML
{
    class selChave
    {
        public X509Certificate2 selecionaChave() {
            X509Certificate2 sa = null;
            X509Certificate2Collection fcollection = null;
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            string caminho = "C:\\Nfe\\conf\\confcert.xml";
            if (File.Exists(caminho))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(caminho);
                string serial = xml["ConfCert"]["serie"].InnerText;
                fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindBySerialNumber, serial, false);
                try
                {
                    sa = fcollection[0];
                }
                catch{
                    sa = null;
                }
            }
            else {
                //fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindBySerialNumber, "17 b6 93 21 56 31 67 4d", false);
                sa = null;
            }
            return sa;

        
        }

    }
}
