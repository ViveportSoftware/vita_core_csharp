using System.Collections.Generic;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class WpdManager.
    /// </summary>
    public static partial class WpdManager
    {
        private static readonly object InstancesLock = new object();

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <returns>List&lt;DeviceInfo&gt;.</returns>
        public static List<DeviceInfo> GetDevices()
        {
            if (!Platform.IsWindows)
            {
                return new List<DeviceInfo>();
            }

            lock (InstancesLock)
            {
                return Windows.GetDevicesInPlatform();
            }
        }
    }
}
