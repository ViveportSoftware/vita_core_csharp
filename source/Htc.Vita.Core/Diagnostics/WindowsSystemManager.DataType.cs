using System;

namespace Htc.Vita.Core.Diagnostics
{
    public partial class WindowsSystemManager
    {
        /// <summary>
        /// Class WindowsUpdateInfo.
        /// </summary>
        public class WindowsUpdateInfo
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>The identifier.</value>
            public string Id { get; set; }
            /// <summary>
            /// Gets or sets the time installed on.
            /// </summary>
            /// <value>The time installed on.</value>
            public DateTime InstalledOn { get; set; }
        }

        /// <summary>
        /// Enum GetInstalledUpdateListStatus
        /// </summary>
        public enum GetInstalledUpdateListStatus
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// Ok
            /// </summary>
            Ok
        }

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
        /// Enum WindowsSecureBootStatus
        /// </summary>
        public enum WindowsSecureBootStatus
        {
            /// <summary>
            /// The status is unknown
            /// </summary>
            Unknown,
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
            Refused
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
