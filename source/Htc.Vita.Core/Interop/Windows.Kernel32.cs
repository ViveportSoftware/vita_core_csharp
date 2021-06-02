using System;
using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Util;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/handleapi/nf-handleapi-closehandle")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(
                /* _In_ HANDLE */ [In] IntPtr hObject
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createfilew")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeFileHandle CreateFileW(
                /* _In_     LPCTSTR               */ [In] string lpFileName,
                /* _In_     DWORD                 */ [In] GenericAccessRights dwDesiredAccess,
                /* _In_     DWORD                 */ [In] FileShareModes dwShareMode,
                /* _In_opt_ LPSECURITY_ATTRIBUTES */ [In] IntPtr lpSecurityAttributes,
                /* _In_     DWORD                 */ [In] FileCreationDisposition dwCreationDisposition,
                /* _In_     DWORD                 */ [In] FileAttributeFlags dwFlagsAndAttributes,
                /* _In_opt_ HANDLE                */ [In] IntPtr hTemplateFile
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createnamedpipea")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafePipeHandle CreateNamedPipeW(
                /* _In_     LPCTSTR               */ [In] string lpName,
                /* _In_     DWORD                 */ [In] PipeOpenModes dwOpenMode,
                /* _In_     DWORD                 */ [In] PipeModes dwPipeMode,
                /* _In_     DWORD                 */ [In] uint nMaxInstances,
                /* _In_     DWORD                 */ [In] uint nOutBufferSize,
                /* _In_     DWORD                 */ [In] uint nInBufferSize,
                /* _In_     DWORD                 */ [In] uint nDefaultTimeOut,
                /* _In_opt_ LPSECURITY_ATTRIBUTES */ [In] SecurityAttributes lpSecurityAttributes
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/libloaderapi/nf-libloaderapi-freelibrary")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(
                /* _In_ HMODULE */ [In] IntPtr hModule
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/nf-processthreadsapi-getcurrentprocess")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeProcessHandle GetCurrentProcess();

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getdiskfreespaceexw")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetDiskFreeSpaceExW(
                /* _In_opt_  LPCTSTR         */ [In] string lpDirectoryName,
                /* _Out_opt_ PULARGE_INTEGER */ [In][Out] ref ulong lpFreeBytesAvailable,
                /* _Out_opt_ PULARGE_INTEGER */ [In][Out] ref ulong lpTotalNumberOfBytes,
                /* _Out_opt_ PULARGE_INTEGER */ [In][Out] ref ulong lpTotalNumberOfFreeBytes
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-getnamedpipeclientprocessid")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNamedPipeClientProcessId(
                /* _In_  HANDLE */ [In] SafePipeHandle pipe,
                /* _Out_ PULONG */ [In][Out] ref uint clientProcessId
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-getnamedpipeserverprocessid")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNamedPipeServerProcessId(
                /* _In_  HANDLE */ [In] SafePipeHandle pipe,
                /* _Out_ PULONG */ [In][Out] ref uint serverProcessId
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-getprivateprofilestringw")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern uint GetPrivateProfileStringW(
                /* _In_opt_            LPCWSTR */ [In] string lpAppName,
                /* _In_opt_            LPCWSTR */ [In] string lpKeyName,
                /* _In_opt_            LPCWSTR */ [In] string lpDefault,
                /* _Out_writes_to_opt_ LPWSTR  */ [Out] StringBuilder lpReturnedString,
                /* _In_                DWORD   */ [In] int nSize,
                /* _In_opt_            LPCWSTR */ [In] string lpFileName
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getversionexw")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetVersionExW(
                /* _Inout_ LPOSVERSIONINFO */ [In][Out] ref OsVersionInfoExW lpVersionInfo
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWow64Process(
                /* _In_  HANDLE */ [In] SafeProcessHandle hProcess,
                /* _Out_ PBOOL  */ [In][Out] ref bool wow64Process
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process2")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWow64Process2(
                /* _In_      HANDLE */ [In] SafeProcessHandle hProcess,
                /* _Out_     USHORT */ [In][Out] ref ImageFileMachine processMachine,
                /* _Out_opt_ USHORT */ [In][Out] ref ImageFileMachine nativeMachine
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/libloaderapi/nf-libloaderapi-loadlibraryw")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern IntPtr LoadLibraryW(
                /* _In_ LPCTSTR */ [In] string lpFileName
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocess")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeProcessHandle OpenProcess(
                /* _In_ DWORD */ [In] ProcessAccessRights dwDesiredAccess,
                /* _In_ BOOL  */ [In] bool bInheritHandle,
                /* _In_ DWORD */ [In] uint dwProcessId
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-queryfullprocessimagenamew")]
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool QueryFullProcessImageNameW(
                /* _In_    HANDLE */ [In] SafeProcessHandle hProcess,
                /* _In_    DWORD  */ [In] int dwFlags,
                /* _Out_   LPWSTR */ [Out] StringBuilder lpExeName,
                /* _Inout_ PDWORD */ [In][Out] ref int lpdwSize
        );
    }
}
