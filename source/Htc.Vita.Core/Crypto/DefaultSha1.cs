using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Crypto
{
    /// <summary>
    /// Class DefaultSha1.
    /// Implements the <see cref="Sha1" />
    /// </summary>
    /// <seealso cref="Sha1" />
    public partial class DefaultSha1 : Sha1
    {
        private const int BufferSizeInByte = 1024 * 128;

        /// <inheritdoc />
        protected override string OnGenerateInBase64(FileInfo file, CancellationToken cancellationToken)
        {
            return DoGenerateInBase64(file, cancellationToken);
        }

        /// <inheritdoc />
        protected override string OnGenerateInBase64(string content)
        {
            return DoGenerateInBase64(content);
        }

        /// <inheritdoc />
        protected override string OnGenerateInHex(FileInfo file, CancellationToken cancellationToken)
        {
            return DoGenerateInHex(file, cancellationToken);
        }

        /// <inheritdoc />
        protected override string OnGenerateInHex(string content)
        {
            return DoGenerateInHex(content);
        }

        private static string DoGenerateInBase64(FileInfo file, CancellationToken cancellationToken)
        {
            return Convert.ToBase64String(GenerateInBytes(
                    file,
                    cancellationToken
            ));
        }

        private static string DoGenerateInBase64(string content)
        {
            using (var digest = SHA1.Create())
            {
                return Convert.ToBase64String(digest.ComputeHash(Encoding.UTF8.GetBytes(content)));
            }
        }

        private static string DoGenerateInHex(FileInfo file, CancellationToken cancellationToken)
        {
            return Convert.ToHexString(GenerateInBytes(
                    file,
                    cancellationToken
            ));
        }

        private static string DoGenerateInHex(string content)
        {
            using (var digest = SHA1.Create())
            {
                return Convert.ToHexString(digest.ComputeHash(Encoding.UTF8.GetBytes(content)));
            }
        }

        private static byte[] GenerateInBytes(FileInfo file, CancellationToken cancellationToken)
        {
            var buffer = new byte[BufferSizeInByte];
            using (var digest = SHA1.Create())
            {
                using (var readStream = file.OpenRead())
                {
                    int length;
                    while ((length = readStream.Read(buffer, 0, buffer.Length)) > 0)
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
