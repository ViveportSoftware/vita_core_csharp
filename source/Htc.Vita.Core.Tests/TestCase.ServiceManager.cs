using System;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void ServiceManager_Default_0_CheckIfExists()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var exists = ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            exists = ServiceManager.CheckIfExists("Winmgmt2");
            Assert.False(exists);
        }

        [Fact]
        public void ServiceManager_Default_1_QueryStartType()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var exists = ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            var serviceInfo = ServiceManager.QueryStartType("Winmgmt");
            Console.WriteLine("serviceInfo.ServiceName: " + serviceInfo.ServiceName);
            Console.WriteLine("serviceInfo.CurrentState: " + serviceInfo.CurrentState);
            Console.WriteLine("serviceInfo.StartType: " + serviceInfo.StartType);
            Console.WriteLine("serviceInfo.ErrorCode: " + serviceInfo.ErrorCode);
            Console.WriteLine("serviceInfo.ErrorMessage: " + serviceInfo.ErrorMessage);
            Assert.True(serviceInfo.ErrorCode == 0);
        }

        [Fact(Skip = "AdministratorPermissionNeeded")]
        public void ServiceManager_Default_2_ChangeStartType()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var exists = ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            var serviceInfo = ServiceManager.ChangeStartType("Winmgmt", ServiceManager.StartType.Automatic);
            Console.WriteLine("serviceInfo.ServiceName: " + serviceInfo.ServiceName);
            Console.WriteLine("serviceInfo.CurrentState: " + serviceInfo.CurrentState);
            Console.WriteLine("serviceInfo.StartType: " + serviceInfo.StartType);
            Console.WriteLine("serviceInfo.ErrorCode: " + serviceInfo.ErrorCode);
            Console.WriteLine("serviceInfo.ErrorMessage: " + serviceInfo.ErrorMessage);
            Assert.True(serviceInfo.ErrorCode == 0);
        }

        [Fact(Skip = "AdministratorPermissionNeeded")]
        public void ServiceManager_Default_3_Start()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var exists = ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            var serviceInfo = ServiceManager.Start("Winmgmt");
            Console.WriteLine("serviceInfo.ServiceName: " + serviceInfo.ServiceName);
            Console.WriteLine("serviceInfo.CurrentState: " + serviceInfo.CurrentState);
            Console.WriteLine("serviceInfo.StartType: " + serviceInfo.StartType);
            Console.WriteLine("serviceInfo.ErrorCode: " + serviceInfo.ErrorCode);
            Console.WriteLine("serviceInfo.ErrorMessage: " + serviceInfo.ErrorMessage);
            Assert.True(serviceInfo.ErrorCode == 0);
        }
    }
}
