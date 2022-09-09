using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace UGN_Client
{
    public class SendGameChecksum
    {
        public static void Run()
        {

            try
            {
                RequestTools.XPfix();

                var request = (HttpWebRequest)WebRequest.Create("https://playugn.com/api/log/2.php?");

                string postData = "uid=" + CommonData.UID;
                postData += "&access_token=" + CommonData.authToken;
                postData += "&client_secret=" + CommonData.clientSecret;
                postData += "&game_id=" + LogData.GameID;
                postData += "&game_checksum=" + LogData.GameChecksum;
                postData += "&game_product_name=" + LogData.GameProductName;
                postData += "&game_exec_path=" + Parser.Base64Encode(LogData.GameExecPath);

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

                string success = "<?xml version=\"1.0\"?><UGN><success>true</success></UGN>";
                string rate_limit = "<?xml version=\"1.0\"?><UGN><error>rate_limit</error></UGN>";

                if (success != reader && rate_limit != reader)
                {
                    GameDetector.DetectAndKillAll();
                }

            }
            catch (WebException WebEx)
            {
                // suppress (loss of connection)
            }

        }


    }
}
