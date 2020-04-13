using System;

namespace Htc.Vita.Core.Util
{
    public static partial class Win32Registry
    {
        [Flags]
        internal enum KeyStateFlag
        {
            SystemKey   = 0x0001,
            WriteAccess = 0x0002
        }
    }
}
