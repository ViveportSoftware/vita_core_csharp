using System;
using System.Diagnostics;
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
                return CertCloseStore(
                        handle,
                        CertCloseStoreFlag.Default
                );
            }
        }

        internal class SafeCryptMsgHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeCryptMsgHandle() : base(true)
            {
            }

            internal SafeCryptMsgHandle(IntPtr handle) : base(true)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                return CryptMsgClose(handle);
            }
        }

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

        internal class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeProcessHandle() : base(true)
            {
            }

            internal SafeProcessHandle(IntPtr handle) : base(true)
            {
                SetHandle(handle);
            }

            internal SafeProcessHandle(Process process, bool ownsHandle) : base(ownsHandle)
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
