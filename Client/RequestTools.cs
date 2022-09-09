using System;

namespace UGN_Client
{
    class RequestTools
    {
        public static void XPfix ()
        {
            OperatingSystem OS = Environment.OSVersion;

            // Only if XP
            if ((OS.Platform == PlatformID.Win32NT) && (OS.Version.Major == 5))
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            }
        }
    }
}
