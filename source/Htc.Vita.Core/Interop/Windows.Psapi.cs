using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://msdn.microsoft.com/en-us/library/windows/desktop/ms683198.aspx
         */
        [DllImport(Libraries.WindowsPsapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern uint GetModuleFileNameExW(
                /* _In_     HANDLE  */ [In] IntPtr hProcess,
                /* _In_opt_ HMODULE */ [In] IntPtr hModule,
                /* _Out_    LPTSTR  */ [Out] StringBuilder lpFilename,
                /* _In_     DWORD   */ [In] uint nSize
        );
    }
}
