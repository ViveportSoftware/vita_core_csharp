using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Crypto
{
    public partial class DefaultMd5
    {
        protected override Task<string> OnGenerateInBase64Async(FileInfo file, CancellationToken cancellationToken)
        {
            return DoGenerateInBase64Async(file, cancellationToken);
        }

        protected override Task<string> OnGenerateInHexAsync(FileInfo file, CancellationToken cancellationToken)
        {
            return DoGenerateInHexAsync(file, cancellationToken);
        }

        public static async Task<string> DoGenerateInBase64Async(FileInfo file, CancellationToken cancellationToken)
        {
            return Convert.ToBase64String(await GenerateInBytesAsync(
                    file,
                    cancellationToken
            ).ConfigureAwait(false));
        }

        public static async Task<string> DoGenerateInHexAsync(FileInfo file, CancellationToken cancellationToken)
        {
            return Convert.ToHexString(await GenerateInBytesAsync(
                    file,
                    cancellationToken
            ).ConfigureAwait(false));
        }

        private static async Task<byte[]> GenerateInBytesAsync(FileInfo file, CancellationToken cancellationToken)
        {
            var buffer = new byte[BufferSizeInByte];
            using (var digest = MD5.Create())
            {
                using (var readStream = file.OpenRead())
                {
                    int length;
                    while ((length = await readStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
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
                    return digest.Hash;
                }
            }
        }
    }
}
