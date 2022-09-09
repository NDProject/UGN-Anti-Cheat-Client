using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;



namespace UGN_Client
{
    public class FetchUserData
    {
        public static void Run()
        {
            try
            {
                RequestTools.XPfix();
                var request = (HttpWebRequest)WebRequest.Create("https://playugn.com/api/data/user.php?");
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
                    XmlNodeList uid = xml2.SelectNodes("/UGN");
                    foreach (XmlNode u in uid)
                    {
                        CommonData.GID = u["gid"].InnerText;
                        CommonData.Read = u["msgs_unread_count"].InnerText;
                        CommonData.statUsersOnline = u["users_online"].InnerText;
                        CommonData.statUsersOnlineClient = u["users_online_client"].InnerText;
                        CommonData.statShame = u["cheaters_recently_caught"].InnerText;
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
