using System;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class SafeServiceHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeServiceHandle() : base(true)
            {
            }

            internal SafeServiceHandle(IntPtr handle) : base(true)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                return CloseServiceHandle(handle);
            }
        }
    }
}
