using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaydevicesw")]
        [DllImport(Libraries.WindowsUser32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool EnumDisplayDevicesW(
                /* _In_    LPCWSTR          */ [In] string lpDevice,
                /* _In_    DWORD            */ [In] uint iDevNum,
                /* _Inout_ PDISPLAY_DEVICEW */ [In][Out] ref DisplayDeviceW lpDisplayDevice,
                /* _In_    DWORD            */ [In] EnumDisplayDeviceFlags dwFlags
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-exitwindowsex")]
        [DllImport(Libraries.WindowsUser32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool ExitWindowsEx(
                /* _In_ UINT  */ [In] ExitTypes uFlags,
                /* _In_ DWORD */ [In] uint dwReason
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmonitorinfow")]
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

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getshellwindow")]
        [DllImport(Libraries.WindowsUser32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern IntPtr GetShellWindow();

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid")]
        [DllImport(Libraries.WindowsUser32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(
                /* _In_      HWND    */ [In] IntPtr hWnd,
                /* _Out_opt_ LPDWORD */ [In][Out] ref uint lpdwProcessId
        );
    }
}
