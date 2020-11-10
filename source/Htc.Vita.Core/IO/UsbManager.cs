using System.Collections.Generic;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class UsbManager.
    /// </summary>
    public static partial class UsbManager
    {
        private static readonly object InstancesLock = new object();

        /// <summary>
        /// Gets the HID devices.
        /// </summary>
        /// <returns>List&lt;DeviceInfo&gt;.</returns>
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

        /// <summary>
        /// Gets the HID feature report.
        /// </summary>
        /// <param name="devicePath">The device path.</param>
        /// <param name="reportId">The report identifier.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] GetHidFeatureReport(
                string devicePath,
                byte reportId)
        {
            if (!Platform.IsWindows)
            {
                return null;
            }

            lock (InstancesLock)
            {
                return Windows.GetHidFeatureReportInPlatform(
                        devicePath,
                        reportId
                );
            }
        }

        /// <summary>
        /// Gets the USB devices.
        /// </summary>
        /// <returns>List&lt;DeviceInfo&gt;.</returns>
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

        /// <summary>
        /// Gets the USB hub devices.
        /// </summary>
        /// <returns>List&lt;DeviceInfo&gt;.</returns>
        public static List<DeviceInfo> GetUsbHubDevices()
        {
            if (!Platform.IsWindows)
            {
                return new List<DeviceInfo>();
            }

            lock (InstancesLock)
            {
                return Windows.GetUsbHubDevicesInPlatform();
            }
        }
    }
}
