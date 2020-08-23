namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class ServiceManager.
    /// </summary>
    public partial class ServiceManager
    {
        /// <summary>
        /// Changes the service start type.
        /// </summary>
        /// <param name="serviceName">The service name.</param>
        /// <param name="startType">The service start type.</param>
        /// <returns>ServiceInfo.</returns>
        public static ServiceInfo ChangeStartType(string serviceName, StartType startType)
        {
            return Windows.ChangeStartTypeInPlatform(serviceName, startType);
        }

        /// <summary>
        /// Checks if the service exists.
        /// </summary>
        /// <param name="serviceName">The service name.</param>
        /// <returns><c>true</c> if the service exists, <c>false</c> otherwise.</returns>
        public static bool CheckIfExists(string serviceName)
        {
            return Windows.CheckIfExistsInPlatform(serviceName);
        }

        /// <summary>
        /// Queries the service start type.
        /// </summary>
        /// <param name="serviceName">The service name.</param>
        /// <returns>ServiceInfo.</returns>
        public static ServiceInfo QueryStartType(string serviceName)
        {
            return Windows.QueryStartTypeInPlatform(serviceName);
        }

        /// <summary>
        /// Starts service with the specified name.
        /// </summary>
        /// <param name="serviceName">The service name.</param>
        /// <returns>ServiceInfo.</returns>
        public static ServiceInfo Start(string serviceName)
        {
            return Windows.StartInPlatform(serviceName);
        }

        /// <summary>
        /// Enum StartType
        /// </summary>
        public enum StartType
        {
            /// <summary>
            /// Unknown service start type
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// Service is not available
            /// </summary>
            NotAvailable = 1,
            /// <summary>
            /// Service is disabled
            /// </summary>
            Disabled = 2,
            /// <summary>
            /// Service will be started manually
            /// </summary>
            Manual = 3,
            /// <summary>
            /// Service will be started automatically
            /// </summary>
            Automatic = 4,
            /// <summary>
            /// Service will delay to be started automatically
            /// </summary>
            DelayedAutomatic = 5
        }

        /// <summary>
        /// Enum CurrentState
        /// </summary>
        public enum CurrentState
        {
            /// <summary>
            /// Unknown current state
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// Service is not available
            /// </summary>
            NotAvailable = 1,
            /// <summary>
            /// Service is stopped
            /// </summary>
            Stopped = 2,
            /// <summary>
            /// Service is pending to start
            /// </summary>
            StartPending = 3,
            /// <summary>
            /// Service is pending to stop
            /// </summary>
            StopPending = 4,
            /// <summary>
            /// Service is running
            /// </summary>
            Running = 5,
            /// <summary>
            /// Service continue pending
            /// </summary>
            ContinuePending = 6,
            /// <summary>
            /// Service pause pending
            /// </summary>
            PausePending = 7,
            /// <summary>
            /// Service is paused
            /// </summary>
            Paused = 8
        }
    }
}
