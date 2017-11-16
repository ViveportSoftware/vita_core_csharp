namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             */
            public enum START_TYPE : uint
            {
                SERVICE_BOOT_START = 0x00000000,
                SERVICE_SYSTEM_START = 0x00000001,
                SERVICE_AUTO_START = 0x00000002,
                SERVICE_DEMAND_START = 0x00000003,
                SERVICE_DISABLED = 0x00000004,
                SERVICE_NO_CHANGE = 0xffffffff
            }
        }
    }
}
