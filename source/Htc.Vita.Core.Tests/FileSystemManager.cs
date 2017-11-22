using System;
using System.IO;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class FileSystemManager
    {
        [Fact]
        public static void Default_0_GetDiskSpaceFor()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var directoryInfo = new DirectoryInfo("C:\\");
            Assert.True(directoryInfo.Exists);
            var diskSpaceInfo = IO.FileSystemManager.GetDiskSpaceFor(directoryInfo);
            Assert.NotNull(diskSpaceInfo);
            Console.WriteLine("diskSpaceInfo.Path: " + diskSpaceInfo.Path);
            Assert.False(string.IsNullOrWhiteSpace(diskSpaceInfo.Path));
            Console.WriteLine("diskSpaceInfo.FreeOfBytes: " + diskSpaceInfo.FreeOfBytes);
            Assert.True(diskSpaceInfo.FreeOfBytes >= 0);
            Console.WriteLine("diskSpaceInfo.TotalOfBytes: " + diskSpaceInfo.TotalOfBytes);
            Assert.True(diskSpaceInfo.TotalOfBytes >= 0);
            Console.WriteLine("diskSpaceInfo.TotalFreeOfBytes: " + diskSpaceInfo.TotalFreeOfBytes);
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
            var diskSpaceInfo = IO.FileSystemManager.GetDiskSpaceFor(directoryInfo);
            Assert.NotNull(diskSpaceInfo);
            Assert.False(string.IsNullOrWhiteSpace(diskSpaceInfo.Path));
            Assert.True(diskSpaceInfo.FreeOfBytes == -1);
            Assert.True(diskSpaceInfo.TotalOfBytes == -1);
            Assert.True(diskSpaceInfo.TotalFreeOfBytes == -1);
        }
    }
}
