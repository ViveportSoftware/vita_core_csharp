using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            public static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388208.aspx
             */
            [DllImport(Libraries.Windows_wintrust,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
            public static extern long WinVerifyTrust(
                [In] IntPtr hWnd,
                [In] [MarshalAs(UnmanagedType.LPStruct)] Guid pgActionId,
                [In] IntPtr pWinTrustData
            );
        }
    }
}
