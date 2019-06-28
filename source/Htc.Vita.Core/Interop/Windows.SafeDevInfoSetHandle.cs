using System;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class SafeDevInfoSetHandle : SafeHandleMinusOneIsInvalid
        {
            private SafeDevInfoSetHandle() : base(true)
            {
            }

            internal SafeDevInfoSetHandle(IntPtr handle) : base(true)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                return SetupDiDestroyDeviceInfoList(handle);
            }
        }
    }
}
