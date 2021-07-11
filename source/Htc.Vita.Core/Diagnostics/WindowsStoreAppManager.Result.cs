namespace Htc.Vita.Core.Diagnostics
{
    public partial class WindowsStoreAppManager
    {
        /// <summary>
        /// Class GetAppPackageResult.
        /// </summary>
        public class GetAppPackageResult
        {
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public GetAppPackageStatus Status { get; set; } = GetAppPackageStatus.Unknown;
            /// <summary>
            /// Gets or sets the application package.
            /// </summary>
            /// <value>The application package.</value>
            public WindowsStoreAppPackageInfo AppPackage { get; set; }
        }

        /// <summary>
        /// Enum GetAppPackageStatus
        /// </summary>
        public enum GetAppPackageStatus
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// Ok
            /// </summary>
            Ok,
            /// <summary>
            /// Invalid data
            /// </summary>
            InvalidData,
            /// <summary>
            /// Package not found
            /// </summary>
            PackageNotFound,
            /// <summary>
            /// Unsupported platform
            /// </summary>
            UnsupportedPlatform
        }
    }
}
