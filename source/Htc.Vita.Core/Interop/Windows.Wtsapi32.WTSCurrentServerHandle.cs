using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wtsapi32
        {
            public static IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
        }
    }
}
