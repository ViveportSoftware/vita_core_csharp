using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/nf-wintrust-winverifytrust
         */
        [DllImport(Libraries.WindowsWintrust,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        internal static extern uint WinVerifyTrust(
                /* _In_ HWND   */ [In] IntPtr hWnd,
                /* _In_ GUID*  */ [In] ref Guid pgActionId,
                /* _In_ LPVOID */ [In] IntPtr pWvtData
        );
    }
}
