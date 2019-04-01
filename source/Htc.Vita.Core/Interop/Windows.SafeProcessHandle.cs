using System;
using System.Diagnostics;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeProcessHandle() : base(true)
            {
            }

            public SafeProcessHandle(IntPtr handle) : base(true)
            {
                SetHandle(handle);
            }

            public SafeProcessHandle(Process process) : base(true)
            {
                SetHandle(process.Handle);
            }

            protected override bool ReleaseHandle()
            {
                return CloseHandle(handle);
            }
        }
    }
}
