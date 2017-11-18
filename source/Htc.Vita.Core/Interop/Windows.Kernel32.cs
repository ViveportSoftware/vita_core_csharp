using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static class Kernel32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
             */
            internal enum CREATION_DISPOSITION : uint
            {
                /* CREATE_NEW        */ CREATE_NEW = 1,
                /* CREATE_ALWAYS     */ CREATE_ALWAYS = 2,
                /* OPEN_EXISTING     */ OPEN_EXISTING = 3,
                /* OPEN_ALWAYS       */ OPEN_ALWAYS = 4,
                /* TRUNCATE_EXISTING */ TRUNCATE_EXISTING = 5
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/gg258117.aspx
             */
            [Flags]
            internal enum FILE_ATTRIBUTE_FLAG : uint
            {
                /* FILE_ATTRIBUTE_READONLY              */ FILE_ATTRIBUTE_READONLY = 0x00000001,
                /* FILE_ATTRIBUTE_HIDDEN                */ FILE_ATTRIBUTE_HIDDEN = 0x00000002,
                /* FILE_ATTRIBUTE_SYSTEM                */ FILE_ATTRIBUTE_SYSTEM = 0x00000004,
                /* FILE_ATTRIBUTE_DIRECTORY             */ FILE_ATTRIBUTE_DIRECTORY = 0x00000010,
                /* FILE_ATTRIBUTE_ARCHIVE               */ FILE_ATTRIBUTE_ARCHIVE = 0x00000020,
                /* FILE_ATTRIBUTE_DEVICE                */ FILE_ATTRIBUTE_DEVICE = 0x00000040,
                /* FILE_ATTRIBUTE_NORMAL                */ FILE_ATTRIBUTE_NORMAL = 0x00000080,
                /* FILE_ATTRIBUTE_TEMPORARY             */ FILE_ATTRIBUTE_TEMPORARY = 0x00000100,
                /* FILE_ATTRIBUTE_SPARSE_FILE           */ FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200,
                /* FILE_ATTRIBUTE_REPARSE_POINT         */ FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400,
                /* FILE_ATTRIBUTE_COMPRESSED            */ FILE_ATTRIBUTE_COMPRESSED = 0x00000800,
                /* FILE_ATTRIBUTE_OFFLINE               */ FILE_ATTRIBUTE_OFFLINE = 0x00001000,
                /* FILE_ATTRIBUTE_NOT_CONTENT_INDEXED   */ FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
                /* FILE_ATTRIBUTE_ENCRYPTED             */ FILE_ATTRIBUTE_ENCRYPTED = 0x00004000,
                /* FILE_ATTRIBUTE_INTEGRITY_STREAM      */ FILE_ATTRIBUTE_INTEGRITY_STREAM = 0x00008000,
                /* FILE_ATTRIBUTE_VIRTUAL               */ FILE_ATTRIBUTE_VIRTUAL = 0x00010000,
                /* FILE_ATTRIBUTE_NO_SCRUB_DATA         */ FILE_ATTRIBUTE_NO_SCRUB_DATA = 0x00020000,
                /* FILE_ATTRIBUTE_RECALL_ON_OPEN        */ FILE_ATTRIBUTE_RECALL_ON_OPEN = 0x00040000,
                /* FILE_FLAG_OPEN_NO_RECALL             */ FILE_FLAG_OPEN_NO_RECALL = 0x00100000,
                /* FILE_FLAG_OPEN_REPARSE_POINT         */ FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000,
                /* FILE_ATTRIBUTE_RECALL_ON_DATA_ACCESS */ FILE_ATTRIBUTE_RECALL_ON_DATA_ACCESS = 0x00400000,
                /* FILE_FLAG_SESSION_AWARE              */ FILE_FLAG_SESSION_AWARE = 0x00800000,
                /* FILE_FLAG_POSIX_SEMANTICS            */ FILE_FLAG_POSIX_SEMANTICS = 0x01000000,
                /* FILE_FLAG_BACKUP_SEMANTICS           */ FILE_FLAG_BACKUP_SEMANTICS = 0x02000000,
                /* FILE_FLAG_DELETE_ON_CLOSE            */ FILE_FLAG_DELETE_ON_CLOSE = 0x04000000,
                /* FILE_FLAG_SEQUENTIAL_SCAN            */ FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000,
                /* FILE_FLAG_RANDOM_ACCESS              */ FILE_FLAG_RANDOM_ACCESS = 0x10000000,
                /* FILE_FLAG_NO_BUFFERING               */ FILE_FLAG_NO_BUFFERING = 0x20000000,
                /* FILE_FLAG_OVERLAPPED                 */ FILE_FLAG_OVERLAPPED = 0x40000000,
                /* FILE_FLAG_WRITE_THROUGH              */ FILE_FLAG_WRITE_THROUGH = 0x80000000
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
             */
            [Flags]
            internal enum FILE_SHARE : uint
            {
                /* FILE_SHARE_NONE   */ FILE_SHARE_NONE = 0x00000000,
                /* FILE_SHARE_READ   */ FILE_SHARE_READ = 0x00000001,
                /* FILE_SHARE_WRITE  */ FILE_SHARE_WRITE = 0x00000002,
                /* FILE_SHARE_DELETE */ FILE_SHARE_DELETE = 0x00000004
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
             */
            [Flags]
            internal enum GENERIC : uint
            {
                /* GENERIC_ALL     */ GENERIC_ALL = 0x10000000,
                /* GENERIC_EXECUTE */ GENERIC_EXECUTE = 0x20000000,
                /* GENERIC_WRITE   */ GENERIC_WRITE = 0x40000000,
                /* GENERIC_READ    */ GENERIC_READ = 0x80000000
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms724211.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool CloseHandle(
                    /* _In_ HANDLE */ [In] IntPtr hObject
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            internal static extern IntPtr CreateFileW(
                    /* _In_     LPCTSTR               */ [In] string lpFileName,
                    /* _In_     DWORD                 */ [In] GENERIC dwDesiredAccess,
                    /* _In_     DWORD                 */ [In] FILE_SHARE dwShareMode,
                    /* _In_opt_ LPSECURITY_ATTRIBUTES */ [In] IntPtr lpSecurityAttributes,
                    /* _In_     DWORD                 */ [In] CREATION_DISPOSITION dwCreationDisposition,
                    /* _In_     DWORD                 */ [In] FILE_ATTRIBUTE_FLAG dwFlagsAndAttributes,
                    /* _In_opt_ HANDLE                */ [In] IntPtr hTemplateFile
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FreeLibrary(
                    /* _In_ HMODULE */ [In] IntPtr hModule
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa364937.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
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
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684139.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool IsWow64Process(
                    /* _In_  HANDLE */ [In] IntPtr hProcess,
                    /* _Out_ PBOOL  */ [In][Out] ref bool wow64Process
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684175.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            internal static extern IntPtr LoadLibraryW(
                    /* _In_ LPCTSTR */ [In] string lpFileName
            );
        }
    }
}
