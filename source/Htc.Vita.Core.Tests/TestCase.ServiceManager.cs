using System;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void UserManager_Default_1_QueryStartType()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var serviceInfo = ServiceManager.QueryStartType("Winmgmt");
            Console.WriteLine("serviceInfo.ServiceName: " + serviceInfo.ServiceName);
            Console.WriteLine("serviceInfo.StartType: " + serviceInfo.StartType);
            Console.WriteLine("serviceInfo.ErrorCode: " + serviceInfo.ErrorCode);
            Console.WriteLine("serviceInfo.ErrorMessage: " + serviceInfo.ErrorMessage);
            Assert.True(serviceInfo.ErrorCode == 0);
        }

        [Fact(Skip = "AdministratorPermissionNeeded")]
        public void UserManager_Default_1_ChangeStartType()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var serviceInfo = ServiceManager.ChangeStartType("Winmgmt", ServiceManager.StartType.Automatic);
            Console.WriteLine("serviceInfo.ServiceName: " + serviceInfo.ServiceName);
            Console.WriteLine("serviceInfo.StartType: " + serviceInfo.StartType);
            Console.WriteLine("serviceInfo.ErrorCode: " + serviceInfo.ErrorCode);
            Console.WriteLine("serviceInfo.ErrorMessage: " + serviceInfo.ErrorMessage);
            Assert.True(serviceInfo.ErrorCode == 0);
        }
    }
}
