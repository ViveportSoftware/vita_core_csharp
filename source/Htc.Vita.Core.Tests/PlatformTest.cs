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
        public static void Default_0_GetMachineId()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var machineId = Platform.GetMachineId();
            Assert.True(machineId != null && machineId.Length == 36);
        }

        [Fact]
        public static void Default_1_DetectOsArch()
        {
            var osArch = Platform.DetectOsArch();
            Assert.True(osArch == Platform.OsArch.Bit64);
        }

        [Fact]
        public void Default_2_GetProductName()
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
        public  void Default_3_GetSystemBootTime()
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
        public static void Default_4_DetectFramework()
        {
            Assert.False(Platform.IsDotNetCore);
            Assert.False(Platform.IsMono);
        }

        [Fact]
        public static void Default_5_DetectOsType()
        {
            if (Platform.IsWindows)
            {
                Assert.False(Platform.IsLinux || Platform.IsMacOsX);
            }
        }

        [Fact]
        public void Default_6_GetFrameworkName()
        {
            var frameworkName = Platform.GetFrameworkName();
            _output.WriteLine("framework: " + frameworkName);
            Assert.NotNull(frameworkName);
            Assert.False(frameworkName.StartsWith("Unknown", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void Default_7_GetMachineName()
        {
            var machineName = Platform.GetMachineName();
            _output.WriteLine("machineName: " + machineName);
            Assert.NotNull(machineName);
            Assert.False(machineName.StartsWith("UNKNOWN-MACHINE-NAME", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void Default_8_DetectProcessArch()
        {
            if (Platform.IsWindows)
            {
                var processArch = Platform.DetectProcessArch();
                _output.WriteLine("processArch: " + processArch);
                Assert.False(processArch == Platform.ProcessArch.Unknown);
            }
        }
    }
}
