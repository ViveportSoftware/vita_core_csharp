using System;
using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class WebBrowserManagerTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var webBrowserManager = WebBrowserManager.GetInstance();
            Assert.NotNull(webBrowserManager);
        }

        [Fact]
        public static void Default_1_GetInstalledWebBrowserList()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var webBrowserManager = WebBrowserManager.GetInstance();
            var getInstalledWebBrowserListResult = webBrowserManager.GetInstalledWebBrowserList();
            var getInstalledWebBrowserListStatus = getInstalledWebBrowserListResult.Status;
            Assert.Equal(WebBrowserManager.GetInstalledWebBrowserListStatus.Ok, getInstalledWebBrowserListStatus);
            var webBrowserList = getInstalledWebBrowserListResult.WebBrowserList;
            if (webBrowserList.Count <= 0)
            {
                Logger.GetInstance(typeof(WebBrowserManagerTest)).Info("Do not find any installed web browser");
                return;
            }

            var index = 0;
            foreach (var webBrowserInfo in webBrowserList)
            {
                var webBrowserType = webBrowserInfo.Type;
                Logger.GetInstance(typeof(WebBrowserManagerTest)).Info($"webBrowserList[{index}].Type: {webBrowserType}");
                Logger.GetInstance(typeof(WebBrowserManagerTest)).Info($"webBrowserList[{index}].DisplayName: {webBrowserInfo.DisplayName}");
                Logger.GetInstance(typeof(WebBrowserManagerTest)).Info($"webBrowserList[{index}].LaunchPath: {webBrowserInfo.LaunchPath}");
                var launchParams = webBrowserInfo.LaunchParams;
                if (launchParams == null)
                {
                    Logger.GetInstance(typeof(WebBrowserManagerTest)).Info($"webBrowserList[{index}].LaunchParams: null");
                }
                else
                {
                    var index2 = 0;
                    foreach (var launchParam in launchParams)
                    {
                        Logger.GetInstance(typeof(WebBrowserManagerTest)).Info($"webBrowserList[{index}].LaunchParams[{index2}]: {launchParam}");
                        index2++;
                    }
                }
                if (webBrowserType == WebBrowserManager.WebBrowserType.Unknown)
                {
                    Logger.GetInstance(typeof(WebBrowserManagerTest)).Info($"webBrowserList[{index}].SchemeDisplayName: {webBrowserInfo.SchemeDisplayName}");
                }
                Logger.GetInstance(typeof(WebBrowserManagerTest)).Info($"webBrowserList[{index}].SupportedScheme: {webBrowserInfo.SupportedScheme}");
                index++;
            }
        }

        [Fact]
        public static void Default_2_Launch()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var webBrowserManager = WebBrowserManager.GetInstance();
            Assert.True(webBrowserManager.Launch(new Uri("https://www.microsoft.com")));
        }
    }
}
