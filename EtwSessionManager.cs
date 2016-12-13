using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Principal;

namespace HttpWatch
{
    internal static class EtwSessionManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Microsoft-Windows-HttpService = {DD5EF90A-6398-47A4-AD34-4DCECDEF795F}
        private static readonly Guid ProviderId = new Guid("{DD5EF90A-6398-47A4-AD34-4DCECDEF795F}");
        private static readonly string LogManExe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "logman.exe");
        private const string SessionName = "HttpWatchListerner";

        public static void Start(string sessionName = SessionName)
        {
            log.InfoFormat(CultureInfo.InvariantCulture, "StartSession({0})", sessionName);

            Stop(sessionName);

            ExecuteLogman("create trace " + sessionName + " -rt -nb 2 2 -bs 1024 -p {" + ProviderId + "} 0xffffffffffffffff -ets");
        }

        public static void Stop(string sessionName = SessionName)
        {
            log.InfoFormat(CultureInfo.InvariantCulture, "StopSession({0})", sessionName);

            ExecuteLogman("stop " + sessionName + " -ets");
        }

        internal static void List()
        {
            /*
            EventTraceSessionCollection etsCollection =
        EventTraceSession.ActiveSessions;

            foreach (EventTraceSession ets in etsCollection)
            {
                Console.WriteLine("\n{0} - {1}", ets.LoggerName,
                    ets.LogFilePath);
                Console.WriteLine("\t*NEW: Provider Name '{0}'",
                    ets.ProviderName);
                Console.WriteLine("\t*NEW: Provider Description '{0}'",
                    ets.ProviderDescription);
            }
            */
        }

        internal static bool IsRunAsAdmin()
        {
            var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        internal static void ExecuteLogman(string arguments)
        {
            var psi = new ProcessStartInfo
            {
                UseShellExecute = false,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = LogManExe,
                Arguments = arguments,
                Verb = (IsRunAsAdmin() ? "" : "runas"),
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            Console.WriteLine("{0} {1}", 
                psi.FileName,
                psi.Arguments);

            try
            {
                Process logman = Process.Start(psi);
                if (logman != null)
                {
                    logman.WaitForExit();
                }
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("Exception {0}\n{1}", ex.Message, ex);
                log.ErrorFormat(CultureInfo.InvariantCulture, "Exception {0}\n{1}", ex.Message, ex);
            }
        }
    }
}