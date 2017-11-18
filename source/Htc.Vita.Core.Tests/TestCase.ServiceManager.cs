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
            Assert.False(string.IsNullOrWhiteSpace(serviceInfo.ServiceName));
            Assert.NotEqual(ServiceManager.CurrentState.Unknown, serviceInfo.CurrentState);
            Assert.NotEqual(ServiceManager.StartType.Unknown, serviceInfo.StartType);
            Assert.Equal(0, serviceInfo.ErrorCode);
            Assert.True(string.IsNullOrWhiteSpace(serviceInfo.ErrorMessage));
        }

        [Fact]
        public void ServiceManager_Default_2_ChangeStartType()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var exists = ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            var serviceInfo = ServiceManager.QueryStartType("Winmgmt");
            Assert.NotNull(serviceInfo);

            if (serviceInfo.StartType != ServiceManager.StartType.Disabled)
            {
                return;
            }
            serviceInfo = ServiceManager.ChangeStartType("Winmgmt", ServiceManager.StartType.Automatic);
            Assert.False(string.IsNullOrWhiteSpace(serviceInfo.ServiceName));
            Assert.NotEqual(ServiceManager.CurrentState.Unknown, serviceInfo.CurrentState);
            Assert.Equal(ServiceManager.StartType.Automatic, serviceInfo.StartType);
            Assert.Equal(0, serviceInfo.ErrorCode);
            Assert.True(string.IsNullOrWhiteSpace(serviceInfo.ErrorMessage));
        }

        [Fact]
        public void ServiceManager_Default_3_Start()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var exists = ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            var serviceInfo = ServiceManager.QueryStartType("Winmgmt");
            Assert.NotNull(serviceInfo);

            if (serviceInfo.StartType != ServiceManager.StartType.Disabled)
            {
                return;
            }
            serviceInfo = ServiceManager.ChangeStartType("Winmgmt", ServiceManager.StartType.Automatic);
            Assert.False(string.IsNullOrWhiteSpace(serviceInfo.ServiceName));
            Assert.NotEqual(serviceInfo.CurrentState, ServiceManager.CurrentState.Unknown);
            Assert.Equal(serviceInfo.StartType, ServiceManager.StartType.Automatic);
            Assert.Equal(serviceInfo.ErrorCode, 0);
            Assert.True(string.IsNullOrWhiteSpace(serviceInfo.ErrorMessage));

            serviceInfo = ServiceManager.Start("Winmgmt");
            Assert.False(string.IsNullOrWhiteSpace(serviceInfo.ServiceName));
            Assert.True(serviceInfo.CurrentState == ServiceManager.CurrentState.Running || serviceInfo.CurrentState == ServiceManager.CurrentState.StartPending);
            Assert.Equal(serviceInfo.StartType, ServiceManager.StartType.Automatic);
            Assert.Equal(serviceInfo.ErrorCode, 0);
            Assert.True(string.IsNullOrWhiteSpace(serviceInfo.ErrorMessage));
        }
    }
}
