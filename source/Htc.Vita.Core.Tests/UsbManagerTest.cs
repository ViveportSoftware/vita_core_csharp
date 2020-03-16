using Htc.Vita.Core.IO;
using Htc.Vita.Core.Runtime;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class UsbManagerTest
    {
        private readonly ITestOutputHelper _output;

        public UsbManagerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Default_0_GetHidDevices()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var deviceInfos = UsbManager.GetHidDevices();
            Assert.NotNull(deviceInfos);
            foreach (var deviceInfo in deviceInfos) {
                _output.WriteLine("deviceInfo.Path: \"" + deviceInfo.Path + "\"");
                _output.WriteLine("deviceInfo.Product: \"" + deviceInfo.Product + "\"");
                _output.WriteLine("deviceInfo.SerialNumber: \"" + deviceInfo.SerialNumber + "\"");
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

        [Fact]
        public void Default_1_GetHidFeatureReport()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var deviceInfos = UsbManager.GetHidDevices();
            Assert.NotNull(deviceInfos);
            foreach (var deviceInfo in deviceInfos)
            {
                _output.WriteLine("deviceInfo.Path: \"" + deviceInfo.Path + "\"");
                _output.WriteLine("deviceInfo.Product: \"" + deviceInfo.Product + "\"");
                _output.WriteLine("deviceInfo.SerialNumber: \"" + deviceInfo.SerialNumber + "\"");
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

                var featureReport = UsbManager.GetHidFeatureReport(deviceInfo.Path, 0);
                if (featureReport != null)
                {
                    _output.WriteLine("deviceInfo.FeatureReport: \"" + Util.Convert.ToHexString(featureReport) + "\"");
                }
            }
        }

        [Fact]
        public void Default_2_GetUsbDevices()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var deviceInfos = UsbManager.GetUsbDevices();
            Assert.NotNull(deviceInfos);
            foreach (var deviceInfo in deviceInfos)
            {
                _output.WriteLine("deviceInfo.Path: \"" + deviceInfo.Path + "\"");
                _output.WriteLine("deviceInfo.Product: \"" + deviceInfo.Product + "\"");
                _output.WriteLine("deviceInfo.SerialNumber: \"" + deviceInfo.SerialNumber + "\"");
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

        [Fact]
        public void Default_3_GetUsbHubDevices()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var deviceInfos = UsbManager.GetUsbHubDevices();
            Assert.NotNull(deviceInfos);
            foreach (var deviceInfo in deviceInfos)
            {
                _output.WriteLine("deviceInfo.Path: \"" + deviceInfo.Path + "\"");
                _output.WriteLine("deviceInfo.Product: \"" + deviceInfo.Product + "\"");
                _output.WriteLine("deviceInfo.SerialNumber: \"" + deviceInfo.SerialNumber + "\"");
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
