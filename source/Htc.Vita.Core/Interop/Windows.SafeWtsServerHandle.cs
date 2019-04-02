using System;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class SafeWtsServerHandle : SafeHandleMinusOneIsInvalid
        {
            private SafeWtsServerHandle() : base(true)
            {
            }

            public SafeWtsServerHandle(IntPtr handle) : base(true)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                if (handle != WindowsTerminalServiceCurrentServerHandle)
                {
                    WTSCloseServer(handle);
                }
                return true;
            }
        }
    }
}
