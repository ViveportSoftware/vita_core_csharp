using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class WindowsSystemManagerTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var windowsSystemManager = WindowsSystemManager.GetInstance();
            Assert.NotNull(windowsSystemManager);
        }

        [Fact]
        public static void Default_1_Check()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var windowsSystemManager = WindowsSystemManager.GetInstance();
            var checkResult = windowsSystemManager.Check();
            var productName = checkResult.ProductName;
            Assert.False(string.IsNullOrWhiteSpace(productName));
            Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"ProductName: {productName}");
            var productType = checkResult.ProductType;
            Assert.NotEqual(WindowsSystemManager.WindowsProductType.Unknown, productType);
            Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"ProductType: {productType}");
            var productVersion = checkResult.ProductVersion;
            Assert.NotNull(productVersion);
            Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"ProductVersion: {productVersion}");
            var fipsStatus = checkResult.FipsStatus;
            Assert.NotEqual(WindowsSystemManager.WindowsFipsStatus.Unknown, fipsStatus);
            Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"FipsStatus: {fipsStatus}");
            var secureBootStatus = checkResult.SecureBootStatus;
            Assert.NotEqual(WindowsSystemManager.WindowsSecureBootStatus.Unknown, secureBootStatus);
            Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"SecureBootStatus: {secureBootStatus}");
        }

        [Fact]
        public static void Default_2_GetInstalledUpdateList()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var windowsSystemManager = WindowsSystemManager.GetInstance();
            var getInstalledUpdateListResult = windowsSystemManager.GetInstalledUpdateList();
            var getInstalledUpdateListStatus = getInstalledUpdateListResult.Status;
            Assert.Equal(WindowsSystemManager.GetInstalledUpdateListStatus.Ok, getInstalledUpdateListStatus);
            var installedUpdateList = getInstalledUpdateListResult.InstalledUpdateList;
            if (installedUpdateList.Count <= 0)
            {
                Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info("Do not find any installed WindowsUpdate");
                return;
            }

            var index = 0;
            foreach (var windowsUpdateInfo in installedUpdateList)
            {
                if (index == 0
                        || index == installedUpdateList.Count - 1)
                {
                    Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"installedUpdateList[{index}].Id: {windowsUpdateInfo.Id}");
                    Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"installedUpdateList[{index}].InstalledOn: {windowsUpdateInfo.InstalledOn}");
                }
                index++;
            }
        }

        [Fact]
        public static void Default_3_GetInstalledApplicationList()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var windowsSystemManager = WindowsSystemManager.GetInstance();
            var getInstalledApplicationListResult = windowsSystemManager.GetInstalledApplicationList();
            var getInstalledApplicationListStatus = getInstalledApplicationListResult.Status;
            Assert.Equal(WindowsSystemManager.GetInstalledApplicationListStatus.Ok, getInstalledApplicationListStatus);
            var installedApplicationList = getInstalledApplicationListResult.InstalledApplicationList;
            if (installedApplicationList.Count <= 0)
            {
                Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info("Do not find any installed Application");
                return;
            }

            var index = 0;
            foreach (var windowsApplicationInfo in installedApplicationList)
            {
                var displayName = windowsApplicationInfo.DisplayName;
                var displayVersion = windowsApplicationInfo.DisplayVersion;
                var installScope = windowsApplicationInfo.InstallScope;
                if (index == 0
                        || index == installedApplicationList.Count - 1
                        || string.IsNullOrWhiteSpace(displayName)
                        || displayVersion == null
                        || installScope == WindowsSystemManager.WindowsApplicationInstallScope.Unknown)
                {
                    Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"installedApplicationList[{index}].DisplayName: {displayName}");
                    Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"installedApplicationList[{index}].DisplayVersion: {displayVersion}");
                    Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"installedApplicationList[{index}].InstallScope: {installScope}");
                }
                index++;
            }
        }
    }
}
