using System;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using UGN_Client;
using System.Threading;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Diagnostics;



namespace UGN_Client
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool newproc = true;
            using (Mutex mutex = new Mutex(true, "UGN Client", out newproc))
            {
                if (newproc)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(new UGN_Client.UpdateCheck());
                }
                else
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            SetForegroundWindow(process.MainWindowHandle);
                            break;


                        }
                    }
                }
            }
        }
    }
}