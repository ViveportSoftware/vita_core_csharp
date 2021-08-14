using System;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Diagnostics
{
    public abstract partial class LocationManager
    {
        static LocationManager()
        {
            TypeRegistry.RegisterDefault<LocationManager, DefaultLocationManager>();
        }

        public static void Register<T>()
                where T : LocationManager, new()
        {
            TypeRegistry.Register<LocationManager, T>();
        }

        public static LocationManager GetInstance()
        {
            return TypeRegistry.GetInstance<LocationManager>();
        }

        public static LocationManager GetInstance<T>()
                where T : LocationManager, new()
        {
            return TypeRegistry.GetInstance<LocationManager, T>();
        }

        public GetLocationResult GetLocation()
        {
            GetLocationResult result = null;
            try
            {
                result = OnGetLocation();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Logger.GetInstance(typeof(LocationManager)).Error(e.ToString());
            }
            return result ?? new GetLocationResult();
        }

        protected abstract GetLocationResult OnGetLocation();
    }
}
