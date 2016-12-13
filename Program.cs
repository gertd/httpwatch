using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Security.Permissions;
using System.Threading;

namespace HttpWatch
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly ManualResetEvent Exit = new ManualResetEvent(false);

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        static void Main(string[] args)
        {
            try
            {
                var app = new CommandLineApplication()
                {
                    Name = "httpwatch",
                    FullName = "HTTP Watch",
                    Description = "Monitor the HTTP endpoint",
                    ShortVersionGetter = () => "0.0.1"
                };
                app.HelpOption("--help");
                app.Command("monitor", c =>
                {
                    c.Description = "Monitor the http.sys event stream";
                    c.HelpOption("--help");
                    c.OnExecute(() =>
                    {
                        Console.WriteLine("monitor");
                        return 1;
                    });
                });
                app.Command("start", c =>
                {
                    c.Description = "Start the ETW event stream for http.sys";
                    c.HelpOption("--help");
                    c.OnExecute(() =>
                    {
                        EtwSessionManager.Start();
                        return 1;
                    });
                });
                app.Command("stop", c =>
                {
                    c.Description = "Stop the ETW event stream for http.sys";
                    c.HelpOption("--help");
                    c.OnExecute(() =>
                    {
                        EtwSessionManager.Stop();
                        return 2;
                    });

                });
                app.Execute(args);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                log.Error(ex.Message, ex);
            }
        }


        static void SetCtrlCHandler()
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                log.Info("Ctrl+C received, exiting...");
                Exit.Set();
            };
        }
    }

}
