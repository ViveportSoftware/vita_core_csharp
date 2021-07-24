using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Htc.Vita.Core.IO
{
    public static partial class FileVerifier
    {
        /// <summary>
        /// Verifies the specified file information asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="size">The size.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
        /// <exception cref="NotSupportedException">Not supported! path: {fileInfo.FullName} size: {size} checksum: {checksum} checksumType: {checksumType}</exception>
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

                    if (checksum.Length == 32 || checksum.Length == 24)
                    {
                        return await FileVerifierV2.GetInstance().VerifyIntegrityAsync(fileInfo, checksum, FileVerifierV2.ChecksumType.Md5, cancellationToken);
                    }

                    break;
                case ChecksumType.Sha1:

                    if (checksum.Length == 40 || checksum.Length == 28)
                    {
                        return await FileVerifierV2.GetInstance().VerifyIntegrityAsync(fileInfo, checksum, FileVerifierV2.ChecksumType.Sha1, cancellationToken);
                    }

                    break;
                case ChecksumType.Sha256:

                    if (checksum.Length == 64 || checksum.Length == 44)
                    {
                        return await FileVerifierV2.GetInstance().VerifyIntegrityAsync(fileInfo, checksum, FileVerifierV2.ChecksumType.Sha256, cancellationToken);
                    }

                    break;
            }

            throw new NotSupportedException($"Not supported! path: {fileInfo.FullName} size: {size} checksum: {checksum} checksumType: {checksumType}");
        }
    }
}
