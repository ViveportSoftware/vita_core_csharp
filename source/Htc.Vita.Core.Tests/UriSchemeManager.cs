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

            var uriSchemeManager = Shell.UriSchemeManager.GetInstance();
            Assert.NotNull(uriSchemeManager);

            var systemUriScheme = uriSchemeManager.GetSystemUriScheme("http");
            Assert.NotNull(systemUriScheme);
            Console.WriteLine("systemUriScheme.Name: \"" + systemUriScheme.Name + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.Name));
            Console.WriteLine("systemUriScheme.DefaultIcon: \"" + systemUriScheme.DefaultIcon + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.DefaultIcon));
            Console.WriteLine("systemUriScheme.CommandPath: \"" + systemUriScheme.CommandPath + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.CommandPath));
            Console.WriteLine("systemUriScheme.CommandParameter: \"" + systemUriScheme.CommandParameter + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.CommandParameter));

            var systemUriScheme2 = uriSchemeManager.GetSystemUriScheme("http2");
            Assert.NotNull(systemUriScheme2);
            Assert.False(string.IsNullOrEmpty(systemUriScheme2.Name));
            Assert.True(string.IsNullOrEmpty(systemUriScheme2.DefaultIcon));
            Assert.True(string.IsNullOrEmpty(systemUriScheme2.CommandPath));
            Assert.True(string.IsNullOrEmpty(systemUriScheme2.CommandParameter));
        }
    }
}
