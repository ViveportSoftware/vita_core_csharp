using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
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
                /* _In_      DWORD     */ [In] ServiceStartType dwStartType,
                /* _In_      DWORD     */ [In] ServiceErrorControl dwErrorControl,
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
         * https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/nf-processthreadsapi-createprocessasuserw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CreateProcessAsUserW(
                /* _In_opt_    HANDLE                */ [In] SafeTokenHandle hToken,
                /* _In_opt_    LPCWSTR               */ [In] string lpApplicationName,
                /* _Inout_opt_ LPWSTR                */ [In] string lpCommandLine,
                /* _In_opt_    LPSECURITY_ATTRIBUTES */ [In] ref SecurityAttributes lpProcessAttributes,
                /* _In_opt_    LPSECURITY_ATTRIBUTES */ [In] ref SecurityAttributes lpThreadAttributes,
                /* _In_        BOOL                  */ [In] bool bInheritHandle,
                /* _In_        DWORD                 */ [In] ProcessCreationFlag dwCreationFlags,
                /* _In_opt_    LPVOID                */ [In] IntPtr lpEnvironment,
                /* _In_opt_    LPCWSTR               */ [In] string lpCurrentDirectory,
                /* _In_        LPSTARTUPINFOW        */ [In] ref StartupInfo lpStartupInfo,
                /* _Out_       LPPROCESS_INFORMATION */ [Out] out ProcessInformation lpProcessInformation
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/securitybaseapi/nf-securitybaseapi-duplicatetokenex
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DuplicateTokenEx(
                /* _In_     HANDLE                       */ [In] SafeTokenHandle existingToken,
                /* _In_     DWORD                        */ [In] TokenAccessRight desiredAccess,
                /* _In_opt_ LPSECURITY_ATTRIBUTES        */ [In][Out] ref SecurityAttributes threadAttributes,
                /* _In_     SECURITY_IMPERSONATION_LEVEL */ [In] SecurityImpersonationLevel impersonationLevel,
                /* _In_     TOKEN_TYPE                   */ [In] TokenType tokenType,
                /* _Outptr_ PHANDLE                      */ [Out] out SafeTokenHandle newToken
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/securitybaseapi/nf-securitybaseapi-gettokeninformation
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool GetTokenInformation(
                /* _In_  HANDLE                  */ [In] SafeTokenHandle tokenHandle,
                /* _In_  TOKEN_INFORMATION_CLASS */ [In] TokenInformationClass tokenInformationClass,
                /* _Out_ LPVOID                  */ [In][Out] IntPtr tokenInformation,
                /* _In_  DWORD                   */ [In] uint tokenInformationLength,
                /* _Out_ PDWORD                  */ [Out] out uint returnLength
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
