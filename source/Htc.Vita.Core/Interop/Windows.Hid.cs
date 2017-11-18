using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static class Hid
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff538924.aspx
             */
            [DllImport(Libraries.Windows_hid,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            internal static extern void HidD_GetHidGuid(
                    /* _Out_ LPGUID */ [In][Out] ref Guid hidGuid
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
            internal static extern bool HidD_GetSerialNumberString(
                    /* _In_  HANDLE */ [In] IntPtr hidDeviceObject,
                    /* _Out_ PVOID  */ [In][Out] StringBuilder buffer,
                    /* _In_  ULONG  */ [In] uint bufferLength
            );
        }
    }
}
