using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * CREATION_DISPOSITION enumeration
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
         */
        internal enum CreationDisposition : uint
        {
            /* CREATE_NEW        */ CreateNew = 1,
            /* CREATE_ALWAYS     */ CreateAlways = 2,
            /* OPEN_EXISTING     */ OpenExisting = 3,
            /* OPEN_ALWAYS       */ OpenAlways = 4,
            /* TRUNCATE_EXISTING */ TruncateExisting = 5
        }

        /**
         * FILE_ATTRIBUTE_FLAG enumeration
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
         * https://msdn.microsoft.com/en-us/library/windows/desktop/gg258117.aspx
         */
        [Flags]
        internal enum FileAttributeFlag : uint
        {
            /* FILE_ATTRIBUTE_READONLY              */ AttributeReadonly = 0x00000001,
            /* FILE_ATTRIBUTE_HIDDEN                */ AttributeHidden = 0x00000002,
            /* FILE_ATTRIBUTE_SYSTEM                */ AttributeSystem = 0x00000004,
            /* FILE_ATTRIBUTE_DIRECTORY             */ AttributeDirectory = 0x00000010,
            /* FILE_ATTRIBUTE_ARCHIVE               */ AttributeArchive = 0x00000020,
            /* FILE_ATTRIBUTE_DEVICE                */ AttributeDevice = 0x00000040,
            /* FILE_ATTRIBUTE_NORMAL                */ AttributeNormal = 0x00000080,
            /* FILE_ATTRIBUTE_TEMPORARY             */ AttributeTemporary = 0x00000100,
            /* FILE_ATTRIBUTE_SPARSE_FILE           */ AttributeSparseFile = 0x00000200,
            /* FILE_ATTRIBUTE_REPARSE_POINT         */ AttributeReparsePoint = 0x00000400,
            /* FILE_ATTRIBUTE_COMPRESSED            */ AttributeCompressed = 0x00000800,
            /* FILE_ATTRIBUTE_OFFLINE               */ AttributeOffline = 0x00001000,
            /* FILE_ATTRIBUTE_NOT_CONTENT_INDEXED   */ AttributeNotContentIndexed = 0x00002000,
            /* FILE_ATTRIBUTE_ENCRYPTED             */ AttributeEncrypted = 0x00004000,
            /* FILE_ATTRIBUTE_INTEGRITY_STREAM      */ AttributeIntegrityStream = 0x00008000,
            /* FILE_ATTRIBUTE_VIRTUAL               */ AttributeVirtual = 0x00010000,
            /* FILE_ATTRIBUTE_NO_SCRUB_DATA         */ AttributeNoScrubData = 0x00020000,
            /* FILE_ATTRIBUTE_RECALL_ON_OPEN        */ AttributeRecallOnOpen = 0x00040000,
            /* FILE_FLAG_OPEN_NO_RECALL             */ FlagOpenNoRecall = 0x00100000,
            /* FILE_FLAG_OPEN_REPARSE_POINT         */ FlagOpenReparsePoint = 0x00200000,
            /* FILE_ATTRIBUTE_RECALL_ON_DATA_ACCESS */ AttributeRecallOnDataAccess = 0x00400000,
            /* FILE_FLAG_SESSION_AWARE              */ FlagSessionAware = 0x00800000,
            /* FILE_FLAG_POSIX_SEMANTICS            */ FlagPosixSemantics = 0x01000000,
            /* FILE_FLAG_BACKUP_SEMANTICS           */ FlagBackupSemantics = 0x02000000,
            /* FILE_FLAG_DELETE_ON_CLOSE            */ FlagDeleteOnClose = 0x04000000,
            /* FILE_FLAG_SEQUENTIAL_SCAN            */ FlagSequentialScan = 0x08000000,
            /* FILE_FLAG_RANDOM_ACCESS              */ FlagRandomAccess = 0x10000000,
            /* FILE_FLAG_NO_BUFFERING               */ FlagNoBuffering = 0x20000000,
            /* FILE_FLAG_OVERLAPPED                 */ FlagOverlapped = 0x40000000,
            /* FILE_FLAG_WRITE_THROUGH              */ FlagWriteThrough = 0x80000000
        }

        internal enum ImageFileMachine : ushort
        {
            /* IMAGE_FILE_MACHINE_UNKNOWN     */ Unknown = 0,
            /* IMAGE_FILE_MACHINE_TARGET_HOST */ TargetHost = 0x0001,
            /* IMAGE_FILE_MACHINE_I386        */ I386 = 0x014c,
            /* IMAGE_FILE_MACHINE_R3000       */ R3000 = 0x0162,
            /* IMAGE_FILE_MACHINE_R4000       */ R4000 = 0x0166,
            /* IMAGE_FILE_MACHINE_R10000      */ R10000 = 0x0168,
            /* IMAGE_FILE_MACHINE_WCEMIPSV2   */ WceMipsV2 = 0x0169,
            /* IMAGE_FILE_MACHINE_ALPHA       */ Alpha = 0x0184,
            /* IMAGE_FILE_MACHINE_SH3         */ Sh3 = 0x01a2,
            /* IMAGE_FILE_MACHINE_SH3DSP      */ Sh3Dsp = 0x01a3,
            /* IMAGE_FILE_MACHINE_SH3E        */ Sh3E = 0x01a4,
            /* IMAGE_FILE_MACHINE_SH4         */ Sh4 = 0x01a6,
            /* IMAGE_FILE_MACHINE_SH5         */ Sh5 = 0x01a8,
            /* IMAGE_FILE_MACHINE_ARM         */ Arm = 0x01c0,
            /* IMAGE_FILE_MACHINE_THUMB       */ Thumb = 0x01c2,
            /* IMAGE_FILE_MACHINE_ARMNT       */ ArmNT = 0x01c4,
            /* IMAGE_FILE_MACHINE_AM33        */ Am33 = 0x01d3,
            /* IMAGE_FILE_MACHINE_POWERPC     */ PowerPC = 0x01F0,
            /* IMAGE_FILE_MACHINE_POWERPCFP   */ PowerPCFp = 0x01f1,
            /* IMAGE_FILE_MACHINE_IA64        */ Ia64 = 0x0200,
            /* IMAGE_FILE_MACHINE_MIPS16      */ Mips16 = 0x0266,
            /* IMAGE_FILE_MACHINE_ALPHA64     */ Alpha64= 0x0284,
            /* IMAGE_FILE_MACHINE_MIPSFPU     */ MipsFpu = 0x0366,
            /* IMAGE_FILE_MACHINE_MIPSFPU16   */ MipsFpu16 = 0x0466,
            /* IMAGE_FILE_MACHINE_AXP64       */ Axp64 = Alpha64,
            /* IMAGE_FILE_MACHINE_TRICORE     */ TriCore = 0x0520,
            /* IMAGE_FILE_MACHINE_CEF         */ Cef = 0x0CEF,
            /* IMAGE_FILE_MACHINE_EBC         */ Ebc = 0x0EBC,
            /* IMAGE_FILE_MACHINE_AMD64       */ Amd64 = 0x8664,
            /* IMAGE_FILE_MACHINE_M32R        */ M32R = 0x9041,
            /* IMAGE_FILE_MACHINE_ARM64       */ Arm64 = 0xAA64,
            /* IMAGE_FILE_MACHINE_CEE         */ Cee = 0xC0EE
        }

        /**
         * FILE_SHARE enumeration
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
         */
        [Flags]
        internal enum FileShare : uint
        {
            /* FILE_SHARE_NONE   */ None = 0x00000000,
            /* FILE_SHARE_READ   */ Read = 0x00000001,
            /* FILE_SHARE_WRITE  */ Write = 0x00000002,
            /* FILE_SHARE_DELETE */ Delete = 0x00000004
        }

        /**
         * GENERIC enumeration
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
         */
        [Flags]
        internal enum Generic : uint
        {
            /* GENERIC_ALL     */ All = 0x10000000,
            /* GENERIC_EXECUTE */ Execute = 0x20000000,
            /* GENERIC_WRITE   */ Write = 0x40000000,
            /* GENERIC_READ    */ Read = 0x80000000
        }

        /**
         * https://msdn.microsoft.com/en-us/library/windows/desktop/ms724211.aspx
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
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
         */
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeFileHandle CreateFileW(
                /* _In_     LPCTSTR               */ [In] string lpFileName,
                /* _In_     DWORD                 */ [In] Generic dwDesiredAccess,
                /* _In_     DWORD                 */ [In] FileShare dwShareMode,
                /* _In_opt_ LPSECURITY_ATTRIBUTES */ [In] IntPtr lpSecurityAttributes,
                /* _In_     DWORD                 */ [In] CreationDisposition dwCreationDisposition,
                /* _In_     DWORD                 */ [In] FileAttributeFlag dwFlagsAndAttributes,
                /* _In_opt_ HANDLE                */ [In] IntPtr hTemplateFile
        );

        /**
         * https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
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
         * https://msdn.microsoft.com/en-us/library/windows/desktop/ms683179.aspx
         */
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeProcessHandle GetCurrentProcess();

        /**
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa364937.aspx
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
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa365440.aspx
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
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa365446.aspx
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
         * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684139.aspx
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
         * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684175.aspx
         */
        [DllImport(Libraries.WindowsKernel32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern IntPtr LoadLibraryW(
                /* _In_ LPCTSTR */ [In] string lpFileName
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
