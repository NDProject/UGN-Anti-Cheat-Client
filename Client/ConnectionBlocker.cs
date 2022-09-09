using System;
using System.IO;

namespace UGN_Client
{
    public class ConnectionBlocker
    {

        public static void unblock()
        {
            string primDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
            try
            {
                using (StreamWriter writer = new StreamWriter(primDrive + @"WINDOWS\system32\drivers\etc\temp"))
                {
                    writer.WriteLine("# Copyright (c) 1993-1999 Microsoft Corp.");
                    writer.WriteLine("#");
                    writer.WriteLine("# This is a sample HOSTS file used by Microsoft TCP/IP for Windows.");
                    writer.WriteLine("#");
                    writer.WriteLine("# This file contains the mappings of IP addresses to host names. Each");
                    writer.WriteLine("# entry should be kept on an individual line. The IP address should");
                    writer.WriteLine("# be placed in the first column followed by the corresponding host name.");
                    writer.WriteLine("# The IP address and the host name should be separated by at least one");
                    writer.WriteLine("# space.");
                    writer.WriteLine("#");
                    writer.WriteLine("# Additionally, comments (such as these) may be inserted on individual");
                    writer.WriteLine("# lines or following the machine name denoted by a '#' symbol.");
                    writer.WriteLine("#");
                    writer.WriteLine("# For example:");
                    writer.WriteLine("#");
                    writer.WriteLine("#      102.54.94.97     rhino.acme.com          # source server");
                    writer.WriteLine("#       38.25.63.10     x.acme.com              # x client host");
            }
                replace();
            }

            catch { }
        
        }

        public static void replace()
        {
            string primDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
            File.Delete(primDrive + @"WINDOWS\system32\drivers\etc\hosts");
            File.Copy(primDrive + @"WINDOWS\system32\drivers\etc\temp", @"C:\WINDOWS\system32\drivers\etc\hosts");
            File.Delete(primDrive + @"WINDOWS\system32\drivers\etc\temp");
        }

    }
}
