using System;
using Htc.Vita.Core.IO;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void UsbManager_Default_0_GetHidDevices()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var deviceInfos = UsbManager.GetHidDevices();
            Assert.NotNull(deviceInfos);
            foreach (var deviceInfo in deviceInfos) {
                Console.WriteLine("deviceInfo.Path: " + deviceInfo.Path);
                Assert.False(string.IsNullOrWhiteSpace(deviceInfo.Path));
                var productId = deviceInfo.ProductId;
                if (!string.IsNullOrEmpty(productId))
                {
                    Assert.True(productId.Length == 4);
                }
                var vendorId = deviceInfo.VendorId;
                if (!string.IsNullOrEmpty(vendorId))
                {
                    Assert.True(vendorId.Length == 4);
                }
                Assert.False(string.IsNullOrWhiteSpace(deviceInfo.Description));
                Assert.False(string.IsNullOrWhiteSpace(deviceInfo.Manufecturer));
                var optional = deviceInfo.Optional;
                if (optional.ContainsKey("type"))
                {
                    Assert.False(string.IsNullOrWhiteSpace(optional["type"]));
                }
            }
        }
    }
}
