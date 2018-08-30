using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class NetworkInterface
    {
        [Fact]
        public static void Default_0_IsNetworkAvailable()
        {
            Assert.True(Net.NetworkInterface.IsNetworkAvailable());
        }

        [Fact]
        public static void Default_0_IsInternetAvailable()
        {
            Assert.True(Net.NetworkInterface.IsInternetAvailable());
        }
    }
}
