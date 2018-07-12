using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa376868.aspx
         * https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-exitwindowsex
         */
        [Flags]
        internal enum ExitType : uint
        {
            /* EWX_LOGOFF          */ Logoff = 0x00000000,
            /* EWX_SHUTDOWN        */ Shutdown = 0x00000001,
            /* EWX_REBOOT          */ Reboot = 0x00000002,
            /* EWX_FORCE           */ Force = 0x00000004,
            /* EWX_POWEROFF        */ Poweroff = 0x00000008,
            /* EWX_FORCEIFHUNG     */ ForceIfHung = 0x00000010,
            /* EWX_QUICKRESOLVE    */ QuickResolve = 0x00000020,
            /* EWX_RESTARTAPPS     */ RestartApps = 0x00000040,
            /* EWX_HYBRID_SHUTDOWN */ HybridShutdown = 0x00400000,
            /* EWX_BOOTOPTIONS     */ BootOptions = 0x01000000
        }

        /**
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa376868.aspx
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
