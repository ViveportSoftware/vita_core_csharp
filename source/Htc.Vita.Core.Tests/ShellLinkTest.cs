using System;
using System.IO;
using Htc.Vita.Core.Runtime;
using Htc.Vita.Core.Shell;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class ShellLinkTest
    {
        [Fact]
        public static void Default_0_Create_withFileLinkInfo()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var target = Environment.GetEnvironmentVariable("Temp");
            Assert.NotNull(target);
            var intermediatePathName = "ShellLink-" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            target = Path.Combine(target, intermediatePathName, "shell32.dll");
            var fileLinkInfo = new ShellLink.FileLinkInfo
            {
                    SourcePath = new FileInfo("C:\\Windows\\System32\\shell32.dll"),
                    TargetPath = new FileInfo(target),
                    TargetIconPath = new FileInfo("C:\\Windows\\System32\\shell32.dll"),
                    TargetIconIndex = 5
            };
            Assert.True(ShellLink.Create(fileLinkInfo));
        }

        [Fact]
        public static void Default_0_Create_withShellLinkInfo()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var target = Environment.GetEnvironmentVariable("Temp");
            Assert.NotNull(target);
            var intermediatePathName = "ShellLink-" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            target = Path.Combine(target, intermediatePathName, "notepad.exe");
            var shellLinkInfo = new ShellLink.ShellLinkInfo
            {
                    SourcePath = new FileInfo("C:\\Windows\\System32\\notepad.exe"),
                    TargetPath = new FileInfo(target),
                    TargetIconPath = new FileInfo("C:\\Windows\\System32\\shell32.dll"),
                    TargetIconIndex = 5,
                    Description = "MyDescription",
                    SourceAppId = "HTC.Vita.Tests",
                    SourceActivatorId = new Guid("3427c537-c3b5-4737-bae3-d7e3dc564d87"),
                    SourceArguments = "--test",
                    SourceWindowState = ShellLink.ShellLinkWindowState.Maximized,
                    SourceWorkingPath = new DirectoryInfo(Environment.GetEnvironmentVariable("Temp"))
            };
            Assert.True(ShellLink.Create(shellLinkInfo));
        }
    }
}
