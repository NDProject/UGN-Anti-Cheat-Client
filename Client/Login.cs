using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using UGN_Client.Properties;
using System.Drawing;
using System.Security.Principal;


namespace UGN_Client
{
    public partial class Form1 : Form
    {
        #region Get Proper Drive

        string primDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region Open a url

        private static void openURL(string url)
        {

            try
            {
                System.Diagnostics.Process.Start(url);
            }

            catch
            {
                MessageBox.Show("You do not have a default browser. Check your browser settings.");
            }

        }

        #endregion

        #region Application Exit
        private void Form1_Closing(object sender, EventArgs e)
        {
            Environment.Exit(exitCode: 0);
        }
        #endregion

        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                .IsInRole(WindowsBuiltInRole.Administrator);
        }

        #region Form init load

        private void Form1_Load(object sender, EventArgs e)
        {

            if (!IsAdministrator())
            {
                MessageBox.Show("Please run the UGN Client as administrator.");
                Environment.Exit(exitCode: 0);
            }

            string fileName = @"" + primDrive + "Windows\\ugn_credentials.dat";

            if (File.Exists(fileName))
            {
                rememberMeDetect();
                if (label8.Text == "1")
                {
                    checkBox1.Checked = true;

                }
                else { checkBox1.Checked = false; }

            }


        }

        #endregion

        #region Login Check
        private void login_check()
        {

            ugN_Button1.Text = "...";

            string username = Username1.Text;
            string password = Password.Text;

            if (!checkBox1.Checked)
            {
                string fileName = primDrive + @"Windows\ugn_credentials.dat";

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }

            if (username == "" || password == "")
            {
                ugN_Button1.Text = "Sign In";
                MessageBox.Show("Please fill in both fields.", "Oops");
            }

            else

            {

                try
                {

                    RequestTools.XPfix();

                    var request = (HttpWebRequest)WebRequest.Create("https://playugn.com/api/auth/index.php?");

                    string postData = "client_secret=" + CommonData.clientSecret;
                    postData += "&signin_username_email=" + Username1.Text;
                    postData += "&signin_password=" + Parser.Base64Encode(Password.Text);
                    postData += "&bios_id=" + HardwareInfo.GetHash(HardwareInfo.BiosId());
                    postData += "&hdd_id=" + HardwareInfo.GetHash(HardwareInfo.DiskId());
                    postData += "&gfx_id=" + HardwareInfo.GetHash(HardwareInfo.VideoId());
                    byte[] data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    request.Proxy = null;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    string xml = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    response.Close();

                    var xml2 = new XmlDocument();
                    xml2.LoadXml(xml);
                    reader.Text = xml2.InnerXml;

                    string invalid = "<?xml version=\"1.0\"?><UGN><error>invalid_credentials</error></UGN>";
                    string suspended = "<?xml version=\"1.0\"?><UGN><error>account_suspended</error></UGN>";
                    string notActivated = "<?xml version=\"1.0\"?><UGN><error>account_not_activated</error></UGN>";
                    string dataType = "<?xml version=\"1.0\"?><UGN><error>invalid_data_type</error></UGN>";
                    string notReceived = "<?xml version=\"1.0\"?><UGN><error>required_data_not_received</error></UGN>";
                    string ip = "<?xml version=\"1.0\"?><UGN><error>too_many_failed_attempts</error></UGN>";
                    string clientVersion = "<?xml version=\"1.0\"?><UGN><error>invalid_client_version</error></UGN>";

                    while (reader.Text != "")
                    {
                        if (invalid == reader.Text)
                        {
                            ugN_Button1.Text = "Sign In";
                            MessageBox.Show("Invalid username or password.");
                            break;
                        }

                        if (ip == reader.Text)
                        {
                            MessageBox.Show("Too many invalid sign in requests. Please try again later.");
                            Environment.Exit(exitCode: 0);
                        }

                        if (clientVersion == reader.Text)
                        {
                            MessageBox.Show("Invalid client version. Please update.");
                            Environment.Exit(exitCode: 0);
                        }

                        if (suspended == reader.Text)
                        {
                            MessageBox.Show("Account suspended, please sign in to the UGN Platform for further details.", "ACCOUNT SUSPENDED");
                            Environment.Exit(exitCode: 0);
                        }

                        if (notActivated == reader.Text)
                        {
                            MessageBox.Show("Your account has not yet been activated, please check your registration email.");
                            break;
                        }


                        if (dataType == reader.Text)
                        {
                            MessageBox.Show("Something went wrong.");
                            Environment.Exit(exitCode: 0);
                        }

                        if (notReceived == reader.Text)
                        {
                            MessageBox.Show("Something went wrong.");
                            Environment.Exit(exitCode: 0);
                        }

                        else
                        {
                            XmlNodeList uid = xml2.SelectNodes("/UGN");
                            foreach (XmlNode u in uid)
                            {
                                CommonData.UID = u["uid"].InnerText;
                                CommonData.GID = u["gid"].InnerText;
                                CommonData.Username = u["username"].InnerText;
                                CommonData.Read = u["msgs_unread_count"].InnerText;
                                CommonData.statUsersOnline = u["users_online"].InnerText;
                                CommonData.statUsersOnlineClient = u["users_online_client"].InnerText;
                                CommonData.authToken = u["access_token"].InnerText;
                                CommonData.statShame = u["cheaters_recently_caught"].InnerText;

                                Hide();
                                UGN_Client.v member = new v();
                                member.ShowDialog();
                            }
                        }

                    }

                }
                catch (WebException WebEx)
                {
                    // suppress (loss of connection)
                }

            }

        }
        #endregion

        #region Remember Me
        private void rememberMe()
        {
            string username = Username1.Text;
            string password = Password.Text;

            if (checkBox1.Checked)
            {
                if (username == "" || password == "")
                {
                    MessageBox.Show("Please fill in both fields.", "Oops");
                    checkBox1.Checked = false;
                }
                else
                {
                    string fileName = primDrive + @"Windows\ugn_credentials.dat";


                    try
                    {

                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        using (FileStream fs = File.Create(fileName))
                        {

                            Byte[] user = new UTF8Encoding(true).GetBytes(username + "\n");

                            fs.Write(user, 0, user.Length);
                            byte[] passs = new UTF8Encoding(true).GetBytes(password + "\n");

                            fs.Write(passs, 0, passs.Length);
                            Byte[] remember = new UTF8Encoding(true).GetBytes("1" + "\n");

                            fs.Write(remember, 0, remember.Length);

                        }
                    }

                    catch
                    {
                        
                    }
                }
            }

        }
        #endregion

        #region Remember Me with loading in ugn_credentials.dat
        private void rememberMeDetect()
        {

            string fileName = @"" + primDrive + "Windows\\ugn_credentials.dat";
            if (File.Exists(fileName))
            {

                TextReader tr = new StreamReader(@"" + primDrive + "Windows\\ugn_credentials.dat");

                int NumberOfLines = 4;

                var ListLines = new string[NumberOfLines];

                for (int i = 1; i < NumberOfLines; i++)
                {
                    ListLines[i] = tr.ReadLine();
                }

                Username1.Text = ListLines[1];
                Password.Text = ListLines[2];
                label8.Text = ListLines[3];

                tr.Close();
            }

        }
        #endregion

        #region Changing Username Back Color
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (Username1.Text == "" || Username1.Text == "Username")
            {
                Username1.BackColor = Color.FromArgb(247, 247, 247);
            }
            else
                Username1.BackColor = Color.FromArgb(247, 247, 247);

        }
        #endregion

        #region Changing Password Back Color
        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

            if (Password.BackColor == Color.FromArgb(247, 247, 247))
            {
                Password.BackColor = Color.FromArgb(247, 247, 247);


            }
            else if (Password.Text == "")
            {

                Password.BackColor = Color.FromArgb(247, 247, 247);
            }

        }
        #endregion

        #region Start Reset Password Link
        private void label5_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/accounts/reset_password");

        }
        #endregion

        #region UGNCheckBox return from theme
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                ugnCheckBox1.Checked = true;
            }
            else
            {
                ugnCheckBox1.Checked = false;
            }

        }
        #endregion
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #region Login Command

        private void ugN_Button1_Click(object sender, EventArgs e)
        {
            rememberMe();
            login_check();
        }

        private void textBox1_PreviewKeyDown(Object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    rememberMe();
                    login_check();
                    break;
            }
        }

        private void textBox2_PreviewKeyDown(Object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    rememberMe();
                    login_check();
                    break;
            }
        }

        #endregion

        #region TEXT BOX mouse overs etc
        private void Username1_MouseEnter(object sender, EventArgs e)
        {
            if (Username1.BackColor == Color.FromArgb(247, 247, 247))
            {
                Username1.BackColor = Color.FromArgb(253, 253, 253);


            }
            else if (Username1.Text == "")
            {

                Username1.BackColor = Color.FromArgb(247, 247, 247);
            }
        }

        private void Username1_MouseLeave(object sender, EventArgs e)
        {
            if (Username1.Text == "")
            {
                Username1.BackColor = Color.FromArgb(247, 247, 247);


            }
            else
                Username1.BackColor = Color.FromArgb(253, 253, 253);
        }

        private void Password_MouseEnter(object sender, EventArgs e)
        {
            if (Password.BackColor == Color.FromArgb(247, 247, 247))
            {
                Password.BackColor = Color.FromArgb(253, 253, 253);


            }
            else if (Password.Text == "")
            {

                Password.BackColor = Color.FromArgb(247, 247, 247);
            }
        }

        private void Password_MouseLeave(object sender, EventArgs e)
        {
            if (Password.Text == "")
            {
                Password.BackColor = Color.FromArgb(247, 247, 247);


            }
            else
                Password.BackColor = Color.FromArgb(253, 253, 253);
        }

        #endregion

        #region UGN CheckBoxes Bools
        private void ugnCheckBox1_CheckedChanged(object sender)
        {
            if (ugnCheckBox1.Checked == true)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        #endregion

        #region Login  Closing Proper Exit
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(exitCode: 0);
        }
        #endregion

        #region Start PlayUGN.com
        private void panel10_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com");
        }
        #endregion

        private void label10_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/accounts/reset_password");
        }

        private void panel8_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

    }
}













