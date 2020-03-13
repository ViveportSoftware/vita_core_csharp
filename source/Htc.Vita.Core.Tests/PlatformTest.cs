using System;
using Htc.Vita.Core.Runtime;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class PlatformTest
    {
        private readonly ITestOutputHelper _output;

        public PlatformTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Default_00_GetMachineId()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var machineId = Platform.GetMachineId();
            Assert.True(machineId != null && machineId.Length == 36);
        }

        [Fact]
        public static void Default_01_DetectOsArch()
        {
            var osArch = Platform.DetectOsArch();
            Assert.True(osArch == Platform.OsArch.Bit64);
        }

        [Fact]
        public void Default_02_GetProductName()
        {
            var productName = Platform.GetProductName();
            Assert.NotEmpty(productName);
            _output.WriteLine("productName: \"" + productName + "\"");
            if (Platform.IsWindows)
            {
                Assert.NotEqual("UNKNOWN", productName);
            }
        }

        [Fact]
        public  void Default_03_GetSystemBootTime()
        {
            var bootTime = Platform.GetSystemBootTime();
            _output.WriteLine("bootTime: " + bootTime);
            Assert.NotEqual(DateTime.MinValue, bootTime);
            Assert.NotEqual(DateTime.Now, bootTime);
            var bootTimeUtc = Platform.GetSystemBootTimeUtc();
            _output.WriteLine("bootTimeUtc: " + bootTimeUtc);
            Assert.NotEqual(DateTime.MinValue, bootTimeUtc);
            Assert.NotEqual(DateTime.UtcNow, bootTimeUtc);
        }

        [Fact]
        public static void Default_04_DetectFramework()
        {
            Assert.False(Platform.IsDotNetCore);
            Assert.False(Platform.IsMono);
        }

        [Fact]
        public static void Default_05_DetectOsType()
        {
            if (Platform.IsWindows)
            {
                Assert.False(Platform.IsLinux || Platform.IsMacOsX);
            }
        }

        [Fact]
        public void Default_06_GetFrameworkName()
        {
            var frameworkName = Platform.GetFrameworkName();
            _output.WriteLine("framework: " + frameworkName);
            Assert.NotNull(frameworkName);
            Assert.False(frameworkName.StartsWith("Unknown", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void Default_07_GetMachineName()
        {
            var machineName = Platform.GetMachineName();
            _output.WriteLine("machineName: " + machineName);
            Assert.NotNull(machineName);
            Assert.False(machineName.StartsWith("UNKNOWN-MACHINE-NAME", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void Default_08_DetectProcessArch()
        {
            if (Platform.IsWindows)
            {
                var processArch = Platform.DetectProcessArch();
                _output.WriteLine("processArch: " + processArch);
                Assert.False(processArch == Platform.ProcessArch.Unknown);
            }
        }

        [Fact]
        public static void Default_09_GetEpochTime()
        {
            var epochTime = Platform.GetEpochTime();
            Assert.Equal(0, Util.Convert.ToTimestampInMilli(epochTime));
        }

        [Fact]
        public static void Default_10_GetMaxTimeUtc()
        {
            var maxTimeUtc = Platform.GetMaxTimeUtc();
            Assert.True(DateTime.MaxValue > maxTimeUtc);
            Assert.True(maxTimeUtc > DateTime.UtcNow);
        }

        [Fact]
        public static void Default_11_GetMinTimeUtc()
        {
            var minTimeUtc = Platform.GetMinTimeUtc();
            Assert.True(DateTime.MinValue < minTimeUtc);
            Assert.True(minTimeUtc < DateTime.UtcNow);
        }
    }
}
