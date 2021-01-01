using System;
using System.Diagnostics;
using Htc.Vita.Core.IO;
using Htc.Vita.Core.Net;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.TestProgram
{
    internal static class Program
    {
        private static void Main()
        {
            SecurityProtocolManager.ApplyAvailableProtocol();

            Console.WriteLine($"SecurityProtocolManager.GetAvailableProtocol(): {SecurityProtocolManager.GetAvailableProtocol()}");
            Console.ReadKey();

            var jobIdList = FileTransfer.GetInstance().GetJobIdList();
            if (jobIdList.Count > 0)
            {
                foreach (var jobId in jobIdList)
                {
                    var job = FileTransfer.GetInstance().GetJob(jobId);
                    Console.WriteLine($"job[{jobId}] name: {job.GetDisplayName()}, state: {job.GetState()}");
                }
            }
            else
            {
                Console.WriteLine("Do not find existing transfer job");
            }
            Console.ReadKey();

            Console.WriteLine($"NetworkInterface.IsNetworkAvailable(): {NetworkInterface.IsNetworkAvailable()}");
            Console.WriteLine($"NetworkInterface.IsInternetAvailable(): {NetworkInterface.IsInternetAvailable()}");
            Console.ReadKey();

            Console.WriteLine($"Platform.DetectProcessArch(): {Platform.DetectProcessArch()}");
            Console.ReadKey();

            Console.WriteLine($"ProcessManager.IsElevatedProcess(Process.GetCurrentProcess()): {ProcessManager.IsElevatedProcess(Process.GetCurrentProcess())}");
            Console.ReadKey();

            Console.WriteLine($"UserManager.IsShellUserElevated(): {UserManager.IsShellUserElevated()}");
            Console.ReadKey();

            var processInfoAsShellUser = ProcessManager.LaunchProcessAsShellUser("C:\\Windows\\System32\\notepad.exe", "");
            if (processInfoAsShellUser == null)
            {
                Console.WriteLine("processInfoAsShellUser == null");
            }
            else
            {
                Console.WriteLine($"processInfoAsShellUser.Id: {processInfoAsShellUser.Id}");
                Console.WriteLine($"processInfoAsShellUser.Name: {processInfoAsShellUser.Name}");
                Console.WriteLine($"processInfoAsShellUser.Path: {processInfoAsShellUser.Path}");
            }
            Console.ReadKey();

            var wpdDeviceInfos = WpdManager.GetDevices();
            var index = 0;
            foreach (var wpdDeviceInfo in wpdDeviceInfos)
            {
                Console.WriteLine($"wpdDeviceInfo[{index}].Path: \"{wpdDeviceInfo.Path}\"");
                Console.WriteLine($"wpdDeviceInfo[{index}].Description: \"{wpdDeviceInfo.Description}\"");
                Console.WriteLine($"wpdDeviceInfo[{index}].Manufacturer: \"{wpdDeviceInfo.Manufacturer}\"");
                Console.WriteLine($"wpdDeviceInfo[{index}].Product: \"{wpdDeviceInfo.Product}\"");
                Console.WriteLine($"wpdDeviceInfo[{index}].ProductId: \"{wpdDeviceInfo.ProductId}\"");
                Console.WriteLine($"wpdDeviceInfo[{index}].VendorId: \"{wpdDeviceInfo.VendorId}\"");
                Console.WriteLine($"wpdDeviceInfo[{index}].SerialNumber: \"{wpdDeviceInfo.SerialNumber}\"");
                index++;
            }

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

            var hidDeviceInfos = UsbManager.GetHidDevices();
            index = 0;
            foreach (var hidDeviceInfo in hidDeviceInfos)
            {
                Console.WriteLine($"hidDeviceInfo[{index}].Path: \"{hidDeviceInfo.Path}\"");
                Console.WriteLine($"hidDeviceInfo[{index}].VendorId: \"{hidDeviceInfo.VendorId}\"");
                Console.WriteLine($"hidDeviceInfo[{index}].ProductId: \"{hidDeviceInfo.ProductId}\"");
                Console.WriteLine($"hidDeviceInfo[{index}].Description: \"{hidDeviceInfo.Description}\"");
                Console.WriteLine($"hidDeviceInfo[{index}].Manufacturer: \"{hidDeviceInfo.Manufacturer}\"");
                Console.WriteLine($"hidDeviceInfo[{index}].SerialNumber: \"{hidDeviceInfo.SerialNumber}\"");
                Console.WriteLine($"hidDeviceInfo[{index}].Product: \"{hidDeviceInfo.Product}\"");
                var featureReport = UsbManager.GetHidFeatureReport(hidDeviceInfo.Path, 0);
                if (featureReport == null)
                {
                    Console.WriteLine($"hidDeviceInfo[{index}].featureReport is null");
                }
                else
                {
                    Console.WriteLine($"hidDeviceInfo[{index}].featureReport is {Util.Convert.ToHexString(featureReport)}");
                }
                index++;
            }
            Console.ReadKey();

            var usbDeviceInfos = UsbManager.GetUsbDevices();
            index = 0;
            foreach (var usbDeviceInfo in usbDeviceInfos)
            {
                Console.WriteLine($"usbDeviceInfo[{index}].Path: \"{usbDeviceInfo.Path}\"");
                Console.WriteLine($"usbDeviceInfo[{index}].VendorId: \"{usbDeviceInfo.VendorId}\"");
                Console.WriteLine($"usbDeviceInfo[{index}].ProductId: \"{usbDeviceInfo.ProductId}\"");
                Console.WriteLine($"usbDeviceInfo[{index}].Description: \"{usbDeviceInfo.Description}\"");
                Console.WriteLine($"usbDeviceInfo[{index}].Manufacturer: \"{usbDeviceInfo.Manufacturer}\"");
                Console.WriteLine($"usbDeviceInfo[{index}].SerialNumber: \"{usbDeviceInfo.SerialNumber}\"");
                Console.WriteLine($"usbDeviceInfo[{index}].Product: \"{usbDeviceInfo.Product}\"");
                index++;
            }
            Console.ReadKey();

            var usbHubDeviceInfos = UsbManager.GetUsbHubDevices();
            index = 0;
            foreach (var usbHubDeviceInfo in usbHubDeviceInfos)
            {
                Console.WriteLine($"usbHubDeviceInfo[{index}].Path: \"{usbHubDeviceInfo.Path}\"");
                Console.WriteLine($"usbHubDeviceInfo[{index}].VendorId: \"{usbHubDeviceInfo.VendorId}\"");
                Console.WriteLine($"usbHubDeviceInfo[{index}].ProductId: \"{usbHubDeviceInfo.ProductId}\"");
                Console.WriteLine($"usbHubDeviceInfo[{index}].Description: \"{usbHubDeviceInfo.Description}\"");
                Console.WriteLine($"usbHubDeviceInfo[{index}].Manufacturer: \"{usbHubDeviceInfo.Manufacturer}\"");
                Console.WriteLine($"usbHubDeviceInfo[{index}].SerialNumber: \"{usbHubDeviceInfo.SerialNumber}\"");
                Console.WriteLine($"usbHubDeviceInfo[{index}].Product: \"{usbHubDeviceInfo.Product}\"");
                index++;
            }
            Console.ReadKey();

            var randomUnusedPort = LocalPortManager.GetRandomUnusedPort();
            var specificPort = 35447;
            var randomUnusedPortStatus = LocalPortManager.GetPortStatus(randomUnusedPort);
            var specificPortStatus = LocalPortManager.GetPortStatus(specificPort);
            var randomUnusedPortVerifyStatus = LocalPortManager.VerifyPortStatus(randomUnusedPort);
            var specificPortVerifyStatus = LocalPortManager.VerifyPortStatus(specificPort);
            Console.WriteLine($"Random unused port [{randomUnusedPort}] status: {randomUnusedPortStatus}, verify: {randomUnusedPortVerifyStatus}");
            Console.WriteLine($"Specific port [{specificPort}] status: {specificPortStatus}, verify: {specificPortVerifyStatus}");
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
            Console.WriteLine($"service[{serviceName}].CurrentState: {ServiceManager.QueryStartType(serviceName).CurrentState}");
            Console.ReadKey();

            Console.WriteLine("WARNING!! This program will reboot system");
            Console.ReadKey();

            Platform.Exit(Platform.ExitType.Reboot);
            Console.ReadKey();
        }

        private static void OnProcessCreated(ProcessWatcher.ProcessInfo processInfo)
        {
            var path = processInfo.Path ?? "<empty path>";
            Console.WriteLine($"{path} ({processInfo.Id}) is created");
        }

        private static void OnProcessDeleted(ProcessWatcher.ProcessInfo processInfo)
        {
            var path = processInfo.Path ?? "<empty path>";
            Console.WriteLine($"{path} ({processInfo.Id}) is deleted");
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
