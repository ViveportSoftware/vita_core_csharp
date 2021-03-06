using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtscloseserver")]
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern void WTSCloseServer(
                /* _In_ HANDLE */ [In] IntPtr hServer
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsenumerateprocessesw")]
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

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsenumeratesessionsw")]
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

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsfreememory")]
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern void WTSFreeMemory(
                /* _In_ PVOID */ [In] IntPtr pMemory
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsopenserverw")]
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeWtsServerHandle WTSOpenServerW(
                /* _In_ LPTSTR */ [In] string pServerName
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsquerysessioninformationw")]
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

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsqueryusertoken")]
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WTSQueryUserToken(
                /* _In_  ULONG   */ [In] uint sessionId,
                /* _Out_ PHANDLE */ [Out] out SafeTokenHandle token
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wtsapi32/nf-wtsapi32-wtssendmessagew")]
        [DllImport(Libraries.WindowsWtsapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WTSSendMessageW(
                /* IN               HANDLE */ [In] SafeWtsServerHandle hServer,
                /* IN               DWORD  */ [In] uint sessionId,
                /* _In_reads_bytes_ LPWSTR */ [In] string pTitle,
                /* IN               DWORD  */ [In] uint titleLength,
                /* _In_reads_bytes_ LPWSTR */ [In] string pMessage,
                /* IN               DWORD  */ [In] uint messageLength,
                /* IN               DWORD  */ [In] MessageBoxStyle style,
                /* IN               DWORD  */ [In] uint timeout,
                /* _Out_            DWORD* */ [In][Out] ref DialogBoxResult pResponse,
                /* IN               BOOL   */ [In] bool bWait
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsterminateprocess")]
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
