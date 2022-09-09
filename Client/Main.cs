using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using System.Windows.Forms;
using UGN_Client.Properties;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;

namespace UGN_Client
{
    public partial class v : Form
    {

        #region Create a Private Timer1 for the function InitTimer
        private Timer timer1;
        #endregion

        public v()
        {
            InitializeComponent();
        }

        #region Mouse Location and Overrides

        private Point downLocation;
        private Point downMousePosition;
        private bool isMouseDown = false;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            lock (this)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (isMouseDown)
                    {
                        Point mousePos = Form.MousePosition;
                        Point location = downLocation;
                        location.X = location.X +
                        (mousePos.X - downMousePosition.X);
                        location.Y = location.Y +
                        (mousePos.Y - downMousePosition.Y);
                        base.Location = location;
                    }
                    else
                    {
                        downLocation = base.Location;
                        downMousePosition = Form.MousePosition;
                        isMouseDown = true;
                    }
                }
                else
                {
                    isMouseDown = false;
                }

                base.OnMouseMove(e);
            }
        }

        #endregion

        #region Members Load

        private void Members_Area_Load(object sender, EventArgs e)
        {

            Shield1.Start();

            // create if our data folders or files do not already exist

            if (!Directory.Exists(CommonData.DataFolder)) {
                Directory.CreateDirectory(CommonData.DataFolder);
            }

            if (!Directory.Exists(CommonData.LogsFolder))
            {
                Directory.CreateDirectory(CommonData.LogsFolder);
            }

            if (!File.Exists(CommonData.LogsFolder + CommonData.ErrorLogFile))
            {
                File.Create(CommonData.LogsFolder + CommonData.ErrorLogFile);
            }
            else
            {
                // error file exists, clear its contents
                File.WriteAllText(CommonData.LogsFolder + CommonData.ErrorLogFile, string.Empty);
            }

            /* Clean screenshots in the UGN folder */
            string[] filePaths = Directory.GetFiles(CommonData.DataFolder);
            foreach (string filePath in filePaths)
                File.Delete(filePath);

            username.Text = CommonData.Username;

            FetchMemorySignatures.Request();

            // make sure it was fetched
            if (MemorySignatures.g1 == null)
            {
                Environment.Exit(exitCode: 0);
            }

            CommonData.publicSSready = false;
            LogData.Memory = "";

            float opacityvalue = float.Parse(label17.Text) / 100;
            pictureBox6.Image = ImageTransparency.ChangeOpacity(Properties.Resources.ugn_logo_32, opacityvalue);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            twitch.Image = ImageTransparency.ChangeOpacity(Properties.Resources.twitch_mini_logo_grayscale, opacityvalue);
            twitch.SizeMode = PictureBoxSizeMode.Zoom;

            username.Text = CommonData.Username;
            textBox1.Text = CommonData.authToken;
            label1.Text = CommonData.Read;
            label7.Text = CommonData.statUsersOnlineClient;
            label5.Text = CommonData.statUsersOnline;
            label11.Text = CommonData.GID;
            label14.Text = CommonData.UID;
            label4.Text = CommonData.statShame;

            GIDCheck();
            InitTimer();
        }

        #endregion

        #region Start InitTimer and Load Timer1

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += timer1_Tick;
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UserDataFetch();
            GIDCheck();

            if (GameDetector.SimpleDetect() == false)
            {
                MemoryHeuristics.Run();
            }
        }

        #endregion

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

        #region Run BGDATA Check, GID

        private void UserDataFetch()
        {
            FetchUserData.Run();

        }

        private void GIDCheck()
        {
            switch (CommonData.GID)
            {
                case "1":
                    {
                        Environment.Exit(exitCode: 0);
                        break;
                    }

                case "3":
                    {
                        pictureBox3.Image = Resources.purple_star1;
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                        break;
                    }
                case "4":
                    {
                        pictureBox3.Image = Resources.red_star1;
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                        break;
                    }

                case "5":
                    {
                        pictureBox3.Image = Resources.red_star1;
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                        break;
                    }

                default:
                    {
                        pictureBox3.Image = Resources.gray_star1;
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                        break;
                    }
            }
        }

        #endregion

        #region Add Contextmenu to button
        private void button5_Click(object sender, EventArgs e)
        {
            var btnSender = (Button)sender;
            var ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);

        }
        #endregion

        #region Process Start Commands in Menu Strips
        private void platformToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openURL("https://playugn.com");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/support/");
        }

        #endregion

        #region Process Start Commands
        private void button7_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/messages/");
        }


        private void rectangleShape3_Click_1(object sender, EventArgs e)
        {
            openURL("https://playugn.com/wall_of_shame");

        }

        private void username_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/users/" + CommonData.Username);
        }


        private void label6_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/users_online");
        }

        private void rectangleShape4_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/users_online");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/users_online");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/wall_of_shame");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/wall_of_shame");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openURL("https://twitter.com/ugn1337");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openURL("http://www.twitch.tv/ugntv");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            openURL("https://www.youtube.com/user/unitedgn");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com");
        }

        #endregion

        #region Shield One

        private void Shield1_Tick(object sender, EventArgs e)
        {

            try
            {

                label12.Text = "Busy";

                // remove this connection blocker in the future (all together)
                ConnectionBlocker.unblock();

                GameDetector.DetectAndKillAll();
                GameDetector.gameClosedMsg();

                MemoryHeuristics.Run();

                Shield1.Stop();
                Shield2.Start();
            }

            catch {
                Shield1.Stop();
                label12.Text = "Something went wrong! Restart the Client.";
            }
        }

        #endregion

        #region Shield Two

        //ManagementEventWatcher processStopEvent = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace");

        private void Shield2_Tick(object sender, EventArgs e)
        {

            timer10.Stop();
            CommonData.timer10 = null;
            CommonData.timer10counter = 0;

            timer7.Stop();
            timer8.Stop();
            TakeScreenshotDelayTimer.Stop();
            CommonData.ScreenshotDelayActive = false;

            label12.Text = "On Watch";

            if (GameDetector.DetectAndVerify() == true)
            {

                //processStopEvent.EventArrived += new EventArrivedEventHandler(processStopEvent_EventArrived);
                //processStopEvent.Start();

                timer10.Start();
                CommonData.timer10 = DateTime.Now;

                TakeScreenshotDelayTimer.Start();
                CommonData.ScreenshotDelayActive = true;

                label12.Text = "Game Detected";

                if (CommonData.GameDetected == "Soldier Front")
                {
                    procNames.Text = "soldierfront";
                    detectedGame.Text = "Soldier Front";
                    LogData.GameID = 1;
                }
                else if (CommonData.GameDetected == "Soldier Front 2")
                {
                    procNames.Text = "sf2";
                    detectedGame.Text = "Soldier Front 2";
                    LogData.GameID = 2;
                }

                MemoryHeuristics.Run();

                Shield2.Stop();
                Shield5.Start();

            }

        }

        /*
        public void processStopEvent_EventArrived(object sender, EventArrivedEventArgs e)
        {
            try
            {

                string processName = e.NewEvent.Properties["ProcessName"].Value.ToString();
                LogData.ClosedProcList = LogData.ClosedProcList + processName + "\n";

                processStopEvent.EventArrived += new EventArrivedEventHandler(processStopEvent_EventArrived);
                processStopEvent.Stop();

                MessageBox.Show("" + LogData.ClosedProcList);
            }
            catch
            {
                processStopEvent.Stop();
            }
        }
        */

        #endregion

        #region Shield Five

        private void Shield5_Tick(object sender, EventArgs e)
        {

            if (GameDetector.GameActiveSecurityChecks(LogData.GameID) == true)
            {
                if (label12.Text != "On Watch")
                {
                    label12.Text = "On Watch";
                    timer7.Start();
                    timer8.Start();
                }
            }
            else
            {
                Shield5.Stop();
                Shield2.Start();

                // this is very important
                GameDetector.DetectAndKillAll();

                label12.Text = "On Watch";
                detectedGame.Text = "No games detected as running";
            }

        }

        #endregion

        #region Timer7: CAPTURE Public Screenshot

        public void timer7_Tick(object sender, EventArgs e)
        {
            Image Watermark = UGN_Client.Properties.Resources.ss_watermark;
            CaptureScreenshot.Run(procNames.Text, Watermark);
        }

        #endregion

        #region Timer8: SEND Public Screenshot

        public void timer8_Tick(object sender, EventArgs e)
        {
            // 2 birds, 1 timer :D
            FetchMemorySignatures.Request();

            if (CommonData.publicSSready)
            {

                // delete old upload file
                File.Delete(CommonData.DataFolder + "public_ss_upload.png");

                if (System.IO.File.Exists(CommonData.DataFolder + "public_ss.png") == true)
                {
                    using (System.IO.FileStream stream = System.IO.File.OpenRead(CommonData.DataFolder + "public_ss.png"))
                    {
                        System.Security.Cryptography.SHA256Managed sha = new System.Security.Cryptography.SHA256Managed();
                        byte[] bytes = sha.ComputeHash(stream);
                        string tempChecksum = BitConverter.ToString(bytes).Replace("-", String.Empty);

                        if (tempChecksum != CommonData.SSchecksum)
                        {
                            Environment.Exit(exitCode: 0);
                        }
                    }

                    // ss is authentic, make a new copy and rename the ss we'll be uploading so timer7 doesn't try to replace it with a new one
                    File.Move(CommonData.DataFolder + "public_ss.png", CommonData.DataFolder + "public_ss_upload.png");
                }
                else
                {
                    Environment.Exit(exitCode: 0);
                }

                // upload the ss
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("uid", CommonData.UID);
                nvc.Add("access_token", CommonData.authToken);
                nvc.Add("client_secret", CommonData.clientSecret);
                SendScreenshot.Run("https://playugn.com/api/screenshots/index.php", CommonData.DataFolder + "public_ss_upload.png", "screenshot", "image/png", nvc);

                // prevent multiple uploading of same ss if no new ss taken
                CommonData.publicSSready = false;
                
            }

        }

        #endregion

        #region TakeScreenshotDelayTimer: Delay Screenshot Capture -- to prevent SF2 login screen ss

        public void TakeScreenshotDelayTimer_Tick(object sender, EventArgs e)
        {
            CommonData.ScreenshotDelayActive = false;
            TakeScreenshotDelayTimer.Stop();
        }

        #endregion

        #region Update Text Objects with UserInfo Class

        private void bgUpdate_Tick(object sender, EventArgs e)
        {

            username.Text = CommonData.Username;
            textBox1.Text = CommonData.authToken;
            label1.Text = CommonData.Read;
            label5.Text = CommonData.statUsersOnline;
            label7.Text = CommonData.statUsersOnlineClient;
            label11.Text = CommonData.GID;
            label14.Text = CommonData.UID;
            label4.Text = CommonData.statShame;

        }

        #endregion

        #region Button 1: Lobby Buttons

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "SF Chat Lobby")
                {
                    openURL("https://playugn.com/game_lobbies?g=SF");

                }
                if (button1.Text == "SF2 Chat Lobby")
                {
                    openURL("https://playugn.com/game_lobbies?g=SF2");
                }
            }
            catch { button1.Visible = false; }
        }

        #endregion

        private void detectedGame_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //UGN_Client.Client.settings member = new UGN_Client.Client.settings();
            //            member.ShowDialog();

        }

        private void v_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(exitCode: 0);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //button5.BackgroundImage = Resources.dropin;
        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // button5.BackgroundImage = Resources.CkL7tnf;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(187, 187, 187));
            Rectangle rect = panel2.ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(pen, rect);
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(187, 187, 187));
            Rectangle rect = panel2.ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(pen, rect);
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            /* var btnSender = (Button)sender;
             var ptLowerLeft = new Point(0, btnSender.Height);
             ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
             contextMenuStrip1.Show(ptLowerLeft);*/
            // contextMenuStrip1.Show();
            if (panel9.Visible == false)
            {
                panel9.Visible = true;
                panel9.BringToFront();

            }
            else
            {
                panel9.Visible = false;
            }
        }

        private void copyright_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel9_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel9_DragLeave(object sender, EventArgs e)
        {
            panel9.Visible = false;
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            openURL("https://playugn.com/forums/");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/settings/");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Environment.Exit(exitCode: 0);
        }

        private void v_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;

            }

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
            openURL("https://playugn.com/users_online");
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
            openURL("https://playugn.com/wall_of_shame");
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
            openURL("https://playugn.com/users_online");
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }

        }

        private void panel9_Leave(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
            else
            {
            }
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            openURL("https://playugn.com/messages/");
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(exitCode: 0);
        }

        private void contextMenuStrip3_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/users/" + CommonData.Username);
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel4.ClientRectangle, Color.FromArgb(187, 187, 187), ButtonBorderStyle.Solid);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel5.ClientRectangle, Color.FromArgb(187, 187, 187), ButtonBorderStyle.Solid);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel6.ClientRectangle, Color.FromArgb(187, 187, 187), ButtonBorderStyle.Solid);
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            openURL("https://playugn.com/messages/");
        }

        /* This function/timer below prevents a scenario where the game is launched, then UGN Client
         * is suspended, and then hacks are quickly injected at game black screen and then UGN Client's process is resumed.
         * If the scenario described above is suspected, then it will detect and close all supported games.
         */
        private void timer10_Tick(object sender, EventArgs e)
        {
            CommonData.timer10counter++;

            if (CommonData.timer10counter <= 12)
            {
                TimeSpan? time_tracker_diff = DateTime.Now - CommonData.timer10;
                TimeSpan? time_span_sec = TimeSpan.FromSeconds(6);

                if (time_tracker_diff > time_span_sec)
                {
                    GameDetector.DetectAndKillAll();
                }

                CommonData.timer10 = DateTime.Now;

                MemoryHeuristics.Run();
            }
            else
            {
                CommonData.timer10 = null;
                CommonData.timer10counter = 0;
                timer10.Stop();
            }
        }

    }

}