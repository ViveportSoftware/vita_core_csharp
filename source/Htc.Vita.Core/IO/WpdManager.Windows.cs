using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    public static partial class WpdManager
    {
        internal static class Windows
        {
            internal static List<DeviceInfo> GetDevicesInPlatform()
            {
                var result = new List<DeviceInfo>();
                using (var portableDeviceManager = Interop.Windows.PortableDeviceManager.GetInstance())
                {
                    if (portableDeviceManager == null)
                    {
                        return result;
                    }

                    var deviceList = portableDeviceManager.GetDeviceList();
                    foreach (var deviceId in deviceList)
                    {
                        if (string.IsNullOrWhiteSpace(deviceId))
                        {
                            continue;
                        }

                        result.Add(new DeviceInfo
                        {
                                Path = deviceId,
                                Description = portableDeviceManager.GetDeviceDescription(deviceId),
                                Product = portableDeviceManager.GetDeviceFriendlyName(deviceId),
                                Manufacturer = portableDeviceManager.GetDeviceManufacturer(deviceId)
                        });
                    }
                }

                return result;
            }
        }
    }
}
