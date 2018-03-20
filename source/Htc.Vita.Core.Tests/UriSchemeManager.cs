using System;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class UriSchemeManager
    {
        [Fact]
        public static void Default_0_GetSystemUriScheme()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }

            var systemUriScheme = Shell.UriSchemeManager.GetSystemUriScheme("http");
            Assert.NotNull(systemUriScheme);
            Console.WriteLine("systemUriScheme.Name: \"" + systemUriScheme.Name + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.Name));
            Console.WriteLine("systemUriScheme.DefaultIcon: \"" + systemUriScheme.DefaultIcon + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.DefaultIcon));
            Console.WriteLine("systemUriScheme.CommandPath: \"" + systemUriScheme.CommandPath + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.CommandPath));
            Console.WriteLine("systemUriScheme.CommandParameter: \"" + systemUriScheme.CommandParameter + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.CommandParameter));
        }

        [Fact]
        public static void Default_0_GetSystemUriScheme_WithHttp2()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }

            var systemUriScheme = Shell.UriSchemeManager.GetSystemUriScheme("http2");
            Assert.NotNull(systemUriScheme);
            Assert.False(string.IsNullOrEmpty(systemUriScheme.Name));
            Assert.True(string.IsNullOrEmpty(systemUriScheme.DefaultIcon));
            Assert.True(string.IsNullOrEmpty(systemUriScheme.CommandPath));
            Assert.True(string.IsNullOrEmpty(systemUriScheme.CommandParameter));
        }
    }
}
