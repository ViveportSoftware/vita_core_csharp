using System;
using Htc.Vita.Core.IO;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void UsbManager_Default_0_GetDevices()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var deviceInfos = UsbManager.GetHidDevices();
            foreach (var deviceInfo in deviceInfos) {
                Console.WriteLine("deviceInfo.Path: " + deviceInfo.Path);
                Assert.False(string.IsNullOrWhiteSpace(deviceInfo.Path));
                Assert.True(deviceInfo.ProductId.Length == 4);
                Assert.True(deviceInfo.VendorId.Length == 4);
                Assert.False(string.IsNullOrWhiteSpace(deviceInfo.Description));
                Assert.False(string.IsNullOrWhiteSpace(deviceInfo.Manufecturer));
                Assert.False(string.IsNullOrWhiteSpace(deviceInfo.Optional["type"]));
            }
        }
    }
}
