using System;
using System.IO;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    public abstract partial class Md5
    {
        public async Task<string> GenerateInBase64Async(FileInfo file)
        {
            if (file == null || !file.Exists)
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = await OnGenerateInBase64Async(file).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Generating checksum in base64 in async error: " + e);
            }
            return result;
        }

        public async Task<string> GenerateInHexAsync(FileInfo file)
        {
            if (file == null || !file.Exists)
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = await OnGenerateInHexAsync(file).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Generating checksum in hex in async error: " + e);
            }
            return result;
        }

        public async Task<bool> ValidateInAllAsync(FileInfo file, string checksum)
        {
            if (string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            if (checksum.Length == Base64Length)
            {
                return await ValidateInBase64Async(file, checksum).ConfigureAwait(false);
            }
            if (checksum.Length == HexLength)
            {
                return await ValidateInHexAsync(file, checksum).ConfigureAwait(false);
            }
            return false;
        }

        public async Task<bool> ValidateInBase64Async(FileInfo file, string checksum)
        {
            if (file == null || !file.Exists || string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            var result = false;
            try
            {
                result = checksum.Equals(await OnGenerateInBase64Async(file).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Validating checksum in base64 in async error: " + e);
            }
            return result;
        }

        public async Task<bool> ValidateInHexAsync(FileInfo file, string checksum)
        {
            if (file == null || !file.Exists || string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            var result = false;
            try
            {
                result = checksum.ToLowerInvariant().Equals(await OnGenerateInHexAsync(file).ConfigureAwait(false));
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Validating checksum in hex in async error: " + e);
            }
            return result;
        }

        protected abstract Task<string> OnGenerateInBase64Async(FileInfo file);
        protected abstract Task<string> OnGenerateInHexAsync(FileInfo file);
    }
}
