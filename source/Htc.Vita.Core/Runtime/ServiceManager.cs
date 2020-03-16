namespace Htc.Vita.Core.Runtime
{
    public partial class ServiceManager
    {
        public static ServiceInfo ChangeStartType(string serviceName, StartType startType)
        {
            return Windows.ChangeStartTypeInPlatform(serviceName, startType);
        }

        public static bool CheckIfExists(string serviceName)
        {
            return Windows.CheckIfExistsInPlatform(serviceName);
        }

        public static ServiceInfo QueryStartType(string serviceName)
        {
            return Windows.QueryStartTypeInPlatform(serviceName);
        }

        public static ServiceInfo Start(string serviceName)
        {
            return Windows.StartInPlatform(serviceName);
        }

        public enum StartType
        {
            Unknown = 0,
            NotAvailable = 1,
            Disabled = 2,
            Manual = 3,
            Automatic = 4,
            DelayedAutomatic = 5
        }

        public enum CurrentState
        {
            Unknown = 0,
            NotAvailable = 1,
            Stopped = 2,
            StartPending = 3,
            StopPending = 4,
            Running = 5,
            ContinuePending = 6,
            PausePending = 7,
            Paused = 8
        }
    }
}
