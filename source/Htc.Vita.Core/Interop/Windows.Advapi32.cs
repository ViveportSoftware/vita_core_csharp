using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/privilege-constants
         */
        internal const string SeShutdownName = "SeShutdownPrivilege";

        /**
         * CONTROL_ACCEPTED enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status
         */
        [Flags]
        internal enum AcceptedControl : uint
        {
            /* SERVICE_ACCEPT_STOP                  */ Stop = 0x00000001,
            /* SERVICE_ACCEPT_PAUSE_CONTINUE        */ PauseContinue = 0x00000002,
            /* SERVICE_ACCEPT_SHUTDOWN              */ Shutdown = 0x00000004,
            /* SERVICE_ACCEPT_PARAMCHANGE           */ ParamChange = 0x00000008,
            /* SERVICE_ACCEPT_NETBINDCHANGE         */ NetBindChange = 0x00000010,
            /* SERVICE_ACCEPT_HARDWAREPROFILECHANGE */ HardwareProfileChange = 0x00000020,
            /* SERVICE_ACCEPT_POWEREVENT            */ PowerEvent = 0x00000040,
            /* SERVICE_ACCEPT_SESSIONCHANGE         */ SessionChange = 0x00000080,
            /* SERVICE_ACCEPT_PRESHUTDOWN           */ PreShutdown = 0x00000100,
            /* SERVICE_ACCEPT_TIMECHANGE            */ TimeChange = 0x00000200,
            /* SERVICE_ACCEPT_TRIGGEREVENT          */ TriggerEvent = 0x00000400,
            /* SERVICE_ACCEPT_USER_LOGOFF           */ UserLogoff = 0x00000800,
            /* SERVICE_ACCEPT_LOWRESOURCES          */ LowResources = 0x00002000,
            /* SERVICE_ACCEPT_SYSTEMLOWRESOURCES    */ SystemLowResources = 0x00004000
        }

        /**
         * CURRENT_STATE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status
         */
        internal enum CurrentState : uint
        {
            /* SERVICE_STOPPED          */ Stopped = 0x00000001,
            /* SERVICE_START_PENDING    */ StartPending = 0x00000002,
            /* SERVICE_STOP_PENDING     */ StopPending = 0x00000003,
            /* SERVICE_RUNNING          */ Running = 0x00000004,
            /* SERVICE_CONTINUE_PENDING */ ContinuePending = 0x00000005,
            /* SERVICE_PAUSE_PENDING    */ PausePending = 0x00000006,
            /* SERVICE_PAUSED           */ Paused = 0x00000007
        }

        /**
         * ERROR_CONTROL_TYPE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-changeserviceconfigw
         */
        internal enum ErrorControlType : uint
        {
            /* SERVICE_ERROR_IGNORE   */ Ignore = 0x00000000,
            /* SERVICE_ERROR_NORMAL   */ Normal = 0x00000001,
            /* SERVICE_ERROR_SEVERE   */ Severe = 0x00000002,
            /* SERVICE_ERROR_CRITICAL */ Critical = 0x00000003,
            /* SERVICE_NO_CHANGE      */ NoChange = 0xffffffff
        }

        [Flags]
        internal enum SePrivilege : uint
        {
            /* SE_PRIVILEGE_ENABLED_BY_DEFAULT */ EnabledByDefault = 0x00000001,
            /* SE_PRIVILEGE_ENABLED            */ Enabled = 0x00000002,
            /* SE_PRIVILEGE_REMOVED            */ Removed = 0x00000004,
            /* SE_PRIVILEGE_USED_FOR_ACCESS    */ UsedForAccess = 0x80000000
        }

        /**
         * SC_MANAGER_ACCESS_RIGHT enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/Services/service-security-and-access-rights
         */
        [Flags]
        internal enum ServiceControlManagerAccessRight : uint
        {
            /* STANDARD_RIGHTS_REQUIRED      */ StandardRightsRequired = AccessRight.StandardRightsRequired,
            /* SC_MANAGER_CONNECT            */ Connect = 0x0001,
            /* SC_MANAGER_CREATE_SERVICE     */ CreateService = 0x0002,
            /* SC_MANAGER_ENUMERATE_SERVICE  */ EnumerateService = 0x0004,
            /* SC_MANAGER_LOCK               */ Lock = 0x0008,
            /* SC_MANAGER_QUERY_LOCK_STATUS  */ QueryLockStatus = 0x0010,
            /* SC_MANAGER_MODIFY_BOOT_CONFIG */ ModifyBootConfig = 0x0020,
            /* SC_MANAGER_ALL_ACCESS         */ AllAccess = StandardRightsRequired
                                                          | Connect
                                                          | CreateService
                                                          | EnumerateService
                                                          | Lock
                                                          | QueryLockStatus
                                                          | ModifyBootConfig
        }

        /**
         * SERVICE_ACCESS_RIGHT enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/Services/service-security-and-access-rights
         */
        [Flags]
        internal enum ServiceAccessRight : uint
        {
            /* STANDARD_RIGHTS_REQUIRED     */ StandardRightsRequired = AccessRight.StandardRightsRequired,
            /* SERVICE_QUERY_CONFIG         */ QueryConfig = 0x0001,
            /* SERVICE_CHANGE_CONFIG        */ ChangeConfig = 0x0002,
            /* SERVICE_QUERY_STATUS         */ QueryStatus = 0x0004,
            /* SERVICE_ENUMERATE_DEPENDENTS */ EnumerateDependents = 0x0008,
            /* SERVICE_START                */ Start = 0x0010,
            /* SERVICE_STOP                 */ Stop = 0x0020,
            /* SERVICE_PAUSE_CONTINUE       */ PauseContinue = 0x0040,
            /* SERVICE_INTERROGATE          */ Interrogate = 0x0080,
            /* SERVICE_USER_DEFINED_CONTROL */ UserDefinedControl = 0x0100,
            /* SERVICE_ALL_ACCESS           */ AllAccess = StandardRightsRequired
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
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status
         */
        [Flags]
        internal enum ServiceType : uint
        {
            /* SERVICE_KERNEL_DRIVER        */ KernelDriver = 0x00000001,
            /* SERVICE_FILE_SYSTEM_DRIVER   */ FileSystemDriver = 0x00000002,
            /* SERVICE_ADAPTER              */ Adapter = 0x00000004,
            /* SERVICE_RECOGNIZER_DRIVER    */ RecognizerDriver = 0x00000008,
            /* SERVICE_DRIVER               */ Driver = KernelDriver
                                                      | FileSystemDriver
                                                      | RecognizerDriver,
            /* SERVICE_WIN32_OWN_PROCESS    */ Win32OwnProcess = 0x00000010,
            /* SERVICE_WIN32_SHARE_PROCESS  */ Win32ShareProcess = 0x00000020,
            /* SERVICE_WIN32                */ Win32 = Win32OwnProcess
                                                     | Win32ShareProcess,
            /* SERVICE_USER_SERVICE         */ UserService = 0x00000040,
            /* SERVICE_USERSERVICE_INSTANCE */ UserServiceInstance = 0x00000080,
            /* SERVICE_USER_SHARE_PROCESS   */ UserShareProcess = UserService
                                                                | Win32ShareProcess,
            /* SERVICE_USER_OWN_PROCESS     */ UserOwnProcess = UserService
                                                              | Win32OwnProcess,
            /* SERVICE_INTERACTIVE_PROCESS  */ InteractiveProcess = 0x00000100,
            /* SERVICE_PKG_SERVICE          */ PkgService = 0x00000200,
            /* SERVICE_TYPE_ALL             */ All = Win32
                                                   | Adapter
                                                   | Driver
                                                   | InteractiveProcess
                                                   | UserService
                                                   | UserServiceInstance
                                                   | PkgService,
            /* SERVICE_NO_CHANGE            */ NoChange = 0xffffffff
        }

        /**
         * SID_NAME_USE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_sid_name_use
         */
        internal enum SidType
        {
            /* SidTypeUser           */ User = 1,
            /* SidTypeGroup          */ Group,
            /* SidTypeDomain         */ Domain,
            /* SidTypeAlias          */ Alias,
            /* SidTypeWellKnownGroup */ WellKnownGroup,
            /* SidTypeDeletedAccount */ DeletedAccount,
            /* SidTypeInvalid        */ Invalid,
            /* SidTypeUnknown        */ Unknown,
            /* SidTypeComputer       */ Computer,
            /* SidTypeLabel          */ Label
        }

        /**
         * START_TYPE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-changeserviceconfigw
         */
        internal enum StartType : uint
        {
            /* SERVICE_BOOT_START   */ BootStart = 0x00000000,
            /* SERVICE_SYSTEM_START */ SystemStart = 0x00000001,
            /* SERVICE_AUTO_START   */ AutoStart = 0x00000002,
            /* SERVICE_DEMAND_START */ DemandStart = 0x00000003,
            /* SERVICE_DISABLED     */ Disabled = 0x00000004,
            /* SERVICE_NO_CHANGE    */ NoChange = 0xffffffff
        }

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/access-rights-for-access-token-objects
         */
        [Flags]
        internal enum TokenAccessRight
        {
            /* TOKEN_ASSIGN_PRIMARY    */ AssignPrimary = 0x0001,
            /* TOKEN_DUPLICATE         */ Duplicate = 0x0002,
            /* TOKEN_IMPERSONATE       */ Impersonate = 0x0004,
            /* TOKEN_QUERY             */ Query = 0x0008,
            /* TOKEN_QUERY_SOURCE      */ QuerySource = 0x0010,
            /* TOKEN_ADJUST_PRIVILEGES */ AdjustPrivileges = 0x0020,
            /* TOKEN_ADJUST_GROUPS     */ AdjustGroups = 0x0040,
            /* TOKEN_ADJUST_DEFAULT    */ AdjustDefault = 0x0080,
            /* TOKEN_ADJUST_SESSIONID  */ AdjustSessionId = 0x0100,
            /* TOKEN_EXECUTE           */ Execute = 0x20000,
            /* TOKEN_READ              */ Read = 0x20000
                                               | Query,
            /* TOKEN_WRITE             */ Write = 0x20000
                                                | AdjustPrivileges
                                                | AdjustGroups
                                                | AdjustDefault,
            /* TOKEN_ALL_ACCESS        */ AllAccess = 0xf0000
                                                    | AssignPrimary
                                                    | Duplicate
                                                    | Impersonate
                                                    | Query
                                                    | QuerySource
                                                    | AdjustPrivileges
                                                    | AdjustGroups
                                                    | AdjustDefault
        }

        /**
         * QUERY_SERVICE_CONFIG structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-query_service_configw
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct QueryServiceConfig
        {
            public /* DWORD  */ ServiceType dwServiceType;
            public /* DWORD  */ StartType dwStartType;
            public /* DWORD  */ ErrorControlType dwErrorControl;
            public /* LPTSTR */ string lpBinaryPathName;
            public /* LPTSTR */ string lpLoadOrderGroup;
            public /* DWORD  */ uint dwTagId;
            public /* LPTSTR */ string lpDependencies;
            public /* LPTSTR */ string lpServiceStartName;
            public /* LPTSTR */ string lpDisplayName;
        }

        /**
         * SERVICE_STATUS structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct ServiceStatus
        {
            public /* DWORD */ ServiceType dwServiceType;
            public /* DWORD */ CurrentState dwCurrentState;
            public /* DWORD */ AcceptedControl dwControlAccepted;
            public /* DWORD */ uint dwWin32ExitCode;
            public /* DWORD */ uint dwServiceSpecificExitCode;
            public /* DWORD */ uint dwCheckPoint;
            public /* DWORD */ uint dwWaitHint;
        }

        /**
         * TOKEN_PRIVILEGES structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ns-winnt-_token_privileges
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokenPrivileges
        {
            public /* DWORD */ int Count;
            public /* LUID  */ long Luid;
            public /* DWORD */ SePrivilege Attr;
        }

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/securitybaseapi/nf-securitybaseapi-adjusttokenprivileges
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(
                /* _In_      HANDLE            */ [In] SafeTokenHandle tokenHandle,
                /* _In_      BOOL              */ [In] bool disableAllPrivileges,
                /* _In_opt_  PTOKEN_PRIVILEGES */ [In] ref TokenPrivileges newState,
                /* _In_      DWORD             */ [In] int bufferLength,
                /* _Out_opt_ PTOKEN_PRIVILEGES */ [In][Out] IntPtr previousState,
                /* _Out_opt_ PDWORD            */ [In][Out] IntPtr returnLength
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-changeserviceconfigw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ChangeServiceConfigW(
                /* _In_      SC_HANDLE */ [In] SafeServiceHandle hService,
                /* _In_      DWORD     */ [In] ServiceType dwServiceType,
                /* _In_      DWORD     */ [In] StartType dwStartType,
                /* _In_      DWORD     */ [In] ErrorControlType dwErrorControl,
                /* _In_opt_  LPCTSTR   */ [In] string lpBinaryPathName,
                /* _In_opt_  LPCTSTR   */ [In] string lpLoadOrderGroup,
                /* _Out_opt_ LPDWORD   */ [In][Out] IntPtr lpdwTagId,
                /* _In_opt_  LPCTSTR   */ [In] string lpDependencies,
                /* _In_opt_  LPCTSTR   */ [In] string lpServiceStartName,
                /* _In_opt_  LPCTSTR   */ [In] string lpPassword,
                /* _In_opt_  LPCTSTR   */ [In] string lpDisplayName
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-closeservicehandle
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseServiceHandle(
                /* _In_ SC_HANDLE */ [In] IntPtr hScObject
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/sddl/nf-sddl-convertsidtostringsidw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ConvertSidToStringSidW(
                /* _In_  PSID    */ [In] IntPtr pSid,
                /* _Out_ LPTSTR* */ [In][Out] ref string sid
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/sddl/nf-sddl-convertstringsidtosidw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ConvertStringSidToSidW(
                /* _In_  LPCTSTR */ [In] string sid,
                /* _Out_ PSID*   */ [In][Out] IntPtr pSid
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-lookupaccountsidw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LookupAccountSidW(
                /* _In_opt_  LPCTSTR       */ [In] string lpSystemName,
                /* _In_      PSID          */ [In] IntPtr lpSid,
                /* _Out_opt_ LPTSTR        */ [In][Out] StringBuilder lpName,
                /* _Inout_   LPDWORD       */ [In][Out] ref uint cchName,
                /* _Out_opt_ LPTSTR        */ [In][Out] StringBuilder lpReferencedDomainName,
                /* _Inout_   LPDWORD       */ [In][Out] ref uint cchReferencedDomainName,
                /* _Out_     PSID_NAME_USE */ [In][Out] ref SidType peUse
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-lookupprivilegevaluew
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool LookupPrivilegeValueW(
                /* _In_opt_ LPCTSTR */ [In] string lpSystemName,
                /* _In_     LPCTSTR */ [In] string lpName,
                /* _Out_    PLUID   */ [In][Out] ref long lpLuid
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/nf-processthreadsapi-openprocesstoken
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool OpenProcessToken(
                /* _In_  HANDLE  */ [In] SafeProcessHandle processHandle,
                /* _In_  DWORD   */ [In] TokenAccessRight desiredAccess,
                /* _Out_ PHANDLE */ [Out] out SafeTokenHandle tokenHandle
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-openscmanagerw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeServiceHandle OpenSCManagerW(
                /* _In_opt_ LPCTSTR */ [In] string lpMachineName,
                /* _In_opt_ LPCTSTR */ [In] string lpDatabaseName,
                /* _In_     DWORD   */ [In] ServiceControlManagerAccessRight dwDesiredAccess
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-openservicew
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeServiceHandle OpenServiceW(
                /* _In_ SC_HANDLE */ [In] SafeServiceHandle hScManager,
                /* _In_ LPCTSTR   */ [In] string lpServiceName,
                /* _In_ DWORD     */ [In] ServiceAccessRight dwDesiredAccess
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-queryserviceconfigw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool QueryServiceConfigW(
                /* _In_      SC_HANDLE              */ [In] SafeServiceHandle hService,
                /* _Out_opt_ LPQUERY_SERVICE_CONFIG */ [In][Out] IntPtr lpServiceConfig,
                /* _In_      DWORD                  */ [In] uint cbBufSize,
                /* _Out_     LPDWORD                */ [In][Out] ref uint pcbBytesNeeded
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-queryservicestatus
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool QueryServiceStatus(
                /* _In_  SC_HANDLE        */ [In] SafeServiceHandle hService,
                /* _Out_ LPSERVICE_STATUS */ [In][Out] ref ServiceStatus lpServiceStatus
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-startservicew
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool StartServiceW(
                /* _In_     SC_HANDLE */ [In] SafeServiceHandle hService,
                /* _In_     DWORD     */ [In] uint dwNumServiceArgs,
                /* _In_opt_ LPCTSTR*  */ [In] string[] lpServiceArgVectors
        );
    }
}
