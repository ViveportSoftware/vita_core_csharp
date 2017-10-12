using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void Platform_Default_0_GetMachineId()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var machineId = Platform.GetMachineId();
            Assert.True(machineId != null && machineId.Length == 36);
        }

        [Fact]
        public void Platform_Default_1_DetectOsArch()
        {
            var osArch = Platform.DetectOsArch();
            Assert.True(osArch == Platform.OsArch.Bit64);
        }
    }
}
