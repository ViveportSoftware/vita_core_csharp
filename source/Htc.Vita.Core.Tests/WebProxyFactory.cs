using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class WebProxyFactory
    {
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
    }
}
