using System;
using System.IO;
using System.Threading;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class FileVerifier.
    /// </summary>
    [Obsolete("This class is obsoleted.")]
    public static partial class FileVerifier
    {
        /// <summary>
        /// Verifies the specified file information.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="size">The size.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if verifying the specific file successfully, <c>false</c> otherwise.</returns>
        /// <exception cref="NotSupportedException">Not supported! path: {fileInfo.FullName} size: {size} checksum: {checksum} checksumType: {checksumType}</exception>
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

                    if (checksum.Length == 32 || checksum.Length == 24)
                    {
                        return FileVerifierV2.GetInstance().VerifyIntegrity(fileInfo, checksum, FileVerifierV2.ChecksumType.Md5, cancellationToken);
                    }

                    break;
                case ChecksumType.Sha1:

                    if (checksum.Length == 40 || checksum.Length == 28)
                    {
                        return FileVerifierV2.GetInstance().VerifyIntegrity(fileInfo, checksum, FileVerifierV2.ChecksumType.Sha1, cancellationToken);
                    }

                    break;
                case ChecksumType.Sha256:

                    if (checksum.Length == 64 || checksum.Length == 44)
                    {
                        return FileVerifierV2.GetInstance().VerifyIntegrity(fileInfo, checksum, FileVerifierV2.ChecksumType.Sha256, cancellationToken);
                    }

                    break;
            }

            throw new NotSupportedException($"Not supported! path: {fileInfo.FullName} size: {size} checksum: {checksum} checksumType: {checksumType}");
        }

        /// <summary>
        /// Enum ChecksumType
        /// </summary>
        [Obsolete("This enumeration is obsoleted.")]
        public enum ChecksumType
        {
            /// <summary>
            /// MD5
            /// </summary>
            Md5 = 0,
            /// <summary>
            /// SHA-1
            /// </summary>
            Sha1 = 1,
            /// <summary>
            /// SHA-2 256
            /// </summary>
            Sha256 = 2,
        }
    }
}
