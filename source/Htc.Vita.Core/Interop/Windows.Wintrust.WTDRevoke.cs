namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            public enum WTD_REVOKE
            {
                WTD_REVOKE_NONE,
                WTD_REVOKE_WHOLECHAIN
            }
        }
    }
}
