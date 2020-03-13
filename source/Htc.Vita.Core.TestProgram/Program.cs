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
            Console.WriteLine($"SecurityProtocolManager.GetAvailableProtocol(): {SecurityProtocolManager.GetAvailableProtocol()}");
            Console.ReadKey();

            Console.WriteLine("NetworkInterface.IsNetworkAvailable(): " + NetworkInterface.IsNetworkAvailable());
            Console.WriteLine("NetworkInterface.IsInternetAvailable(): " + NetworkInterface.IsInternetAvailable());
            Console.ReadKey();

            Console.WriteLine("Platform.DetectProcessArch(): " + Platform.DetectProcessArch());
            Console.ReadKey();

            using (var usbWatcher = UsbWatcherFactory.GetInstance().CreateUsbWatcher())
            {
                usbWatcher.OnDeviceConnected += OnUsbDeviceConnected;
                usbWatcher.OnDeviceDisconnected += OnUsbDeviceDisconnected;
                var isRunning = usbWatcher.IsRunning();
                if (isRunning)
                {
                    Console.WriteLine("usbWatcher is running");
                }
                else
                {
                    Console.WriteLine("usbWatcher.Start(): " + usbWatcher.Start());
                }
                Console.ReadKey();

                Console.WriteLine("usbWatcher.Stop(): " + usbWatcher.Stop());
                Console.ReadKey();
            }

            var deviceInfos = UsbManager.GetHidDevices();
            var index = 0;
            foreach (var deviceInfo in deviceInfos)
            {
                Console.WriteLine($"deviceInfo[{index}].Path: \"{deviceInfo.Path}\"");
                Console.WriteLine($"deviceInfo[{index}].VendorId: \"{deviceInfo.VendorId}\"");
                Console.WriteLine($"deviceInfo[{index}].ProductId: \"{deviceInfo.ProductId}\"");
                Console.WriteLine($"deviceInfo[{index}].Description: \"{deviceInfo.Description}\"");
                Console.WriteLine($"deviceInfo[{index}].Manufacturer: \"{deviceInfo.Manufacturer}\"");
                Console.WriteLine($"deviceInfo[{index}].SerialNumber: \"{deviceInfo.SerialNumber}\"");
                Console.WriteLine($"deviceInfo[{index}].Product: \"{deviceInfo.Product}\"");
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

            deviceInfos = UsbManager.GetUsbDevices();
            index = 0;
            foreach (var deviceInfo in deviceInfos)
            {
                Console.WriteLine($"deviceInfo[{index}].Path: \"{deviceInfo.Path}\"");
                Console.WriteLine($"deviceInfo[{index}].VendorId: \"{deviceInfo.VendorId}\"");
                Console.WriteLine($"deviceInfo[{index}].ProductId: \"{deviceInfo.ProductId}\"");
                Console.WriteLine($"deviceInfo[{index}].Description: \"{deviceInfo.Description}\"");
                Console.WriteLine($"deviceInfo[{index}].Manufacturer: \"{deviceInfo.Manufacturer}\"");
                Console.WriteLine($"deviceInfo[{index}].SerialNumber: \"{deviceInfo.SerialNumber}\"");
                Console.WriteLine($"deviceInfo[{index}].Product: \"{deviceInfo.Product}\"");
                index++;
            }
            Console.ReadKey();

            deviceInfos = UsbManager.GetUsbHubDevices();
            index = 0;
            foreach (var deviceInfo in deviceInfos)
            {
                Console.WriteLine($"deviceInfo[{index}].Path: \"{deviceInfo.Path}\"");
                Console.WriteLine($"deviceInfo[{index}].VendorId: \"{deviceInfo.VendorId}\"");
                Console.WriteLine($"deviceInfo[{index}].ProductId: \"{deviceInfo.ProductId}\"");
                Console.WriteLine($"deviceInfo[{index}].Description: \"{deviceInfo.Description}\"");
                Console.WriteLine($"deviceInfo[{index}].Manufacturer: \"{deviceInfo.Manufacturer}\"");
                Console.WriteLine($"deviceInfo[{index}].SerialNumber: \"{deviceInfo.SerialNumber}\"");
                Console.WriteLine($"deviceInfo[{index}].Product: \"{deviceInfo.Product}\"");
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
            Console.ReadKey();

            var processWatcherFactory = ProcessWatcherFactory.GetInstance();
            var notepadProcessWatcher = processWatcherFactory.CreateProcessWatcher("notepad.exe");
            notepadProcessWatcher.ProcessCreated += OnProcessCreated;
            notepadProcessWatcher.ProcessDeleted += OnProcessDeleted;
            notepadProcessWatcher.Start();
            Console.WriteLine("notepadProcessWatcher is running");
            Console.ReadKey();

            notepadProcessWatcher.Stop();
            Console.WriteLine("notepadProcessWatcher is stopping");
            Console.ReadKey();

            var allProcessWatcher = processWatcherFactory.CreateProcessWatcher();
            allProcessWatcher.ProcessCreated += OnProcessCreated;
            allProcessWatcher.ProcessDeleted += OnProcessDeleted;
            allProcessWatcher.Start();
            Console.WriteLine("allProcessWatcher is running");
            Console.ReadKey();

            allProcessWatcher.Stop();
            Console.WriteLine("allProcessWatcher is stopping");
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
            var path = processInfo.Path ?? "<empty path>";
            Console.WriteLine("" + path + " (" + processInfo.Id + ") is created");
        }

        private static void OnProcessDeleted(ProcessWatcher.ProcessInfo processInfo)
        {
            var path = processInfo.Path ?? "<empty path>";
            Console.WriteLine("" + path + " (" + processInfo.Id + ") is deleted");
        }

        private static void OnUsbDeviceConnected()
        {
            Console.WriteLine("Some USB device connected");
        }

        private static void OnUsbDeviceDisconnected()
        {
            Console.WriteLine("Some USB device disconnected");
        }
    }
}
