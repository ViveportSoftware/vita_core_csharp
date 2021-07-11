using System;
using System.Diagnostics;
using System.IO;
using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.IO;
using Htc.Vita.Core.Net;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.TestProgram
{
    internal static class Program
    {
        private const int MaxPath = 260 - 1;

        private static void Main()
        {
            /*
            var testServiceName = "HtcVitaCoreTestService";
            var exists = ServiceManager.CheckIfExists(testServiceName);
            if (!exists)
            {
                Console.WriteLine($"service[{testServiceName}] does NOT exist");
            }
            else
            {
                var testServiceInfo = ServiceManager.QueryStartType(testServiceName);
                var testServiceCurrentState = testServiceInfo.CurrentState;
                var testServiceStartType = testServiceInfo.StartType;
                Console.WriteLine($"service[{testServiceName}].CurrentState: {testServiceCurrentState}");
                Console.WriteLine($"service[{testServiceName}].StartType: {testServiceStartType}");
                Console.ReadKey();
                if (testServiceCurrentState == ServiceManager.CurrentState.Running)
                {
                    Console.WriteLine($"service[{testServiceName}] is running. skipped");
                }
                else
                {
                    var newStartType = ServiceManager.StartType.DelayedAutomatic;
                    Console.WriteLine($"Change start type to: {newStartType}");
                    var testServiceInfo2 = ServiceManager.ChangeStartType(testServiceName, newStartType);
                    var testServiceStartType2 = testServiceInfo2.StartType;
                    Console.WriteLine($"service[{testServiceName}].StartType: {testServiceStartType2}");
                    Console.ReadKey();

                    newStartType = ServiceManager.StartType.Manual;
                    Console.WriteLine($"Change start type to: {newStartType}");
                    var testServiceInfo3 = ServiceManager.ChangeStartType(testServiceName, newStartType);
                    var testServiceStartType3 = testServiceInfo3.StartType;
                    Console.WriteLine($"service[{testServiceName}].StartType: {testServiceStartType3}");
                    Console.ReadKey();

                    newStartType = ServiceManager.StartType.Automatic;
                    Console.WriteLine($"Change start type to: {newStartType}");
                    var testServiceInfo4 = ServiceManager.ChangeStartType(testServiceName, newStartType);
                    var testServiceStartType4 = testServiceInfo4.StartType;
                    Console.WriteLine($"service[{testServiceName}].StartType: {testServiceStartType4}");
                    Console.ReadKey();
                }
            }
            */

            SecurityProtocolManager.ApplyAvailableProtocol();

            Console.WriteLine($"SecurityProtocolManager.GetAvailableProtocol(): {SecurityProtocolManager.GetAvailableProtocol()}");
            Console.ReadKey();

            var windowsStoreAppManager = WindowsStoreAppManager.GetInstance();
            Console.WriteLine($"windowsStoreAppManager.IsIdentityAvailableWithCurrentProcess(): {windowsStoreAppManager.IsIdentityAvailableWithCurrentProcess()}");
            const string packageFamilyName = "Microsoft.SkypeApp_kzf8qxf38zg5c";
            var getAppPackageResult = windowsStoreAppManager.GetAppPackageByFamilyName(packageFamilyName);
            var getAppPackageStatus = getAppPackageResult.Status;
            if (getAppPackageStatus != WindowsStoreAppManager.GetAppPackageStatus.Ok)
            {
                Console.WriteLine($"Can not get app package. status: {getAppPackageStatus}");
            }
            else
            {
                var appPackageInfo = getAppPackageResult.AppPackage;
                Console.WriteLine($"appPackageInfo.FamilyName: {appPackageInfo.FamilyName}");
                Console.WriteLine($"appPackageInfo.FullName: {appPackageInfo.FullName}");
            }
            Console.ReadKey();

            var fileSystemManagerV2 = FileSystemManagerV2.GetInstance();
            var path = new DirectoryInfo("C:\\");
            var depth = MaxPath;
            var verifyPathDepthResult = fileSystemManagerV2.VerifyPathDepth(path, depth);
            var verifyPathDepthStatus = verifyPathDepthResult.Status;
            Console.WriteLine($"Path: {path}, depth: [{depth}/{path.ToString().Length + depth}], verifyPathDepthStatus: {verifyPathDepthStatus}");

            depth = MaxPath - 10;
            verifyPathDepthResult = fileSystemManagerV2.VerifyPathDepth(path, depth);
            verifyPathDepthStatus = verifyPathDepthResult.Status;
            Console.WriteLine($"Path: {path}, depth: [{depth}/{path.ToString().Length + depth}], verifyPathDepthStatus: {verifyPathDepthStatus}");

            var tempPathString = Environment.GetEnvironmentVariable("Temp") ?? string.Empty;
            path = new DirectoryInfo(tempPathString);
            depth = MaxPath + 3000;
            verifyPathDepthResult = fileSystemManagerV2.VerifyPathDepth(path, depth);
            verifyPathDepthStatus = verifyPathDepthResult.Status;
            Console.WriteLine($"Path: {path}, depth: [{depth}/{path.ToString().Length + 1 + depth}], verifyPathDepthStatus: {verifyPathDepthStatus}");

            depth = MaxPath - tempPathString.Length - 1;
            while (true)
            {
                verifyPathDepthResult = fileSystemManagerV2.VerifyPathDepth(path, depth);
                verifyPathDepthStatus = verifyPathDepthResult.Status;
                if (verifyPathDepthStatus == FileSystemManagerV2.VerifyPathDepthStatus.Ok || depth < 2)
                {
                    Console.WriteLine($"Path: {path}, depth: [{depth}/{path.ToString().Length + 1 + depth}], verifyPathDepthStatus: {verifyPathDepthStatus}");
                    break;
                }
                depth--;
            }

            var webBrowserManager = WebBrowserManager.GetInstance();
            var getInstalledWebBrowserListResult = webBrowserManager.GetInstalledWebBrowserList();
            var getInstalledWebBrowserListStatus = getInstalledWebBrowserListResult.Status;
            if (getInstalledWebBrowserListStatus != WebBrowserManager.GetInstalledWebBrowserListStatus.Ok)
            {
                Console.WriteLine($"Can not get installed web browser list. Status: {getInstalledWebBrowserListStatus}");
            }
            else
            {
                var webBrowserList = getInstalledWebBrowserListResult.WebBrowserList;
                var webBrowserIndex = 0;
                foreach (var webBrowserInfo in webBrowserList)
                {
                    Console.WriteLine($"webBrowserInfo[{webBrowserIndex}].Type: {webBrowserInfo.Type}");
                    Console.WriteLine($"webBrowserInfo[{webBrowserIndex}].DisplayName: {webBrowserInfo.DisplayName}");
                    Console.WriteLine($"webBrowserInfo[{webBrowserIndex}].LaunchPath: {webBrowserInfo.LaunchPath}");
                    Console.WriteLine($"webBrowserInfo[{webBrowserIndex}].SupportedScheme: {webBrowserInfo.SupportedScheme}");
                    webBrowserIndex++;
                }
            }
            Console.ReadKey();

            var windowsSystemManager = WindowsSystemManager.GetInstance();
            var checkResult = windowsSystemManager.Check();
            Console.WriteLine($"checkResult.ProductName: {checkResult.ProductName}");
            Console.WriteLine($"checkResult.ProductType: {checkResult.ProductType}");
            Console.WriteLine($"checkResult.ProductVersion: {checkResult.ProductVersion}");
            Console.WriteLine($"checkResult.FipsStatus: {checkResult.FipsStatus}");
            Console.WriteLine($"checkResult.SecureBootStatus: {checkResult.SecureBootStatus}");

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
            var serviceInfo = ServiceManager.QueryStartType(serviceName);
            Console.WriteLine($"service[{serviceName}].CurrentState: {serviceInfo.CurrentState}");
            Console.WriteLine($"service[{serviceName}].StartType: {serviceInfo.StartType}");
            serviceName = "BITS";
            serviceInfo = ServiceManager.QueryStartType(serviceName);
            Console.WriteLine($"service[{serviceName}].CurrentState: {serviceInfo.CurrentState}");
            Console.WriteLine($"service[{serviceName}].StartType: {serviceInfo.StartType}");
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
