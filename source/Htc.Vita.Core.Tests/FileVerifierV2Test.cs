using System;
using System.IO;
using Htc.Vita.Core.IO;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class FileVerifierV2Test
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var fileVerifierV2 = FileVerifierV2.GetInstance();
            Assert.NotNull(fileVerifierV2);
        }

        [Fact]
        public static void Default_1_GenerateInBase64()
        {
            var fileVerifierV2 = FileVerifierV2.GetInstance();
            var path1 = @"%USERPROFILE%\.htc_test\TestData.Md5.txt";
            var path2 = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path1 = @"%HOME%/TestData.Md5.txt";
                path2 = @"%HOME%/TestData.Sha1.txt";
            }
            var file1 = new FileInfo(Environment.ExpandEnvironmentVariables(path1));
            var file2 = new FileInfo(Environment.ExpandEnvironmentVariables(path2));

            Assert.Equal("pq/Xu7jVnluxLJ28xOws/w==", fileVerifierV2.GenerateChecksumInBase64(file1, FileVerifierV2.ChecksumType.Md5));
            Assert.Equal("9eJAeMCTbKeIFSYOfVjRqUCWbro=", fileVerifierV2.GenerateChecksumInBase64(file2, FileVerifierV2.ChecksumType.Sha1));
            Assert.Equal("ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo=", fileVerifierV2.GenerateChecksumInBase64(file2, FileVerifierV2.ChecksumType.Sha256));

            Assert.Equal("pq/Xu7jVnluxLJ28xOws/w==", fileVerifierV2.GenerateChecksumInBase64Async(file1, FileVerifierV2.ChecksumType.Md5).Result);
            Assert.Equal("9eJAeMCTbKeIFSYOfVjRqUCWbro=", fileVerifierV2.GenerateChecksumInBase64Async(file2, FileVerifierV2.ChecksumType.Sha1).Result);
            Assert.Equal("ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo=", fileVerifierV2.GenerateChecksumInBase64Async(file2, FileVerifierV2.ChecksumType.Sha256).Result);
        }

        [Fact]
        public static void Default_2_GenerateInHex()
        {
            var fileVerifierV2 = FileVerifierV2.GetInstance();
            var path1 = @"%USERPROFILE%\.htc_test\TestData.Md5.txt";
            var path2 = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path1 = @"%HOME%/TestData.Md5.txt";
                path2 = @"%HOME%/TestData.Sha1.txt";
            }
            var file1 = new FileInfo(Environment.ExpandEnvironmentVariables(path1));
            var file2 = new FileInfo(Environment.ExpandEnvironmentVariables(path2));

            Assert.Equal("a6afd7bbb8d59e5bb12c9dbcc4ec2cff", fileVerifierV2.GenerateChecksumInHex(file1, FileVerifierV2.ChecksumType.Md5));
            Assert.Equal("f5e24078c0936ca78815260e7d58d1a940966eba", fileVerifierV2.GenerateChecksumInHex(file2, FileVerifierV2.ChecksumType.Sha1));
            Assert.Equal("125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a", fileVerifierV2.GenerateChecksumInHex(file2, FileVerifierV2.ChecksumType.Sha256));

            Assert.Equal("a6afd7bbb8d59e5bb12c9dbcc4ec2cff", fileVerifierV2.GenerateChecksumInHexAsync(file1, FileVerifierV2.ChecksumType.Md5).Result);
            Assert.Equal("f5e24078c0936ca78815260e7d58d1a940966eba", fileVerifierV2.GenerateChecksumInHexAsync(file2, FileVerifierV2.ChecksumType.Sha1).Result);
            Assert.Equal("125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a", fileVerifierV2.GenerateChecksumInHexAsync(file2, FileVerifierV2.ChecksumType.Sha256).Result);
        }

        [Fact]
        public static void Default_2_VerifyIntegrity()
        {
            var fileVerifierV2 = FileVerifierV2.GetInstance();
            var path1 = @"%USERPROFILE%\.htc_test\TestData.Md5.txt";
            var path2 = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path1 = @"%HOME%/TestData.Md5.txt";
                path2 = @"%HOME%/TestData.Sha1.txt";
            }
            var file1 = new FileInfo(Environment.ExpandEnvironmentVariables(path1));
            var file2 = new FileInfo(Environment.ExpandEnvironmentVariables(path2));

            Assert.True(fileVerifierV2.VerifyIntegrity(file1, "pq/Xu7jVnluxLJ28xOws/w==", FileVerifierV2.ChecksumType.Md5));
            Assert.True(fileVerifierV2.VerifyIntegrity(file1, "a6afd7bbb8d59e5bb12c9dbcc4ec2cff", FileVerifierV2.ChecksumType.Md5));
            Assert.True(fileVerifierV2.VerifyIntegrity(file2, "9eJAeMCTbKeIFSYOfVjRqUCWbro=", FileVerifierV2.ChecksumType.Sha1));
            Assert.True(fileVerifierV2.VerifyIntegrity(file2, "f5e24078c0936ca78815260e7d58d1a940966eba", FileVerifierV2.ChecksumType.Sha1));
            Assert.True(fileVerifierV2.VerifyIntegrity(file2, "ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo=", FileVerifierV2.ChecksumType.Sha256));
            Assert.True(fileVerifierV2.VerifyIntegrity(file2, "125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a", FileVerifierV2.ChecksumType.Sha256));

            Assert.True(fileVerifierV2.VerifyIntegrity(file1, "pq/Xu7jVnluxLJ28xOws/w=="));
            Assert.True(fileVerifierV2.VerifyIntegrity(file1, "a6afd7bbb8d59e5bb12c9dbcc4ec2cff"));
            Assert.True(fileVerifierV2.VerifyIntegrity(file2, "9eJAeMCTbKeIFSYOfVjRqUCWbro="));
            Assert.True(fileVerifierV2.VerifyIntegrity(file2, "f5e24078c0936ca78815260e7d58d1a940966eba"));
            Assert.True(fileVerifierV2.VerifyIntegrity(file2, "ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo="));
            Assert.True(fileVerifierV2.VerifyIntegrity(file2, "125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a"));

            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file1, "pq/Xu7jVnluxLJ28xOws/w==", FileVerifierV2.ChecksumType.Md5).Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file1, "a6afd7bbb8d59e5bb12c9dbcc4ec2cff", FileVerifierV2.ChecksumType.Md5).Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file2, "9eJAeMCTbKeIFSYOfVjRqUCWbro=", FileVerifierV2.ChecksumType.Sha1).Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file2, "f5e24078c0936ca78815260e7d58d1a940966eba", FileVerifierV2.ChecksumType.Sha1).Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file2, "ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo=", FileVerifierV2.ChecksumType.Sha256).Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file2, "125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a", FileVerifierV2.ChecksumType.Sha256).Result);

            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file1, "pq/Xu7jVnluxLJ28xOws/w==").Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file1, "a6afd7bbb8d59e5bb12c9dbcc4ec2cff").Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file2, "9eJAeMCTbKeIFSYOfVjRqUCWbro=").Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file2, "f5e24078c0936ca78815260e7d58d1a940966eba").Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file2, "ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo=").Result);
            Assert.True(fileVerifierV2.VerifyIntegrityAsync(file2, "125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a").Result);
        }
    }
}
