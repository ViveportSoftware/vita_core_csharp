using Htc.Vita.Core.Net;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class WebUserAgentV2FactoryTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var webUserAgentV2Factory = WebUserAgentV2Factory.GetInstance();
            Assert.NotNull(webUserAgentV2Factory);
        }

        [Fact]
        public void Default_1_GetWebUserAgent()
        {
            WebUserAgentV2Factory.Name = "TestName";
            var webUserAgentV2Factory = WebUserAgentV2Factory.GetInstance();
            Assert.NotNull(webUserAgentV2Factory);
            var webUserAgentV2 = webUserAgentV2Factory.GetWebUserAgent();
            Assert.NotNull(webUserAgentV2);
            Assert.Equal("TestName", webUserAgentV2.GetModuleName());
            Assert.NotEqual("0.0.0.0", webUserAgentV2.GetModuleVersion());
            Assert.NotEqual("UnknownInstance", webUserAgentV2.GetModuleInstanceName());
        }
    }
}
