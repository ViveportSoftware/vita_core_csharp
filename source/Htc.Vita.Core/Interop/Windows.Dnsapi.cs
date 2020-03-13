using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * Undocumented API
         */
        [DllImport(Libraries.WindowsDnsapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DnsFlushResolverCache();

        /**
         * Undocumented API
         */
        [DllImport(Libraries.WindowsDnsapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DnsFlushResolverCacheEntry_W(
                string hostName
        );
    }
}
