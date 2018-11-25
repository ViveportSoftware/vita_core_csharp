using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    public abstract partial class Md5
    {
        private static Dictionary<string, Md5> Instances { get; } = new Dictionary<string, Md5>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(DefaultMd5);

        private const int Base64Length = 24; // "pq/Xu7jVnluxLJ28xOws/w=="
        private const int HexLength = 32;    // "202cb962ac59075b964b07152d234b70"

        public static void Register<T>() where T : Md5
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(Md5)).Info("Registered default " + typeof(Md5).Name + " type to " + _defaultType);
        }

        public static Md5 GetInstance()
        {
            Md5 instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(Md5)).Info("Initializing " + typeof(DefaultMd5).FullName + "...");
                instance = new DefaultMd5();
            }
            return instance;
        }

        private static Md5 DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get " + typeof(Md5).Name + " instance");
            }

            var key = type.FullName + "_";
            Md5 instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Md5)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (Md5)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Md5)).Info("Initializing " + typeof(DefaultMd5).FullName + "...");
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
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal("Generating checksum in base64 error: " + e);
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
                Logger.GetInstance(typeof(Md5)).Fatal("Generating checksum in base64 error: " + e);
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
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal("Generating checksum in hex error: " + e);
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
                Logger.GetInstance(typeof(Md5)).Fatal("Generating checksum in hex error: " + e);
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
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal("Validating checksum in base64 error: " + e);
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
                Logger.GetInstance(typeof(Md5)).Fatal("Validating checksum in base64 error: " + e);
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
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Md5)).Fatal("Validating checksum in hex error: " + e);
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
                Logger.GetInstance(typeof(Md5)).Fatal("Validating checksum in hex error: " + e);
            }
            return result;
        }

        protected abstract string OnGenerateInBase64(FileInfo file, CancellationToken cancellationToken);
        protected abstract string OnGenerateInBase64(string content);
        protected abstract string OnGenerateInHex(FileInfo file, CancellationToken cancellationToken);
        protected abstract string OnGenerateInHex(string content);
    }
}
