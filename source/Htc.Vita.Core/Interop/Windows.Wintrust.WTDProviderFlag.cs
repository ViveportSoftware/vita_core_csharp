namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            public enum WTD_PROVIDERFLAG
            {
                WTD_USE_IE4_TRUST_FLAG = 1,
                WTD_NO_IE4_CHAIN_FLAG = 2,
                WTD_NO_POLICY_USAGE_FLAG = 4,
                WTD_REVOCATION_CHECK_NONE = 16,
                WTD_REVOCATION_CHECK_END_CERT = 32,
                WTD_REVOCATION_CHECK_CHAIN = 64,
                WTD_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = 128,
                WTD_SAFER_FLAG = 256,
                WTD_HASH_ONLY_FLAG = 512,
                WTD_USE_DEFAULT_OSVER_CHECK = 1024,
                WTD_LIFETIME_SIGNING_FLAG = 2048,
                WTD_CACHE_ONLY_URL_RETRIEVAL = 4096,
                WTD_DISABLE_MD2_MD4 = 8192,
                WTD_MOTW = 16384
            }
        }
    }
}
