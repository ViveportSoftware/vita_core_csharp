using System.Security.Cryptography;

namespace Htc.Vita.Core.Crypto
{
    internal static class LegacyDigest
    {
        internal static MD5 CreateMd5()
        {
            return MD5.Create();
        }

        internal static SHA1 CreateSha1()
        {
            return SHA1.Create();
        }
    }
}
