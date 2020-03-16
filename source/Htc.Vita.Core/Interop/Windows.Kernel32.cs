using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/handleapi/nf-handleapi-closehandle
         */
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(
                /* _In_ HANDLE */ [In] IntPtr hObject
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createfilew
         */
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeFileHandle CreateFileW(
                /* _In_     LPCTSTR               */ [In] string lpFileName,
                /* _In_     DWORD                 */ [In] GenericAccessRight dwDesiredAccess,
                /* _In_     DWORD                 */ [In] FileShare dwShareMode,
                /* _In_opt_ LPSECURITY_ATTRIBUTES */ [In] IntPtr lpSecurityAttributes,
                /* _In_     DWORD                 */ [In] FileCreationDisposition dwCreationDisposition,
                /* _In_     DWORD                 */ [In] FileAttributeFlag dwFlagsAndAttributes,
                /* _In_opt_ HANDLE                */ [In] IntPtr hTemplateFile
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/libloaderapi/nf-libloaderapi-freelibrary
         */
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(
                /* _In_ HMODULE */ [In] IntPtr hModule
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/nf-processthreadsapi-getcurrentprocess
         */
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeProcessHandle GetCurrentProcess();

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getdiskfreespaceexw
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-getnamedpipeclientprocessid
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-getnamedpipeserverprocessid
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wow64apiset/nf-wow64apiset-iswow64process2
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/libloaderapi/nf-libloaderapi-loadlibraryw
         */
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern IntPtr LoadLibraryW(
                /* _In_ LPCTSTR */ [In] string lpFileName
        );

        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeProcessHandle OpenProcess(
                /* _In_ DWORD */ [In] ProcessAccessRight dwDesiredAccess,
                /* _In_ BOOL  */ [In] bool bInheritHandle,
                /* _In_ DWORD */ [In] uint dwProcessId
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-queryfullprocessimagenamew
         */
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
