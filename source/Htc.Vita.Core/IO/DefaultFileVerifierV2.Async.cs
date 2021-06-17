using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public partial class DefaultFileVerifierV2
    {
        /// <inheritdoc />
        protected override async Task<string> OnGenerateChecksumInBase64Async(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken)
        {
            if (checksumType == ChecksumType.Md5)
            {
                return await Md5.GetInstance().GenerateInBase64Async(
                        fileInfo,
                        cancellationToken
                ).ConfigureAwait(false);
            }

            if (checksumType == ChecksumType.Sha1)
            {
                return await Sha1.GetInstance().GenerateInBase64Async(
                        fileInfo,
                        cancellationToken
                ).ConfigureAwait(false);
            }

            if (checksumType == ChecksumType.Sha256)
            {
                return await Sha256.GetInstance().GenerateInBase64Async(
                        fileInfo,
                        cancellationToken
                ).ConfigureAwait(false);
            }

            Logger.GetInstance(typeof(DefaultFileVerifierV2)).Error($"Do not find suitable checksum implementation: {checksumType}");
            return null;
        }

        /// <inheritdoc />
        protected override async Task<string> OnGenerateChecksumInHexAsync(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken)
        {
            if (checksumType == ChecksumType.Md5)
            {
                return await Md5.GetInstance().GenerateInHexAsync(
                        fileInfo,
                        cancellationToken
                ).ConfigureAwait(false);
            }

            if (checksumType == ChecksumType.Sha1)
            {
                return await Sha1.GetInstance().GenerateInHexAsync(
                        fileInfo,
                        cancellationToken
                ).ConfigureAwait(false);
            }

            if (checksumType == ChecksumType.Sha256)
            {
                return await Sha256.GetInstance().GenerateInHexAsync(
                        fileInfo,
                        cancellationToken
                ).ConfigureAwait(false);
            }

            Logger.GetInstance(typeof(DefaultFileVerifierV2)).Error($"Do not find suitable checksum implementation: {checksumType}");
            return null;
        }

        /// <inheritdoc />
        protected override async Task<bool> OnVerifyIntegrityAsync(
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
                return await Md5.GetInstance().ValidateInAllAsync(
                        fileInfo,
                        checksum,
                        cancellationToken
                ).ConfigureAwait(false);
            }

            if (newChecksumType == ChecksumType.Sha1)
            {
                return await Sha1.GetInstance().ValidateInAllAsync(
                        fileInfo,
                        checksum,
                        cancellationToken
                ).ConfigureAwait(false);
            }

            if (newChecksumType == ChecksumType.Sha256)
            {
                return await Sha256.GetInstance().ValidateInAllAsync(
                        fileInfo,
                        checksum,
                        cancellationToken
                ).ConfigureAwait(false);
            }

            Logger.GetInstance(typeof(DefaultFileVerifierV2)).Error($"Do not find suitable checksum implementation: {newChecksumType}");
            return false;
        }
    }
}
