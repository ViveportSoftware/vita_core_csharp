using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    public abstract partial class Sha1
    {
        private static Dictionary<string, Sha1> Instances { get; } = new Dictionary<string, Sha1>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(DefaultSha1);

        private const int Base64Length = 28; // "2jmj7l5rSw0yVb/vlWAYkK/YBwk="
        private const int HexLength = 40;    // "da39a3ee5e6b4b0d3255bfef95601890afd80709"

        public static void Register<T>() where T : Sha1
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(Sha1)).Info("Registered default " + typeof(Sha1).Name + " type to " + _defaultType);
        }

        public static Sha1 GetInstance()
        {
            Sha1 instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha1)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(Sha1)).Info("Initializing " + typeof(DefaultSha1).FullName + "...");
                instance = new DefaultSha1();
            }
            return instance;
        }

        private static Sha1 DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get " + typeof(Sha1).Name + " instance");
            }

            var key = type.FullName + "_";
            Sha1 instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Sha1)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (Sha1)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Sha1)).Info("Initializing " + typeof(DefaultSha1).FullName + "...");
                instance = new DefaultSha1();
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

        public string GenerateInBase64(FileInfo file)
        {
            return GenerateInBase64(file, new CancellationToken());
        }

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
                Logger.GetInstance(typeof(Sha1)).Warn("Generating checksum in base64 cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha1)).Fatal("Generating checksum in base64 error: " + e);
            }
            return result;
        }

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
                Logger.GetInstance(typeof(Sha1)).Fatal("Generating checksum in base64 error: " + e);
            }
            return result;
        }

        public string GenerateInHex(FileInfo file)
        {
            return GenerateInHex(file, new CancellationToken());
        }

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
                Logger.GetInstance(typeof(Sha1)).Warn("Generating checksum in hex cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha1)).Fatal("Generating checksum in hex error: " + e);
            }
            return result;
        }

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
                Logger.GetInstance(typeof(Sha1)).Fatal("Generating checksum in hex error: " + e);
            }
            return result;
        }

        public bool ValidateInAll(FileInfo file, string checksum)
        {
            return ValidateInAll(file, checksum, new CancellationToken());
        }

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

        public bool ValidateInBase64(FileInfo file, string checksum)
        {
            return ValidateInBase64(file, checksum, new CancellationToken());
        }

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
                Logger.GetInstance(typeof(Sha1)).Warn("Validating checksum in base64 cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha1)).Fatal("Validating checksum in base64 error: " + e);
            }
            return result;
        }

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
                Logger.GetInstance(typeof(Sha1)).Fatal("Validating checksum in base64 error: " + e);
            }
            return result;
        }

        public bool ValidateInHex(FileInfo file, string checksum)
        {
            return ValidateInHex(file, checksum, new CancellationToken());
        }

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
                Logger.GetInstance(typeof(Sha1)).Warn("Validating checksum in hex cancelled");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha1)).Fatal("Validating checksum in hex error: " + e);
            }
            return result;
        }

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
                Logger.GetInstance(typeof(Sha1)).Fatal("Validating checksum in hex error: " + e);
            }
            return result;
        }

        protected abstract string OnGenerateInBase64(FileInfo file, CancellationToken cancellationToken);
        protected abstract string OnGenerateInBase64(string content);
        protected abstract string OnGenerateInHex(FileInfo file, CancellationToken cancellationToken);
        protected abstract string OnGenerateInHex(string content);
    }
}
