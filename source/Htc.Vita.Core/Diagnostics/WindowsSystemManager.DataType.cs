using System;

namespace Htc.Vita.Core.Diagnostics
{
    public partial class WindowsSystemManager
    {
        /// <summary>
        /// Class WindowsApplicationInfo.
        /// </summary>
        public class WindowsApplicationInfo
        {
            /// <summary>
            /// Gets or sets the display name.
            /// </summary>
            /// <value>The display name.</value>
            public string DisplayName { get; set; }
            /// <summary>
            /// Gets or sets the display version.
            /// </summary>
            /// <value>The display version.</value>
            public Version DisplayVersion { get; set; }
            /// <summary>
            /// Gets or sets the install scope.
            /// </summary>
            /// <value>The install scope.</value>
            public WindowsApplicationInstallScope InstallScope { get; set; }
        }

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
        /// Enum GetInstalledApplicationListStatus
        /// </summary>
        public enum GetInstalledApplicationListStatus
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
        /// Enum WindowsApplicationInstallScope
        /// </summary>
        public enum WindowsApplicationInstallScope
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// The application is installed per user
            /// </summary>
            PerUser,
            /// <summary>
            /// The application is installed per machine
            /// </summary>
            PerMachine
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
