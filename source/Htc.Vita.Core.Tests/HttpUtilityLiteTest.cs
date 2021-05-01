using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class HttpUtilityLiteTest
    {
        [Fact]
        public static void Default_0_ParseQueryString()
        {
            var testUrl = "https://download.visualstudio.microsoft.com/download/pr/12319034/ccd261eb0e095411af3b306273231b68/VC_redist.x86.exe";
            var queryStringMap = HttpUtilityLite.ParseQueryString(testUrl);
            foreach (var key in queryStringMap.AllKeys)
            {
                Assert.False(string.IsNullOrWhiteSpace(key));
            }

            testUrl = "https://download.visualstudio.microsoft.com/download/pr/12319034/ccd261eb0e095411af3b306273231b68/VC_redist.x86.exe?testKey=testValue";
            queryStringMap = HttpUtilityLite.ParseQueryString(testUrl);
            foreach (var key in queryStringMap.AllKeys)
            {
                Assert.False(string.IsNullOrWhiteSpace(key));
            }
            Assert.Equal("testValue", queryStringMap["testKey"]);
            Assert.Equal("testKey=testValue", queryStringMap.ToString());
        }
    }
}
