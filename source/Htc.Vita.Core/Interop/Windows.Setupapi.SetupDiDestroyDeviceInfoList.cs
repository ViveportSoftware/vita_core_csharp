using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Setupapi
        {
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
        }
    }
}
