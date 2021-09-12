using Htc.Vita.Core.Net;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
#pragma warning disable CS0618
    public class RouteTracerTest
    {
        private readonly ITestOutputHelper _output;

        public RouteTracerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Default_0_Trace_WithHostname()
        {
            const string hostname = "www.google.com";
            var hops = RouteTracer.Trace(hostname, 20, 5000);
            Assert.NotNull(hops);
            Assert.True(hops.Count > 0);
            foreach (var hop in hops)
            {
                _output.WriteLine("Hop" + hop);
            }
        }

        [Fact]
        public void Default_0_Trace_WithIpAddress()
        {
            const string ipAddress = "8.8.8.8";
            var hops = RouteTracer.Trace(ipAddress, 20, 5000);
            Assert.NotNull(hops);
            Assert.True(hops.Count > 0);
            foreach (var hop in hops)
            {
                _output.WriteLine("Hop" + hop);
            }
        }
    }
#pragma warning restore CS0618
}
