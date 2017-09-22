using Htc.Vita.Core.Net;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void WebProxyFactory_Default_0_GetInstance()
        {
            var webProxyFactory = WebProxyFactory.GetInstance();
            Assert.NotNull(webProxyFactory);
        }

        [Fact]
        public void WebProxyFactory_Default_1_GetWebProxy()
        {
            var webProxyFactory = WebProxyFactory.GetInstance();
            Assert.NotNull(webProxyFactory);
            var webProxy = webProxyFactory.GetWebProxy();
            Assert.NotNull(webProxy);
        }
    }
}
