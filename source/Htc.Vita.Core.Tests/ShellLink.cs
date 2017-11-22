using System;
using System.IO;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class ShellLink
    {
        [Fact]
        public static void Default_0_CreateInWindows()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var target = Environment.GetEnvironmentVariable("Temp");
            Assert.NotNull(target);
            var intermediatePathName = "ShellLink-" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            target = Path.Combine(target, intermediatePathName, intermediatePathName, "shell32.dll");
            var fileLinkInfo = new Shell.ShellLink.FileLinkInfo
            {
                SourcePath = new FileInfo("C:\\Windows\\System32\\shell32.dll"),
                TargetPath = new FileInfo(target),
                TargetIconPath = new FileInfo("C:\\Windows\\System32\\shell32.dll"),
                TargetIconIndex = 5
            };
            Assert.True(Shell.ShellLink.Create(fileLinkInfo));
        }
    }
}
