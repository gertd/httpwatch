using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.CommandLine;

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
                var parsedArgs = new CmdLineArgs();
                if (Parser.ParseArguments(args, parsedArgs))
                {
                    try
                    {
                        // Console.CancelKeyPress += (sender, eventArgs) => Exit.Set();

                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message, ex);
                    }
                }
                else
                {
                    Console.Out.Write(Parser.ArgumentsUsage(typeof(CmdLineArgs)));
                }
            }
            catch (Exception ex)
            {
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
