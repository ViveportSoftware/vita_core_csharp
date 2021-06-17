using System;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.IO;
using Xunit;

namespace Htc.Vita.Core.Tests
{
#pragma warning disable CS0618
    public class FileVerifierTest
    {
        [Fact]
        public static void FileVerifierTest_Default_0_Md5_Base64()
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"__test_{Guid.NewGuid()}__");
            var fileInfo = new FileInfo(filePath);
            File.WriteAllText(filePath, Guid.NewGuid().ToString());
            var checksum = Md5.GetInstance().GenerateInBase64(fileInfo);
            var verified = FileVerifier.Verify(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Md5, CancellationToken.None);
            Assert.True(verified);
            verified = FileVerifier.VerifyAsync(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Md5, CancellationToken.None).Result;
            Assert.True(verified);
            try { fileInfo.Delete(); } catch (Exception) { }
        }

        [Fact]
        public static void FileVerifierTest_Default_1_Md5_Hex()
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"__test_{Guid.NewGuid()}__");
            var fileInfo = new FileInfo(filePath);
            File.WriteAllText(filePath, Guid.NewGuid().ToString());
            var checksum = Md5.GetInstance().GenerateInHex(fileInfo);
            var verified = FileVerifier.Verify(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Md5, CancellationToken.None);
            Assert.True(verified);
            verified = FileVerifier.VerifyAsync(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Md5, CancellationToken.None).Result;
            Assert.True(verified);
            try { fileInfo.Delete(); } catch (Exception) { }
        }

        [Fact]
        public static void FileVerifierTest_Default_2_Sha1_Base64()
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"__test_{Guid.NewGuid()}__");
            var fileInfo = new FileInfo(filePath);
            File.WriteAllText(filePath, Guid.NewGuid().ToString());
            var checksum = Sha1.GetInstance().GenerateInBase64(fileInfo);
            var verified = FileVerifier.Verify(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Sha1, CancellationToken.None);
            Assert.True(verified);
            verified = FileVerifier.VerifyAsync(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Sha1, CancellationToken.None).Result;
            Assert.True(verified);
            try { fileInfo.Delete(); } catch (Exception) { }
        }

        [Fact]
        public static void FileVerifierTest_Default_3_Sha1_Hex()
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"__test_{Guid.NewGuid()}__");
            var fileInfo = new FileInfo(filePath);
            File.WriteAllText(filePath, Guid.NewGuid().ToString());
            var checksum = Sha1.GetInstance().GenerateInHex(fileInfo);
            var verified = FileVerifier.Verify(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Sha1, CancellationToken.None);
            Assert.True(verified);
            verified = FileVerifier.VerifyAsync(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Sha1, CancellationToken.None).Result;
            Assert.True(verified);
            try { fileInfo.Delete(); } catch (Exception) { }
        }

        [Fact]
        public static void FileVerifierTest_Default_4_Sha256_Base64()
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"__test_{Guid.NewGuid()}__");
            var fileInfo = new FileInfo(filePath);
            File.WriteAllText(filePath, Guid.NewGuid().ToString());
            var checksum = Sha256.GetInstance().GenerateInBase64(fileInfo);
            var verified = FileVerifier.Verify(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Sha256, CancellationToken.None);
            Assert.True(verified);
            verified = FileVerifier.VerifyAsync(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Sha256, CancellationToken.None).Result;
            Assert.True(verified);
            try { fileInfo.Delete(); } catch (Exception) { }
        }

        [Fact]
        public static void FileVerifierTest_Default_5_Sha256_Hex()
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"__test_{Guid.NewGuid()}__");
            var fileInfo = new FileInfo(filePath);
            File.WriteAllText(filePath, Guid.NewGuid().ToString());
            var checksum = Sha256.GetInstance().GenerateInHex(fileInfo);
            var verified = FileVerifier.Verify(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Sha256, CancellationToken.None);
            Assert.True(verified);
            verified = FileVerifier.VerifyAsync(fileInfo, fileInfo.Length, checksum, FileVerifier.ChecksumType.Sha256, CancellationToken.None).Result;
            Assert.True(verified);
            try { fileInfo.Delete(); } catch (Exception) { }
        }
    }
#pragma warning restore CS0618
}
