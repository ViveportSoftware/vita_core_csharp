using System;
using System.IO;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void Extract_0_FromFileToIconInWindows()
        {
            var sourceFileInfo = new FileInfo("C:\\Windows\\System32\\shell32.dll");
            Assert.True(sourceFileInfo.Exists);
            var target = Environment.GetEnvironmentVariable("Temp");
            Assert.NotNull(target);
            var intermediatePathName = "Icon-" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            target = Path.Combine(target, intermediatePathName, intermediatePathName, "shell32.ico");
            var targetFileInfo = new FileInfo(target);
            Assert.True(Extract.FromFileToIcon(sourceFileInfo, targetFileInfo));
            Assert.True(targetFileInfo.Exists);
        }
    }
}
