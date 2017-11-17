using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Setupapi
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551069.aspx
             */
            [Flags]
            public enum DIGCF
            {
                DIGCF_DEFAULT = 0x00000001,
                DIGCF_PRESENT = 0x00000002,
                DIGCF_ALLCLASSES = 0x00000004,
                DIGCF_PROFILE = 0x00000008,
                DIGCF_DEVICEINTERFACE = 0x00000010
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
            public static extern bool SetupDiDestroyDeviceInfoList(
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
            public static extern bool SetupDiEnumDeviceInterfaces(
                    IntPtr deviceInfoSet,
                    IntPtr deviceInfoData,
                    ref Guid interfaceClassGuid,
                    int memberIndex,
                    ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData
            );
        }
    }
}
