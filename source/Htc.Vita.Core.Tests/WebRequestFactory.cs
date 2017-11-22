using System;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class WebRequestFactory
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var webRequestFactory = Net.WebRequestFactory.GetInstance();
            Assert.NotNull(webRequestFactory);
        }

        [Fact]
        public static void Default_1_GetHttpWebRequest()
        {
            var webRequestFactory = Net.WebRequestFactory.GetInstance();
            Assert.NotNull(webRequestFactory);
            var webRequest = webRequestFactory.GetHttpWebRequest(new Uri("http://www.google.com"));
            Assert.NotNull(webRequest);
            Console.WriteLine("WebRequest.UserAgent: " + webRequest.UserAgent);
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
