using System;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeCertContextHandle() : base(true)
            {
            }

            internal SafeCertContextHandle(IntPtr handle) : base(true)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                return CertFreeCertificateContext(handle);
            }
        }
    }
}
