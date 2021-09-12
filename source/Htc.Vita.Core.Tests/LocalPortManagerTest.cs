using System.Net;
using System.Net.Sockets;
using Htc.Vita.Core.Net;
using Xunit;

namespace Htc.Vita.Core.Tests
{
#pragma warning disable CS0618
    public static class LocalPortManagerTest
    {
        [Fact]
        public static void Default_0_GetRandomUnusedPort()
        {
            var unusedPort0 = LocalPortManager.GetRandomUnusedPort();
            Assert.True(unusedPort0 > 0);
            var unusedPort1 = LocalPortManager.GetRandomUnusedPort();
            Assert.True(unusedPort1 > 0);
            Assert.NotEqual(unusedPort0, unusedPort1);
            var unusedPort2 = LocalPortManager.GetRandomUnusedPort(true);
            Assert.True(unusedPort2 > 0);
            Assert.Equal(unusedPort1, unusedPort2);
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
#pragma warning restore CS0618
}
