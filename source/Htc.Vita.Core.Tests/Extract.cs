using System;
using System.IO;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class Extract
    {
        [Fact]
        public static void Default_0_FromFileToIconInWindows()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var sourceFileInfo = new FileInfo("C:\\Windows\\System32\\shell32.dll");
            Assert.True(sourceFileInfo.Exists);
            var target = Environment.GetEnvironmentVariable("Temp");
            Assert.NotNull(target);
            var intermediatePathName = "Icon-" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            target = Path.Combine(target, intermediatePathName, intermediatePathName, "shell32.ico");
            var targetFileInfo = new FileInfo(target);
            Assert.True(Util.Extract.FromFileToIcon(sourceFileInfo, targetFileInfo));
            Assert.True(targetFileInfo.Exists);
        }
    }
}
