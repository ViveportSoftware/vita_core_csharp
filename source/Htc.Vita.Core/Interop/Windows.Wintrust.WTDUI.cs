namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            public enum WTD_UI
            {
                WTD_UI_ALL = 1,
                WTD_UI_NONE,
                WTD_UI_NOBAD,
                WTD_UI_NOGOOD
            }
        }
    }
}
