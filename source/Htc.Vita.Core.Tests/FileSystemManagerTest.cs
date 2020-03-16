using System.IO;
using Htc.Vita.Core.IO;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class FileSystemManagerTest
    {
        private readonly ITestOutputHelper _output;

        public FileSystemManagerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Default_0_GetDiskSpaceFor()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var directoryInfo = new DirectoryInfo("C:\\");
            Assert.True(directoryInfo.Exists);
            var diskSpaceInfo = FileSystemManager.GetDiskSpaceFor(directoryInfo);
            Assert.NotNull(diskSpaceInfo);
            _output.WriteLine("diskSpaceInfo.Path: " + diskSpaceInfo.Path);
            Assert.False(string.IsNullOrWhiteSpace(diskSpaceInfo.Path));
            _output.WriteLine("diskSpaceInfo.FreeOfBytes: " + diskSpaceInfo.FreeOfBytes);
            Assert.True(diskSpaceInfo.FreeOfBytes >= 0);
            _output.WriteLine("diskSpaceInfo.TotalOfBytes: " + diskSpaceInfo.TotalOfBytes);
            Assert.True(diskSpaceInfo.TotalOfBytes >= 0);
            _output.WriteLine("diskSpaceInfo.TotalFreeOfBytes: " + diskSpaceInfo.TotalFreeOfBytes);
            Assert.True(diskSpaceInfo.TotalFreeOfBytes >= 0);
        }

        [Fact]
        public static void Default_0_GetDiskSpaceFor_WithNonExistPath()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var directoryInfo = new DirectoryInfo("R:\\");
            Assert.False(directoryInfo.Exists);
            var diskSpaceInfo = FileSystemManager.GetDiskSpaceFor(directoryInfo);
            Assert.NotNull(diskSpaceInfo);
            Assert.False(string.IsNullOrWhiteSpace(diskSpaceInfo.Path));
            Assert.True(diskSpaceInfo.FreeOfBytes == -1);
            Assert.True(diskSpaceInfo.TotalOfBytes == -1);
            Assert.True(diskSpaceInfo.TotalFreeOfBytes == -1);
        }
    }
}
