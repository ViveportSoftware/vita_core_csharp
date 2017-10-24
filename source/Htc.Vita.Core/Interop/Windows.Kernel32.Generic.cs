using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Kernel32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
             */
            [Flags]
            public enum GENERIC : uint
            {
                GENERIC_ALL = 0x10000000,
                GENERIC_EXECUTE = 0x20000000,
                GENERIC_WRITE = 0x40000000,
                GENERIC_READ = 0x80000000
            }
        }
    }
}
