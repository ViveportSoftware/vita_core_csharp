using System.Net;
using System.Net.Sockets;
using Htc.Vita.Core.Net;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class LocalPortManagerTest
    {
        [Fact]
        public static void Default_0_GetRandomUnusedPort()
        {
            var unusedPort = LocalPortManager.GetRandomUnusedPort();
            Assert.True(unusedPort > 0);
        }

        [Fact]
        public static void Default_1_GetPortStatus()
        {
            var unusedPort = LocalPortManager.GetRandomUnusedPort();
            Assert.True(unusedPort > 0);
            var portStatus = LocalPortManager.GetPortStatus(unusedPort);
            Assert.Equal(LocalPortManager.PortStatus.Available, portStatus);
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
                var portStatus = LocalPortManager.GetPortStatus(usedPort);
                Assert.Equal(LocalPortManager.PortStatus.InUse, portStatus);
            }
            finally
            {
                listener.Stop();
            }
        }

        [Fact]
        public static void Default_2_VerifyPortStatus()
        {
            var unusedPort = LocalPortManager.GetRandomUnusedPort();
            Assert.True(unusedPort > 0);
            var portStatus = LocalPortManager.GetPortStatus(unusedPort);
            Assert.Equal(LocalPortManager.PortStatus.Available, portStatus);
            portStatus = LocalPortManager.VerifyPortStatus(unusedPort);
            Assert.Equal(LocalPortManager.PortStatus.Available, portStatus);
        }

        [Fact]
        public static void Default_2_VerifyPortStatus_WithUsedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listener.Start();
                var usedPort = ((IPEndPoint)listener.LocalEndpoint).Port;
                Assert.True(usedPort > 0);
                var portStatus = LocalPortManager.GetPortStatus(usedPort);
                Assert.Equal(LocalPortManager.PortStatus.InUse, portStatus);
                portStatus = LocalPortManager.VerifyPortStatus(usedPort);
                Assert.Equal(LocalPortManager.PortStatus.InUse, portStatus);
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}
