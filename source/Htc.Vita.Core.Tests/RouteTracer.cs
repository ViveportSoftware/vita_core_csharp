using System;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class RouteTracer
    {
        [Fact]
        public static void Default_0_Trace_WithHostname()
        {
            var hostname = "www.google.com";
            var hops = Net.RouteTracer.Trace(hostname, 20, 5000);
            Assert.NotNull(hops);
            Assert.True(hops.Count > 0);
            foreach (var hop in hops)
            {
                Console.WriteLine("Hop" + hop);
            }
        }

        [Fact]
        public static void Default_0_Trace_WithIpAddress()
        {
            var ipAddress = "8.8.8.8";
            var hops = Net.RouteTracer.Trace(ipAddress, 20, 5000);
            Assert.NotNull(hops);
            Assert.True(hops.Count > 0);
            foreach (var hop in hops)
            {
                Console.WriteLine("Hop" + hop);
            }
        }
    }
}
