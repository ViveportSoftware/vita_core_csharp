using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public static partial class UsbManager
    {
        public static List<DeviceInfo> GetHidDevices()
        {
            return GetHidDevicesInWindows();
        }

        private static List<DeviceInfo> GetHidDevicesInWindows()
        {
            var deviceInfos = new List<DeviceInfo>();
            var classGuid = GetHidGuidInWindows();
            using (var deviceInfoSetHandle = Windows.SetupDiGetClassDevsW(
                    ref classGuid,
                    null,
                    IntPtr.Zero,
                    Windows.DeviceInfoGetClassFlag.Present | Windows.DeviceInfoGetClassFlag.DeviceInterface
            ))
            {
                if (deviceInfoSetHandle == null || deviceInfoSetHandle.IsInvalid)
                {
                    Logger.GetInstance(typeof(UsbManager)).Error("Can not find USB HID devices, error code: " + Marshal.GetLastWin32Error());
                    return deviceInfos;
                }

                var deviceIndex = 0U;
                while (true)
                {
                    var deviceInterfaceData = new Windows.SetupDeviceInterfaceData();
                    deviceInterfaceData.cbSize = (uint)Marshal.SizeOf(deviceInterfaceData);
                    var success = Windows.SetupDiEnumDeviceInterfaces(
                            deviceInfoSetHandle,
                            IntPtr.Zero,
                            ref classGuid,
                            deviceIndex,
                            ref deviceInterfaceData
                    );
                    if (!success)
                    {
                        var win32Error = Marshal.GetLastWin32Error();
                        if (win32Error != (int)Windows.Error.NoMoreItems)
                        {
                            Logger.GetInstance(typeof(UsbManager)).Error("Can not enumerate USB HID device on index: " + deviceIndex + ", error code: " + win32Error);
                        }
                        break;
                    }

                    var bufferSize = 0;
                    success = Windows.SetupDiGetDeviceInterfaceDetailW(
                            deviceInfoSetHandle,
                            ref deviceInterfaceData,
                            IntPtr.Zero,
                            0,
                            ref bufferSize,
                            IntPtr.Zero
                    );
                    if (!success)
                    {
                        var win32Error = Marshal.GetLastWin32Error();
                        if (win32Error != (int)Windows.Error.InsufficientBuffer)
                        {
                            Logger.GetInstance(typeof(UsbManager)).Error("Can not query USB HID device interface detail on index: " + deviceIndex + ", error code: " + win32Error);
                            break;
                        }
                    }

                    var deviceInterfaceDetailData = Marshal.AllocHGlobal(bufferSize);
                    Marshal.WriteInt32(deviceInterfaceDetailData, IntPtr.Size == 8 ? 8 : 6);
                    var deviceInfoData = new Windows.SetupDeviceInfoData();
                    deviceInfoData.cbSize = (uint)Marshal.SizeOf(deviceInfoData);
                    success = Windows.SetupDiGetDeviceInterfaceDetailW(
                            deviceInfoSetHandle,
                            ref deviceInterfaceData,
                            deviceInterfaceDetailData,
                            bufferSize,
                            ref bufferSize,
                            ref deviceInfoData
                    );
                    if (!success)
                    {
                        var win32Error = Marshal.GetLastWin32Error();
                        Logger.GetInstance(typeof(UsbManager)).Error("Can not get USB HID device interface detail on index: " + deviceIndex + ", error code: " + win32Error);
                        break;
                    }

                    var devicePath = Marshal.PtrToStringUni(deviceInterfaceDetailData + 4);
                    Marshal.FreeHGlobal(deviceInterfaceDetailData);

                    var deviceInfo = new DeviceInfo
                    {
                        Path = devicePath,
                        Description = GetUsbDeviceStringPropertyInWindows(
                                    deviceInfoSetHandle,
                                    ref deviceInfoData,
                                    Windows.SetupDeviceRegistryProperty.DeviceDesc
                            ),
                        Manufacturer = GetUsbDeviceStringPropertyInWindows(
                                    deviceInfoSetHandle,
                                    ref deviceInfoData,
                                    Windows.SetupDeviceRegistryProperty.Mfg
                            ),
                        SerialNumber = GetHidDeviceSerialNumberInWindows(devicePath)
                    };
                    var hardwareIds = GetUsbDeviceMultiStringPropertyInWindows(
                            deviceInfoSetHandle,
                            ref deviceInfoData,
                            Windows.SetupDeviceRegistryProperty.HardwareId
                    );
                    var regex = new Regex("^(\\w{3})\\\\VID_([0-9A-F]{4})&PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
                    foreach (var hardwareId in hardwareIds)
                    {
                        var match = regex.Match(hardwareId);
                        if (!match.Success)
                        {
                            continue;
                        }
                        deviceInfo.Optional["type"] = match.Groups[1].Value;
                        deviceInfo.VendorId = match.Groups[2].Value;
                        deviceInfo.ProductId = match.Groups[3].Value;
                        break;
                    }

                    if (string.IsNullOrWhiteSpace(deviceInfo.VendorId) || string.IsNullOrWhiteSpace(deviceInfo.ProductId))
                    {
                        continue;
                    }

                    deviceInfos.Add(deviceInfo);

                    deviceIndex++;
                }

                return deviceInfos;
            }
        }

        private static Guid GetHidGuidInWindows()
        {
            var guid = Guid.Empty;
            Windows.HidD_GetHidGuid(ref guid);
            if (!Guid.Empty.ToString().Equals(guid.ToString()))
            {
                return guid;
            }
            Logger.GetInstance(typeof(UsbManager)).Warn("HID Guid is empty. Use default HID Guid");
            return new Guid("4d1e55b2-f16f-11cf-88cb-001111000030"); // default HID GUID
        }

        public static byte[] GetHidFeatureReport(string devicePath, byte reportId)
        {
            return GetHidFeatureReportInWindows(devicePath, reportId);
        }

        private static byte[] GetHidFeatureReportInWindows(string devicePath, byte reportId)
        {
            if (string.IsNullOrWhiteSpace(devicePath))
            {
                return null;
            }

            using (var deviceHandle = Windows.CreateFileW(
                    devicePath,
                    Windows.Generic.Read | Windows.Generic.Write,
                    Windows.FileShare.Read | Windows.FileShare.Write,
                    IntPtr.Zero,
                    Windows.CreationDisposition.OpenExisting,
                    Windows.FileAttributeFlag.FlagOverlapped,
                    IntPtr.Zero
            ))
            {
                if (deviceHandle == null || deviceHandle.IsInvalid)
                {
                    Logger.GetInstance(typeof(UsbManager)).Error($"Can not get valid device handle for path: {devicePath}");
                    return null;
                }

                var preparsedData = new IntPtr();
                var success = Windows.HidD_GetPreparsedData(deviceHandle, ref preparsedData);
                if (!success)
                {
                    Logger.GetInstance(typeof(UsbManager)).Error($"Can not get valid preparsed data for path: {devicePath}");
                    return null;
                }

                var hidDeviceCapability = new Windows.HidDeviceCapability();
                var ntStatus = Windows.HidP_GetCaps(preparsedData, ref hidDeviceCapability);
                if (ntStatus != Windows.NtStatus.HidpStatusSuccess)
                {
                    Logger.GetInstance(typeof(UsbManager)).Error($"Can not get device capability for path: {devicePath}, status: {ntStatus}");
                    return null;
                }

                var featureReportByteLength = hidDeviceCapability.featureReportByteLength;
                if (featureReportByteLength <= 0)
                {
                    Logger.GetInstance(typeof(UsbManager)).Error($"Can not get valid feature report length for path: {devicePath}");
                    return null;
                }

                var result = new byte[featureReportByteLength];
                result[0] = reportId;
                success = Windows.HidD_GetFeature(
                        deviceHandle,
                        result,
                        featureReportByteLength
                );
                if (!success)
                {
                    Logger.GetInstance(typeof(UsbManager)).Error($"Can not get valid feature report for path: {devicePath}");
                    return null;
                }

                success = Windows.HidD_FreePreparsedData(preparsedData);
                if (!success)
                {
                    Logger.GetInstance(typeof(UsbManager)).Error($"Can not free preparsed data for path: {devicePath}");
                }
                return result;
            }
        }

        private static byte[] GetUsbDevicePropertyInWindows(
                Windows.SafeDevInfoSetHandle deviceInfoSetHandle,
                ref Windows.SetupDeviceInfoData deviceInfoData,
                Windows.SetupDeviceRegistryProperty property,
                ref Windows.RegType regType)
        {
            var requiredSize = 0U;
            var success = Windows.SetupDiGetDeviceRegistryPropertyW(
                    deviceInfoSetHandle,
                    ref deviceInfoData,
                    property,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    0,
                    ref requiredSize
            );
            if (!success)
            {
                var win32Error = Marshal.GetLastWin32Error();
                if (win32Error != (int) Windows.Error.InsufficientBuffer)
                {
                    Logger.GetInstance(typeof(UsbManager)).Error("Can not query USB device registry property, error code: " + win32Error);
                    regType = 0;
                    return null;
                }
            }

            var propertyBuffer = new byte[requiredSize];
            success = Windows.SetupDiGetDeviceRegistryPropertyW(
                    deviceInfoSetHandle,
                    ref deviceInfoData,
                    property,
                    ref regType,
                    propertyBuffer,
                    (uint) propertyBuffer.Length,
                    ref requiredSize
            );
            if (!success)
            {
                var win32Error = Marshal.GetLastWin32Error();
                Logger.GetInstance(typeof(UsbManager)).Error("Can not get USB device registry property, error code: " + win32Error);
                return null;
            }
            return propertyBuffer;
        }

        private static string GetUsbDeviceStringPropertyInWindows(
                Windows.SafeDevInfoSetHandle deviceInfoSetHandle,
                ref Windows.SetupDeviceInfoData deviceInfoData,
                Windows.SetupDeviceRegistryProperty property)
        {
            var regType = Windows.RegType.None;
            var bytes = GetUsbDevicePropertyInWindows(
                    deviceInfoSetHandle,
                    ref deviceInfoData,
                    property,
                    ref regType
            );
            if (bytes == null || bytes.Length == 0 || regType != Windows.RegType.Sz)
            {
                return string.Empty;
            }
            return Encoding.Unicode.GetString(bytes, 0, bytes.Length - sizeof(char)).Trim();
        }

        private static string[] GetUsbDeviceMultiStringPropertyInWindows(
                Windows.SafeDevInfoSetHandle deviceInfoSetHandle,
                ref Windows.SetupDeviceInfoData deviceInfoData,
                Windows.SetupDeviceRegistryProperty property)
        {
            var regType = Windows.RegType.None;
            var bytes = GetUsbDevicePropertyInWindows(
                    deviceInfoSetHandle,
                    ref deviceInfoData,
                    property,
                    ref regType
            );
            if (bytes == null || bytes.Length == 0 || regType != Windows.RegType.MultiSz)
            {
                return new string[] {};
            }
            return Encoding.Unicode.GetString(bytes).Split(
                    (char[]) null,
                    StringSplitOptions.RemoveEmptyEntries
            );
        }

        private static string GetHidDeviceSerialNumberInWindows(string devicePath)
        {
            if (string.IsNullOrWhiteSpace(devicePath))
            {
                return string.Empty;
            }

            var result = string.Empty;
            using (var deviceHandle = Windows.CreateFileW(
                    devicePath,
                    Windows.Generic.Read | Windows.Generic.Write,
                    Windows.FileShare.Read | Windows.FileShare.Write,
                    IntPtr.Zero,
                    Windows.CreationDisposition.OpenExisting,
                    Windows.FileAttributeFlag.FlagOverlapped,
                    IntPtr.Zero
            ))
            {
                if (deviceHandle == null || deviceHandle.IsInvalid)
                {
                    return result;
                }

                var buffer = new StringBuilder(126);
                var success = Windows.HidD_GetSerialNumberString(
                        deviceHandle,
                        buffer,
                        (uint)buffer.Capacity
                );
                if (success)
                {
                    result = buffer.ToString();
                }
                else
                {
                    var win32Error = Marshal.GetLastWin32Error();
                    if (win32Error == (int)Windows.Error.NotSupported)
                    {
                        Logger.GetInstance(typeof(UsbManager)).Debug("The device \"" + devicePath + "\" does not support to get serial number");
                    }
                    else if (win32Error == (int)Windows.Error.InvalidParameter)
                    {
                        Logger.GetInstance(typeof(UsbManager)).Debug("Can not get USB HID serial number with invalid parameter on the device \"" + devicePath + "\"");
                    }
                    else
                    {
                        Logger.GetInstance(typeof(UsbManager)).Error("Can not get USB HID serial number on the device \"" + devicePath + "\", error code: " + win32Error);
                    }
                }
                return result;
            }
        }
    }
}
