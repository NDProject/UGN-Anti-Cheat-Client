using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.Reflection;

namespace UGN_Client
{
    public partial class UpdateCheck : Form
    {
        public UpdateCheck()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string downloadUrl = "";
            Version newVersion = null;
            string aboutUpdate = "";

            string xmlUrl = "https://playugn.com/api/updates/latest.xml";

            XmlTextReader reader = null;

            try
            {

                RequestTools.XPfix();

                reader = new XmlTextReader(xmlUrl);
                reader.MoveToContent();
                string elementName = "";
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "latest_update_info"))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            elementName = reader.Name;
                        }
                        else
                        {
                            if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                                switch (elementName)
                                {
                                    case "version":
                                        newVersion = new Version(reader.Value);
                                        break;
                                    case "url":
                                        downloadUrl = reader.Value;
                                        if (downloadUrl == "0.0.0.0")
                                        {

                                            MessageBox.Show("Client is currently under maintenance, check the forums for updates.");
                                            Environment.Exit(exitCode: 0);
                                        }

                                        break;
                                    case "about":
                                        aboutUpdate = reader.Value;
                                        break;
                                }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Environment.Exit(exitCode: 0);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            Version applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;

            if (applicationVersion.CompareTo(newVersion) < 0)
            {

                MessageBoxManager.OK = "Download";
                MessageBoxManager.Cancel = "Exit";
                MessageBoxManager.Register();

                string str =
                    String.Format(
                        "An update has been found!\n\nYour Version: {0}\nLatest Version: {1} \n\nUpdate Notes:\n {2} ",
                        applicationVersion, newVersion, aboutUpdate);

                if (DialogResult.Cancel != MessageBox.Show(str, "UGN Client Update", MessageBoxButtons.OKCancel))
                {

                    downloadNewInstaller(downloadUrl);

                }
                else
                {
                    Environment.Exit(exitCode: 0);
                }

            }
            else
            {
                Hide();
                UGN_Client.Form1 login = new Form1();
                login.ShowDialog();
            }

        }

        public static void downloadNewInstaller(string downloadUrl)
        {
            WebClient _downloadClient = new WebClient();

            Uri downloadUri = new Uri(downloadUrl);

            // Download the new installer and save it to our UGN folder
            _downloadClient.DownloadFileAsync(downloadUri, CommonData.DataFolder + "UGN Client Installer.exe");

            _downloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
        }

        public static void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string new_installer_path = CommonData.DataFolder + "UGN Client Installer.exe";
            System.Diagnostics.Process.Start(new_installer_path);
            Environment.Exit(exitCode: 0);
        }

    }
}
