using System;

namespace Htc.Vita.Core.Util
{
    public partial class Win32Registry
    {
        [Flags]
        internal enum KeyStateFlag
        {
            SystemKey   = 0x0001,
            WriteAccess = 0x0002
        }
    }
}
