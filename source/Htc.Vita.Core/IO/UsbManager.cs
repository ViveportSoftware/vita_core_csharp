using System;
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
            var classGuid = GetHidGuidInWindows();
            var deviceInfoSetPtr = Windows.Setupapi.SetupDiGetClassDevsW(
                    ref classGuid,
                    null,
                    IntPtr.Zero,
                    Windows.Setupapi.DIGCF.DIGCF_PRESENT | Windows.Setupapi.DIGCF.DIGCF_DEVICEINTERFACE
            );
            if (deviceInfoSetPtr == Windows.INVALID_HANDLE_VALUE)
            {
                Log.Error("Can not find HID devices: " + Marshal.GetLastWin32Error());
            }

            var deviceInfos = new List<DeviceInfo>();
            var memberIndex = 0;
            while (true)
            {
                var deviceInterfaceData = new Windows.Setupapi.SP_DEVICE_INTERFACE_DATA();
                deviceInterfaceData.cbSize = Marshal.SizeOf(deviceInterfaceData);
                var success = Windows.Setupapi.SetupDiEnumDeviceInterfaces(
                        deviceInfoSetPtr,
                        IntPtr.Zero,
                        ref classGuid,
                        memberIndex,
                        ref deviceInterfaceData
                );
                if (!success)
                {
                    var win32Error = Marshal.GetLastWin32Error();
                    if (win32Error != Windows.ERROR_NO_MORE_ITEMS)
                    {
                        Log.Error("Can not enumerate HID device index: " + memberIndex + ", error code: " + win32Error);
                    }
                    break;
                }

                var bufferSize = 0;
                success = Windows.Setupapi.SetupDiGetDeviceInterfaceDetailW(
                        deviceInfoSetPtr,
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
                        Log.Error("Can not query HID device interface detail on index: " + memberIndex + ", error code: " + win32Error);
                        break;
                    }
                }

                var deviceInterfaceDetailDataPtr = Marshal.AllocHGlobal(bufferSize);
                Marshal.WriteInt32(deviceInterfaceDetailDataPtr, IntPtr.Size == 8 ? 8 : 6);
                var devinfoData = new Windows.Setupapi.SP_DEVINFO_DATA();
                devinfoData.cbSize = Marshal.SizeOf(devinfoData);
                success = Windows.Setupapi.SetupDiGetDeviceInterfaceDetailW(
                        deviceInfoSetPtr,
                        ref deviceInterfaceData,
                        deviceInterfaceDetailDataPtr,
                        bufferSize,
                        ref bufferSize,
                        ref devinfoData
                );
                if (!success)
                {
                    var win32Error = Marshal.GetLastWin32Error();
                    Log.Error("Can not get HID device interface detail on index: " + memberIndex + ", error code: " + win32Error);
                    break;
                }

                var deviceInfo = new DeviceInfo
                {
                        Path = Marshal.PtrToStringUni(deviceInterfaceDetailDataPtr + 4),
                        Description = GetDeviceStringPropertyInWindows(
                            deviceInfoSetPtr,
                            ref devinfoData,
                            Windows.Setupapi.SPDRP.SPDRP_DEVICEDESC
                        ),
                        Manufecturer = GetDeviceStringPropertyInWindows(
                            deviceInfoSetPtr,
                            ref devinfoData,
                            Windows.Setupapi.SPDRP.SPDRP_MFG
                        )
                };
                var hardwareIds = GetDeviceMultiStringPropertyInWindows(
                        deviceInfoSetPtr,
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

                Marshal.FreeHGlobal(deviceInterfaceDetailDataPtr);
                memberIndex++;
            }
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

        private static byte[] GetDevicePropertyInWindows(
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
                    Log.Error("Can not query device registry property, error code: " + win32Error);
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
                Log.Error("Can not get device registry property, error code: " + win32Error);
                return null;
            }
            return propertyBuffer;
        }

        private static string GetDeviceStringPropertyInWindows(
                IntPtr deviceInfoSetPtr,
                ref Windows.Setupapi.SP_DEVINFO_DATA devinfoData,
                Windows.Setupapi.SPDRP property)
        {
            int regType;
            var bytes = GetDevicePropertyInWindows(deviceInfoSetPtr, ref devinfoData, property, out regType);
            if (bytes == null || bytes.Length == 0 || regType != Windows.REG_SZ)
            {
                return string.Empty;
            }
            return Encoding.Unicode.GetString(bytes, 0, bytes.Length - sizeof(char)).Trim();
        }

        private static string[] GetDeviceMultiStringPropertyInWindows(
                IntPtr deviceInfoSetPtr,
                ref Windows.Setupapi.SP_DEVINFO_DATA devinfoData,
                Windows.Setupapi.SPDRP property)
        {
            int regType;
            var bytes = GetDevicePropertyInWindows(deviceInfoSetPtr, ref devinfoData, property, out regType);
            if (bytes == null || bytes.Length == 0 || regType != Windows.REG_MULTI_SZ)
            {
                return new string[] {};
            }
            return Encoding.Unicode.GetString(bytes).Split(
                    (char[]) null,
                    StringSplitOptions.RemoveEmptyEntries
            );
        }

        public class DeviceInfo
        {
            public string VendorId { get; set; } = "";
            public string ProductId { get; set; } = "";
            public string Path { get; set; } = "";
            public string Description { get; set; } = "";
            public string Manufecturer { get; set; } = "";
            public Dictionary<string, string> Optional { get; set; } = new Dictionary<string, string>();
        }
    }
}
