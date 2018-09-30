using System;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class UsbManager
    {
        [Fact]
        public static void Default_0_GetHidDevices()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var deviceInfos = IO.UsbManager.GetHidDevices();
            Assert.NotNull(deviceInfos);
            foreach (var deviceInfo in deviceInfos) {
                Console.WriteLine("deviceInfo.Path: \"" + deviceInfo.Path + "\"");
                Console.WriteLine("deviceInfo.SerialNumber: \"" + deviceInfo.SerialNumber + "\"");
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
                Assert.False(string.IsNullOrWhiteSpace(deviceInfo.Manufacturer));
                var optional = deviceInfo.Optional;
                if (optional.ContainsKey("type"))
                {
                    Assert.False(string.IsNullOrWhiteSpace(optional["type"]));
                }
            }
        }
    }
}
