using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://msdn.microsoft.com/en-us/library/windows/hardware/ff538924.aspx
         */
        [DllImport(Libraries.WindowsHid,
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
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HidD_GetSerialNumberString(
                /* _In_  HANDLE */ [In] SafeFileHandle hidDeviceObject,
                /* _Out_ PVOID  */ [In][Out] StringBuilder buffer,
                /* _In_  ULONG  */ [In] uint bufferLength
        );
    }
}
