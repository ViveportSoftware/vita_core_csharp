using System.Net;
using System.Net.Sockets;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Net;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class NetworkManagerTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var networkManager = NetworkManager.GetInstance();
            Assert.NotNull(networkManager);
        }

        [Fact]
        public static void Default_1_GetRandomUnusedPort()
        {
            var networkManager = NetworkManager.GetInstance();
            var getUnusedLocalPortResult = networkManager.GetUnusedLocalPort();
            var getUnusedLocalPortStatus = getUnusedLocalPortResult.Status;
            Assert.Equal(NetworkManager.GetUnusedLocalPortStatus.Ok, getUnusedLocalPortStatus);
            var unusedLocalPort0 = getUnusedLocalPortResult.LocalPort;
            Assert.True(unusedLocalPort0 > 0);
            getUnusedLocalPortResult = networkManager.GetUnusedLocalPort(true);
            getUnusedLocalPortStatus = getUnusedLocalPortResult.Status;
            Assert.Equal(NetworkManager.GetUnusedLocalPortStatus.Ok, getUnusedLocalPortStatus);
            var unusedLocalPort1 = getUnusedLocalPortResult.LocalPort;
            Assert.True(unusedLocalPort1 > 0);
            Assert.Equal(unusedLocalPort0, unusedLocalPort1);
        }

        [Fact]
        public static void Default_2_GetLocalPortStatus()
        {
            var networkManager = NetworkManager.GetInstance();
            var getUnusedLocalPortResult = networkManager.GetUnusedLocalPort();
            var getUnusedLocalPortStatus = getUnusedLocalPortResult.Status;
            Assert.Equal(NetworkManager.GetUnusedLocalPortStatus.Ok, getUnusedLocalPortStatus);
            var unusedLocalPort = getUnusedLocalPortResult.LocalPort;
            Assert.True(unusedLocalPort > 0);
            var getLocalPortStatusResult = networkManager.GetLocalPortStatus(unusedLocalPort);
            var getLocalPortStatusStatus = getLocalPortStatusResult.Status;
            Assert.Equal(NetworkManager.GetLocalPortStatusStatus.Ok, getLocalPortStatusStatus);
            var localPortStatus = getLocalPortStatusResult.LocalPortStatus;
            Assert.Equal(NetworkManager.PortStatus.Available, localPortStatus);
        }

        [Fact]
        public static void Default_2_GetLocalPortStatus_WithUsedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listener.Start();
                var usedLocalPort = ((IPEndPoint)listener.LocalEndpoint).Port;
                Assert.True(usedLocalPort > 0);

                var networkManager = NetworkManager.GetInstance();
                var getLocalPortStatusResult = networkManager.GetLocalPortStatus(usedLocalPort);
                var getLocalPortStatusStatus = getLocalPortStatusResult.Status;
                Assert.Equal(NetworkManager.GetLocalPortStatusStatus.Ok, getLocalPortStatusStatus);
                var localPortStatus = getLocalPortStatusResult.LocalPortStatus;
                Assert.Equal(NetworkManager.PortStatus.InUse, localPortStatus);
            }
            finally
            {
                listener.Stop();
            }
        }

        [Fact]
        public static void Default_3_VerifyPortStatus()
        {
            var networkManager = NetworkManager.GetInstance();
            var getUnusedLocalPortResult = networkManager.GetUnusedLocalPort();
            var getUnusedLocalPortStatus = getUnusedLocalPortResult.Status;
            Assert.Equal(NetworkManager.GetUnusedLocalPortStatus.Ok, getUnusedLocalPortStatus);
            var unusedLocalPort = getUnusedLocalPortResult.LocalPort;
            Assert.True(unusedLocalPort > 0);
            var getLocalPortStatusResult = networkManager.GetLocalPortStatus(unusedLocalPort);
            var getLocalPortStatusStatus = getLocalPortStatusResult.Status;
            Assert.Equal(NetworkManager.GetLocalPortStatusStatus.Ok, getLocalPortStatusStatus);
            var localPortStatus = getLocalPortStatusResult.LocalPortStatus;
            Assert.Equal(NetworkManager.PortStatus.Available, localPortStatus);
            var verifyLocalPortStatusResult = networkManager.VerifyLocalPortStatus(unusedLocalPort);
            var verifyLocalPortStatusStatus = verifyLocalPortStatusResult.Status;
            Assert.Equal(NetworkManager.VerifyLocalPortStatusStatus.Ok, verifyLocalPortStatusStatus);
            localPortStatus = verifyLocalPortStatusResult.LocalPortStatus;
            Assert.Equal(NetworkManager.PortStatus.Available, localPortStatus);
        }

        [Fact]
        public static void Default_3_VerifyPortStatus_WithUsedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listener.Start();
                var usedLocalPort = ((IPEndPoint)listener.LocalEndpoint).Port;
                Assert.True(usedLocalPort > 0);

                var networkManager = NetworkManager.GetInstance();
                var getLocalPortStatusResult = networkManager.GetLocalPortStatus(usedLocalPort);
                var getLocalPortStatusStatus = getLocalPortStatusResult.Status;
                Assert.Equal(NetworkManager.GetLocalPortStatusStatus.Ok, getLocalPortStatusStatus);
                var localPortStatus = getLocalPortStatusResult.LocalPortStatus;
                Assert.Equal(NetworkManager.PortStatus.InUse, localPortStatus);
                var verifyLocalPortStatusResult = networkManager.VerifyLocalPortStatus(usedLocalPort);
                var verifyLocalPortStatusStatus = verifyLocalPortStatusResult.Status;
                Assert.Equal(NetworkManager.VerifyLocalPortStatusStatus.Ok, verifyLocalPortStatusStatus);
                localPortStatus = verifyLocalPortStatusResult.LocalPortStatus;
                Assert.Equal(NetworkManager.PortStatus.InUse, localPortStatus);
            }
            finally
            {
                listener.Stop();
            }
        }

        [Fact]
        public static void Default_4_TraceRoute()
        {
            var networkManager = NetworkManager.GetInstance();
            var hostNameOrIpAddress = "www.google.com";
            var traceRouteResult = networkManager.TraceRoute(hostNameOrIpAddress);
            var traceRouteStatus = traceRouteResult.Status;
            Assert.Equal(NetworkManager.TraceRouteStatus.Ok, traceRouteStatus);
            var route = traceRouteResult.Route;
            var hops = route.Hops;
            foreach (var hop in hops)
            {
                Logger.GetInstance(typeof(NetworkManagerTest)).Info($"{route.Target}/{hop}");
            }

            hostNameOrIpAddress = "8.8.8.8";
            traceRouteResult = networkManager.TraceRoute(hostNameOrIpAddress);
            traceRouteStatus = traceRouteResult.Status;
            Assert.Equal(NetworkManager.TraceRouteStatus.Ok, traceRouteStatus);
            route = traceRouteResult.Route;
            hops = route.Hops;
            foreach (var hop in hops)
            {
                Logger.GetInstance(typeof(NetworkManagerTest)).Info($"{route.Target}/{hop}");
            }
        }
    }
}
