using System;
using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/psapi/nf-psapi-getmodulefilenameexw")]
        [DllImport(Libraries.WindowsPsapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern uint GetModuleFileNameExW(
                /* _In_     HANDLE  */ [In] SafeProcessHandle hProcess,
                /* _In_opt_ HMODULE */ [In] IntPtr hModule,
                /* _Out_    LPTSTR  */ [Out] StringBuilder lpFilename,
                /* _In_     DWORD   */ [In] uint nSize
        );
    }
}
