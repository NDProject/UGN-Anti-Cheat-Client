using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace UGN_Client
{
    public class CaptureScreenshot
    {

        private static string captionWindowLabel = "";

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        // Load all the .dll magic

        // This is needed to detect if Aero is enabled or not (hacks use them to display overlays)
        [DllImport("dwmapi.dll", EntryPoint="DwmIsCompositionEnabled")]
        private static extern int DwmIsCompositionEnabled(out bool enabled);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect); //screen resolution

        [DllImport("user32.dll")]
        private static extern int GetForegroundWindow(); //current active window

        [DllImport("user32.dll")]
        private static extern int GetWindowText(int hWnd, StringBuilder text, int count);//specified window's title text
        
        //
        private static void GetActiveWindow()
        {

            const int nChars = 256;
            int handle = 0;
            StringBuilder Buff = new StringBuilder(nChars);

            handle = GetForegroundWindow();
     
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                captionWindowLabel = Buff.ToString();
            }

        }

        public static void Run(string procName, Image Watermark)
        {

            try
            {

                if (CommonData.ScreenshotDelayActive == false)
                {

                    GetActiveWindow();

                    if (captionWindowLabel == "Soldier Front" || captionWindowLabel == "Soldier Front 2")
                    {

                        var proc = Process.GetProcessesByName(procName)[0];

                        var rect = new Rect();
                        GetWindowRect(proc.MainWindowHandle, ref rect);

                        bool aeroEnabled = false;

                        OperatingSystem OS = Environment.OSVersion;

                        // If OS is Vista or up, check aero status
                        if ((OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6))
                        {
                            DwmIsCompositionEnabled(out aeroEnabled);
                        }

                        System.Drawing.Rectangle activeRectangle = Screen.PrimaryScreen.WorkingArea;

                        bool isFullScreen = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top).Contains(activeRectangle);

                        // only take screenshots if the game is NOT fullscreen ||OR|| aero is not enabled
                        if (isFullScreen == false || aeroEnabled == false)
                        {

                            // check to see if previously taken ss has been modified
                            if (CommonData.publicSSready == true)
                            {
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
                                }
                                else
                                {
                                    Environment.Exit(exitCode: 0);
                                }
                            }

                            int width = rect.right - rect.left;
                            int height = rect.bottom - rect.top;

                            var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                            Graphics graphics = Graphics.FromImage(bmp);
                            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
                            // add watermark
                            graphics.DrawImage(Watermark, width - 156, height - 42);
                            Graphics graphicImage = Graphics.FromImage(bmp);
                            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
                            graphicImage.CompositingQuality = CompositingQuality.AssumeLinear;
                            graphicImage.InterpolationMode = InterpolationMode.Low;

                            bmp.Save(CommonData.DataFolder + "public_ss.png");
                            ImageResize.Run(CommonData.DataFolder + "public_ss.png", 800, 600);

                            // save checksum to prevent the deletion/moving/replacing of the ss
                            using (System.IO.FileStream stream = System.IO.File.OpenRead(CommonData.DataFolder + "public_ss.png"))
                            {
                                System.Security.Cryptography.SHA256Managed sha = new System.Security.Cryptography.SHA256Managed();
                                byte[] bytes = sha.ComputeHash(stream);
                                CommonData.SSchecksum = BitConverter.ToString(bytes).Replace("-", String.Empty);
                            }

                            // do a second active window check (to prevent full desktop ss)
                            GetActiveWindow();

                            if (captionWindowLabel == "Soldier Front" || captionWindowLabel == "Soldier Front 2")
                            {
                                CommonData.publicSSready = true;
                            }
                            else
                            {
                                // Set to false as most likely it took a desktop ss
                                CommonData.publicSSready = false;
                            }
                        }
                    }
                }

            }

            catch 
            {
                
            }

        }

    }
}
