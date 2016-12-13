using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.ServiceModel.Security;

namespace HttpWatch
{
    internal static class EtwSessionManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Microsoft-Windows-HttpService = {DD5EF90A-6398-47A4-AD34-4DCECDEF795F}
        private static readonly Guid ProviderId = new Guid("{DD5EF90A-6398-47A4-AD34-4DCECDEF795F}");

        public static void StartSession(string sessionName)
        {
            log.InfoFormat(CultureInfo.InvariantCulture, "StartSession({0})", sessionName);

            var identity = WindowsIdentity.GetCurrent();
            if (identity == null)
            {
                throw new IdentityNotMappedException("WindowsIdentity.GetCurrent");
            }

            var principal = new WindowsPrincipal(identity);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                throw new SecurityAccessDeniedException("To use ETW real-time session you must be administrator");
            }

            StopSession(sessionName);

            Process logman = Process.Start("logman.exe", "create trace " + sessionName + " -rt -nb 2 2 -bs 1024 -p {" + ProviderId + "} 0xffffffffffffffff -ets");
            if (logman != null)
            {
                logman.WaitForExit();
            }
        }

        public static void StopSession(string sessionName)
        {
            log.InfoFormat(CultureInfo.InvariantCulture, "StopSession({0})", sessionName);

            var identity = WindowsIdentity.GetCurrent();
            if (identity == null)
            {
                throw new IdentityNotMappedException("WindowsIdentity.GetCurrent");
            }

            var principal = new WindowsPrincipal(identity);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                throw new SecurityAccessDeniedException("To use ETW real-time session you must be administrator");
            }

            Process logman = Process.Start("logman.exe", "stop " + sessionName + " -ets");
            if (logman != null)
            {
                logman.WaitForExit();
            }
        }
    }
}