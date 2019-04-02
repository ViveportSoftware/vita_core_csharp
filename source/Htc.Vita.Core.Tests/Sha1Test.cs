using System;
using System.IO;
using Htc.Vita.Core.Crypto;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class Sha1Test
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
        }

        [Fact]
        public static void Default_1_GenerateInBase64_WithContent()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var value = sha1.GenerateInBase64("");
            Assert.Equal("2jmj7l5rSw0yVb/vlWAYkK/YBwk=", value);
            var value2 = sha1.GenerateInBase64("123");
            Assert.Equal("QL0AFWMIX8NRZTKeof9cXsvbvu8=", value2);
        }

        [Fact]
        public static void Default_1_GenerateInBase64_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("9eJAeMCTbKeIFSYOfVjRqUCWbro=", sha1.GenerateInBase64(file));
        }

        [Fact]
        public static void Default_1_GenerateInBase64Async_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("9eJAeMCTbKeIFSYOfVjRqUCWbro=", sha1.GenerateInBase64Async(file).Result);
        }

        [Fact]
        public static void Default_2_ValidateInBase64_WithContent()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            Assert.True(sha1.ValidateInBase64("", "2jmj7l5rSw0yVb/vlWAYkK/YBwk="));
            Assert.True(sha1.ValidateInBase64("123", "QL0AFWMIX8NRZTKeof9cXsvbvu8="));
        }

        [Fact]
        public static void Default_2_ValidateInBase64_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha1.ValidateInBase64(file, "9eJAeMCTbKeIFSYOfVjRqUCWbro="));
        }

        [Fact]
        public static void Default_2_ValidateInBase64Async_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha1.ValidateInBase64Async(file, "9eJAeMCTbKeIFSYOfVjRqUCWbro=").Result);
        }

        [Fact]
        public static void Default_3_GenerateInHex_WithContent()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var value = sha1.GenerateInHex("");
            Assert.Equal("da39a3ee5e6b4b0d3255bfef95601890afd80709", value);
            var value2 = sha1.GenerateInHex("123");
            Assert.Equal("40bd001563085fc35165329ea1ff5c5ecbdbbeef", value2);
        }

        [Fact]
        public static void Default_3_GenerateInHex_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("f5e24078c0936ca78815260e7d58d1a940966eba", sha1.GenerateInHex(file));
        }

        [Fact]
        public static void Default_3_GenerateInHexAsync_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("f5e24078c0936ca78815260e7d58d1a940966eba", sha1.GenerateInHexAsync(file).Result);
        }

        [Fact]
        public static void Default_4_ValidateInHex_WithContent()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            Assert.True(sha1.ValidateInHex("", "da39a3ee5e6b4b0d3255bfef95601890afd80709"));
            Assert.True(sha1.ValidateInHex("123", "40bd001563085fc35165329ea1ff5c5ecbdbbeef"));
        }

        [Fact]
        public static void Default_4_ValidateInHex_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha1.ValidateInHex(file, "f5e24078c0936ca78815260e7d58d1a940966eba"));
        }

        [Fact]
        public static void Default_4_ValidateInHexAsync_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha1.ValidateInHexAsync(file, "f5e24078c0936ca78815260e7d58d1a940966eba").Result);
        }

        [Fact]
        public static void Default_5_ValidateInAll_WithContent()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            Assert.True(sha1.ValidateInAll("", "2jmj7l5rSw0yVb/vlWAYkK/YBwk="));
            Assert.True(sha1.ValidateInAll("123", "QL0AFWMIX8NRZTKeof9cXsvbvu8="));
            Assert.True(sha1.ValidateInAll("", "da39a3ee5e6b4b0d3255bfef95601890afd80709"));
            Assert.True(sha1.ValidateInAll("123", "40bd001563085fc35165329ea1ff5c5ecbdbbeef"));
        }

        [Fact]
        public static void Default_5_ValidateInAll_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha1.ValidateInAll(file, "9eJAeMCTbKeIFSYOfVjRqUCWbro="));
            Assert.True(sha1.ValidateInAll(file, "f5e24078c0936ca78815260e7d58d1a940966eba"));
        }

        [Fact]
        public static void Default_5_ValidateInAllAsync_WithFile()
        {
            var sha1 = Sha1.GetInstance();
            Assert.NotNull(sha1);
            var path = @"%USERPROFILE%\TestData.Sha1.txt";
            if (!Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Sha1.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(sha1.ValidateInAllAsync(file, "9eJAeMCTbKeIFSYOfVjRqUCWbro=").Result);
            Assert.True(sha1.ValidateInAllAsync(file, "f5e24078c0936ca78815260e7d58d1a940966eba").Result);
        }
    }
}
