using System;
using Htc.Vita.Core.IO;
using Htc.Vita.Core.Net;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.TestProgram
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("NetworkInterface.IsNetworkAvailable(): " + NetworkInterface.IsNetworkAvailable());
            Console.WriteLine("NetworkInterface.IsInternetAvailable(): " + NetworkInterface.IsInternetAvailable());
            Console.ReadKey();

            Console.WriteLine("Platform.DetectProcessArch(): " + Platform.DetectProcessArch());
            Console.ReadKey();

            var deviceInfos = UsbManager.GetHidDevices();
            var index = 0;
            foreach (var deviceInfo in deviceInfos)
            {
                Console.WriteLine($"deviceInfo[{index}].Path: \"{deviceInfo.Path}\"");
                Console.WriteLine($"deviceInfo[{index}].VendorId: \"{deviceInfo.VendorId}\"");
                Console.WriteLine($"deviceInfo[{index}].ProductId: \"{deviceInfo.ProductId}\"");
                Console.WriteLine($"deviceInfo[{index}].SerialNumber: \"{deviceInfo.SerialNumber}\"");
                var featureReport = UsbManager.GetHidFeatureReport(deviceInfo.Path, 0);
                if (featureReport == null)
                {
                    Console.WriteLine($"deviceInfo[{index}].featureReport is null");
                }
                else
                {
                    Console.WriteLine($"deviceInfo[{index}].featureReport is {Util.Convert.ToHexString(featureReport)}");
                }
                index++;
            }
            Console.ReadKey();

            var randomUnusedPort = LocalPortManager.GetRandomUnusedPort();
            var specificPort = 35447;
            var randomUnusedPortStatus = LocalPortManager.GetPortStatus(randomUnusedPort);
            var specificPortStatus = LocalPortManager.GetPortStatus(specificPort);
            var randomUnusedPortVerifyStatus = LocalPortManager.VerifyPortStatus(randomUnusedPort);
            var specificPortVerifyStatus = LocalPortManager.VerifyPortStatus(specificPort);
            Console.WriteLine("Random unused port [" + randomUnusedPort + "] status: " + randomUnusedPortStatus + ", verify: " + randomUnusedPortVerifyStatus);
            Console.WriteLine("Specific port [" + specificPort + "] status: " + specificPortStatus + ", verify: " + specificPortVerifyStatus);

            var processWatcherFactory = ProcessWatcherFactory.GetInstance();
            var notepadProcessWatcher = processWatcherFactory.CreateProcessWatcher("notepad.exe");
            notepadProcessWatcher.ProcessCreated += OnProcessCreated;
            notepadProcessWatcher.ProcessDeleted += OnProcessDeleted;
            notepadProcessWatcher.Start();
            Console.ReadKey();

            notepadProcessWatcher.Stop();
            Console.ReadKey();

            var allProcessWatcher = processWatcherFactory.CreateProcessWatcher();
            allProcessWatcher.ProcessCreated += OnProcessCreated;
            allProcessWatcher.ProcessDeleted += OnProcessDeleted;
            allProcessWatcher.Start();
            Console.ReadKey();

            allProcessWatcher.Stop();
            Console.ReadKey();

            var serviceName = "Winmgmt";
            Console.WriteLine("service[" + serviceName + "].CurrentState: " + ServiceManager.QueryStartType(serviceName).CurrentState);
            Console.ReadKey();

            Console.WriteLine("WARNING!! This program will reboot system");
            Console.ReadKey();

            Platform.Exit(Platform.ExitType.Reboot);
            Console.ReadKey();
        }

        private static void OnProcessCreated(ProcessWatcher.ProcessInfo processInfo)
        {
            Console.WriteLine("" + processInfo.Path + " (" + processInfo.Id + ") is created");
        }

        private static void OnProcessDeleted(ProcessWatcher.ProcessInfo processInfo)
        {
            Console.WriteLine("" + processInfo.Path + " (" + processInfo.Id + ") is deleted");
        }
    }
}
