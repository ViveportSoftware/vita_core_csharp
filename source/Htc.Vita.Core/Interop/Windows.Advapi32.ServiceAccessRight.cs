using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684330.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685981.aspx
             */
            [Flags]
            public enum ServiceAccessRight : uint
            {
                STANDARD_RIGHTS_REQUIRED = 0xF0000,
                SERVICE_QUERY_CONFIG = 0x0001,
                SERVICE_CHANGE_CONFIG = 0x0002,
                SERVICE_QUERY_STATUS = 0x0004,
                SERVICE_ENUMERATE_DEPENDENTS = 0x0008,
                SERVICE_START = 0x0010,
                SERVICE_STOP = 0x0020,
                SERVICE_PAUSE_CONTINUE = 0x0040,
                SERVICE_INTERROGATE = 0x0080,
                SERVICE_USER_DEFINED_CONTROL = 0x0100,
                SERVICE_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED
                                     | SERVICE_QUERY_CONFIG
                                     | SERVICE_CHANGE_CONFIG
                                     | SERVICE_QUERY_STATUS
                                     | SERVICE_ENUMERATE_DEPENDENTS
                                     | SERVICE_START
                                     | SERVICE_STOP
                                     | SERVICE_PAUSE_CONTINUE
                                     | SERVICE_INTERROGATE
                                     | SERVICE_USER_DEFINED_CONTROL
            }
        }
    }
}
