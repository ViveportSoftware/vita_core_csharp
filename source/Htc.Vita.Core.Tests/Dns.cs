using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class Dns
    {
        private readonly ITestOutputHelper _output;

        public Dns(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Dns_Default_0_GetInstance()
        {
            var dns = Net.Dns.GetInstance();
            Assert.NotNull(dns);
        }

        [Fact]
        public void Dns_Default_1_GetHostAddresses()
        {
            var dns = Net.Dns.GetInstance();
            Assert.NotNull(dns);
            var host = "www.google.com";
            var addresses = dns.GetHostAddresses(host);
            Assert.NotNull(addresses);
            Assert.NotEmpty(addresses);
            foreach (var address in addresses)
            {
                _output.WriteLine("address for \"" + host + "\": " + address);
            }
            var host2 = "172.217.27.132";
            var addresses2 = dns.GetHostAddresses(host2);
            Assert.NotNull(addresses2);
            Assert.NotEmpty(addresses2);
            foreach (var address2 in addresses2)
            {
                Assert.Equal(host2, address2.ToString());
            }
        }

        [Fact]
        public void Dns_Default_2_GetHostEntry()
        {
            var dns = Net.Dns.GetInstance();
            Assert.NotNull(dns);
            var host = "8.8.8.8";
            var entry = dns.GetHostEntry(host);
            Assert.NotNull(entry);
            _output.WriteLine("entry for \"" + host + "\": " + entry.HostName);
            var host2 = "www.google.com";
            var entry2 = dns.GetHostEntry(host2);
            Assert.NotNull(entry2);
            Assert.Equal(host2, entry2.HostName.ToLowerInvariant());
        }

        [Fact]
        public void Dns_Default_2_GetHostEntry_WithIPAddress()
        {
            var dns = Net.Dns.GetInstance();
            Assert.NotNull(dns);
            var host = IPAddress.Parse("8.8.8.8");
            var entry = dns.GetHostEntry(host);
            Assert.NotNull(entry);
            _output.WriteLine("entry for \"" + host + "\": " + entry.HostName);
        }

        [Fact]
        public static void Dns_Default_3_FlushCache()
        {
            var dns = Net.Dns.GetInstance();
            Assert.NotNull(dns);
            Assert.True(dns.FlushCache());
        }

        [Fact]
        public static void Dns_Default_3_FlushCache_WithHostName()
        {
            var dns = Net.Dns.GetInstance();
            Assert.NotNull(dns);
            Assert.False(dns.FlushCache(""));
            Assert.True(dns.FlushCache("www.google.com"));
        }
    }
}
