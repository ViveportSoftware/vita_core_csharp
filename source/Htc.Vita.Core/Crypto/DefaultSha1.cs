using System.IO;
using System.Security.Cryptography;
using System.Text;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Crypto
{
    public class DefaultSha1 : Sha1
    {
        protected override string OnGenerateInBase64(FileInfo file)
        {
            return DoGenerateInBase64(file);
        }

        protected override string OnGenerateInBase64(string content)
        {
            return DoGenerateInBase64(content);
        }

        protected override string OnGenerateInHex(FileInfo file)
        {
            return DoGenerateInHex(file);
        }

        protected override string OnGenerateInHex(string content)
        {
            return DoGenerateInHex(content);
        }

        public static string DoGenerateInBase64(FileInfo file)
        {
            using (var digest = SHA1.Create())
            {
                using (var readStream = file.OpenRead())
                {
                    return Convert.ToBase64String(digest.ComputeHash(readStream));
                }
            }
        }

        public static string DoGenerateInBase64(string content)
        {
            using (var digest = SHA1.Create())
            {
                return Convert.ToBase64String(digest.ComputeHash(Encoding.UTF8.GetBytes(content)));
            }
        }

        public static string DoGenerateInHex(FileInfo file)
        {
            using (var digest = SHA1.Create())
            {
                using (var readStream = file.OpenRead())
                {
                    return Convert.ToHexString(digest.ComputeHash(readStream));
                }
            }
        }

        public static string DoGenerateInHex(string content)
        {
            using (var digest = SHA1.Create())
            {
                return Convert.ToHexString(digest.ComputeHash(Encoding.UTF8.GetBytes(content)));
            }
        }
    }
}
