using System;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class Platform
    {
        private readonly ITestOutputHelper _output;

        public Platform(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Default_0_GetMachineId()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var machineId = Runtime.Platform.GetMachineId();
            Assert.True(machineId != null && machineId.Length == 36);
        }

        [Fact]
        public static void Default_1_DetectOsArch()
        {
            var osArch = Runtime.Platform.DetectOsArch();
            Assert.True(osArch == Runtime.Platform.OsArch.Bit64);
        }

        [Fact]
        public void Default_2_GetProductName()
        {
            var productName = Runtime.Platform.GetProductName();
            Assert.NotEmpty(productName);
            _output.WriteLine("productName: \"" + productName + "\"");
            if (Runtime.Platform.IsWindows)
            {
                Assert.NotEqual("UNKNOWN", productName);
            }
        }

        [Fact]
        public  void Default_3_GetSystemBootTime()
        {
            var bootTime = Runtime.Platform.GetSystemBootTime();
            _output.WriteLine("bootTime: " + bootTime);
            Assert.NotEqual(DateTime.MinValue, bootTime);
            Assert.NotEqual(DateTime.Now, bootTime);
            var bootTimeUtc = Runtime.Platform.GetSystemBootTimeUtc();
            _output.WriteLine("bootTimeUtc: " + bootTimeUtc);
            Assert.NotEqual(DateTime.MinValue, bootTimeUtc);
            Assert.NotEqual(DateTime.UtcNow, bootTimeUtc);
        }

        [Fact]
        public static void Default_4_DetectFramework()
        {
            Assert.False(Runtime.Platform.IsDotNetCore);
            Assert.False(Runtime.Platform.IsMono);
        }

        [Fact]
        public static void Default_5_DetectOsType()
        {
            if (Runtime.Platform.IsWindows)
            {
                Assert.False(Runtime.Platform.IsLinux || Runtime.Platform.IsMacOsX);
            }
        }

        [Fact]
        public void Default_6_GetFrameworkName()
        {
            var frameworkName = Runtime.Platform.GetFrameworkName();
            _output.WriteLine("framework: " + frameworkName);
            Assert.NotNull(frameworkName);
            Assert.False(frameworkName.StartsWith("Unknown", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
