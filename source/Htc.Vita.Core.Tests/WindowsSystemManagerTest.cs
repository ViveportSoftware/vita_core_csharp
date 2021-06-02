using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.Log;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class WindowsSystemManagerTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var windowsSystemManager = WindowsSystemManager.GetInstance();
            Assert.NotNull(windowsSystemManager);
        }

        [Fact]
        public static void Default_1_Check()
        {
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
                Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"installedUpdateList[{index}].Id: {windowsUpdateInfo.Id}");
                Logger.GetInstance(typeof(WindowsSystemManagerTest)).Info($"installedUpdateList[{index}].InstalledOn: {windowsUpdateInfo.InstalledOn}");
                index++;
            }
        }
    }
}
