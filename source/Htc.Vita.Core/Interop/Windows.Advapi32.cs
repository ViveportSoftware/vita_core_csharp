using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685996.aspx
             */
            [Flags]
            internal enum CONTROL_ACCEPTED : uint
            {
                SERVICE_ACCEPT_STOP = 0x00000001,
                SERVICE_ACCEPT_PAUSE_CONTINUE = 0x00000002,
                SERVICE_ACCEPT_SHUTDOWN = 0x00000004,
                SERVICE_ACCEPT_PARAMCHANGE = 0x00000008,
                SERVICE_ACCEPT_NETBINDCHANGE = 0x00000010,
                SERVICE_ACCEPT_HARDWAREPROFILECHANGE = 0x00000020,
                SERVICE_ACCEPT_POWEREVENT = 0x00000040,
                SERVICE_ACCEPT_SESSIONCHANGE = 0x00000080,
                SERVICE_ACCEPT_PRESHUTDOWN = 0x00000100,
                SERVICE_ACCEPT_TIMECHANGE = 0x00000200,
                SERVICE_ACCEPT_TRIGGEREVENT = 0x00000400,
                SERVICE_ACCEPT_USER_LOGOFF = 0x00000800,
                SERVICE_ACCEPT_LOWRESOURCES = 0x00002000,
                SERVICE_ACCEPT_SYSTEMLOWRESOURCES = 0x00004000
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685996.aspx
             */
            internal enum CURRENT_STATE : uint
            {
                SERVICE_STOPPED = 0x00000001,
                SERVICE_START_PENDING = 0x00000002,
                SERVICE_STOP_PENDING = 0x00000003,
                SERVICE_RUNNING = 0x00000004,
                SERVICE_CONTINUE_PENDING = 0x00000005,
                SERVICE_PAUSE_PENDING = 0x00000006,
                SERVICE_PAUSED = 0x00000007
            }

            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             */
            internal enum ERROR_CONTROL_TYPE : uint
            {
                SERVICE_ERROR_IGNORE = 0x00000000,
                SERVICE_ERROR_NORMAL = 0x00000001,
                SERVICE_ERROR_SEVERE = 0x00000002,
                SERVICE_ERROR_CRITICAL = 0x00000003,
                SERVICE_NO_CHANGE = 0xffffffff
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684323.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685981.aspx
             */
            [Flags]
            internal enum SCMAccessRight : uint
            {
                STANDARD_RIGHTS_REQUIRED = 0xF0000,
                SC_MANAGER_CONNECT = 0x0001,
                SC_MANAGER_CREATE_SERVICE = 0x0002,
                SC_MANAGER_ENUMERATE_SERVICE = 0x0004,
                SC_MANAGER_LOCK = 0x0008,
                SC_MANAGER_QUERY_LOCK_STATUS = 0x0010,
                SC_MANAGER_MODIFY_BOOT_CONFIG = 0x0020,
                SC_MANAGER_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED
                                        | SC_MANAGER_CONNECT
                                        | SC_MANAGER_CREATE_SERVICE
                                        | SC_MANAGER_ENUMERATE_SERVICE
                                        | SC_MANAGER_LOCK
                                        | SC_MANAGER_QUERY_LOCK_STATUS
                                        | SC_MANAGER_MODIFY_BOOT_CONFIG
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684330.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685981.aspx
             */
            [Flags]
            internal enum ServiceAccessRight : uint
            {
                STANDARD_RIGHTS_REQUIRED = 0xF0000,
                SERVICE_QUERY_CONFIG = 0x0001,
                SERVICE_CHANGE_CONFIG = 0x0002,
                SERVICE_QUERY_STATUS = 0x0004,
                SERVICE_ENUMERATE_DEPENDENTS = 0x0008,
                SERVICE_START = 0x0010,
                SERVICE_STOP = 0x0020,
                SERVICE_PAUSE_CONTINUE = 0x0040,
                SERVICE_INTERROGATE = 0x0080,
                SERVICE_USER_DEFINED_CONTROL = 0x0100,
                SERVICE_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED
                                     | SERVICE_QUERY_CONFIG
                                     | SERVICE_CHANGE_CONFIG
                                     | SERVICE_QUERY_STATUS
                                     | SERVICE_ENUMERATE_DEPENDENTS
                                     | SERVICE_START
                                     | SERVICE_STOP
                                     | SERVICE_PAUSE_CONTINUE
                                     | SERVICE_INTERROGATE
                                     | SERVICE_USER_DEFINED_CONTROL
            }

            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685996.aspx
             */
            [Flags]
            internal enum SERVICE_TYPE : uint
            {
                SERVICE_KERNEL_DRIVER = 0x00000001,
                SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,
                SERVICE_ADAPTER = 0x00000004,
                SERVICE_RECOGNIZER_DRIVER = 0x00000008,
                SERVICE_DRIVER = SERVICE_KERNEL_DRIVER
                                 | SERVICE_FILE_SYSTEM_DRIVER
                                 | SERVICE_RECOGNIZER_DRIVER,
                SERVICE_WIN32_OWN_PROCESS = 0x00000010,
                SERVICE_WIN32_SHARE_PROCESS = 0x00000020,
                SERVICE_WIN32 = SERVICE_WIN32_OWN_PROCESS
                                | SERVICE_WIN32_SHARE_PROCESS,
                SERVICE_USER_SERVICE = 0x00000040,
                SERVICE_USERSERVICE_INSTANCE = 0x00000080,
                SERVICE_USER_SHARE_PROCESS = SERVICE_USER_SERVICE
                                             | SERVICE_WIN32_SHARE_PROCESS,
                SERVICE_USER_OWN_PROCESS = SERVICE_USER_SERVICE
                                           | SERVICE_WIN32_OWN_PROCESS,
                SERVICE_INTERACTIVE_PROCESS = 0x00000100,
                SERVICE_PKG_SERVICE = 0x00000200,
                SERVICE_TYPE_ALL = SERVICE_WIN32
                                   | SERVICE_ADAPTER
                                   | SERVICE_DRIVER
                                   | SERVICE_INTERACTIVE_PROCESS
                                   | SERVICE_USER_SERVICE
                                   | SERVICE_USERSERVICE_INSTANCE
                                   | SERVICE_PKG_SERVICE,
                SERVICE_NO_CHANGE = 0xffffffff
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa379601.aspx
             */
            internal enum SID_NAME_USE
            {
                SidTypeUser = 1,
                SidTypeGroup,
                SidTypeDomain,
                SidTypeAlias,
                SidTypeWellKnownGroup,
                SidTypeDeletedAccount,
                SidTypeInvalid,
                SidTypeUnknown,
                SidTypeComputer,
                SidTypeLabel
            }

            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             */
            internal enum START_TYPE : uint
            {
                SERVICE_BOOT_START = 0x00000000,
                SERVICE_SYSTEM_START = 0x00000001,
                SERVICE_AUTO_START = 0x00000002,
                SERVICE_DEMAND_START = 0x00000003,
                SERVICE_DISABLED = 0x00000004,
                SERVICE_NO_CHANGE = 0xffffffff
            }

            /**
             * QUERY_SERVICE_CONFIG structure
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684950.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            internal struct QueryServiceConfig
            {
                /* DWORD */
                public SERVICE_TYPE dwServiceType;

                /* DWORD */
                public START_TYPE dwStartType;

                /* DWORD */
                public ERROR_CONTROL_TYPE dwErrorControl;

                /* LPTSTR */
                public string lpBinaryPathName;

                /* LPTSTR */
                public string lpLoadOrderGroup;

                /* DWORD */
                public uint dwTagId;

                /* LPTSTR */
                public string lpDependencies;

                /* LPTSTR */
                public string lpServiceStartName;

                /* LPTSTR */
                public string lpDisplayName;
            }

            /**
             * SERVICE_STATUS structure
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685996.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            internal struct ServiceStatus
            {
                /* DWORD */
                public SERVICE_TYPE dwServiceType;

                /* DWORD */
                public CURRENT_STATE dwCurrentState;

                /* DWORD */
                public CONTROL_ACCEPTED dwControlAccepted;

                /* DWORD */
                public uint dwWin32ExitCode;

                /* DWORD */
                public uint dwServiceSpecificExitCode;

                /* DWORD */
                public uint dwCheckPoint;

                /* DWORD */
                public uint dwWaitHint;
            }

            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool ChangeServiceConfigW(
                    IntPtr hService,
                    SERVICE_TYPE serviceType,
                    START_TYPE startType,
                    ERROR_CONTROL_TYPE errorControl,
                    string binaryPathName,
                    string loadOrderGroup,
                    IntPtr lpTagId,
                    string dependencies,
                    string serviceStartName,
                    string password,
                    string displayName
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms682028.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool CloseServiceHandle(
                    IntPtr hSCObject
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa376399.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool ConvertSidToStringSidW(
                    [In] IntPtr pSid,
                    [MarshalAs(UnmanagedType.LPTStr)] ref string sid
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa376402.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool ConvertStringSidToSidW(
                    string sid,
                    IntPtr pSid
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa379166.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool LookupAccountSidW(
                    [In] string pSystemName,
                    IntPtr pSid,
                    StringBuilder name,
                    [MarshalAs(UnmanagedType.U4)] ref int cchName,
                    StringBuilder referencedDomainName,
                    [MarshalAs(UnmanagedType.U4)] ref int cchReferencedDomainName,
                    out SID_NAME_USE peUse
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684323.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            internal static extern IntPtr OpenSCManagerW(
                    [In] string machineName,
                    [In] string databaseName,
                    SCMAccessRight desiredAccess
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684330.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            internal static extern IntPtr OpenServiceW(
                    IntPtr hSCManager,
                    [In] string serviceName,
                    ServiceAccessRight desiredAccess
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684932.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool QueryServiceConfigW(
                    [In] IntPtr hService,
                    IntPtr lpServiceConfig,
                    uint cbBufSize,
                    out uint pcbBytesNeeded
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684939.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool QueryServiceStatus(
                    [In] IntPtr hService,
                    ref ServiceStatus lpServiceStatus
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms686321.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool StartServiceW(
                    IntPtr hService,
                    uint dwNumServiceArgs,
                    string[] lpServiceArgVectors
            );
        }
    }
}
