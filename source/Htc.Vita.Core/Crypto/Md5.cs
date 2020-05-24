using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    /// <summary>
    /// Class Md5.
    /// </summary>
    public abstract partial class Md5
    {
        private static Dictionary<string, Md5> Instances { get; } = new Dictionary<string, Md5>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(DefaultMd5);

        private const int Base64Length = 24; // "pq/Xu7jVnluxLJ28xOws/w=="
        private const int HexLength = 32;    // "202cb962ac59075b964b07152d234b70"

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T : Md5
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(Md5)).Info($"Registered default {nameof(Md5)} type to {_defaultType}");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>Md5.</returns>
        public static Md5 GetInstance()
        {
            Md5 instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(Md5)).Info($"Initializing {typeof(DefaultMd5).FullName}...");
                instance = new DefaultMd5();
            }
            return instance;
        }

        private static Md5 DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException($"Invalid arguments to get {nameof(Md5)} instance");
            }

            var key = $"{type.FullName}_";
            Md5 instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Md5)).Info($"Initializing {key}...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (Md5)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Md5)).Info($"Initializing {typeof(DefaultMd5).FullName}...");
                instance = new DefaultMd5();
            }
            lock (InstancesLock)
            {
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
            }
            return instance;
        }

        /// <summary>
        /// Generates the checksum value in Base64 form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>System.String.</returns>
        public string GenerateInBase64(FileInfo file)
        {
            return GenerateInBase64(file, new CancellationToken());
        }

        /// <summary>
        /// Generates the checksum value in Base64 form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.String.</returns>
        public string GenerateInBase64(FileInfo file, CancellationToken cancellationToken)
        {
            if (file == null || !file.Exists)
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = OnGenerateInBase64(file, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Logger.GetInstance(typeof(Md5)).Warn("Generating checksum in base64 cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal("Generating checksum in base64 error: " + e);
            }
            return result;
        }

        /// <summary>
        /// Generates the checksum value in Base64 form.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.String.</returns>
        public string GenerateInBase64(string content)
        {
            if (content == null)
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = OnGenerateInBase64(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal($"Generating checksum in base64 error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Generates the checksum value in hexadecimal form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>System.String.</returns>
        public string GenerateInHex(FileInfo file)
        {
            return GenerateInHex(file, new CancellationToken());
        }

        /// <summary>
        /// Generates the checksum value in hexadecimal form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.String.</returns>
        public string GenerateInHex(FileInfo file, CancellationToken cancellationToken)
        {
            if (file == null || !file.Exists)
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = OnGenerateInHex(file, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Logger.GetInstance(typeof(Md5)).Warn("Generating checksum in hex cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal($"Generating checksum in hex error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Generates the checksum value in hexadecimal form.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.String.</returns>
        public string GenerateInHex(string content)
        {
            if (content == null)
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = OnGenerateInHex(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal($"Generating checksum in hex error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Validates the file in all checksum form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        public bool ValidateInAll(FileInfo file, string checksum)
        {
            return ValidateInAll(file, checksum, new CancellationToken());
        }

        /// <summary>
        /// Validates the file in all checksum form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        public bool ValidateInAll(FileInfo file, string checksum, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            if (checksum.Length == Base64Length)
            {
                return ValidateInBase64(file, checksum, cancellationToken);
            }
            if (checksum.Length == HexLength)
            {
                return ValidateInHex(file, checksum, cancellationToken);
            }
            return false;
        }

        /// <summary>
        /// Validates the content in all checksum form.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        public bool ValidateInAll(string content, string checksum)
        {
            if (string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            if (checksum.Length == HexLength)
            {
                return ValidateInHex(content, checksum);
            }
            return ValidateInBase64(content, checksum);
        }

        /// <summary>
        /// Validates the file in Base64 form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        public bool ValidateInBase64(FileInfo file, string checksum)
        {
            return ValidateInBase64(file, checksum, new CancellationToken());
        }

        /// <summary>
        /// Validates the file in Base64 form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        public bool ValidateInBase64(FileInfo file, string checksum, CancellationToken cancellationToken)
        {
            if (file == null || !file.Exists || string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            var result = false;
            try
            {
                result = checksum.Equals(OnGenerateInBase64(file, cancellationToken));
            }
            catch (OperationCanceledException)
            {
                Logger.GetInstance(typeof(Md5)).Warn("Validating checksum in base64 cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal($"Validating checksum in base64 error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Validates the file in Base64 form.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        public bool ValidateInBase64(string content, string checksum)
        {
            if (content == null || string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            var result = false;
            try
            {
                result = checksum.Equals(OnGenerateInBase64(content));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal($"Validating checksum in base64 error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Validates the file in hexadecimal form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        public bool ValidateInHex(FileInfo file, string checksum)
        {
            return ValidateInHex(file, checksum, new CancellationToken());
        }

        /// <summary>
        /// Validates the file in hexadecimal form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="checksum">The checksum.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if form, <c>false</c> otherwise.</returns>
        public bool ValidateInHex(FileInfo file, string checksum, CancellationToken cancellationToken)
        {
            if (file == null || !file.Exists || string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            var result = false;
            try
            {
                result = checksum.ToLowerInvariant().Equals(OnGenerateInHex(file, cancellationToken));
            }
            catch (OperationCanceledException)
            {
                Logger.GetInstance(typeof(Md5)).Warn("Validating checksum in hex cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal($"Validating checksum in hex error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Validates the content in hexadecimal form.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="checksum">The checksum.</param>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        public bool ValidateInHex(string content, string checksum)
        {
            if (content == null || string.IsNullOrWhiteSpace(checksum))
            {
                return false;
            }

            var result = false;
            try
            {
                result = checksum.ToLowerInvariant().Equals(OnGenerateInHex(content));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal($"Validating checksum in hex error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Called when generating the checksum in Base64 form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnGenerateInBase64(FileInfo file, CancellationToken cancellationToken);
        /// <summary>
        /// Called when generating the checksum in Base64 form.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnGenerateInBase64(string content);
        /// <summary>
        /// Called when generating the checksum in hexadecimal form.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnGenerateInHex(FileInfo file, CancellationToken cancellationToken);
        /// <summary>
        /// Called when generating the checksum in hexadecimal form.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnGenerateInHex(string content);
    }
}
