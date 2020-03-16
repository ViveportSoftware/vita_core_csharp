using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Crypto;

namespace Htc.Vita.Core.IO
{
    public static partial class FileVerifier
    {
        public static async Task<bool> VerifyAsync(
                FileInfo fileInfo,
                long size,
                string checksum,
                ChecksumType checksumType,
                CancellationToken cancellationToken)
        {
            if (fileInfo == null || !fileInfo.Exists || fileInfo.Length != size) return false;

            switch (checksumType)
            {
                case ChecksumType.Md5:

                    if (checksum.Length == 32)
                    {
                        return checksum == await Md5.GetInstance()
                                   .GenerateInHexAsync(fileInfo, cancellationToken).ConfigureAwait(false);
                    }

                    if (checksum.Length == 24)
                    {
                        return checksum == await Md5.GetInstance()
                                   .GenerateInBase64Async(fileInfo, cancellationToken).ConfigureAwait(false);
                    }

                    break;
                case ChecksumType.Sha1:

                    if (checksum.Length == 40)
                    {
                        return checksum == await Sha1.GetInstance()
                                   .GenerateInHexAsync(fileInfo, cancellationToken).ConfigureAwait(false);
                    }

                    if (checksum.Length == 28)
                    {
                        return checksum == await Sha1.GetInstance()
                                   .GenerateInBase64Async(fileInfo, cancellationToken).ConfigureAwait(false);
                    }

                    break;
                case ChecksumType.Sha256:

                    if (checksum.Length == 64)
                    {
                        return checksum == await Sha256.GetInstance()
                                   .GenerateInHexAsync(fileInfo, cancellationToken).ConfigureAwait(false);
                    }

                    if (checksum.Length == 44)
                    {
                        return checksum == await Sha256.GetInstance()
                                   .GenerateInBase64Async(fileInfo, cancellationToken).ConfigureAwait(false);
                    }

                    break;
            }

            throw new NotSupportedException($"Not supported! path: {fileInfo.FullName} size: {size} checksum: {checksum} checksumType: {checksumType}");
        }
    }
}
