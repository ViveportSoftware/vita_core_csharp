using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static readonly IntPtr /* WTS_CURRENT_SERVER_HANDLE */ WindowsTerminalServiceCurrentServerHandle = IntPtr.Zero;

        /**
         * WTS_CONNECTSTATE_CLASS enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ne-wtsapi32-_wts_connectstate_class
         */
        internal enum WindowsTerminalServiceConnectStateClass
        {
            /* WTSActive       */ Active,
            /* WTSConnected    */ Connected,
            /* WTSConnectQuery */ ConnectQuery,
            /* WTSShadow       */ Shadow,
            /* WTSDisconnected */ Disconnected,
            /* WTSIdle         */ Idle,
            /* WTSListen       */ Listen,
            /* WTSReset        */ Reset,
            /* WTSDown         */ Down,
            /* WTSInit         */ Init
        }

        /**
         * WTS_INFO_CLASS enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ne-wtsapi32-_wts_info_class
         */
        internal enum WindowsTerminalServiceInfoClass
        {
            /* WTSInitialProgram     */ InitialProgram,
            /* WTSApplicationName    */ ApplicationName,
            /* WTSWorkingDirectory   */ WorkingDirectory,
            /* WTSOEMId              */ OemId,
            /* WTSSessionId          */ SessionId,
            /* WTSUserName           */ UserName,
            /* WTSWinStationName     */ WinStationName,
            /* WTSDomainName         */ DomainName,
            /* WTSConnectState       */ ConnectState,
            /* WTSClientBuildNumber  */ ClientBuildNumber,
            /* WTSClientName         */ ClientName,
            /* WTSClientDirectory    */ ClientDirectory,
            /* WTSClientProductId    */ ClientProductId,
            /* WTSClientHardwareId   */ ClientHardwareId,
            /* WTSClientAddress      */ ClientAddress,
            /* WTSClientDisplay      */ ClientDisplay,
            /* WTSClientProtocolType */ ClientProtocolType,
            /* WTSIdleTime           */ IdleTime,
            /* WTSLogonTime          */ LogonTime,
            /* WTSIncomingBytes      */ IncomingBytes,
            /* WTSOutgoingBytes      */ OutgoingBytes,
            /* WTSIncomingFrames     */ IncomingFrames,
            /* WTSOutgoingFrames     */ OutgoingFrames,
            /* WTSClientInfo         */ ClientInfo,
            /* WTSSessionInfo        */ SessionInfo,
            /* WTSSessionInfoEx      */ SessionInfoEx,
            /* WTSConfigInfo         */ ConfigInfo,
            /* WTSValidationInfo     */ ValidationInfo,
            /* WTSSessionAddressV4   */ SessionAddressV4,
            /* WTSIsRemoteSession    */ IsRemoteSession
        }

        /**
         * WTS_PROCESS_INFO structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_process_infow
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowsTerminalServiceProcessInfo
        {
            public /* DWORD  */ uint sessionId;
            public /* DWORD  */ uint processId;
            public /* LPTSTR */ string pProcessName;
            public /* PSID   */ IntPtr pUserSid;
        }

        /**
         * WTS_SESSION_INFO structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_session_infow
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowsTerminalServiceSessionInfo
        {
            public /* DWORD                  */ uint sessionId;
            public /* LPTSTR                 */ string pWinStationName;
            public /* WTS_CONNECTSTATE_CLASS */ WindowsTerminalServiceConnectStateClass state;
        }

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
                /* _In_  WTS_INFO_CLASS */ [In] WindowsTerminalServiceInfoClass wtsInfoClass,
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
