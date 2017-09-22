using System;
using Htc.Vita.Core.Net;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void WebRequestFactory_Default_0_GetInstance()
        {
            var webRequestFactory = WebRequestFactory.GetInstance();
            Assert.NotNull(webRequestFactory);
        }

        [Fact]
        public void WebRequestFactory_Default_1_GetHttpWebRequest()
        {
            var webRequestFactory = WebRequestFactory.GetInstance();
            Assert.NotNull(webRequestFactory);
            var webRequest = webRequestFactory.GetHttpWebRequest(new Uri("http://www.google.com"));
            Assert.NotNull(webRequest);
            Console.WriteLine("WebRequest.UserAgent: " + webRequest.UserAgent);
        }

        [Fact]
        public void WebRequestFactory_Default_1_GetHttpWebRequest_WithEmptyUri()
        {
            var webRequestFactory = WebRequestFactory.GetInstance();
            Assert.NotNull(webRequestFactory);
            var webRequest = webRequestFactory.GetHttpWebRequest(null);
            Assert.Null(webRequest);
        }
    }
}
