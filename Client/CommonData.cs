using System;
using System.IO;

namespace UGN_Client
{
    public class CommonData
    {
        public static string UID { get; set; }
        public static string GID { get; set; }
        public static string Username { get; set; }
        public static string Read { get; set; }
        public static string statUsersOnline { get; set; }
        public static string statShame { get; set; }
        public static string statUsersOnlineClient { get; set; }
        public static string authToken { get; set; }
        public static string clientSecret = "QtLApEaseJ9YhPE626je";
        public static string GameDetected { get; set; }
        public static string primDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
        public static string DataFolder = @"" + primDrive + "UGN\\";
        public static string LogsFolder = DataFolder + "logs\\";
        public static string ErrorLogFile = "errors.txt";
        public static bool publicSSready { get; set; }
        public static string SSchecksum { get; set; }
        public static bool ScreenshotDelayActive { get; set; }
        public static DateTime? timer10 = null;
        public static int timer10counter = 0;
    }
}
