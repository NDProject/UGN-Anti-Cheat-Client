using System;
using System.Diagnostics;
using System.Windows.Forms;

// Be careful modifying this code, do good testing

namespace UGN_Client
{
    public class MemoryHeuristics
    {

        public static void Run()
        {

            int ProcessID = 0;
            string ProcessHeuristicsInfo = "";
            string cheatName = "";

            Process[] localProcesses = Process.GetProcesses(".");

            foreach (Process p in localProcesses)
            {

                try
                {

                    ProcessID = p.Id;
                    ProcessHeuristicsInfo = p.MainModule.ModuleMemorySize + " - " + p.MainModule.FileVersionInfo.InternalName + ".";

                    if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot1) == true)
                    {
                        cheatName = "Aim Junkies VIP" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot2) == true)
                    {
                        cheatName = "NXGEN VIP" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot3) == true)
                    {
                        cheatName = "Game Anarchy VIP" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot4) == true)
                    {
                        cheatName = "System Cheats VIP" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot5) == true)
                    {
                        cheatName = "GameMerk Public" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot6) == true)
                    {
                        cheatName = "GameMerk VIP" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot7) == true)
                    {
                        cheatName = "Mombot VIP" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot8) == true)
                    {
                        cheatName = "Pinoy Hideout Public" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot9) == true)
                    {
                        cheatName = "Pinoy Hideout VIP" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot10) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot11) == true)
                    {
                        cheatName = "Virtual Advantage (VIP)" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot12) == true)
                    {
                        cheatName = "GameMerk SF1 Dummy (VIP)" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot13) == true)
                    {
                        cheatName = "Lust Gaming" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot14) == true)
                    {
                        cheatName = "Unreal Cheats" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot15) == true)
                    {
                        cheatName = "PHGamers Public" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot16) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot17) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot18) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot19) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot20) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot21) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot22) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot23) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot24) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }
                    else if (ProcessHeuristicsInfo.Contains(MemorySignatures.slot25) == true)
                    {
                        cheatName = "N/A" + "\n\n";
                        break;
                    }

                }

                catch
                {

                }

            }

            if (cheatName != "")
            {

               // only send a detection log if a game is detected as running at the same time the cheat is or it is a forced log
               // improve this comment later to explain better
               if (GameDetector.SimpleDetect() == true)
               {

                    // prepare
                    Process suspiciousProcess = Process.GetProcessById(ProcessID);

                    string cheat_file_path = suspiciousProcess.MainModule.FileName;

                    long cheat_file_size = new System.IO.FileInfo(cheat_file_path).Length;

                    string suspiciousProcessDesc = "[b]Path:[/b] " + cheat_file_path + "\n [b]Name:[/b] " + suspiciousProcess.MainModule.ModuleName + "\n [b]Size:[/b] " + cheat_file_size + " bytes" + "\n [b]Version:[/b] " + suspiciousProcess.MainModule.FileVersionInfo.FileVersion + "\n [b]Signature:[/b] ";
                    
                    LogData.AlertLevel = 3;
                    LogData.Memory = cheatName + suspiciousProcessDesc + ProcessHeuristicsInfo;

                    // send
                    SendMemoryDetection.Run();

                    // clean up
                    LogData.Memory = "";
                    LogData.AlertLevel = 0;

                }

            }
           
        }

    }

}

