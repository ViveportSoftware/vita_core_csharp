using Htc.Vita.Core.Net;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class SecurityProtocolManagerTest
    {
        private readonly ITestOutputHelper _output;

        public SecurityProtocolManagerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Default_0_GetAvailableProtocol()
        {
            var availableProtocol = SecurityProtocolManager.GetAvailableProtocol();
            Assert.NotEqual(0, (int) availableProtocol);
            _output.WriteLine($@"availableProtocol: {availableProtocol}");
        }
    }
}
