namespace Htc.Vita.Core.Diagnostics
{
    public partial class LocationManager
    {
        /// <summary>
        /// Class LocationInfo.
        /// </summary>
        public class LocationInfo
        {
            /// <summary>
            /// Gets or sets the country code alpha2.
            /// </summary>
            /// <value>The country code alpha2.</value>
            public string CountryCodeAlpha2 { get; set; }
            /// <summary>
            /// Gets or sets the provider name.
            /// </summary>
            /// <value>The provider name.</value>
            public string ProviderName { get; set; }
            /// <summary>
            /// Gets or sets the provider type.
            /// </summary>
            /// <value>The provider type.</value>
            public LocationProviderType ProviderType { get; set; } = LocationProviderType.Unknown;
        }

        /// <summary>
        /// Enum LocationProviderType
        /// </summary>
        public enum LocationProviderType
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// Provided by operating system
            /// </summary>
            OperatingSystem,
            /// <summary>
            /// Provided by network
            /// </summary>
            Network,
            /// <summary>
            /// Provided by user
            /// </summary>
            User
        }
    }
}
