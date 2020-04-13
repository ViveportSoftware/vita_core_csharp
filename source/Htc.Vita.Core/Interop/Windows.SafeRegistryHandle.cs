using System;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            internal SafeRegistryHandle() : base(true)
            {
            }

            internal SafeRegistryHandle(IntPtr existingHandle, bool ownsHandle) : base(ownsHandle)
            {
                SetHandle(existingHandle);
            }

            protected override bool ReleaseHandle()
            {
                return RegCloseKey(handle) == Error.Success;
            }
        }
    }
}
