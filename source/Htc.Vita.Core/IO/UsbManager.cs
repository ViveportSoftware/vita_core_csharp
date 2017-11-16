﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public class UsbManager
    {
        private static readonly Logger Log = Logger.GetInstance(typeof(UsbManager));

        public static List<DeviceInfo> GetHidDevices()
        {
            return GetHidDevicesInWindows();
        }

        private static List<DeviceInfo> GetHidDevicesInWindows()
        {
            var deviceInfos = new List<DeviceInfo>();
            var classGuid = GetHidGuidInWindows();
            var deviceInfoSet = Windows.Setupapi.SetupDiGetClassDevsW(
                    ref classGuid,
                    null,
                    IntPtr.Zero,
                    Windows.Setupapi.DIGCF.DIGCF_PRESENT | Windows.Setupapi.DIGCF.DIGCF_DEVICEINTERFACE
            );
            if (deviceInfoSet == Windows.INVALID_HANDLE_VALUE)
            {
                Log.Error("Can not find USB HID devices, error code: " + Marshal.GetLastWin32Error());
                return deviceInfos;
            }

            var deviceIndex = 0;
            while (true)
            {
                var deviceInterfaceData = new Windows.Setupapi.SP_DEVICE_INTERFACE_DATA();
                deviceInterfaceData.cbSize = Marshal.SizeOf(deviceInterfaceData);
                var success = Windows.Setupapi.SetupDiEnumDeviceInterfaces(
                        deviceInfoSet,
                        IntPtr.Zero,
                        ref classGuid,
                        deviceIndex,
                        ref deviceInterfaceData
                );
                if (!success)
                {
                    var win32Error = Marshal.GetLastWin32Error();
                    if (win32Error != Windows.ERROR_NO_MORE_ITEMS)
                    {
                        Log.Error("Can not enumerate USB HID device on index: " + deviceIndex + ", error code: " + win32Error);
                    }
                    break;
                }

                var bufferSize = 0;
                success = Windows.Setupapi.SetupDiGetDeviceInterfaceDetailW(
                        deviceInfoSet,
                        ref deviceInterfaceData,
                        IntPtr.Zero, 
                        0,
                        ref bufferSize,
                        IntPtr.Zero
                );
                if (!success)
                {
                    var win32Error = Marshal.GetLastWin32Error();
                    if (win32Error != Windows.ERROR_INSUFFICIENT_BUFFER)
                    {
                        Log.Error("Can not query USB HID device interface detail on index: " + deviceIndex + ", error code: " + win32Error);
                        break;
                    }
                }

                var deviceInterfaceDetailData = Marshal.AllocHGlobal(bufferSize);
                Marshal.WriteInt32(deviceInterfaceDetailData, IntPtr.Size == 8 ? 8 : 6);
                var devinfoData = new Windows.Setupapi.SP_DEVINFO_DATA();
                devinfoData.cbSize = Marshal.SizeOf(devinfoData);
                success = Windows.Setupapi.SetupDiGetDeviceInterfaceDetailW(
                        deviceInfoSet,
                        ref deviceInterfaceData,
                        deviceInterfaceDetailData,
                        bufferSize,
                        ref bufferSize,
                        ref devinfoData
                );
                if (!success)
                {
                    var win32Error = Marshal.GetLastWin32Error();
                    Log.Error("Can not get USB HID device interface detail on index: " + deviceIndex + ", error code: " + win32Error);
                    break;
                }

                var devicePath = Marshal.PtrToStringUni(deviceInterfaceDetailData + 4);
                Marshal.FreeHGlobal(deviceInterfaceDetailData);

                var deviceInfo = new DeviceInfo
                {
                        Path = devicePath,
                        Description = GetUsbDeviceStringPropertyInWindows(
                                deviceInfoSet,
                                ref devinfoData,
                                Windows.Setupapi.SPDRP.SPDRP_DEVICEDESC
                        ),
                        Manufecturer = GetUsbDeviceStringPropertyInWindows(
                                deviceInfoSet,
                                ref devinfoData,
                                Windows.Setupapi.SPDRP.SPDRP_MFG
                        ),
                        SerialNumber = GetHidDeviceSerialNumberInWindows(devicePath)
                };
                var hardwareIds = GetUsbDeviceMultiStringPropertyInWindows(
                        deviceInfoSet,
                        ref devinfoData,
                        Windows.Setupapi.SPDRP.SPDRP_HARDWAREID
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

                deviceInfos.Add(deviceInfo);

                deviceIndex++;
            }
            Windows.Setupapi.SetupDiDestroyDeviceInfoList(deviceInfoSet);
            return deviceInfos;
        }

        private static Guid GetHidGuidInWindows()
        {
            Guid guid;
            Windows.Hid.HidD_GetHidGuid(out guid);
            if (!Guid.Empty.ToString().Equals(guid.ToString()))
            {
                return guid;
            }
            Log.Warn("HID Guid is empty. Use default HID Guid");
            return new Guid("4d1e55b2-f16f-11cf-88cb-001111000030"); // default HID GUID
        }

        private static byte[] GetUsbDevicePropertyInWindows(
                IntPtr deviceInfoSetPtr,
                ref Windows.Setupapi.SP_DEVINFO_DATA devinfoData,
                Windows.Setupapi.SPDRP property,
                out int regType)
        {
            int requiredSize;
            var success = Windows.Setupapi.SetupDiGetDeviceRegistryPropertyW(
                    deviceInfoSetPtr,
                    ref devinfoData,
                    property,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    0,
                    out requiredSize
            );
            if (!success)
            {
                var win32Error = Marshal.GetLastWin32Error();
                if (win32Error != Windows.ERROR_INSUFFICIENT_BUFFER)
                {
                    Log.Error("Can not query USB device registry property, error code: " + win32Error);
                    regType = 0;
                    return null;
                }
            }

            var propertyBuffer = new byte[requiredSize];
            success = Windows.Setupapi.SetupDiGetDeviceRegistryPropertyW(
                    deviceInfoSetPtr,
                    ref devinfoData,
                    property,
                    out regType,
                    propertyBuffer,
                    propertyBuffer.Length,
                    out requiredSize
            );
            if (!success)
            {
                var win32Error = Marshal.GetLastWin32Error();
                Log.Error("Can not get USB device registry property, error code: " + win32Error);
                return null;
            }
            return propertyBuffer;
        }

        private static string GetUsbDeviceStringPropertyInWindows(
                IntPtr deviceInfoSetPtr,
                ref Windows.Setupapi.SP_DEVINFO_DATA devinfoData,
                Windows.Setupapi.SPDRP property)
        {
            int regType;
            var bytes = GetUsbDevicePropertyInWindows(deviceInfoSetPtr, ref devinfoData, property, out regType);
            if (bytes == null || bytes.Length == 0 || regType != Windows.REG_SZ)
            {
                return string.Empty;
            }
            return Encoding.Unicode.GetString(bytes, 0, bytes.Length - sizeof(char)).Trim();
        }

        private static string[] GetUsbDeviceMultiStringPropertyInWindows(
                IntPtr deviceInfoSetPtr,
                ref Windows.Setupapi.SP_DEVINFO_DATA devinfoData,
                Windows.Setupapi.SPDRP property)
        {
            int regType;
            var bytes = GetUsbDevicePropertyInWindows(deviceInfoSetPtr, ref devinfoData, property, out regType);
            if (bytes == null || bytes.Length == 0 || regType != Windows.REG_MULTI_SZ)
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
            var deviceHandle = Windows.Kernel32.CreateFileW(
                    devicePath,
                    Windows.Kernel32.GENERIC.GENERIC_READ | Windows.Kernel32.GENERIC.GENERIC_WRITE,
                    Windows.Kernel32.FILE_SHARE.FILE_SHARE_READ | Windows.Kernel32.FILE_SHARE.FILE_SHARE_WRITE,
                    IntPtr.Zero,
                    Windows.Kernel32.CREATION_DISPOSITION.OPEN_EXISTING,
                    Windows.Kernel32.FILE_ATTRIBUTE_FLAG.FILE_FLAG_OVERLAPPED,
                    IntPtr.Zero
            );
            if (deviceHandle == Windows.INVALID_HANDLE_VALUE)
            {
                return result;
            }

            var buffer = new StringBuilder(126);
            var success = Windows.Hid.HidD_GetSerialNumberString(
                    deviceHandle,
                    buffer,
                    buffer.Capacity
            );
            if (success)
            {
                result = buffer.ToString();
            }
            else
            {
                var win32Error = Marshal.GetLastWin32Error();
                Log.Error("Can not get USB HID serial number, error code: " + win32Error);
            }
            Windows.Kernel32.CloseHandle(deviceHandle);
            return result;
        }

        public class DeviceInfo
        {
            public string Description { get; set; } = "";
            public string Manufecturer { get; set; } = "";
            public string Path { get; set; } = "";
            public string ProductId { get; set; } = "";
            public string SerialNumber { get; set; } = "";
            public string VendorId { get; set; } = "";
            public Dictionary<string, string> Optional { get; set; } = new Dictionary<string, string>();
        }
    }
}