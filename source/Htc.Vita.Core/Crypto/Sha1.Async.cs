using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    public abstract partial class Sha1
    {
        /// <summary>
        /// Generates the checksum value in Base64 form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public Task<string> GenerateInBase64Async(FileInfo file)
        {
            return GenerateInBase64Async(file, new CancellationToken());
        }

        /// <summary>
        /// Generates the checksum value in Base64 form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> GenerateInBase64Async(FileInfo file, CancellationToken cancellationToken)
        {
            if (file == null || !file.Exists)
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = await OnGenerateInBase64Async(file, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                Logger.GetInstance(typeof(Sha1)).Warn("Generating checksum in base64 in async cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha1)).Fatal($"Generating checksum in base64 in async error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Generates the checksum value in hexadecimal form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public Task<string> GenerateInHexAsync(FileInfo file)
        {
            return GenerateInHexAsync(file, new CancellationToken());
        }

        /// <summary>
        /// Generates the checksum value in hexadecimal form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> GenerateInHexAsync(FileInfo file, CancellationToken cancellationToken)
        {
            if (file == null || !file.Exists)
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = await OnGenerateInHexAsync(file, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                Logger.GetInstance(typeof(Sha1)).Warn("Generating checksum in hex in async cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha1)).Fatal($"Generating checksum in hex in async error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Validates the file in all checksum form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> ValidateInAllAsync(FileInfo file, string checksum)
        {
            return ValidateInAllAsync(file, checksum, new CancellationToken());
        }

        /// <summary>
        /// Validates the file in all checksum form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> ValidateInAllAsync(FileInfo file, string checksum, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            if (checksum.Length == Base64Length)
            {
                return await ValidateInBase64Async(file, checksum, cancellationToken).ConfigureAwait(false);
            }
            if (checksum.Length == HexLength)
            {
                return await ValidateInHexAsync(file, checksum, cancellationToken).ConfigureAwait(false);
            }
            return false;
        }

        /// <summary>
        /// Validates the file in Base64 form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> ValidateInBase64Async(FileInfo file, string checksum)
        {
            return ValidateInBase64Async(file, checksum, new CancellationToken());
        }

        /// <summary>
        /// Validates the file in Base64 form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> ValidateInBase64Async(FileInfo file, string checksum, CancellationToken cancellationToken)
        {
            if (file == null || !file.Exists || string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            var result = false;
            try
            {
                result = checksum.Equals(await OnGenerateInBase64Async(file, cancellationToken).ConfigureAwait(false));
            }
            catch (OperationCanceledException)
            {
                Logger.GetInstance(typeof(Sha1)).Warn("Validating checksum in base64 in async cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha1)).Fatal($"Validating checksum in base64 in async error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Validates the file in hexadecimal form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> ValidateInHexAsync(FileInfo file, string checksum)
        {
            return ValidateInHexAsync(file, checksum, new CancellationToken());
        }

        /// <summary>
        /// Validates the file in hexadecimal form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> ValidateInHexAsync(FileInfo file, string checksum, CancellationToken cancellationToken)
        {
            if (file == null || !file.Exists || string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            var result = false;
            try
            {
                result = checksum.ToLowerInvariant().Equals(await OnGenerateInHexAsync(file, cancellationToken).ConfigureAwait(false));
            }
            catch (OperationCanceledException)
            {
                Logger.GetInstance(typeof(Sha1)).Warn("Validating checksum in hex in async cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha1)).Fatal($"Validating checksum in hex in async error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Called when generating the checksum in Base64 form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        protected abstract Task<string> OnGenerateInBase64Async(FileInfo file, CancellationToken cancellationToken);
        /// <summary>
        /// Called when generating the checksum in hexadecimal form asynchronously.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        protected abstract Task<string> OnGenerateInHexAsync(FileInfo file, CancellationToken cancellationToken);
    }
}
