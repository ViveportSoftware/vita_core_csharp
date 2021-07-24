using System.IO;
using System.Threading;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class DefaultFileVerifierV2.
    /// Implements the <see cref="FileVerifierV2" />
    /// </summary>
    /// <seealso cref="FileVerifierV2" />
    public partial class DefaultFileVerifierV2 : FileVerifierV2
    {
        /// <inheritdoc />
        protected override string OnGenerateChecksumInBase64(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken)
        {
            if (checksumType == ChecksumType.Md5)
            {
                return Md5.GetInstance().GenerateInBase64(
                        fileInfo,
                        cancellationToken
                );
            }

            if (checksumType == ChecksumType.Sha1)
            {
                return Sha1.GetInstance().GenerateInBase64(
                        fileInfo,
                        cancellationToken
                );
            }

            if (checksumType == ChecksumType.Sha256)
            {
                return Sha256.GetInstance().GenerateInBase64(
                        fileInfo,
                        cancellationToken
                );
            }

            Logger.GetInstance(typeof(DefaultFileVerifierV2)).Error($"Do not find suitable checksum implementation: {checksumType}");
            return null;
        }

        /// <inheritdoc />
        protected override string OnGenerateChecksumInHex(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken)
        {
            if (checksumType == ChecksumType.Md5)
            {
                return Md5.GetInstance().GenerateInHex(
                        fileInfo,
                        cancellationToken
                );
            }

            if (checksumType == ChecksumType.Sha1)
            {
                return Sha1.GetInstance().GenerateInHex(
                        fileInfo,
                        cancellationToken
                );
            }

            if (checksumType == ChecksumType.Sha256)
            {
                return Sha256.GetInstance().GenerateInHex(
                        fileInfo,
                        cancellationToken
                );
            }

            Logger.GetInstance(typeof(DefaultFileVerifierV2)).Error($"Do not find suitable checksum implementation: {checksumType}");
            return null;
        }

        /// <inheritdoc />
        protected override bool OnVerifyIntegrity(
                FileInfo fileInfo,
                string checksum,
                ChecksumType checksumType,
                CancellationToken cancellationToken)
        {
            var newChecksumType = checksumType;
            if (newChecksumType == ChecksumType.Auto)
            {
                if (checksum.Length == Md5.Base64FormLength || checksum.Length == Md5.HexFormLength)
                {
                    newChecksumType = ChecksumType.Md5;
                }

                if (checksum.Length == Sha1.Base64FormLength || checksum.Length == Sha1.HexFormLength)
                {
                    newChecksumType = ChecksumType.Sha1;
                }

                if (checksum.Length == Sha256.Base64FormLength || checksum.Length == Sha256.HexFormLength)
                {
                    newChecksumType = ChecksumType.Sha256;
                }
            }

            if (newChecksumType == ChecksumType.Md5)
            {
                return Md5.GetInstance().ValidateInAll(
                        fileInfo,
                        checksum,
                        cancellationToken
                );
            }

            if (newChecksumType == ChecksumType.Sha1)
            {
                return Sha1.GetInstance().ValidateInAll(
                        fileInfo,
                        checksum,
                        cancellationToken
                );
            }

            if (newChecksumType == ChecksumType.Sha256)
            {
                return Sha256.GetInstance().ValidateInAll(
                        fileInfo,
                        checksum,
                        cancellationToken
                );
            }

            Logger.GetInstance(typeof(DefaultFileVerifierV2)).Error($"Do not find suitable checksum implementation: {newChecksumType}");
            return false;
        }
    }
}
