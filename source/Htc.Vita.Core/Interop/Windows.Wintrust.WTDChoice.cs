namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            public enum WTD_CHOICE
            {
                WTD_CHOICE_FILE = 1,
                WTD_CHOICE_CATALOG,
                WTD_CHOICE_BLOB,
                WTD_CHOICE_SIGNER,
                WTD_CHOICE_CERT
            }
        }
    }
}
