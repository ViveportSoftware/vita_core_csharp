using Htc.Vita.Core.Shell;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class ShellIconTest
    {
        [Fact]
        public static void Default_0_FlushCacheInWindows()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.True(ShellIcon.FlushCache());
        }
    }
}
