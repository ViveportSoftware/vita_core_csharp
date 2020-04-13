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
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regclosekey
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegCloseKey(
                /* _In_ HKEY */ [In] IntPtr hKey
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regcreatekeyexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegCreateKeyExW(
                /* _In_       HKEY                  */ [In] SafeRegistryHandle hKey,
                /* _In_       LPCWSTR               */ [In] string lpSubKey,
                /* _Reserved_ DWORD                 */ [In] IntPtr reserved,
                /* _In_opt_   LPWSTR                */ [In] string lpClass,
                /* _In_       DWORD                 */ [In] int dwOptions,
                /* _In_       REGSAM                */ [In] RegistryKeyAccessRight samDesired,
                /* _In_opt_   LPSECURITY_ATTRIBUTES */ [In][Out] ref SecurityAttributes lpSecurityAttributes,
                /* _Out_      PHKEY                 */ [Out] out SafeRegistryHandle phkResult,
                /* _Out_opt_  LPDWORD               */ [Out] out int lpdwDisposition
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regdeletekeyexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegDeleteKeyExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_       LPCWSTR */ [In] string lpSubKey,
                /* _In_       REGSAM  */ [In] RegistryKeyAccessRight samDesired,
                /* _Reserved_ DWORD   */ [In] IntPtr reserved
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regdeletevaluew
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegDeleteValueW(
                /* _In_     HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_ LPCWSTR */ [In] string lpValueName
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regenumkeyexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegEnumKeyExW(
                /* _In_        HKEY      */ [In] SafeRegistryHandle hKey,
                /* _In_        DWORD     */ [In] uint index,
                /* _Out_opt_   LPWSTR    */ [In] char[] lpName,
                /* _Inout_     LPDWORD   */ [In][Out] ref int lpcbName,
                /* _Reserved_  LPDWORD   */ [In] IntPtr reserved,
                /* _Out_opt_   LPWSTR    */ [In][Out] StringBuilder lpClass,
                /* _Inout_opt_ LPDWORD   */ [In][Out] ref uint lpcbClass,
                /* _Out_opt_   PFILETIME */ [In][Out] IntPtr lpftLastWriteTime
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regenumvaluew
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegEnumValueW(
                /* _In_        HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_        DWORD   */ [In] uint dwIndex,
                /* _Out_opt_   LPWSTR  */ [In] char[] lpValueName,
                /* _Inout_     LPDWORD */ [In][Out] ref int lpcchValueName,
                /* _Reserved_  LPDWORD */ [In] IntPtr lpReserved,
                /* _Out_opt_   LPDWORD */ [In][Out] IntPtr lpType,
                /* _Out_opt_   LPBYTE  */ [In][Out] IntPtr lpData,
                /* _Inout_opt_ LPDWORD */ [In][Out] IntPtr lpcbData
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regopenkeyexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegOpenKeyExW(
                /* _In_     HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_ LPCWSTR */ [In] string subKey,
                /* _In_opt_ DWORD   */ [In] int ulOptions,
                /* _In_     REGSAM  */ [In] RegistryKeyAccessRight samDesired,
                /* _Out_    PHKEY   */ [Out] out SafeRegistryHandle hkResult
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regqueryinfokeyw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegQueryInfoKeyW(
                /* _In_        HKEY      */ [In] SafeRegistryHandle hKey,
                /* _Out_opt_   LPWSTR    */ [In][Out] StringBuilder lpClass,
                /* _Inout_opt_ LPDWORD   */ [In][Out] ref uint lpcchClass,
                /* _Reserved_  LPDWORD   */ [In] IntPtr lpReserved,
                /* _Out_opt_   LPDWORD   */ [In][Out] ref uint lpcSubKeys,
                /* _Out_opt_   LPDWORD   */ [In][Out] ref uint lpcbMaxSubKeyLen,
                /* _Out_opt_   LPDWORD   */ [In][Out] ref uint lpcbMaxClassLen,
                /* _Out_opt_   LPDWORD   */ [In][Out] ref uint lpcValues,
                /* _Out_opt_   LPDWORD   */ [In][Out] ref uint lpcbMaxValueNameLen,
                /* _Out_opt_   LPDWORD   */ [In][Out] ref uint lpcbMaxValueLen,
                /* _Out_opt_   LPDWORD   */ [In][Out] IntPtr lpcbSecurityDescriptor,
                /* _Out_opt_   PFILETIME */ [In][Out] IntPtr lpftLastWriteTime
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regqueryvalueexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegQueryValueExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_   LPCWSTR */ [In] string lpValueName,
                /* _Reserved_ LPDWORD */ [In] IntPtr lpReserved,
                /* _Out_opt_  LPDWORD */ [In][Out] ref RegistryValueType lpType,
                /* _Out_      LPBYTE  */ [Out] byte[] lpData,
                /* _Out_opt_  LPDWORD */ [In][Out] ref uint lpcbData
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regqueryvalueexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegQueryValueExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_   LPCWSTR */ [In] string lpValueName,
                /* _Reserved_ LPDWORD */ [In] IntPtr lpReserved,
                /* _Out_opt_  LPDWORD */ [In][Out] ref RegistryValueType lpType,
                /* _Out_      LPBYTE  */ [Out] char[] lpData,
                /* _Out_opt_  LPDWORD */ [In][Out] ref uint lpcbData
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regqueryvalueexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegQueryValueExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_   LPCWSTR */ [In] string lpValueName,
                /* _Reserved_ LPDWORD */ [In] IntPtr lpReserved,
                /* _Out_opt_  LPDWORD */ [In][Out] ref RegistryValueType lpType,
                /* _Out_      LPBYTE  */ [In][Out] ref int lpData,
                /* _Out_opt_  LPDWORD */ [In][Out] ref uint lpcbData
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regqueryvalueexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegQueryValueExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_   LPCWSTR */ [In] string lpValueName,
                /* _Reserved_ LPDWORD */ [In] IntPtr lpReserved,
                /* _Out_opt_  LPDWORD */ [In][Out] ref RegistryValueType lpType,
                /* _Out_      LPBYTE  */ [In][Out] ref long lpData,
                /* _Out_opt_  LPDWORD */ [In][Out] ref uint lpcbData
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regsetvalueexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegSetValueExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_   LPCWSTR */ [In] string lpValueName,
                /* _Reserved_ DWORD   */ [In] int reserved,
                /* _In_       DWORD   */ [In] RegistryValueType dwType,
                /* _In_opt_   BYTE*   */ [In] ref int lpData,
                /* _In_       DWORD   */ [In] int cbData
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regsetvalueexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegSetValueExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_   LPCWSTR */ [In] string lpValueName,
                /* _Reserved_ DWORD   */ [In] int reserved,
                /* _In_       DWORD   */ [In] RegistryValueType dwType,
                /* _In_opt_   BYTE*   */ [In] ref long lpData,
                /* _In_       DWORD   */ [In] int cbData
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regsetvalueexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegSetValueExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_   LPCWSTR */ [In] string lpValueName,
                /* _Reserved_ DWORD   */ [In] int reserved,
                /* _In_       DWORD   */ [In] RegistryValueType dwType,
                /* _In_opt_   BYTE*   */ [In] string lpData,
                /* _In_       DWORD   */ [In] int cbData
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regsetvalueexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegSetValueExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_   LPCWSTR */ [In] string lpValueName,
                /* _Reserved_ DWORD   */ [In] int reserved,
                /* _In_       DWORD   */ [In] RegistryValueType dwType,
                /* _In_opt_   BYTE*   */ [In] byte[] lpData,
                /* _In_       DWORD   */ [In] int cbData
        );

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regsetvalueexw
         */
        [DllImport(Libraries.WindowsAdvapi32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error RegSetValueExW(
                /* _In_       HKEY    */ [In] SafeRegistryHandle hKey,
                /* _In_opt_   LPCWSTR */ [In] string lpValueName,
                /* _Reserved_ DWORD   */ [In] int reserved,
                /* _In_       DWORD   */ [In] RegistryValueType dwType,
                /* _In_opt_   BYTE*   */ [In] char[] lpData,
                /* _In_       DWORD   */ [In] int cbData
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
