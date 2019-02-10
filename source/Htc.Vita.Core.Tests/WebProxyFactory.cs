using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class WebProxyFactory
    {
        private readonly ITestOutputHelper _output;

        public WebProxyFactory(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Default_0_GetInstance()
        {
            var webProxyFactory = Net.WebProxyFactory.GetInstance();
            Assert.NotNull(webProxyFactory);
        }

        [Fact]
        public static void Default_1_GetWebProxy()
        {
            var webProxyFactory = Net.WebProxyFactory.GetInstance();
            Assert.NotNull(webProxyFactory);
            var webProxy = webProxyFactory.GetWebProxy();
            Assert.NotNull(webProxy);
        }

        [Fact]
        public void Default_2_GetWebProxyStatus()
        {
            var webProxyFactory = Net.WebProxyFactory.GetInstance();
            Assert.NotNull(webProxyFactory);
            var webProxy = webProxyFactory.GetWebProxy();
            Assert.NotNull(webProxy);
            var webProxyStatus = webProxyFactory.GetWebProxyStatus(webProxy);
            Assert.True(webProxyStatus != Net.WebProxyFactory.WebProxyStatus.Unknown);
            _output.WriteLine("WebProxyStatus: " + webProxyStatus);
        }
    }
}
