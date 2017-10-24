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
            public enum FILE_SHARE
            {
                FILE_SHARE_NONE = 0x00000000,
                FILE_SHARE_READ = 0x00000001,
                FILE_SHARE_WRITE = 0x00000002,
                FILE_SHARE_DELETE = 0x00000004
            }
        }
    }
}
