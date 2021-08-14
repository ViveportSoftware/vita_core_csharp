using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Diagnostics
{
    /// <summary>
    /// Class DefaultLocationManager.
    /// Implements the <see cref="LocationManager" />
    /// </summary>
    /// <seealso cref="LocationManager" />
    public class DefaultLocationManager : LocationManager
    {
        /// <summary>
        /// Gets the location via Windows 7 API.
        /// </summary>
        /// <returns>GetLocationResult.</returns>
        protected GetLocationResult GetLocationViaWindows7Api()
        {
            var reportType = Windows.LocationReportType.ICivicAddressReport;
            using (var location = Windows.Location.GetInstance())
            {
                var success = location.RequestPermission(reportType);
                if (!success)
                {
                    return new GetLocationResult
                    {
                            Status = GetLocationStatus.InsufficientPermission
                    };
                }

                using (var locationReport = location.GetReport(reportType))
                {
                    if (locationReport == null)
                    {
                        return new GetLocationResult
                        {
                                Status = GetLocationStatus.NotAvailable
                        };
                    }

                    var countryRegion = locationReport.GetCountryRegion();
                    if (string.IsNullOrWhiteSpace(countryRegion))
                    {
                        return new GetLocationResult
                        {
                                Status = GetLocationStatus.NotAvailable
                        };
                    }

                    if (countryRegion.Length != 2)
                    {
                        Logger.GetInstance(typeof(DefaultLocationManager)).Error($"Can not parse country code: {countryRegion}");
                        return new GetLocationResult
                        {
                                Status = GetLocationStatus.NotAvailable
                        };
                    }

                    return new GetLocationResult
                    {
                            Location = new LocationInfo
                            {
                                    CountryCodeAlpha2 = countryRegion
                            },
                            Status = GetLocationStatus.Ok
                    };
                }
            }
        }

        /// <inheritdoc />
        protected override GetLocationResult OnGetLocation()
        {
            return GetLocationViaWindows7Api();
        }
    }
}
