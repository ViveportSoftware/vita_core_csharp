using System;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
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
    }
}
