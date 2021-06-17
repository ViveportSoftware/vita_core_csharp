using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public abstract partial class FileVerifierV2
    {
        /// <summary>
        /// Generates the checksum in Base64 form asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public Task<string> GenerateChecksumInBase64Async(
                FileInfo fileInfo,
                ChecksumType checksumType)
        {
            return GenerateChecksumInBase64Async(
                    fileInfo,
                    checksumType,
                    CancellationToken.None
            );
        }

        /// <summary>
        /// Generates the checksum in Base64 form asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        public async Task<string> GenerateChecksumInBase64Async(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken)
        {
            if (fileInfo == null)
            {
                return null;
            }

            if (checksumType == ChecksumType.Unknown || checksumType == ChecksumType.Auto)
            {
                Logger.GetInstance(typeof(FileVerifierV2)).Error("You should specify the checksum type");
                return null;
            }

            var newFileInfo = new FileInfo(fileInfo.ToString());
            if (!newFileInfo.Exists)
            {
                return null;
            }

            string result = null;
            try
            {
                result = await OnGenerateChecksumInBase64Async(
                        newFileInfo,
                        checksumType,
                        cancellationToken
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileVerifierV2)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Generates the checksum in hexadecimal form asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public Task<string> GenerateChecksumInHexAsync(
                FileInfo fileInfo,
                ChecksumType checksumType)
        {
            return GenerateChecksumInHexAsync(
                    fileInfo,
                    checksumType,
                    CancellationToken.None
            );
        }

        /// <summary>
        /// Generates the checksum in hexadecimal form asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        public async Task<string> GenerateChecksumInHexAsync(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken)
        {
            if (fileInfo == null)
            {
                return null;
            }

            if (checksumType == ChecksumType.Unknown || checksumType == ChecksumType.Auto)
            {
                Logger.GetInstance(typeof(FileVerifierV2)).Error("You should specify the checksum type");
                return null;
            }

            var newFileInfo = new FileInfo(fileInfo.ToString());
            if (!newFileInfo.Exists)
            {
                return null;
            }

            string result = null;
            try
            {
                result = await OnGenerateChecksumInHexAsync(
                        newFileInfo,
                        checksumType,
                        cancellationToken
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileVerifierV2)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Verifies the integrity asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> VerifyIntegrityAsync(
                FileInfo fileInfo,
                string checksum)
        {
            return VerifyIntegrityAsync(
                    fileInfo,
                    checksum,
                    CancellationToken.None
            );
        }

        /// <summary>
        /// Verifies the integrity asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> VerifyIntegrityAsync(
                FileInfo fileInfo,
                string checksum,
                CancellationToken cancellationToken)
        {
            return VerifyIntegrityAsync(
                    fileInfo,
                    checksum,
                    ChecksumType.Auto,
                    cancellationToken
            );
        }

        /// <summary>
        /// Verifies the integrity asynchronous.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> VerifyIntegrityAsync(
                FileInfo fileInfo,
                string checksum,
                ChecksumType checksumType)
        {
            return VerifyIntegrityAsync(
                    fileInfo,
                    checksum,
                    checksumType,
                    CancellationToken.None
            );
        }

        /// <summary>
        /// Verifies the integrity asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
        public async Task<bool> VerifyIntegrityAsync(
                FileInfo fileInfo,
                string checksum,
                ChecksumType checksumType,
                CancellationToken cancellationToken)
        {
            if (fileInfo == null || string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            if (checksumType == ChecksumType.Unknown)
            {
                Logger.GetInstance(typeof(FileVerifierV2)).Error("You should specify the checksum type");
                return false;
            }

            var newFileInfo = new FileInfo(fileInfo.ToString());
            if (!newFileInfo.Exists)
            {
                return false;
            }

            var result = false;
            try
            {
                result = await OnVerifyIntegrityAsync(
                        newFileInfo,
                        checksum,
                        checksumType,
                        cancellationToken
                ).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileVerifierV2)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when generating checksum in Base64 form asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        protected abstract Task<string> OnGenerateChecksumInBase64Async(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken
        );
        /// <summary>
        /// Called when generating checksum in hexadecimal form asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        protected abstract Task<string> OnGenerateChecksumInHexAsync(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken
        );
        /// <summary>
        /// Called when verifying integrity asynchronously.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        protected abstract Task<bool> OnVerifyIntegrityAsync(
                FileInfo fileInfo,
                string checksum,
                ChecksumType checksumType,
                CancellationToken cancellationToken
        );
    }
}
