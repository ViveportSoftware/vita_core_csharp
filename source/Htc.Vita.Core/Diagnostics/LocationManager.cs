using System;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Diagnostics
{
    /// <summary>
    /// Class LocationManager.
    /// </summary>
    public abstract partial class LocationManager
    {
        static LocationManager()
        {
            TypeRegistry.RegisterDefault<LocationManager, DefaultLocationManager>();
        }

        /// <summary>
        /// Registers this instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : LocationManager, new()
        {
            TypeRegistry.Register<LocationManager, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>LocationManager.</returns>
        public static LocationManager GetInstance()
        {
            return TypeRegistry.GetInstance<LocationManager>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>LocationManager.</returns>
        public static LocationManager GetInstance<T>()
                where T : LocationManager, new()
        {
            return TypeRegistry.GetInstance<LocationManager, T>();
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <returns>GetLocationResult.</returns>
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

        /// <summary>
        /// Called when getting location.
        /// </summary>
        /// <returns>GetLocationResult.</returns>
        protected abstract GetLocationResult OnGetLocation();
    }
}
