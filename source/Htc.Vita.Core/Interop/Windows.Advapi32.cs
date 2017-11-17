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
             * SC_MANAGER_ACCESS_RIGHT enumeration
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684323.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685981.aspx
             */
            [Flags]
            internal enum ServiceControlManagerAccessRight : uint
            {
                /* STANDARD_RIGHTS_REQUIRED */ StandardRightsRequired = 0xF0000,
                /* SC_MANAGER_CONNECT */ Connect = 0x0001,
                /* SC_MANAGER_CREATE_SERVICE */ CreateService = 0x0002,
                /* SC_MANAGER_ENUMERATE_SERVICE */ EnumerateService = 0x0004,
                /* SC_MANAGER_LOCK */ Lock = 0x0008,
                /* SC_MANAGER_QUERY_LOCK_STATUS */ QueryLockStatus = 0x0010,
                /* SC_MANAGER_MODIFY_BOOT_CONFIG */ ModifyBootConfig = 0x0020,
                /* SC_MANAGER_ALL_ACCESS */ AllAccess = StandardRightsRequired
                                                      | Connect
                                                      | CreateService
                                                      | EnumerateService
                                                      | Lock
                                                      | QueryLockStatus
                                                      | ModifyBootConfig
            }

            /**
             * SERVICE_ACCESS_RIGHT enumeration
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684330.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685981.aspx
             */
            [Flags]
            internal enum ServiceAccessRight : uint
            {
                /* STANDARD_RIGHTS_REQUIRED */ StandardRightsRequired = 0xF0000,
                /* SERVICE_QUERY_CONFIG */ QueryConfig = 0x0001,
                /* SERVICE_CHANGE_CONFIG */ ChangeConfig = 0x0002,
                /* SERVICE_QUERY_STATUS */ QueryStatus = 0x0004,
                /* SERVICE_ENUMERATE_DEPENDENTS */ EnumerateDependents = 0x0008,
                /* SERVICE_START */ Start = 0x0010,
                /* SERVICE_STOP */ Stop = 0x0020,
                /* SERVICE_PAUSE_CONTINUE */ PauseContinue = 0x0040,
                /* SERVICE_INTERROGATE */ Interrogate = 0x0080,
                /* SERVICE_USER_DEFINED_CONTROL */ UserDefinedControl = 0x0100,
                /* SERVICE_ALL_ACCESS */ AllAccess = StandardRightsRequired
                                                     | QueryConfig
                                                     | ChangeConfig
                                                     | QueryStatus
                                                     | EnumerateDependents
                                                     | Start
                                                     | Stop
                                                     | PauseContinue
                                                     | Interrogate
                                                     | UserDefinedControl
            }

            /**
             * SERVICE_TYPE enumeration
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685996.aspx
             */
            [Flags]
            internal enum ServiceType : uint
            {
                /* SERVICE_KERNEL_DRIVER */ KernelDriver = 0x00000001,
                /* SERVICE_FILE_SYSTEM_DRIVER */ FileSystemDriver = 0x00000002,
                /* SERVICE_ADAPTER */ Adapter = 0x00000004,
                /* SERVICE_RECOGNIZER_DRIVER */ RecognizerDriver = 0x00000008,
                /* SERVICE_DRIVER */ Driver = KernelDriver
                                              | FileSystemDriver
                                              | RecognizerDriver,
                /* SERVICE_WIN32_OWN_PROCESS */ Win32OwnProcess = 0x00000010,
                /* SERVICE_WIN32_SHARE_PROCESS */ Win32ShareProcess = 0x00000020,
                /* SERVICE_WIN32 */ Win32 = Win32OwnProcess
                                            | Win32ShareProcess,
                /* SERVICE_USER_SERVICE */ UserService = 0x00000040,
                /* SERVICE_USERSERVICE_INSTANCE */ UserServiceInstance = 0x00000080,
                /* SERVICE_USER_SHARE_PROCESS */ UserShareProcess = UserService
                                                                    | Win32ShareProcess,
                /* SERVICE_USER_OWN_PROCESS */ UserOwnProcess = UserService
                                                                | Win32OwnProcess,
                /* SERVICE_INTERACTIVE_PROCESS */ InteractiveProcess = 0x00000100,
                /* SERVICE_PKG_SERVICE */ PkgService = 0x00000200,
                /* SERVICE_TYPE_ALL */ All = Win32
                                             | Adapter
                                             | Driver
                                             | InteractiveProcess
                                             | UserService
                                             | UserServiceInstance
                                             | PkgService,
                /* SERVICE_NO_CHANGE */ NoChange = 0xffffffff
            }

            /**
             * SID_NAME_USE enumeration
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa379601.aspx
             */
            internal enum SidType
            {
                /* SidTypeUser */ User = 1,
                /* SidTypeGroup */ Group,
                /* SidTypeDomain */ Domain,
                /* SidTypeAlias */ Alias,
                /* SidTypeWellKnownGroup */ WellKnownGroup,
                /* SidTypeDeletedAccount */ DeletedAccount,
                /* SidTypeInvalid */ Invalid,
                /* SidTypeUnknown */ Unknown,
                /* SidTypeComputer */ Computer,
                /* SidTypeLabel */ Label
            }

            /**
             * START_TYPE enumeration
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             */
            internal enum StartType : uint
            {
                /* SERVICE_BOOT_START */ BootStart = 0x00000000,
                /* SERVICE_SYSTEM_START */ SystemStart = 0x00000001,
                /* SERVICE_AUTO_START */ AutoStart = 0x00000002,
                /* SERVICE_DEMAND_START */ DemandStart = 0x00000003,
                /* SERVICE_DISABLED */ Disabled = 0x00000004,
                /* SERVICE_NO_CHANGE */ NoChange = 0xffffffff
            }

            /**
             * QUERY_SERVICE_CONFIG structure
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684950.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            internal struct QueryServiceConfig
            {
                /* DWORD */
                public ServiceType dwServiceType;

                /* DWORD */
                public StartType dwStartType;

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
                public ServiceType dwServiceType;

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
                    ServiceType serviceType,
                    StartType startType,
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
                    out SidType peUse
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
                    ServiceControlManagerAccessRight desiredAccess
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
