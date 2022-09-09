using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace UGN_Client
{


    public class GameDetector
    {

        public static string showGameCloseMsg = "";

        public static void DetectAndKillAll()
        {
            Process[] SF_Processes_Array = Process.GetProcessesByName("soldierfront");

            foreach (Process soldierfront in SF_Processes_Array)
            {
                try
                {
                    soldierfront.Kill();
                }
                catch
                {
                    Environment.Exit(exitCode: 0);
                }

                showGameCloseMsg = "Soldier Front";
            }

            Process[] SF2_Processes_Array = Process.GetProcessesByName("sf2");

            // this also has to be an "if" in case both games are running at the same time
            foreach (Process sf2 in SF2_Processes_Array)
            {
                try
                {
                    sf2.Kill();
                }
                catch
                {
                    Environment.Exit(exitCode: 0);
                }

                showGameCloseMsg = "Soldier Front 2";
            }
        }

        public static void gameClosedMsg()
        {

            Task.Factory.StartNew(() =>
            {

                if (showGameCloseMsg != "")
                {
                    MessageBox.Show("UGN Shield has closed " + showGameCloseMsg + ", please re-launch.", "UGN Shield", MessageBoxButtons.OK);
                    showGameCloseMsg = "";
                }

            });

        }

        public static bool SimpleDetect()
        {

            Process[] SF_Processes_Array = Process.GetProcessesByName("soldierfront");

            if (SF_Processes_Array.Length == 1)
            {
                CommonData.GameDetected = "Soldier Front";
                LogData.GameID = 1;

                return true;
            }

            Process[] SF2_Processes_Array = Process.GetProcessesByName("sf2");

            if (SF2_Processes_Array.Length == 1)
            {
                CommonData.GameDetected = "Soldier Front 2";
                LogData.GameID = 2;

                return true;
            }

            else
            {
                CommonData.GameDetected = "No games detected as running";
                return false;
            }
        }

        /* Assures: that user is running a legit game .exe
         * Prevents: multiple instances of the same game
         */
        public static bool DetectAndVerify()
        {

            Process[] SF_Processes_Array = Process.GetProcessesByName("soldierfront");
            Process[] SF2_Processes_Array = Process.GetProcessesByName("sf2");

            if (SF_Processes_Array.Length == 1)
            {
                CommonData.GameDetected = "Soldier Front";
                LogData.GameID = 1;

                foreach (Process soldierfront in SF_Processes_Array)
                {
                    try
                    {
                        LogData.GameExecPath = soldierfront.MainModule.FileName;

                        TimeSpan exe_start_time = DateTime.Now - soldierfront.StartTime;
                        TimeSpan time_span_sec = TimeSpan.FromSeconds(2);

                        if (exe_start_time > time_span_sec)
                        {
                            DetectAndKillAll();
                        }

                        using (System.IO.FileStream stream = System.IO.File.OpenRead(LogData.GameExecPath))
                        {
                            System.Security.Cryptography.SHA256Managed sha = new System.Security.Cryptography.SHA256Managed();
                            byte[] bytes = sha.ComputeHash(stream);
                            LogData.GameChecksum = BitConverter.ToString(bytes).Replace("-", String.Empty);

                            // soldierfront.exe doesn't have a product name, that's fine, we'll just add a fake one (for the log)
                            LogData.GameProductName = "Soldier Front";

                            // verify game exe's checksum
                            if (LogData.GameChecksum != MemorySignatures.g1)
                            {
                                // did not match... send in a log
                                SendGameChecksum.Run();
                            }
                        }
                    }
                    catch
                    {
                        DetectAndKillAll();
                    }
                }

                return true;

            }
            else if (SF2_Processes_Array.Length == 1)
            {
                CommonData.GameDetected = "Soldier Front 2";
                LogData.GameID = 2;

                foreach (Process soldierfront2 in SF2_Processes_Array)
                {
                    try
                    {
                        LogData.GameExecPath = soldierfront2.MainModule.FileName;

                        TimeSpan exe_start_time = DateTime.Now - soldierfront2.StartTime;
                        TimeSpan time_span_sec = TimeSpan.FromSeconds(2);

                        if (exe_start_time > time_span_sec)
                        {
                            DetectAndKillAll();
                        }

                        using (System.IO.FileStream stream = System.IO.File.OpenRead(LogData.GameExecPath))
                        {
                            System.Security.Cryptography.SHA256Managed sha = new System.Security.Cryptography.SHA256Managed();
                            byte[] bytes = sha.ComputeHash(stream);

                            LogData.GameChecksum = BitConverter.ToString(bytes).Replace("-", String.Empty);
                            LogData.GameProductName = soldierfront2.MainModule.FileVersionInfo.ProductName;

                            // verify game exe's checksum
                            if (LogData.GameChecksum != MemorySignatures.g2)
                            {
                                // did not match... send in a log
                                SendGameChecksum.Run();
                            }

                            if (LogData.GameProductName != "Soldier Front 2")
                            {
                                DetectAndKillAll();

                                Task.Factory.StartNew(() =>
                                {
                                    MessageBox.Show("Please only run Aeria Games version of Soldier Front 2.", "UGN Shield", MessageBoxButtons.OK);
                                });
                            }
                        }
                    }
                    catch
                    {
                        DetectAndKillAll();
                    }
                }

                return true;
            }

            else
            {
                CommonData.GameDetected = "No games detected as running";
                return false;
            }
        }

        /* Prevents: running multiple instances of the same game
         * Prevents: launch SF, then launch SF2 and inject hacks, then close SF
         */
        public static bool GameActiveSecurityChecks(int game_id)
        {

            if (game_id == 1)
            {
                Process[] SF_Processes_Array = Process.GetProcessesByName("soldierfront");

                // can only run 1 instance
                if (SF_Processes_Array.Length == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (game_id == 2)
            {
                Process[] SF2_Processes_Array = Process.GetProcessesByName("sf2");

                // can only run 1 instance
                if (SF2_Processes_Array.Length == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

    }

}

