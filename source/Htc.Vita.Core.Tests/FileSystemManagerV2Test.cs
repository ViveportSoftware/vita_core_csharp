using System;
using System.IO;
using Htc.Vita.Core.IO;
using Htc.Vita.Core.Runtime;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class FileSystemManagerV2Test
    {
        private const int MaxPath = 260 - 1;

        private readonly ITestOutputHelper _output;

        public FileSystemManagerV2Test(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Default_0_GetInstance()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var fileSystemManager = FileSystemManagerV2.GetInstance();
            Assert.NotNull(fileSystemManager);
        }

        [Fact]
        public void Default_1_GetDiskSpace()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var directoryInfo = new DirectoryInfo("C:\\");
            Assert.True(directoryInfo.Exists);
            var fileSystemManager = FileSystemManagerV2.GetInstance();
            var getDiskSpaceResult = fileSystemManager.GetDiskSpace(directoryInfo);
            var getDiskSpaceStatus = getDiskSpaceResult.Status;
            Assert.Equal(FileSystemManagerV2.GetDiskSpaceStatus.Ok, getDiskSpaceStatus);
            var diskSpaceInfo = getDiskSpaceResult.DiskSpace;
            _output.WriteLine("diskSpaceInfo.Path: " + diskSpaceInfo.Path);
            Assert.True(diskSpaceInfo.Path.Exists);
            _output.WriteLine("diskSpaceInfo.FreeOfBytes: " + diskSpaceInfo.FreeOfBytes);
            Assert.True(diskSpaceInfo.FreeOfBytes >= 0);
            _output.WriteLine("diskSpaceInfo.TotalOfBytes: " + diskSpaceInfo.TotalOfBytes);
            Assert.True(diskSpaceInfo.TotalOfBytes >= 0);
            _output.WriteLine("diskSpaceInfo.TotalFreeOfBytes: " + diskSpaceInfo.TotalFreeOfBytes);
            Assert.True(diskSpaceInfo.TotalFreeOfBytes >= 0);
        }

        [Fact]
        public void Default_1_GetDiskSpace_WithNonExistPath()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var directoryInfo = new DirectoryInfo("R:\\");
            Assert.False(directoryInfo.Exists);
            var fileSystemManager = FileSystemManagerV2.GetInstance();
            var getDiskSpaceResult = fileSystemManager.GetDiskSpace(directoryInfo);
            var getDiskSpaceStatus = getDiskSpaceResult.Status;
            Assert.Equal(FileSystemManagerV2.GetDiskSpaceStatus.InvalidData, getDiskSpaceStatus);
        }

        [Fact]
        public void Default_2_VerifyPathDepth()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var tempPathString = Environment.GetEnvironmentVariable("Temp") ?? string.Empty;
            var path = new DirectoryInfo(tempPathString);
            var depth = MaxPath - tempPathString.Length - 1;
            var fileSystemManagerV2 = FileSystemManagerV2.GetInstance();
            var verifyPathDepthResult = fileSystemManagerV2.VerifyPathDepth(path, depth);
            var verifyPathDepthStatus = verifyPathDepthResult.Status;
            Assert.Equal(FileSystemManagerV2.VerifyPathDepthStatus.Ok, verifyPathDepthStatus);
            depth++;
            verifyPathDepthResult = fileSystemManagerV2.VerifyPathDepth(path, depth);
            verifyPathDepthStatus = verifyPathDepthResult.Status;
            Assert.Equal(FileSystemManagerV2.VerifyPathDepthStatus.Unsupported, verifyPathDepthStatus);
        }
    }
}
