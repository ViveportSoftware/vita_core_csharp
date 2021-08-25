using System.Collections.Generic;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class HttpUtilityLiteTest
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

        [Fact]
        public static void Default_1_ToEncodedQueryString()
        {
            var data = new Dictionary<string, string>
            {
                    { "key1", "value1" },
                    { "key2", "value2" }
            };
            var result = HttpUtilityLite.ToEncodedQueryString(data);
            Assert.Contains("key1=value1", result);
            Assert.Contains("key2=value2", result);
        }
    }
}
