namespace Htc.Vita.Core.Diagnostics
{
    public partial class LocationManager
    {
        public class GetLocationResult
        {
            public LocationInfo Location { get; set; }
            public GetLocationStatus Status { get; set; } = GetLocationStatus.Unknown;
        }

        public enum GetLocationStatus
        {
            Unknown,
            Ok,
            NotAvailable,
            InsufficientPermission,
            UnsupportedPlatform
        }
    }
}
