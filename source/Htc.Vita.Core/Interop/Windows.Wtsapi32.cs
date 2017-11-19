using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static class Wtsapi32
        {
            internal static IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

            /**
             * WTS_CONNECTSTATE_CLASS enumeration
             * https://msdn.microsoft.com/en-us/library/aa383860.aspx
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
             * https://msdn.microsoft.com/en-us/library/aa383861.aspx
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
             * https://msdn.microsoft.com/en-us/library/aa383862.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            internal struct WTS_PROCESS_INFO
            {
                public /* DWORD  */ uint sessionId;
                public /* DWORD  */ uint processId;
                public /* LPTSTR */ string pProcessName;
                public /* PSID   */ IntPtr pUserSid;
            }

            /**
             * https://msdn.microsoft.com/en-us/library/aa383864.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            internal struct WTS_SESSION_INFO
            {
                public /* DWORD                  */ uint sessionId;
                public /* LPTSTR                 */ string pWinStationName;
                public /* WTS_CONNECTSTATE_CLASS */ WindowsTerminalServiceConnectStateClass state;
            }

            /**
             * https://msdn.microsoft.com/en-us/library/aa383829.aspx
             */
            [DllImport(Libraries.Windows_wtsapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            internal static extern void WTSCloseServer(
                    /* _In_ HANDLE */ [In] IntPtr hServer
            );

            /**
             * https://msdn.microsoft.com/en-us/library/aa383831.aspx
             */
            [DllImport(Libraries.Windows_wtsapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool WTSEnumerateProcessesW(
                    /* _In_  HANDLE             */ [In] IntPtr hServer,
                    /* _In_  DWORD              */ [In] uint reserved,
                    /* _In_  DWORD              */ [In] uint version,
                    /* _Out_ PWTS_PROCESS_INFO* */ [In][Out] ref IntPtr ppProcessInfo,
                    /* _Out_ DWORD*             */ [In][Out] ref uint pCount
            );

            /**
             * https://msdn.microsoft.com/en-us/library/aa383833.aspx
             */
            [DllImport(Libraries.Windows_wtsapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool WTSEnumerateSessionsW(
                    /* _In_  HANDLE             */ [In] IntPtr hServer,
                    /* _In_  DWORD              */ [In] uint reserved,
                    /* _In_  DWORD              */ [In] uint version,
                    /* _Out_ PWTS_SESSION_INFO* */ [In][Out] ref IntPtr ppSessionInfo,
                    /* _Out_ DWORD*             */ [In][Out] ref uint pCount
            );

            /**
             * https://msdn.microsoft.com/en-us/library/aa383834.aspx
             */
            [DllImport(Libraries.Windows_wtsapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            internal static extern void WTSFreeMemory(
                    /* _In_ PVOID */ [In] IntPtr pMemory
            );

            /**
             * https://msdn.microsoft.com/en-us/library/aa383837.aspx
             */
            [DllImport(Libraries.Windows_wtsapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            internal static extern IntPtr WTSOpenServerW(
                    /* _In_ LPTSTR */ [In] string pServerName
            );

            /**
             * https://msdn.microsoft.com/en-us/library/aa383838.aspx
             */
            [DllImport(Libraries.Windows_wtsapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool WTSQuerySessionInformationW(
                    /* _In_  HANDLE         */ [In] IntPtr hServer,
                    /* _In_  DWORD          */ [In] uint sessionId,
                    /* _In_  WTS_INFO_CLASS */ [In] WindowsTerminalServiceInfoClass wtsInfoClass,
                    /* _Out_ LPTSTR*        */ [In][Out] ref IntPtr ppBuffer,
                    /* _Out_ DWORD*         */ [In][Out] ref uint pBytesReturned
            );

            /**
             * https://msdn.microsoft.com/en-us/library/aa383846.aspx
             */
            [DllImport(Libraries.Windows_wtsapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool WTSTerminateProcess(
                    /* _In_ HANDLE */ [In] IntPtr hServer,
                    /* _In_ DWORD  */ [In] uint processId,
                    /* _In_ DWORD  */ [In] uint exitCode
            );
        }
    }
}
