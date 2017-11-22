using System;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class UserManager
    {
        [Fact]
        public static void Default_0_GetFirstActiveUser()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var username = Runtime.UserManager.GetFirstActiveUser();
            Console.WriteLine("username: " + username);
            Assert.True(username != null && username.Length >= 3);
        }
    }
}
