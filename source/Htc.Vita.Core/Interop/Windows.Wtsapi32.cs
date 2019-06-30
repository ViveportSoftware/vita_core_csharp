using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtscloseserver
         */
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern void WTSCloseServer(
                /* _In_ HANDLE */ [In] IntPtr hServer
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsenumerateprocessesw
         */
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WTSEnumerateProcessesW(
                /* _In_  HANDLE             */ [In] SafeWtsServerHandle hServer,
                /* _In_  DWORD              */ [In] uint reserved,
                /* _In_  DWORD              */ [In] uint version,
                /* _Out_ PWTS_PROCESS_INFO* */ [In][Out] ref IntPtr ppProcessInfo,
                /* _Out_ DWORD*             */ [In][Out] ref uint pCount
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsenumeratesessionsw
         */
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WTSEnumerateSessionsW(
                /* _In_  HANDLE             */ [In] SafeWtsServerHandle hServer,
                /* _In_  DWORD              */ [In] uint reserved,
                /* _In_  DWORD              */ [In] uint version,
                /* _Out_ PWTS_SESSION_INFO* */ [In][Out] ref IntPtr ppSessionInfo,
                /* _Out_ DWORD*             */ [In][Out] ref uint pCount
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsfreememory
         */
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern void WTSFreeMemory(
                /* _In_ PVOID */ [In] IntPtr pMemory
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsopenserverw
         */
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeWtsServerHandle WTSOpenServerW(
                /* _In_ LPTSTR */ [In] string pServerName
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsquerysessioninformationw
         */
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WTSQuerySessionInformationW(
                /* _In_  HANDLE         */ [In] SafeWtsServerHandle hServer,
                /* _In_  DWORD          */ [In] uint sessionId,
                /* _In_  WTS_INFO_CLASS */ [In] WindowsTerminalServiceInfo wtsInfoClass,
                /* _Out_ LPTSTR*        */ [In][Out] ref IntPtr ppBuffer,
                /* _Out_ DWORD*         */ [In][Out] ref uint pBytesReturned
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsterminateprocess
         */
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WTSTerminateProcess(
                /* _In_ HANDLE */ [In] SafeWtsServerHandle hServer,
                /* _In_ DWORD  */ [In] uint processId,
                /* _In_ DWORD  */ [In] uint exitCode
        );
    }
}
