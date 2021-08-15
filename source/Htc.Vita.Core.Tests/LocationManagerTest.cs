using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class LocationManagerTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var locationManager = LocationManager.GetInstance();
            Assert.NotNull(locationManager);
        }

        [Fact]
        public static void Default_1_GetLocation()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var locationManager = LocationManager.GetInstance();
            var getLocationResult = locationManager.GetLocation();
            var getLocationStatus = getLocationResult.Status;
            Assert.Equal(LocationManager.GetLocationStatus.Ok, getLocationStatus);
            var location = getLocationResult.Location;
            Logger.GetInstance(typeof(LocationManagerTest)).Info($"Location.CountryCodeAlpha2: {location.CountryCodeAlpha2}");
            Logger.GetInstance(typeof(LocationManagerTest)).Info($"Location.ProviderName: {location.ProviderName}");
            Logger.GetInstance(typeof(LocationManagerTest)).Info($"Location.ProviderType: {location.ProviderType}");
        }
    }
}
