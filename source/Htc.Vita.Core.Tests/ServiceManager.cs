using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class ServiceManager
    {
        [Fact]
        public static void Default_0_CheckIfExists()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var exists = Runtime.ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            exists = Runtime.ServiceManager.CheckIfExists("Winmgmt2");
            Assert.False(exists);
        }

        [Fact]
        public static void Default_1_QueryStartType()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var exists = Runtime.ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            var serviceInfo = Runtime.ServiceManager.QueryStartType("Winmgmt");
            Assert.False(string.IsNullOrWhiteSpace(serviceInfo.ServiceName));
            Assert.NotEqual(Runtime.ServiceManager.CurrentState.Unknown, serviceInfo.CurrentState);
            Assert.NotEqual(Runtime.ServiceManager.StartType.Unknown, serviceInfo.StartType);
            Assert.Equal(0, serviceInfo.ErrorCode);
            Assert.True(string.IsNullOrWhiteSpace(serviceInfo.ErrorMessage));
        }

        [Fact]
        public static void Default_2_ChangeStartType()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var exists = Runtime.ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            var serviceInfo = Runtime.ServiceManager.QueryStartType("Winmgmt");
            Assert.NotNull(serviceInfo);

            if (serviceInfo.StartType != Runtime.ServiceManager.StartType.Disabled)
            {
                return;
            }
            serviceInfo = Runtime.ServiceManager.ChangeStartType("Winmgmt", Runtime.ServiceManager.StartType.Automatic);
            Assert.False(string.IsNullOrWhiteSpace(serviceInfo.ServiceName));
            Assert.NotEqual(Runtime.ServiceManager.CurrentState.Unknown, serviceInfo.CurrentState);
            Assert.Equal(Runtime.ServiceManager.StartType.Automatic, serviceInfo.StartType);
            Assert.Equal(0, serviceInfo.ErrorCode);
            Assert.True(string.IsNullOrWhiteSpace(serviceInfo.ErrorMessage));
        }

        [Fact]
        public static void Default_3_Start()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var exists = Runtime.ServiceManager.CheckIfExists("Winmgmt");
            Assert.True(exists);
            var serviceInfo = Runtime.ServiceManager.QueryStartType("Winmgmt");
            Assert.NotNull(serviceInfo);

            if (serviceInfo.StartType != Runtime.ServiceManager.StartType.Disabled)
            {
                return;
            }
            serviceInfo = Runtime.ServiceManager.ChangeStartType("Winmgmt", Runtime.ServiceManager.StartType.Automatic);
            Assert.False(string.IsNullOrWhiteSpace(serviceInfo.ServiceName));
            Assert.NotEqual(serviceInfo.CurrentState, Runtime.ServiceManager.CurrentState.Unknown);
            Assert.Equal(serviceInfo.StartType, Runtime.ServiceManager.StartType.Automatic);
            Assert.Equal(serviceInfo.ErrorCode, 0);
            Assert.True(string.IsNullOrWhiteSpace(serviceInfo.ErrorMessage));

            serviceInfo = Runtime.ServiceManager.Start("Winmgmt");
            Assert.False(string.IsNullOrWhiteSpace(serviceInfo.ServiceName));
            Assert.True(serviceInfo.CurrentState == Runtime.ServiceManager.CurrentState.Running || serviceInfo.CurrentState == Runtime.ServiceManager.CurrentState.StartPending);
            Assert.Equal(serviceInfo.StartType, Runtime.ServiceManager.StartType.Automatic);
            Assert.Equal(serviceInfo.ErrorCode, 0);
            Assert.True(string.IsNullOrWhiteSpace(serviceInfo.ErrorMessage));
        }
    }
}
