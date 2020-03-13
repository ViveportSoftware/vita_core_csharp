using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaydevicesw
         */
        [DllImport(Libraries.WindowsUser32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool EnumDisplayDevicesW(
                /* _In_    LPCWSTR          */ [In] string lpDevice,
                /* _In_    DWORD            */ [In] uint iDevNum,
                /* _Inout_ PDISPLAY_DEVICEW */ [In][Out] ref DisplayDeviceW lpDisplayDevice,
                /* _In_    DWORD            */ [In] uint dwFlags
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-exitwindowsex
         */
        [DllImport(Libraries.WindowsUser32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool ExitWindowsEx(
                /* _In_ UINT  */ [In] ExitType uFlags,
                /* _In_ DWORD */ [In] uint dwReason
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmonitorinfow
         */
        [DllImport(Libraries.WindowsUser32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMonitorInfoW(
                /* _In_    HMONITOR      */ [In] IntPtr hMonitor,
                /* _Inout_ LPMONITORINFO */ [In][Out] ref MonitorInfoExW monitorInfo
        );
    }
}
