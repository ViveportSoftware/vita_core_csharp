using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wtsapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/aa383831.aspx
             */
            [DllImport(Libraries.Windows_wtsapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool WTSEnumerateProcessesW(
                    [In] IntPtr hServer,
                    [In] [MarshalAs(UnmanagedType.U4)] int reserved,
                    [In] [MarshalAs(UnmanagedType.U4)] int version,
                    ref IntPtr ppProcessInfo,
                    [MarshalAs(UnmanagedType.U4)] ref int pCount
            );
        }
    }
}
