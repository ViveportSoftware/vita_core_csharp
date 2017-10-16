using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Setupapi
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551967.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetupDiGetDeviceRegistryPropertyW(
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
            public static extern bool SetupDiGetDeviceRegistryPropertyW(
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
