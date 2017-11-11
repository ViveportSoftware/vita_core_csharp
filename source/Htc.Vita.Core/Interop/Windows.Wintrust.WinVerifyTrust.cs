using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388208.aspx
             */
            [DllImport(Libraries.Windows_wintrust,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.U4)]
            public static extern uint WinVerifyTrust(
                    [In] IntPtr hWnd,
                    [In] [MarshalAs(UnmanagedType.LPStruct)] Guid pgActionId,
                    [In] IntPtr pWinTrustData
            );
        }
    }
}
