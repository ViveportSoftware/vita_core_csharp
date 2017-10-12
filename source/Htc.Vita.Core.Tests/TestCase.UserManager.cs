using System;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void UserManager_Default_0_GetActiveUser()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var username = UserManager.GetFirstActiveUser();
            Console.WriteLine("username: " + username);
            Assert.True(username != null && username.Length >= 3);
        }
    }
}
