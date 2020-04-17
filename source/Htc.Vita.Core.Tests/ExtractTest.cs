using System;
using System.IO;
using System.Reflection;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class ExtractTest
    {
        [Fact]
        public static void Default_0_FromFileToIconInWindows()
        {
            if (!Runtime.Platform.IsWindows)
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
            Assert.True(Extract.FromFileToIcon(sourceFileInfo, targetFileInfo));
            Assert.True(targetFileInfo.Exists);
        }

        [Fact]
        public static void Default_0_FromAssemblyToFileByResourceName()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var fileName = "TestData.Sha1.txt";
            var target = Environment.GetEnvironmentVariable("Temp");
            Assert.NotNull(target);
            var destPath = Path.Combine(target, fileName);
            var file = new FileInfo(destPath);

            var result = Extract.FromAssemblyToFileByResourceName(
                    assemblyName + "." + fileName + ".gz",
                    file,
                    Extract.CompressionType.Gzip
            );
            Assert.True(result);
            Assert.Equal("9eJAeMCTbKeIFSYOfVjRqUCWbro=", Crypto.Sha1.GetInstance().GenerateInBase64(file));

            try
            {
                Directory.Delete(destPath, true);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
