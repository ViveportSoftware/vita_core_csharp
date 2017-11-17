using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Kernel32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms724211.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CloseHandle(
                    [In] IntPtr hObject
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            public static extern IntPtr CreateFileW(
                    [In] string fileName,
                    [In] [MarshalAs(UnmanagedType.U4)] GENERIC desiredAccess,
                    [In] [MarshalAs(UnmanagedType.U4)] FILE_SHARE shareMode,
                    IntPtr lpSecurityAttributes,
                    [In] [MarshalAs(UnmanagedType.U4)] CREATION_DISPOSITION creationDisposition,
                    [In] [MarshalAs(UnmanagedType.U4)] FILE_ATTRIBUTE_FLAG flagsAndAttributes,
                    IntPtr hTemplateFile
            );
        }
    }
}
