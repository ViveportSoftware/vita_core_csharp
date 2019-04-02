using Htc.Vita.Core.Net;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class NetworkInterfaceTest
    {
        [Fact]
        public static void Default_0_IsNetworkAvailable()
        {
            Assert.True(NetworkInterface.IsNetworkAvailable());
        }

        [Fact]
        public static void Default_0_IsInternetAvailable()
        {
            Assert.True(NetworkInterface.IsInternetAvailable());
        }
    }
}
