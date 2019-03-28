using Htc.Vita.Core.Runtime;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class UserManagerTest
    {
        private readonly ITestOutputHelper _output;

        public UserManagerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Default_0_GetFirstActiveUser()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var username = UserManager.GetFirstActiveUser();
            _output.WriteLine("username: " + username);
            Assert.True(username != null && username.Length >= 3);
        }
    }
}
