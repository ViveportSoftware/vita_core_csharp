namespace Htc.Vita.Core.IO
{
    public partial class FileSystemManagerV2
    {
        /// <summary>
        /// Class GetDiskSpaceResult.
        /// </summary>
        public class GetDiskSpaceResult
        {
            /// <summary>
            /// Gets or sets the disk space.
            /// </summary>
            /// <value>The disk space.</value>
            public DiskSpaceInfo DiskSpace { get; set; }
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public GetDiskSpaceStatus Status { get; set; }
        }

        /// <summary>
        /// Class VerifyPathDepthResult.
        /// </summary>
        public class VerifyPathDepthResult
        {
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public VerifyPathDepthStatus Status { get; set; }
        }

        /// <summary>
        /// Enum GetDiskSpaceStatus
        /// </summary>
        public enum GetDiskSpaceStatus
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
            /// Insufficient permission
            /// </summary>
            InsufficientPermission,
            /// <summary>
            /// Unsupported platform
            /// </summary>
            UnsupportedPlatform
        }

        /// <summary>
        /// Enum VerifyPathDepthStatus
        /// </summary>
        public enum VerifyPathDepthStatus
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
            /// Insufficient permission
            /// </summary>
            InsufficientPermission,
            /// <summary>
            /// Unsupported
            /// </summary>
            Unsupported
        }
    }
}
