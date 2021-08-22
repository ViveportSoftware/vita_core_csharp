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
    }
}
