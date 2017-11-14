using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684323.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685981.aspx
             */
            [Flags]
            public enum SCMAccessRight : uint
            {
                STANDARD_RIGHTS_REQUIRED = 0xF0000,
                SC_MANAGER_CONNECT = 0x0001,
                SC_MANAGER_CREATE_SERVICE = 0x0002,
                SC_MANAGER_ENUMERATE_SERVICE = 0x0004,
                SC_MANAGER_LOCK = 0x0008,
                SC_MANAGER_QUERY_LOCK_STATUS = 0x0010,
                SC_MANAGER_MODIFY_BOOT_CONFIG = 0x0020,
                SC_MANAGER_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED
                                        | SC_MANAGER_CONNECT
                                        | SC_MANAGER_CREATE_SERVICE
                                        | SC_MANAGER_ENUMERATE_SERVICE
                                        | SC_MANAGER_LOCK
                                        | SC_MANAGER_QUERY_LOCK_STATUS
                                        | SC_MANAGER_MODIFY_BOOT_CONFIG
            }
        }
    }
}
