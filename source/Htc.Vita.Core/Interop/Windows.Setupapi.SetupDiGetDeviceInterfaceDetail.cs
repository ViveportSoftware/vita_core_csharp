using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Setupapi
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551120.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetupDiGetDeviceInterfaceDetailW(
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
            public static extern bool SetupDiGetDeviceInterfaceDetailW(
                    IntPtr hDevInfo,
                    ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
                    IntPtr deviceInterfaceDetailData,
                    int deviceInterfaceDetailDataSize,
                    ref int requiredSize,
                    ref SP_DEVINFO_DATA deviceInfoData
            );
        }
    }
}
