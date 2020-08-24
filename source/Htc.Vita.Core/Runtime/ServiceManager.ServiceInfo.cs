namespace Htc.Vita.Core.Runtime
{
    public partial class ServiceManager
    {
        /// <summary>
        /// Class ServiceInfo.
        /// </summary>
        public class ServiceInfo
        {
            /// <summary>
            /// Gets or sets the service name.
            /// </summary>
            /// <value>The service name.</value>
            public string ServiceName { get; set; }
            /// <summary>
            /// Gets or sets the current service state.
            /// </summary>
            /// <value>The current service state.</value>
            public CurrentState CurrentState { get; set; } = CurrentState.Unknown;
            /// <summary>
            /// Gets or sets the service start type.
            /// </summary>
            /// <value>The service start type.</value>
            public StartType StartType { get; set; } = StartType.Unknown;
            /// <summary>
            /// Gets or sets the error code.
            /// </summary>
            /// <value>The error code.</value>
            public int ErrorCode { get; set; }
            /// <summary>
            /// Gets or sets the error message.
            /// </summary>
            /// <value>The error message.</value>
            public string ErrorMessage { get; set; }
        }
    }
}
