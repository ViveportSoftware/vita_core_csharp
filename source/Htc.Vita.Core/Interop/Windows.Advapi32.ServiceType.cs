using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             */
            [Flags]
            public enum SERVICE_TYPE : uint
            {
                SERVICE_KERNEL_DRIVER = 0x00000001,
                SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,
                SERVICE_ADAPTER = 0x00000004,
                SERVICE_RECOGNIZER_DRIVER = 0x00000008,
                SERVICE_DRIVER = SERVICE_KERNEL_DRIVER
                                 | SERVICE_FILE_SYSTEM_DRIVER
                                 | SERVICE_RECOGNIZER_DRIVER,
                SERVICE_INTERACTIVE_PROCESS = 0x00000100,
                SERVICE_NO_CHANGE = 0xffffffff
            }
        }
    }
}
