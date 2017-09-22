using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wtsapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/aa383838.aspx
             */
            [DllImport(Libraries.Windows_wtsapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool WTSQuerySessionInformationW(
                    [In] IntPtr hServer,
                    [In] int sessionId,
                    [In] WTS_INFO_CLASS wtsInfoClass,
                    out IntPtr ppBuffer,
                    out uint pBytesReturned
            );
        }
    }
}
