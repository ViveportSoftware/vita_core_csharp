using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-exitwindowsex
         */
        [DllImport(Libraries.WindowsUser32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool ExitWindowsEx(
                /* _In_ UINT  */ [In] ExitType uFlags,
                /* _In_ DWORD */ [In] int dwReason
        );
    }
}
