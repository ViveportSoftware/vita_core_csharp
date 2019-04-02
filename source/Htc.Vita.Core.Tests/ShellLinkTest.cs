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
        public static void Default_0_CreateInWindows()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var target = Environment.GetEnvironmentVariable("Temp");
            Assert.NotNull(target);
            var intermediatePathName = "ShellLink-" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            target = Path.Combine(target, intermediatePathName, intermediatePathName, "shell32.dll");
            var fileLinkInfo = new ShellLink.FileLinkInfo
            {
                SourcePath = new FileInfo("C:\\Windows\\System32\\shell32.dll"),
                TargetPath = new FileInfo(target),
                TargetIconPath = new FileInfo("C:\\Windows\\System32\\shell32.dll"),
                TargetIconIndex = 5
            };
            Assert.True(ShellLink.Create(fileLinkInfo));
        }
    }
}
