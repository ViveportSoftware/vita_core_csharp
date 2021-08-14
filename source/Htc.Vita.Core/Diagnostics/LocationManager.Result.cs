namespace Htc.Vita.Core.Diagnostics
{
    public partial class LocationManager
    {
        /// <summary>
        /// Class GetLocationResult.
        /// </summary>
        public class GetLocationResult
        {
            /// <summary>
            /// Gets or sets the location.
            /// </summary>
            /// <value>The location.</value>
            public LocationInfo Location { get; set; }
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public GetLocationStatus Status { get; set; } = GetLocationStatus.Unknown;
        }

        /// <summary>
        /// Enum GetLocationStatus
        /// </summary>
        public enum GetLocationStatus
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
            /// Not available
            /// </summary>
            NotAvailable,
            /// <summary>
            /// Insufficient permission
            /// </summary>
            InsufficientPermission,
            /// <summary>
            /// Unsupported platform
            /// </summary>
            UnsupportedPlatform
        }
    }
}
