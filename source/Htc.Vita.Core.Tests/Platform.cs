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
    }
}
