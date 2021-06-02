namespace Htc.Vita.Core.Diagnostics
{
    public partial class WindowsSystemManager
    {
        /// <summary>
        /// Enum WindowsFipsStatus
        /// </summary>
        public enum WindowsFipsStatus
        {
            /// <summary>
            /// The status unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// The status is unchanged
            /// </summary>
            Unchanged,
            /// <summary>
            /// The status is disabled
            /// </summary>
            Disabled,
            /// <summary>
            /// The status is enabled
            /// </summary>
            Enabled,
            /// <summary>
            /// The operation is refused
            /// </summary>
            Refused,
            /// <summary>
            /// The system reboot is required
            /// </summary>
            RebootRequired
        }

        /// <summary>
        /// Enum WindowsProductType
        /// </summary>
        public enum WindowsProductType
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// The client product
            /// </summary>
            Client,
            /// <summary>
            /// The server product
            /// </summary>
            Server
        }
    }
}
