using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static class Setupapi
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551069.aspx
             */
            [Flags]
            internal enum DIGCF
            {
                DIGCF_DEFAULT = 0x00000001,
                DIGCF_PRESENT = 0x00000002,
                DIGCF_ALLCLASSES = 0x00000004,
                DIGCF_PROFILE = 0x00000008,
                DIGCF_DEVICEINTERFACE = 0x00000010
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551967.aspx
             */
            internal enum SPDRP
            {
                SPDRP_DEVICEDESC = 0,
                SPDRP_HARDWAREID = 1,
                SPDRP_COMPATIBLEIDS = 2,
                SPDRP_UNUSED0 = 3,
                SPDRP_SERVICE = 4,
                SPDRP_UNUSED1 = 5,
                SPDRP_UNUSED2 = 6,
                SPDRP_CLASS = 7,
                SPDRP_CLASSGUID = 8,
                SPDRP_DRIVER = 9,
                SPDRP_CONFIGFLAGS = 10,
                SPDRP_MFG = 11,
                SPDRP_FRIENDLYNAME = 12,
                SPDRP_LOCATION_INFORMATION = 13,
                SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 14,
                SPDRP_CAPABILITIES = 15,
                SPDRP_UI_NUMBER = 16,
                SPDRP_UPPERFILTERS = 17,
                SPDRP_LOWERFILTERS = 18,
                SPDRP_BUSTYPEGUID = 19,
                SPDRP_LEGACYBUSTYPE = 20,
                SPDRP_BUSNUMBER = 21,
                SPDRP_ENUMERATOR_NAME = 22,
                SPDRP_SECURITY = 23,
                SPDRP_SECURITY_SDS = 24,
                SPDRP_DEVTYPE = 25,
                SPDRP_EXCLUSIVE = 26,
                SPDRP_CHARACTERISTICS = 27,
                SPDRP_ADDRESS = 28,
                SPDRP_UI_NUMBER_DESC_FORMAT = 29,
                SPDRP_DEVICE_POWER_DATA = 30,
                SPDRP_REMOVAL_POLICY = 31,
                SPDRP_REMOVAL_POLICY_HW_DEFAULT = 32,
                SPDRP_REMOVAL_POLICY_OVERRIDE = 33,
                SPDRP_INSTALL_STATE = 34,
                SPDRP_LOCATION_PATHS = 35,
                SPDRP_BASE_CONTAINERID = 36
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff552342.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            internal struct SP_DEVICE_INTERFACE_DATA
            {
                public int cbSize;

                public Guid interfaceClassGuid;

                public int flags;

                public IntPtr reserved;
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff552344.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            internal struct SP_DEVINFO_DATA
            {
                public int cbSize;

                public Guid classGuid;

                public int devInst;

                public IntPtr reserved;
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff550996.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetupDiDestroyDeviceInfoList(
                    IntPtr deviceInfoSet
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551015.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetupDiEnumDeviceInterfaces(
                    IntPtr deviceInfoSet,
                    IntPtr deviceInfoData,
                    ref Guid interfaceClassGuid,
                    int memberIndex,
                    ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551069.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            internal static extern IntPtr SetupDiGetClassDevsW(
                    ref Guid classGuid,
                    [MarshalAs(UnmanagedType.LPTStr)] string enumerator,
                    IntPtr hwndParent,
                    DIGCF flags
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551120.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetupDiGetDeviceInterfaceDetailW(
                    IntPtr hDevInfo,
                    ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
                    IntPtr deviceInterfaceDetailData,
                    int deviceInterfaceDetailDataSize,
                    ref int requiredSize,
                    IntPtr deviceInfoData
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551120.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetupDiGetDeviceInterfaceDetailW(
                    IntPtr hDevInfo,
                    ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
                    IntPtr deviceInterfaceDetailData,
                    int deviceInterfaceDetailDataSize,
                    ref int requiredSize,
                    ref SP_DEVINFO_DATA deviceInfoData
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551967.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetupDiGetDeviceRegistryPropertyW(
                    IntPtr deviceInfoSet,
                    ref SP_DEVINFO_DATA deviceInfoData,
                    SPDRP property,
                    IntPtr propertyRegDataType,
                    IntPtr propertyBuffer,
                    int propertyBufferSize,
                    out int requiredSize
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551967.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetupDiGetDeviceRegistryPropertyW(
                    IntPtr deviceInfoSet,
                    ref SP_DEVINFO_DATA deviceInfoData,
                    SPDRP property,
                    out int propertyRegDataType,
                    byte[] propertyBuffer,
                    int propertyBufferSize,
                    out int requiredSize
            );
        }
    }
}
