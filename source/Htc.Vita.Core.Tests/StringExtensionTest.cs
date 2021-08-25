using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class StringExtensionTest
    {
        [Fact]
        public static void Default_0_ToUri()
        {
            Assert.Null("1".ToUri());
            Assert.NotNull("https://www.google.com".ToUri());
        }

        [Fact]
        public static void Default_1_SplitToSet()
        {
            const string data = "1 2 3 4 5";
            var result = data.SplitToSet();
            Assert.Equal(5, result.Count);
            Assert.True(result.Contains("3"));
            Assert.False(result.Contains("6"));
        }
    }
}
