using System;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class WebRequestFactory
    {
        private readonly ITestOutputHelper _output;

        public WebRequestFactory(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Default_0_GetInstance()
        {
            var webRequestFactory = Net.WebRequestFactory.GetInstance();
            Assert.NotNull(webRequestFactory);
        }

        [Fact]
        public void Default_1_GetHttpWebRequest()
        {
            var webRequestFactory = Net.WebRequestFactory.GetInstance();
            Assert.NotNull(webRequestFactory);
            var webRequest = webRequestFactory.GetHttpWebRequest(new Uri("https://www.google.com/search?q=firefox"));
            Assert.NotNull(webRequest);
            _output.WriteLine("WebRequest.UserAgent: " + webRequest.UserAgent);
        }

        [Fact]
        public static void Default_1_GetHttpWebRequest_WithEmptyUri()
        {
            var webRequestFactory = Net.WebRequestFactory.GetInstance();
            Assert.NotNull(webRequestFactory);
            var webRequest = webRequestFactory.GetHttpWebRequest(null);
            Assert.Null(webRequest);
        }
    }
}
