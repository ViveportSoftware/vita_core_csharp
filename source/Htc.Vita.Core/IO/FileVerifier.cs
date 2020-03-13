using System;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Crypto;

namespace Htc.Vita.Core.IO
{
    public static partial class FileVerifier
    {
        public static bool Verify(
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
                        return checksum == Md5.GetInstance()
                                   .GenerateInHex(fileInfo, cancellationToken);
                    }

                    if (checksum.Length == 24)
                    {
                        return checksum == Md5.GetInstance()
                                   .GenerateInBase64(fileInfo, cancellationToken);
                    }

                    break;
                case ChecksumType.Sha1:

                    if (checksum.Length == 40)
                    {
                        return checksum == Sha1.GetInstance()
                                   .GenerateInHex(fileInfo, cancellationToken);
                    }

                    if (checksum.Length == 28)
                    {
                        return checksum == Sha1.GetInstance()
                                   .GenerateInBase64(fileInfo, cancellationToken);
                    }

                    break;
                case ChecksumType.Sha256:

                    if (checksum.Length == 64)
                    {
                        return checksum == Sha256.GetInstance()
                                   .GenerateInHex(fileInfo, cancellationToken);
                    }

                    if (checksum.Length == 44)
                    {
                        return checksum == Sha256.GetInstance()
                                   .GenerateInBase64(fileInfo, cancellationToken);
                    }

                    break;
            }

            throw new NotSupportedException($"Not supported! path: {fileInfo.FullName} size: {size} checksum: {checksum} checksumType: {checksumType}");
        }

        public enum ChecksumType
        {
            Md5 = 0,
            Sha1 = 1,
            Sha256 = 2,
        }
    }
}
