using System;
using System.ServiceProcess;
using Htc.Vita.Core.Config;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.TestService
{
    internal static class Program
    {
        static Program()
        {
            Logger.Register<LoggerImpl>();
            Config.Config.Register<RegistryConfig>();
        }

        private static void Main(string[] args)
        {
            Logger.GetInstance(typeof(Program)).Info("=============================================================================");
            Logger.GetInstance(typeof(Program)).Info($"Last boot time: {Runtime.Platform.GetSystemBootTime()}");

            if (Environment.UserInteractive)
            {
                Console.WriteLine("Press any key to start...");
                Console.ReadKey(true);

                DefaultService.DoStart(args);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);

                DefaultService.DoStop();

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(true);
            }
            else
            {
                using (var defaultService = new DefaultService())
                {
                    ServiceBase.Run(defaultService);
                }
            }
        }
    }
}
