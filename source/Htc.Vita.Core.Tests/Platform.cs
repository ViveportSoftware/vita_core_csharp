using System;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class Platform
    {
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
        public static void Default_2_GetProductName()
        {
            var productName = Runtime.Platform.GetProductName();
            Assert.NotEmpty(productName);
            Console.WriteLine("productName: \"" + productName + "\"");
            if (Runtime.Platform.IsWindows)
            {
                Assert.NotEqual("UNKNOWN", productName);
            }
        }

        [Fact]
        public static void Default_3_GetSystemBootTime()
        {
            var bootTime = Runtime.Platform.GetSystemBootTime();
            Console.WriteLine("bootTime: " + bootTime);
            Assert.NotEqual(DateTime.MinValue, bootTime);
            Assert.NotEqual(DateTime.Now, bootTime);
            var bootTimeUtc = Runtime.Platform.GetSystemBootTimeUtc();
            Console.WriteLine("bootTimeUtc: " + bootTimeUtc);
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
        public static void Default_6_GetFrameworkName()
        {
            var frameworkName = Runtime.Platform.GetFrameworkName();
            Console.WriteLine("framework: " + frameworkName);
            Assert.NotNull(frameworkName);
            Assert.False(frameworkName.StartsWith("Unknown", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
