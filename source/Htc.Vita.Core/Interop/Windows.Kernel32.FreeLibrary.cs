using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Kernel32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FreeLibrary(
                    [In] IntPtr hModule
            );
        }
    }
}
