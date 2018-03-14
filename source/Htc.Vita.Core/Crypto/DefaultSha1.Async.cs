using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Crypto
{
    public partial class DefaultSha1
    {
        private const int BufferSizeInByte = 4096;

        protected override async Task<string> OnGenerateInBase64Async(FileInfo file)
        {
            return await DoGenerateInBase64Async(file).ConfigureAwait(false);
        }

        protected override async Task<string> OnGenerateInHexAsync(FileInfo file)
        {
            return await DoGenerateInHexAsync(file).ConfigureAwait(false);
        }

        public static async Task<string> DoGenerateInBase64Async(FileInfo file)
        {
            var buffer = new byte[BufferSizeInByte];
            using (var digest = SHA1.Create())
            {
                using (var readStream = file.OpenRead())
                {
                    int length;
                    while ((length = await readStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
                    {
                        digest.TransformBlock(
                                buffer,
                                0,
                                length,
                                null,
                                0
                        );
                    }
                    digest.TransformFinalBlock(
                            buffer,
                            0,
                            0
                    );
                }
                return Convert.ToBase64String(digest.Hash);
            }
        }

        public static async Task<string> DoGenerateInHexAsync(FileInfo file)
        {
            var buffer = new byte[BufferSizeInByte];
            using (var digest = SHA1.Create())
            {
                using (var readStream = file.OpenRead())
                {
                    int length;
                    while ((length = await readStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
                    {
                        digest.TransformBlock(
                                buffer,
                                0,
                                length,
                                null,
                                0
                        );
                    }
                    digest.TransformFinalBlock(
                            buffer,
                            0,
                            0
                    );
                }
                return Convert.ToHexString(digest.Hash);
            }
        }
    }
}
