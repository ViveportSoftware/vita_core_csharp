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
    }
}
