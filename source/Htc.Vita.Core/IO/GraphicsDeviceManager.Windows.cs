using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public static partial class GraphicsDeviceManager
    {
        internal static class Windows
        {
            internal static List<GraphicsAdapterInfo> GetGraphicsAdapterListInPlatform()
            {
                var result = new List<GraphicsAdapterInfo>();

                using (var dxgiFactory = Interop.Windows.DxgiFactory.GetInstance())
                {
                    if (dxgiFactory == null)
                    {
                        return result;
                    }

                    foreach (var dxgiAdapter in dxgiFactory.EnumerateAdapters())
                    {
                        if (dxgiAdapter == null)
                        {
                            continue;
                        }

                        using (dxgiAdapter)
                        {
                            var dxgiAdapterDescription = dxgiAdapter.GetDescription();
                            var graphicsAdapterInfo = new GraphicsAdapterInfo
                            {
                                    Description = dxgiAdapterDescription.description,
                                    DeviceId = ParseDeviceId(dxgiAdapterDescription.deviceId),
                                    RevisionId = ParseRevisionId(dxgiAdapterDescription.revision),
                                    SubsystemDeviceId = ParseSubsystemDeviceId(dxgiAdapterDescription.subSysId),
                                    SubsystemVendorId = ParseSubsystemVendorId(dxgiAdapterDescription.subSysId),
                                    VendorId = ParseVendorId(dxgiAdapterDescription.vendorId)
                            };

                            foreach (var dxgiOutput in dxgiAdapter.EnumerateOutputs())
                            {
                                if (dxgiOutput == null)
                                {
                                    continue;
                                }

                                using (dxgiOutput)
                                {
                                    var dxgiOutputDescription = dxgiOutput.GetDescription();
                                    var graphicsDisplayInfo = new GraphicsDisplayInfo
                                    {
                                            Name = dxgiOutputDescription.deviceName
                                    };

                                    var monitorInfo = new Interop.Windows.MonitorInfoEx();
                                    monitorInfo.size = Marshal.SizeOf(monitorInfo);
                                    var success = Interop.Windows.GetMonitorInfoW(
                                            dxgiOutputDescription.monitor,
                                            ref monitorInfo
                                    );
                                    if (!success)
                                    {
                                        Logger.GetInstance(typeof(Windows)).Error("Can not get monitor info. error: " + Marshal.GetLastWin32Error());
                                        continue;
                                    }

                                    var displayDevice = new Interop.Windows.DisplayDevice();
                                    displayDevice.cb = Marshal.SizeOf(displayDevice);

                                    try
                                    {
                                        var enumDisplayDeviceFlags = Interop.Windows.EnumDisplayDeviceFlags.None;
                                        for (uint adapterId = 0; Interop.Windows.EnumDisplayDevicesW(null, adapterId, ref displayDevice, enumDisplayDeviceFlags); adapterId++)
                                        {
                                            var adapterDeviceName = displayDevice.deviceName;
                                            if (string.IsNullOrWhiteSpace(adapterDeviceName))
                                            {
                                                continue;
                                            }

                                            if (!adapterDeviceName.Equals(monitorInfo.DeviceName))
                                            {
                                                continue;
                                            }

                                            for (uint monitorId = 0; Interop.Windows.EnumDisplayDevicesW(adapterDeviceName, monitorId, ref displayDevice, enumDisplayDeviceFlags); monitorId++)
                                            {
                                                graphicsDisplayInfo.MonitorList.Add(new GraphicsMonitorInfo
                                                {
                                                        Name = displayDevice.deviceName,
                                                        Description = displayDevice.deviceString,
                                                        DeviceId = displayDevice.deviceId,
                                                        DeviceKey = displayDevice.deviceKey
                                                });
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Logger.GetInstance(typeof(Windows)).Error($"{e}");
                                    }

                                    graphicsAdapterInfo.DisplayList.Add(graphicsDisplayInfo);
                                }
                            }

                            result.Add(graphicsAdapterInfo);
                        }
                    }
                }

                return result;
            }

            private static string ParseDeviceId(uint deviceId)
            {
                return deviceId.ToString("X4");
            }

            private static string ParseRevisionId(uint revisionId)
            {
                return revisionId.ToString("X2");
            }

            private static string ParseSubsystemDeviceId(uint subsystemId)
            {
                return subsystemId.ToString("X8").Substring(4, 4);
            }

            private static string ParseSubsystemVendorId(uint subsystemId)
            {
                return subsystemId.ToString("X8").Substring(0, 4);
            }

            private static string ParseVendorId(uint vendorId)
            {
                return vendorId.ToString("X4");
            }
        }
    }
}
