using Htc.Vita.Core.IO;
using Htc.Vita.Core.Runtime;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class WpdManagerTest
    {
        private readonly ITestOutputHelper _output;

        public WpdManagerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Default_0_GetDevices()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var deviceInfos = WpdManager.GetDevices();
            Assert.NotNull(deviceInfos);
            foreach (var deviceInfo in deviceInfos)
            {
                _output.WriteLine("deviceInfo.Path: \"" + deviceInfo.Path + "\"");
                _output.WriteLine("deviceInfo.Manufacturer: \"" + deviceInfo.Manufacturer + "\"");
                _output.WriteLine("deviceInfo.Description: \"" + deviceInfo.Description + "\"");
                _output.WriteLine("deviceInfo.Product: \"" + deviceInfo.Product + "\"");
                _output.WriteLine("deviceInfo.ProductId: \"" + deviceInfo.ProductId + "\"");
                _output.WriteLine("deviceInfo.VendorId: \"" + deviceInfo.VendorId + "\"");
                _output.WriteLine("deviceInfo.SerialNumber: \"" + deviceInfo.SerialNumber + "\"");
                Assert.False(string.IsNullOrWhiteSpace(deviceInfo.Path));
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
