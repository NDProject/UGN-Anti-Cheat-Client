using System;
using System.IO;
using System.Text;

namespace UGN_Client
{
    public class Logger
    {
        public static void LogError(string error_string)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(DateTime.Now.ToString("HH:mm:ss tt") + ": " + error_string + Environment.NewLine);

            File.AppendAllText(CommonData.LogsFolder + CommonData.ErrorLogFile, sb.ToString());
            sb.Clear();
        }
    }
}
