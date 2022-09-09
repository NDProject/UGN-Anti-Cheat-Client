using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace UGN_Client
{
    public class FetchMemorySignatures
    {

        public static void Request()
        {

            try
            {
                RequestTools.XPfix();
                var request = (HttpWebRequest)WebRequest.Create("https://playugn.com/api/data/memory_signatures.php?");
                string postData = "uid=" + CommonData.UID;
                postData += "&access_token=" + CommonData.authToken;
                postData += "&client_secret=" + CommonData.clientSecret;
                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.Proxy = null;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                string xml = new StreamReader(response.GetResponseStream()).ReadToEnd();

                string reader = "";

                var xml2 = new XmlDocument();
                xml2.LoadXml(xml);
                reader = xml2.InnerXml;

                string invalid = "<?xml version=\"1.0\"?><UGN><error>invalid_access_token</error></UGN>";
                string version = "<?xml version=\"1.0\"?><UGN><error>invalid_client_version</error></UGN>";
                string suspended = "<?xml version=\"1.0\"?><UGN><error>account_suspended</error></UGN>";
                string dataType = "<?xml version=\"1.0\"?><UGN><error>invalid_data_type</error></UGN>";
                string notReceived = "<?xml version=\"1.0\"?><UGN><error>required_data_not_received</error></UGN>";


                if (invalid == reader)
                {
                    Environment.Exit(exitCode: 0);
                }


                if (version == reader)
                {
                    Environment.Exit(exitCode: 0);
                }

                if (suspended == reader)
                {
                    Environment.Exit(exitCode: 0);
                }


                if (dataType == reader)
                {
                    Environment.Exit(exitCode: 0);

                }
                if (notReceived == reader)
                {
                    Environment.Exit(exitCode: 0);

                }

                else
                {
                    XmlNodeList uid = xml2.SelectNodes("/signatures");
                    foreach (XmlNode u in uid)
                    {
                        MemorySignatures.g1 = u["g1"].InnerText;
                        MemorySignatures.g2 = u["g2"].InnerText;
                        MemorySignatures.slot1 = u["slot1"].InnerText;
                        MemorySignatures.slot2 = u["slot2"].InnerText;
                        MemorySignatures.slot3 = u["slot3"].InnerText;
                        MemorySignatures.slot4 = u["slot4"].InnerText;
                        MemorySignatures.slot5 = u["slot5"].InnerText;
                        MemorySignatures.slot6 = u["slot6"].InnerText;
                        MemorySignatures.slot7 = u["slot7"].InnerText;
                        MemorySignatures.slot8 = u["slot8"].InnerText;
                        MemorySignatures.slot9 = u["slot9"].InnerText;
                        MemorySignatures.slot10 = u["slot10"].InnerText;
                        MemorySignatures.slot11 = u["slot11"].InnerText;
                        MemorySignatures.slot12 = u["slot12"].InnerText;
                        MemorySignatures.slot13 = u["slot13"].InnerText;
                        MemorySignatures.slot14 = u["slot14"].InnerText;
                        MemorySignatures.slot15 = u["slot15"].InnerText;
                        MemorySignatures.slot16 = u["slot16"].InnerText;
                        MemorySignatures.slot17 = u["slot17"].InnerText;
                        MemorySignatures.slot18 = u["slot18"].InnerText;
                        MemorySignatures.slot19 = u["slot19"].InnerText;
                        MemorySignatures.slot20 = u["slot20"].InnerText;
                        MemorySignatures.slot21 = u["slot21"].InnerText;
                        MemorySignatures.slot22 = u["slot22"].InnerText;
                        MemorySignatures.slot23 = u["slot23"].InnerText;
                        MemorySignatures.slot24 = u["slot24"].InnerText;
                        MemorySignatures.slot25 = u["slot25"].InnerText;
                    }

                }

            }
            catch (WebException WebEx)
            {
                // suppress (loss of connection)
            }
        }
    }

}