using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class WindowsStoreAppManagerTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var windowsStoreAppManager = WindowsStoreAppManager.GetInstance();
            Assert.NotNull(windowsStoreAppManager);
        }

        [Fact]
        public static void Default_1_GetCurrentAppPackage()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var windowsStoreAppManager = WindowsStoreAppManager.GetInstance();
            var getAppPackageResult = windowsStoreAppManager.GetCurrentAppPackage();
            var getAppPackageStatus = getAppPackageResult.Status;
            Assert.True(getAppPackageStatus == WindowsStoreAppManager.GetAppPackageStatus.PackageNotFound);
            Assert.False(windowsStoreAppManager.IsIdentityAvailableWithCurrentProcess());
        }

        [Fact]
        public static void Default_2_GetAppPackageByFamilyName()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            const string packageFamilyName = "Microsoft.SkypeApp_kzf8qxf38zg5c";
            // const string packageFamilyName = "Microsoft.WindowsTerminal_8wekyb3d8bbwe";
            var windowsStoreAppManager = WindowsStoreAppManager.GetInstance();
            var getAppPackageResult = windowsStoreAppManager.GetAppPackageByFamilyName(packageFamilyName);
            var getAppPackageStatus = getAppPackageResult.Status;
            Assert.True(getAppPackageStatus == WindowsStoreAppManager.GetAppPackageStatus.PackageNotFound
                    || getAppPackageStatus == WindowsStoreAppManager.GetAppPackageStatus.Ok);

            if (getAppPackageStatus == WindowsStoreAppManager.GetAppPackageStatus.Ok)
            {
                var appPackageInfo = getAppPackageResult.AppPackage;
                Assert.NotNull(appPackageInfo);
                Logger.GetInstance(typeof(WindowsStoreAppManagerTest)).Info($"appPackageInfo.FamilyName: {appPackageInfo.FamilyName}");
                Logger.GetInstance(typeof(WindowsStoreAppManagerTest)).Info($"appPackageInfo.FullName: {appPackageInfo.FullName}");
                Logger.GetInstance(typeof(WindowsStoreAppManagerTest)).Info($"appPackageInfo.Path: {appPackageInfo.Path}");
            }
        }
    }
}
