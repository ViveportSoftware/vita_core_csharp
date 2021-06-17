using System;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class FileVerifierV2.
    /// </summary>
    public abstract partial class FileVerifierV2
    {
        static FileVerifierV2()
        {
            TypeRegistry.RegisterDefault<FileVerifierV2, DefaultFileVerifierV2>();
        }

        /// <summary>
        /// Registers this instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : FileVerifierV2, new()
        {
            TypeRegistry.Register<FileVerifierV2, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>FileVerifierV2.</returns>
        public static FileVerifierV2 GetInstance()
        {
            return TypeRegistry.GetInstance<FileVerifierV2>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>FileVerifierV2.</returns>
        public static FileVerifierV2 GetInstance<T>()
                where T : FileVerifierV2, new()
        {
            return TypeRegistry.GetInstance<FileVerifierV2, T>();
        }

        /// <summary>
        /// Generates the checksum in Base64 form.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <returns>System.String.</returns>
        public string GenerateChecksumInBase64(
                FileInfo fileInfo,
                ChecksumType checksumType)
        {
            return GenerateChecksumInBase64(
                    fileInfo,
                    checksumType,
                    CancellationToken.None
            );
        }

        /// <summary>
        /// Generates the checksum in Base64 form.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>System.String.</returns>
        public string GenerateChecksumInBase64(
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
                result = OnGenerateChecksumInBase64(
                        newFileInfo,
                        checksumType,
                        cancellationToken
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileVerifierV2)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Generates the checksum in hexadecimal form.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <returns>System.String.</returns>
        public string GenerateChecksumInHex(
                FileInfo fileInfo,
                ChecksumType checksumType)
        {
            return GenerateChecksumInHex(
                    fileInfo,
                    checksumType,
                    CancellationToken.None
            );
        }

        /// <summary>
        /// Generates the checksum in hexadecimal form.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>System.String.</returns>
        public string GenerateChecksumInHex(
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
                result = OnGenerateChecksumInHex(
                        newFileInfo,
                        checksumType,
                        cancellationToken
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileVerifierV2)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Verifies the integrity.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns><c>true</c> if verifying the integrity successfully, <c>false</c> otherwise.</returns>
        public bool VerifyIntegrity(
                FileInfo fileInfo,
                string checksum)
        {
            return VerifyIntegrity(
                    fileInfo,
                    checksum,
                    CancellationToken.None
            );
        }

        /// <summary>
        /// Verifies the integrity.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if verifying the integrity successfully, <c>false</c> otherwise.</returns>
        public bool VerifyIntegrity(
                FileInfo fileInfo,
                string checksum,
                CancellationToken cancellationToken)
        {
            return VerifyIntegrity(
                    fileInfo,
                    checksum,
                    ChecksumType.Auto,
                    cancellationToken
            );
        }

        /// <summary>
        /// Verifies the integrity.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <returns><c>true</c> if verifying the integrity successfully, <c>false</c> otherwise.</returns>
        public bool VerifyIntegrity(
                FileInfo fileInfo,
                string checksum,
                ChecksumType checksumType)
        {
            return VerifyIntegrity(
                    fileInfo,
                    checksum,
                    checksumType,
                    CancellationToken.None
            );
        }

        /// <summary>
        /// Verifies the integrity.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if verifying the integrity successfully, <c>false</c> otherwise.</returns>
        public bool VerifyIntegrity(
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
                result = OnVerifyIntegrity(
                        newFileInfo,
                        checksum,
                        checksumType,
                        cancellationToken
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileVerifierV2)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when generating checksum in Base64 form.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnGenerateChecksumInBase64(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken
        );
        /// <summary>
        /// Called when generating checksum in hexadecimal form.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnGenerateChecksumInHex(
                FileInfo fileInfo,
                ChecksumType checksumType,
                CancellationToken cancellationToken
        );
        /// <summary>
        /// Called when verifying integrity.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="checksumType">The checksum type.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if verifying the integrity successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnVerifyIntegrity(
                FileInfo fileInfo,
                string checksum,
                ChecksumType checksumType,
                CancellationToken cancellationToken
        );
    }
}
