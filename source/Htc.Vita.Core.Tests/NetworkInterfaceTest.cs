using System.Net;
using Htc.Vita.Core.Log;
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
        public static void Default_1_IsInternetAvailable()
        {
            Assert.True(NetworkInterface.IsInternetAvailable());
        }

        [Fact]
        public static void Default_2_GetLocalIPv4AddressesWithSubnetMask()
        {
            var map = NetworkInterface.GetLocalIPv4AddressesWithSubnetMask();
            Assert.NotEmpty(map);
            foreach (var key in map.Keys)
            {
                Logger.GetInstance(typeof(NetworkInterfaceTest)).Info($"local address: {key}, mask: {map[key]}");
            }
        }

        [Fact]
        public static void Default_3_GetIPv4BroadcastAddress()
        {
            var address = IPAddress.Parse("127.0.0.1");
            var subnetMask = IPAddress.Parse("255.0.0.0");
            var broadcastAddress = NetworkInterface.GetIPv4BroadcastAddress(address, subnetMask);
            Assert.Equal("127.255.255.255", broadcastAddress.ToString());
            address = IPAddress.Parse("172.17.24.225");
            subnetMask = IPAddress.Parse("255.255.255.240");
            broadcastAddress = NetworkInterface.GetIPv4BroadcastAddress(address, subnetMask);
            Assert.Equal("172.17.24.239", broadcastAddress.ToString());
        }

        [Fact]
        public static void Default_4_IsInSameIPv4Subnet()
        {
            var firstAddress = IPAddress.Parse("192.168.56.1");
            var secondAddress = IPAddress.Parse("192.168.56.234");
            var subnetMask = IPAddress.Parse("255.255.255.0");
            Assert.True(NetworkInterface.IsInSameIPv4Subnet(firstAddress, secondAddress, subnetMask));
            secondAddress = IPAddress.Parse("192.168.99.1");
            Assert.False(NetworkInterface.IsInSameIPv4Subnet(firstAddress, secondAddress, subnetMask));
        }

        [Fact]
        public static void Default_5_GetLocalIPv4AddressesWithBroadcastAddress()
        {
            var map = NetworkInterface.GetLocalIPv4AddressesWithBroadcastAddress();
            Assert.NotEmpty(map);
            foreach (var key in map.Keys)
            {
                Logger.GetInstance(typeof(NetworkInterfaceTest)).Info($"local address: {key}, broadcast address: {map[key]}");
            }
        }
    }
}
