using System;
using System.IO;
using Htc.Vita.Core.Crypto;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class Sha256Test
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
        }

        [Fact]
        public static void Default_1_GenerateInBase64_WithContent()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var value = sha256.GenerateInBase64("");
            Assert.Equal("47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU=", value);
            var value2 = sha256.GenerateInBase64("123");
            Assert.Equal("pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", value2);
        }

        [Fact]
        public static void Default_1_GenerateInBase64_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo=", sha256.GenerateInBase64(file));
        }

        [Fact]
        public static void Default_1_GenerateInBase64Async_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo=", sha256.GenerateInBase64Async(file).Result);
        }

        [Fact]
        public static void Default_2_ValidateInBase64_WithContent()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            Assert.True(sha256.ValidateInBase64("", "47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU="));
            Assert.True(sha256.ValidateInBase64("123", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM="));
        }

        [Fact]
        public static void Default_2_ValidateInBase64_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha256.ValidateInBase64(file, "ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo="));
        }

        [Fact]
        public static void Default_2_ValidateInBase64Async_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha256.ValidateInBase64Async(file, "ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo=").Result);
        }

        [Fact]
        public static void Default_3_GenerateInHex_WithContent()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var value = sha256.GenerateInHex("");
            Assert.Equal("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", value);
            var value2 = sha256.GenerateInHex("123");
            Assert.Equal("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", value2);
        }

        [Fact]
        public static void Default_3_GenerateInHex_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a", sha256.GenerateInHex(file));
        }

        [Fact]
        public static void Default_3_GenerateInHexAsync_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a", sha256.GenerateInHexAsync(file).Result);
        }

        [Fact]
        public static void Default_4_ValidateInHex_WithContent()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            Assert.True(sha256.ValidateInHex("", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"));
            Assert.True(sha256.ValidateInHex("123", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3"));
        }

        [Fact]
        public static void Default_4_ValidateInHex_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha256.ValidateInHex(file, "125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a"));
        }

        [Fact]
        public static void Default_4_ValidateInHexAsync_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha256.ValidateInHexAsync(file, "125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a").Result);
        }

        [Fact]
        public static void Default_5_ValidateInAll_WithContent()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            Assert.True(sha256.ValidateInAll("", "47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU="));
            Assert.True(sha256.ValidateInAll("123", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM="));
            Assert.True(sha256.ValidateInAll("", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"));
            Assert.True(sha256.ValidateInAll("123", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3"));
        }

        [Fact]
        public static void Default_5_ValidateInAll_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha256.ValidateInAll(file, "ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo="));
            Assert.True(sha256.ValidateInAll(file, "125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a"));
        }

        [Fact]
        public static void Default_5_ValidateInAllAsync_WithFile()
        {
            var sha256 = Sha256.GetInstance();
            Assert.NotNull(sha256);
            var path = @"%USERPROFILE%\.htc_test\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha256.ValidateInAllAsync(file, "ElwW3xccv1CczBJBzICd7wi1Sgc8PIoKo8DkweLMhWo=").Result);
            Assert.True(sha256.ValidateInAllAsync(file, "125c16df171cbf509ccc1241cc809def08b54a073c3c8a0aa3c0e4c1e2cc856a").Result);
        }
    }
}
