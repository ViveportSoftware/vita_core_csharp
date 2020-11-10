using System.Collections.Generic;
using System.Text.RegularExpressions;

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
                    // \\?\usb#vid_2833&pid_0182&mi_00#6&1388f94e&0&0000#{6ac27878-a6fa-4155-ba85-f98f491d4f33}
                    var regex = new Regex("^(\\\\\\\\\\?\\\\\\w{3})\\#VID_([0-9A-F]{4})&PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
                    foreach (var deviceId in deviceList)
                    {
                        if (string.IsNullOrWhiteSpace(deviceId))
                        {
                            continue;
                        }

                        var type = string.Empty;
                        var vid = string.Empty;
                        var pid = string.Empty;
                        var match = regex.Match(deviceId);
                        if (match.Success)
                        {
                            type = match.Groups[1].Value;
                            vid = match.Groups[2].Value;
                            pid = match.Groups[3].Value;
                        }

                        var deviceInfo = new DeviceInfo
                        {
                                Path = deviceId,
                                Description = portableDeviceManager.GetDeviceDescription(deviceId),
                                Product = portableDeviceManager.GetDeviceFriendlyName(deviceId),
                                Manufacturer = portableDeviceManager.GetDeviceManufacturer(deviceId),
                                ProductId = pid,
                                VendorId = vid
                        };
                        if (!string.IsNullOrWhiteSpace(type))
                        {
                            deviceInfo.Optional["type"] = type;
                        }

                        result.Add(deviceInfo);
                    }
                }

                return result;
            }
        }
    }
}
