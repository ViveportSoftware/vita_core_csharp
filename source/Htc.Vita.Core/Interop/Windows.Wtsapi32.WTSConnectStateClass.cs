namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wtsapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/aa383860.aspx
             */
            public enum WTS_CONNECTSTATE_CLASS
            {
                WTSActive,
                WTSConnected,
                WTSConnectQuery,
                WTSShadow,
                WTSDisconnected,
                WTSIdle,
                WTSListen,
                WTSReset,
                WTSDown,
                WTSInit
            }
        }
    }
}
