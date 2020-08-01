using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shchangenotify")]
        [DllImport(Libraries.WindowsShell32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern void SHChangeNotify(
                /*          LONG    */ [In] ShellChangeNotifyEventId wEventId,
                /*          UINT    */ [In] ShellChangeNotifyFlags uFlags,
                /* _In_opt_ LPCVOID */ [In] IntPtr dwItem1,
                /* _In_opt_ LPCVOID */ [In] IntPtr dwItem2
        );
    }
}
