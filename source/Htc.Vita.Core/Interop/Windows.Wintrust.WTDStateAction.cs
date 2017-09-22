namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            public enum WTD_STATEACTION
            {
                WTD_STATEACTION_IGNORE,
                WTD_STATEACTION_VERIFY,
                WTD_STATEACTION_CLOSE,
                WTD_STATEACTION_AUTO_CACHE,
                WTD_STATEACTION_AUTO_CACHE_FLUSH
            }
        }
    }
}
