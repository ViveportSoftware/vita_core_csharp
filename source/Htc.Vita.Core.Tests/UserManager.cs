using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class UserManager
    {
        private readonly ITestOutputHelper _output;

        public UserManager(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Default_0_GetFirstActiveUser()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var username = Runtime.UserManager.GetFirstActiveUser();
            _output.WriteLine("username: " + username);
            Assert.True(username != null && username.Length >= 3);
        }
    }
}
