using System.Collections.Generic;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.IO
{
    public static partial class UsbManager
    {
        private static readonly object InstancesLock = new object();

        public static List<DeviceInfo> GetHidDevices()
        {
            if (!Platform.IsWindows)
            {
                return new List<DeviceInfo>();
            }

            lock (InstancesLock)
            {
                return Windows.GetHidDevicesInPlatform();
            }
        }

        public static byte[] GetHidFeatureReport(string devicePath, byte reportId)
        {
            if (!Platform.IsWindows)
            {
                return null;
            }

            lock (InstancesLock)
            {
                return Windows.GetHidFeatureReportInPlatform(devicePath, reportId);
            }
        }

        public static List<DeviceInfo> GetUsbDevices()
        {
            if (!Platform.IsWindows)
            {
                return new List<DeviceInfo>();
            }

            lock (InstancesLock)
            {
                return Windows.GetUsbDevicesInPlatform();
            }
        }
    }
}
