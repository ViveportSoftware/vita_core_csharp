namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            public enum WTD_UICONTEXT
            {
                WTD_UICONTEXT_EXECUTE,
                WTD_UICONTEXT_INSTALL
            }
        }
    }
}
