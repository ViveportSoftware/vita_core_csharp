using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Hid
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff538924.aspx
             */
            [DllImport(Libraries.Windows_hid,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            public static extern void HidD_GetHidGuid(
                    out Guid guid
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff539683.aspx
             */
            [DllImport(Libraries.Windows_hid,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool HidD_GetSerialNumberString(
                    IntPtr hidDeviceObject,
                    [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer,
                    [MarshalAs(UnmanagedType.U4)] int bufferLength
            );
        }
    }
}
