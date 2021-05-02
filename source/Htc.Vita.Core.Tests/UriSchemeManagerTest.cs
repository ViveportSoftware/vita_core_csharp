using System.Collections.Generic;
using Htc.Vita.Core.Shell;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
#pragma warning disable 618
    public class UriSchemeManagerTest
    {
        private readonly ITestOutputHelper _output;

        public UriSchemeManagerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Default_0_GetSystemUriScheme()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }

            var uriSchemeManager = UriSchemeManager.GetInstance();
            Assert.NotNull(uriSchemeManager);

            var systemUriScheme = uriSchemeManager.GetSystemUriScheme("http");
            Assert.NotNull(systemUriScheme);
            _output.WriteLine("systemUriScheme.Name: \"" + systemUriScheme.Name + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.Name));
            _output.WriteLine("systemUriScheme.DefaultIcon: \"" + systemUriScheme.DefaultIcon + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.DefaultIcon));
            _output.WriteLine("systemUriScheme.CommandPath: \"" + systemUriScheme.CommandPath + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.CommandPath));
            _output.WriteLine("systemUriScheme.CommandParameter: \"" + systemUriScheme.CommandParameter + "\"");
            Assert.False(string.IsNullOrEmpty(systemUriScheme.CommandParameter));

            var systemUriScheme2 = uriSchemeManager.GetSystemUriScheme("http2");
            Assert.NotNull(systemUriScheme2);
            Assert.False(string.IsNullOrEmpty(systemUriScheme2.Name));
            Assert.True(string.IsNullOrEmpty(systemUriScheme2.DefaultIcon));
            Assert.True(string.IsNullOrEmpty(systemUriScheme2.CommandPath));
            Assert.True(string.IsNullOrEmpty(systemUriScheme2.CommandParameter));
        }

        [Fact]
        public void Default_0_GetSystemUriScheme_WithWhitelist()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }

            var uriSchemeManager = UriSchemeManager.GetInstance();
            Assert.NotNull(uriSchemeManager);

            var options = new Dictionary<string, string>
            {
                {UriSchemeManager.OptionAcceptWhitelistOnly, "true"}
            };
            var systemUriScheme = uriSchemeManager.GetSystemUriScheme("http", options);
            Assert.NotNull(systemUriScheme);
            _output.WriteLine("systemUriScheme.Name: \"" + systemUriScheme.Name + "\"");
            _output.WriteLine("systemUriScheme.DefaultIcon: \"" + systemUriScheme.DefaultIcon + "\"");
            _output.WriteLine("systemUriScheme.CommandPath: \"" + systemUriScheme.CommandPath + "\"");
            _output.WriteLine("systemUriScheme.CommandParameter: \"" + systemUriScheme.CommandParameter + "\"");

            var systemUriScheme2 = uriSchemeManager.GetSystemUriScheme("http2", options);
            Assert.NotNull(systemUriScheme2);
            Assert.False(string.IsNullOrEmpty(systemUriScheme2.Name));
            Assert.True(string.IsNullOrEmpty(systemUriScheme2.DefaultIcon));
            Assert.True(string.IsNullOrEmpty(systemUriScheme2.CommandPath));
            Assert.True(string.IsNullOrEmpty(systemUriScheme2.CommandParameter));
        }

        [Fact]
        public void Default_1_IsSystemUriSchemeValid()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }

            var uriSchemeManager = UriSchemeManager.GetInstance();
            Assert.NotNull(uriSchemeManager);

            var schemeName = "http";
            Assert.True(uriSchemeManager.IsSystemUriSchemeValid(schemeName));
            var systemUriScheme = uriSchemeManager.GetSystemUriScheme(schemeName);
            Assert.True(uriSchemeManager.IsSystemUriSchemeValid(systemUriScheme));

            schemeName = "http2";
            Assert.False(uriSchemeManager.IsSystemUriSchemeValid(schemeName));
            systemUriScheme = uriSchemeManager.GetSystemUriScheme(schemeName);
            Assert.False(uriSchemeManager.IsSystemUriSchemeValid(systemUriScheme));
        }
    }
#pragma warning restore 618
}
