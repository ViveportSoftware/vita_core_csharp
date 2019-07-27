﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public static partial class UsbManager
    {
        internal static class Windows
        {
            internal static List<DeviceInfo> GetHidDevicesInPlatform()
            {
                var deviceInfos = new List<DeviceInfo>();
                var classGuid = GetHidGuid();
                using (var deviceInfoSetHandle = Interop.Windows.SetupDiGetClassDevsW(
                        ref classGuid,
                        null,
                        IntPtr.Zero,
                        Interop.Windows.DeviceInfoGetClassFlag.Present | Interop.Windows.DeviceInfoGetClassFlag.DeviceInterface
                ))
                {
                    if (deviceInfoSetHandle == null || deviceInfoSetHandle.IsInvalid)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not find USB HID devices, error code: {Marshal.GetLastWin32Error()}");
                        return deviceInfos;
                    }

                    var deviceIndex = 0U;
                    while (true)
                    {
                        var deviceInterfaceData = new Interop.Windows.SetupDeviceInterfaceData();
                        deviceInterfaceData.cbSize = (uint)Marshal.SizeOf(deviceInterfaceData);
                        var success = Interop.Windows.SetupDiEnumDeviceInterfaces(
                                deviceInfoSetHandle,
                                IntPtr.Zero,
                                ref classGuid,
                                deviceIndex,
                                ref deviceInterfaceData
                        );
                        if (!success)
                        {
                            var win32Error = Marshal.GetLastWin32Error();
                            if (win32Error != (int)Interop.Windows.Error.NoMoreItems)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not enumerate USB HID device on index: {deviceIndex}, error code: {win32Error}");
                            }
                            break;
                        }

                        var bufferSize = 0;
                        success = Interop.Windows.SetupDiGetDeviceInterfaceDetailW(
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
                            if (win32Error != (int)Interop.Windows.Error.InsufficientBuffer)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not query USB HID device interface detail on index: {deviceIndex}, error code: {win32Error}");
                                break;
                            }
                        }

                        var deviceInterfaceDetailData = Marshal.AllocHGlobal(bufferSize);
                        Marshal.WriteInt32(deviceInterfaceDetailData, IntPtr.Size == 8 ? 8 : 6);
                        var deviceInfoData = new Interop.Windows.SetupDeviceInfoData();
                        deviceInfoData.cbSize = (uint)Marshal.SizeOf(deviceInfoData);
                        success = Interop.Windows.SetupDiGetDeviceInterfaceDetailW(
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
                            Logger.GetInstance(typeof(Windows)).Error($"Can not get USB HID device interface detail on index: {deviceIndex}, error code: {win32Error}");
                            break;
                        }

                        var devicePath = Marshal.PtrToStringUni(deviceInterfaceDetailData + 4);
                        Marshal.FreeHGlobal(deviceInterfaceDetailData);

                        var deviceInfo = new DeviceInfo
                        {
                                Path = devicePath,
                                Description = GetUsbDeviceStringProperty(
                                        deviceInfoSetHandle,
                                        ref deviceInfoData,
                                        Interop.Windows.SetupDeviceRegistryProperty.DeviceDesc
                                ),
                                Manufacturer = GetUsbDeviceStringProperty(
                                        deviceInfoSetHandle,
                                        ref deviceInfoData,
                                        Interop.Windows.SetupDeviceRegistryProperty.Mfg
                                ),
                                Product = GetHidDeviceProduct(devicePath),
                                SerialNumber = GetHidDeviceSerialNumber(devicePath)
                        };
                        var hardwareIds = GetUsbDeviceMultiStringProperty(
                                deviceInfoSetHandle,
                                ref deviceInfoData,
                                Interop.Windows.SetupDeviceRegistryProperty.HardwareId
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

                        if (!string.IsNullOrWhiteSpace(deviceInfo.VendorId) && !string.IsNullOrWhiteSpace(deviceInfo.ProductId))
                        {
                            deviceInfos.Add(deviceInfo);
                        }

                        deviceIndex++;
                    }

                    return deviceInfos;
                }
            }

            private static string GetHidDeviceProduct(string devicePath)
            {
                if (string.IsNullOrWhiteSpace(devicePath))
                {
                    return string.Empty;
                }

                var result = string.Empty;
                using (var deviceHandle = Interop.Windows.CreateFileW(
                        devicePath,
                        Interop.Windows.GenericAccessRight.Read | Interop.Windows.GenericAccessRight.Write,
                        Interop.Windows.FileShare.Read | Interop.Windows.FileShare.Write,
                        IntPtr.Zero,
                        Interop.Windows.FileCreationDisposition.OpenExisting,
                        Interop.Windows.FileAttributeFlag.FlagOverlapped,
                        IntPtr.Zero
                ))
                {
                    if (deviceHandle == null || deviceHandle.IsInvalid)
                    {
                        return result;
                    }

                    SpinWait.SpinUntil(() => false, 1);

                    var buffer = new StringBuilder(126);
                    var success = Interop.Windows.HidD_GetProductString(
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
                        if (win32Error == (int)Interop.Windows.Error.FileNotFound)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"The file on the device \"{devicePath}\" is not found");
                        }
                        if (win32Error == (int)Interop.Windows.Error.GenFailure)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"The device \"{devicePath}\" is not functioning");
                        }
                        else if (win32Error == (int)Interop.Windows.Error.NotSupported)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"The device \"{devicePath}\" does not support to get product");
                        }
                        else if (win32Error == (int)Interop.Windows.Error.InvalidParameter)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"Can not get USB HID product with invalid parameter on the device \"{devicePath}\"");
                        }
                        else if (win32Error == (int)Interop.Windows.Error.DeviceNotConnected)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"The device \"{devicePath}\" is not connected");
                        }
                        else
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not get USB HID product on the device \"{devicePath}\", error code: {win32Error}");
                        }
                    }
                    return result;
                }
            }

            private static string GetHidDeviceSerialNumber(string devicePath)
            {
                if (string.IsNullOrWhiteSpace(devicePath))
                {
                    return string.Empty;
                }

                var result = string.Empty;
                using (var deviceHandle = Interop.Windows.CreateFileW(
                        devicePath,
                        Interop.Windows.GenericAccessRight.Read | Interop.Windows.GenericAccessRight.Write,
                        Interop.Windows.FileShare.Read | Interop.Windows.FileShare.Write,
                        IntPtr.Zero,
                        Interop.Windows.FileCreationDisposition.OpenExisting,
                        Interop.Windows.FileAttributeFlag.FlagOverlapped,
                        IntPtr.Zero
                ))
                {
                    if (deviceHandle == null || deviceHandle.IsInvalid)
                    {
                        return result;
                    }

                    SpinWait.SpinUntil(() => false, 1);

                    var buffer = new StringBuilder(126);
                    var success = Interop.Windows.HidD_GetSerialNumberString(
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
                        if (win32Error == (int)Interop.Windows.Error.FileNotFound)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"The file on the device \"{devicePath}\" is not found");
                        }
                        if (win32Error == (int)Interop.Windows.Error.GenFailure)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"The device \"{devicePath}\" is not functioning");
                        }
                        else if (win32Error == (int)Interop.Windows.Error.NotSupported)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"The device \"{devicePath}\" does not support to get serial number");
                        }
                        else if (win32Error == (int)Interop.Windows.Error.InvalidParameter)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"Can not get USB HID serial number with invalid parameter on the device \"{devicePath}\"");
                        }
                        else if (win32Error == (int)Interop.Windows.Error.DeviceNotConnected)
                        {
                            Logger.GetInstance(typeof(Windows)).Debug($"The device \"{devicePath}\" is not connected");
                        }
                        else
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not get USB HID serial number on the device \"{devicePath}\", error code: {win32Error}");
                        }
                    }
                    return result;
                }
            }

            internal static byte[] GetHidFeatureReportInPlatform(string devicePath, byte reportId)
            {
                if (string.IsNullOrWhiteSpace(devicePath))
                {
                    return null;
                }

                using (var deviceHandle = Interop.Windows.CreateFileW(
                        devicePath,
                        Interop.Windows.GenericAccessRight.Read | Interop.Windows.GenericAccessRight.Write,
                        Interop.Windows.FileShare.Read | Interop.Windows.FileShare.Write,
                        IntPtr.Zero,
                        Interop.Windows.FileCreationDisposition.OpenExisting,
                        Interop.Windows.FileAttributeFlag.FlagOverlapped,
                        IntPtr.Zero
                ))
                {
                    if (deviceHandle == null || deviceHandle.IsInvalid)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not get valid device handle for path: {devicePath}");
                        return null;
                    }

                    var preparsedData = new IntPtr();
                    var success = Interop.Windows.HidD_GetPreparsedData(deviceHandle, ref preparsedData);
                    if (!success)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not get valid preparsed data for path: {devicePath}");
                        return null;
                    }

                    var hidDeviceCapability = new Interop.Windows.HidDeviceCapability();
                    var ntStatus = Interop.Windows.HidP_GetCaps(preparsedData, ref hidDeviceCapability);
                    if (ntStatus != Interop.Windows.NtStatus.HidpStatusSuccess)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not get device capability for path: {devicePath}, status: {ntStatus}");
                        return null;
                    }

                    var featureReportByteLength = hidDeviceCapability.featureReportByteLength;
                    if (featureReportByteLength <= 0)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not get valid feature report length for path: {devicePath}");
                        return null;
                    }

                    var result = new byte[featureReportByteLength];
                    result[0] = reportId;
                    success = Interop.Windows.HidD_GetFeature(
                            deviceHandle,
                            result,
                            featureReportByteLength
                    );
                    if (!success)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not get valid feature report for path: {devicePath}");
                        return null;
                    }

                    success = Interop.Windows.HidD_FreePreparsedData(preparsedData);
                    if (!success)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not free preparsed data for path: {devicePath}");
                    }
                    return result;
                }
            }

            private static Guid GetHidGuid()
            {
                var guid = Guid.Empty;
                Interop.Windows.HidD_GetHidGuid(ref guid);
                if (!Guid.Empty.ToString().Equals(guid.ToString()))
                {
                    return guid;
                }
                Logger.GetInstance(typeof(Windows)).Warn("HID Guid is empty. Use default HID Guid");
                return Interop.Windows.DeviceInterfaceHid;
            }

            internal static List<DeviceInfo> GetUsbDevicesInPlatform()
            {
                var deviceInfos = new List<DeviceInfo>();
                var classGuid = GetUsbGuid();
                using (var deviceInfoSetHandle = Interop.Windows.SetupDiGetClassDevsW(
                        ref classGuid,
                        null,
                        IntPtr.Zero,
                        Interop.Windows.DeviceInfoGetClassFlag.Present | Interop.Windows.DeviceInfoGetClassFlag.DeviceInterface
                ))
                {
                    if (deviceInfoSetHandle == null || deviceInfoSetHandle.IsInvalid)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not find USB devices, error code: {Marshal.GetLastWin32Error()}");
                        return deviceInfos;
                    }

                    var deviceIndex = 0U;
                    while (true)
                    {
                        var deviceInterfaceData = new Interop.Windows.SetupDeviceInterfaceData();
                        deviceInterfaceData.cbSize = (uint)Marshal.SizeOf(deviceInterfaceData);
                        var success = Interop.Windows.SetupDiEnumDeviceInterfaces(
                                deviceInfoSetHandle,
                                IntPtr.Zero,
                                ref classGuid,
                                deviceIndex,
                                ref deviceInterfaceData
                        );
                        if (!success)
                        {
                            var win32Error = Marshal.GetLastWin32Error();
                            if (win32Error != (int)Interop.Windows.Error.NoMoreItems)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not enumerate USB device on index: {deviceIndex}, error code: {win32Error}");
                            }
                            break;
                        }

                        var bufferSize = 0;
                        success = Interop.Windows.SetupDiGetDeviceInterfaceDetailW(
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
                            if (win32Error != (int)Interop.Windows.Error.InsufficientBuffer)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not query USB device interface detail on index: {deviceIndex}, error code: {win32Error}");
                                break;
                            }
                        }

                        var deviceInterfaceDetailData = Marshal.AllocHGlobal(bufferSize);
                        Marshal.WriteInt32(deviceInterfaceDetailData, IntPtr.Size == 8 ? 8 : 6);
                        var deviceInfoData = new Interop.Windows.SetupDeviceInfoData();
                        deviceInfoData.cbSize = (uint)Marshal.SizeOf(deviceInfoData);
                        success = Interop.Windows.SetupDiGetDeviceInterfaceDetailW(
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
                            Logger.GetInstance(typeof(Windows)).Error($"Can not get USB device interface detail on index: {deviceIndex}, error code: {win32Error}");
                            break;
                        }

                        var devicePath = Marshal.PtrToStringUni(deviceInterfaceDetailData + 4);
                        Marshal.FreeHGlobal(deviceInterfaceDetailData);

                        var deviceInfo = new DeviceInfo
                        {
                                Path = devicePath,
                                Description = GetUsbDeviceStringProperty(
                                        deviceInfoSetHandle,
                                        ref deviceInfoData,
                                        Interop.Windows.SetupDeviceRegistryProperty.DeviceDesc
                                ),
                                Manufacturer = GetUsbDeviceStringProperty(
                                        deviceInfoSetHandle,
                                        ref deviceInfoData,
                                        Interop.Windows.SetupDeviceRegistryProperty.Mfg
                                ),
                                SerialNumber = GetHidDeviceSerialNumber(devicePath)
                        };
                        var hardwareIds = GetUsbDeviceMultiStringProperty(
                                deviceInfoSetHandle,
                                ref deviceInfoData,
                                Interop.Windows.SetupDeviceRegistryProperty.HardwareId
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

                        if (!string.IsNullOrWhiteSpace(deviceInfo.VendorId) && !string.IsNullOrWhiteSpace(deviceInfo.ProductId))
                        {
                            deviceInfos.Add(deviceInfo);
                        }

                        deviceIndex++;
                    }

                    return deviceInfos;
                }
            }

            private static string[] GetUsbDeviceMultiStringProperty(
                    Interop.Windows.SafeDevInfoSetHandle deviceInfoSetHandle,
                    ref Interop.Windows.SetupDeviceInfoData deviceInfoData,
                    Interop.Windows.SetupDeviceRegistryProperty property)
            {
                var regType = Interop.Windows.RegType.None;
                var bytes = GetUsbDeviceProperty(
                        deviceInfoSetHandle,
                        ref deviceInfoData,
                        property,
                        ref regType
                );
                if (bytes == null || bytes.Length == 0 || regType != Interop.Windows.RegType.MultiSz)
                {
                    return new string[] { };
                }
                return Encoding.Unicode.GetString(bytes).Split(
                        new[] { '\0' },
                        StringSplitOptions.RemoveEmptyEntries
                );
            }

            private static byte[] GetUsbDeviceProperty(
                    Interop.Windows.SafeDevInfoSetHandle deviceInfoSetHandle,
                    ref Interop.Windows.SetupDeviceInfoData deviceInfoData,
                    Interop.Windows.SetupDeviceRegistryProperty property,
                    ref Interop.Windows.RegType regType)
            {
                var requiredSize = 0U;
                var success = Interop.Windows.SetupDiGetDeviceRegistryPropertyW(
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
                    if (win32Error == (int)Interop.Windows.Error.InvalidData)
                    {
                        Logger.GetInstance(typeof(Windows)).Debug($"The requested property {property} does not exist");
                        regType = 0;
                        return null;
                    }
                    if (win32Error != (int)Interop.Windows.Error.InsufficientBuffer)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not query USB device registry property, error code: {win32Error}");
                        regType = 0;
                        return null;
                    }
                }

                var propertyBuffer = new byte[requiredSize];
                success = Interop.Windows.SetupDiGetDeviceRegistryPropertyW(
                        deviceInfoSetHandle,
                        ref deviceInfoData,
                        property,
                        ref regType,
                        propertyBuffer,
                        (uint)propertyBuffer.Length,
                        ref requiredSize
                );
                if (!success)
                {
                    var win32Error = Marshal.GetLastWin32Error();
                    Logger.GetInstance(typeof(Windows)).Error($"Can not get USB device registry property, error code: {win32Error}");
                    return null;
                }
                return propertyBuffer;
            }

            private static string GetUsbDeviceStringProperty(
                    Interop.Windows.SafeDevInfoSetHandle deviceInfoSetHandle,
                    ref Interop.Windows.SetupDeviceInfoData deviceInfoData,
                    Interop.Windows.SetupDeviceRegistryProperty property)
            {
                var regType = Interop.Windows.RegType.None;
                var bytes = GetUsbDeviceProperty(
                        deviceInfoSetHandle,
                        ref deviceInfoData,
                        property,
                        ref regType
                );
                if (bytes == null || bytes.Length == 0 || regType != Interop.Windows.RegType.Sz)
                {
                    return string.Empty;
                }
                return Encoding.Unicode.GetString(bytes, 0, bytes.Length - sizeof(char)).Trim();
            }

            private static Guid GetUsbGuid()
            {
                return Interop.Windows.DeviceInterfaceUsbDevice;
            }
        }
    }
}
