using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685996.aspx
             */
            [Flags]
            public enum CONTROL_ACCEPTED : uint
            {
                SERVICE_ACCEPT_STOP = 0x00000001,
                SERVICE_ACCEPT_PAUSE_CONTINUE = 0x00000002,
                SERVICE_ACCEPT_SHUTDOWN = 0x00000004,
                SERVICE_ACCEPT_PARAMCHANGE = 0x00000008,
                SERVICE_ACCEPT_NETBINDCHANGE = 0x00000010,
                SERVICE_ACCEPT_HARDWAREPROFILECHANGE = 0x00000020,
                SERVICE_ACCEPT_POWEREVENT = 0x00000040,
                SERVICE_ACCEPT_SESSIONCHANGE = 0x00000080,
                SERVICE_ACCEPT_PRESHUTDOWN = 0x00000100,
                SERVICE_ACCEPT_TIMECHANGE = 0x00000200,
                SERVICE_ACCEPT_TRIGGEREVENT = 0x00000400,
                SERVICE_ACCEPT_USER_LOGOFF = 0x00000800,
                SERVICE_ACCEPT_LOWRESOURCES = 0x00002000,
                SERVICE_ACCEPT_SYSTEMLOWRESOURCES = 0x00004000
            }
        }
    }
}
