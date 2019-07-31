using System;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeCertStoreHandle() : base(true)
            {
            }

            internal SafeCertStoreHandle(IntPtr handle) : base(true)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                return CertCloseStore(handle, CertCloseStoreFlag.Default);
            }
        }
    }
}
