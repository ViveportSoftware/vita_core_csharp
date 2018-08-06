using System.Net;
using System.Net.Sockets;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class LocalPortManager
    {
        [Fact]
        public static void Default_0_GetRandomUnusedPort()
        {
            var unusedPort = Net.LocalPortManager.GetRandomUnusedPort();
            Assert.True(unusedPort > 0);
        }

        [Fact]
        public static void Default_1_GetPortStatus()
        {
            var unusedPort = Net.LocalPortManager.GetRandomUnusedPort();
            Assert.True(unusedPort > 0);
            var portStatus = Net.LocalPortManager.GetPortStatus(unusedPort);
            Assert.Equal(Net.LocalPortManager.PortStatus.Available, portStatus);
        }

        [Fact]
        public static void Default_1_GetPortStatus_WithUsedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listener.Start();
                var usedPort = ((IPEndPoint)listener.LocalEndpoint).Port;
                Assert.True(usedPort > 0);
                var portStatus = Net.LocalPortManager.GetPortStatus(usedPort);
                Assert.Equal(Net.LocalPortManager.PortStatus.InUse, portStatus);
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}
