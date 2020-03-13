using System;
using System.IO;
using System.Security.Cryptography;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    public class DefaultAes : Aes
    {
        private static System.Security.Cryptography.CipherMode ConvertToImpl(CipherMode cipherMode)
        {
            if (cipherMode == CipherMode.Cbc)
            {
                return System.Security.Cryptography.CipherMode.CBC;
            }
            Logger.GetInstance(typeof(DefaultAes)).Error("unknown cipher mode: " + cipherMode);
            return System.Security.Cryptography.CipherMode.CBC;
        }

        private static System.Security.Cryptography.PaddingMode ConvertToImpl(PaddingMode paddingMode)
        {
            if (paddingMode == PaddingMode.Pkcs7)
            {
                return System.Security.Cryptography.PaddingMode.PKCS7;
            }
            Logger.GetInstance(typeof(DefaultAes)).Error("unknown padding mode: " + paddingMode);
            return System.Security.Cryptography.PaddingMode.PKCS7;
        }

        protected override byte[] OnDecrypt(byte[] input, byte[] key, byte[] iv)
        {
            if (iv == null || iv.Length != IvSize128BitInByte)
            {
                throw new ArgumentException("iv size is not match");
            }

            if (key.Length != KeySize128BitInByte
                    && key.Length != KeySize192BitInByte
                    && key.Length != KeySize256BitInByte)
            {
                throw new ArgumentException("key size is not match");
            }

            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                if (aes == null)
                {
                    Logger.GetInstance(typeof(DefaultAes)).Info("can not create aes instance");
                    return null;
                }

                aes.Mode = ConvertToImpl(Cipher);
                aes.Padding = ConvertToImpl(Padding);

                using (var decryptor = aes.CreateDecryptor(key, iv))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(
                                    input,
                                    0,
                                    input.Length
                            );
                        }
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        protected override byte[] OnEncrypt(byte[] input, byte[] key, byte[] iv)
        {
            if (iv == null || iv.Length != IvSize128BitInByte)
            {
                throw new ArgumentException("iv size is not match");
            }

            if (key.Length != KeySize128BitInByte
                    && key.Length != KeySize192BitInByte
                    && key.Length != KeySize256BitInByte)
            {
                throw new ArgumentException("key size is not match");
            }

            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                if (aes == null)
                {
                    Logger.GetInstance(typeof(DefaultAes)).Info("can not create aes instance");
                    return null;
                }

                aes.Mode = ConvertToImpl(Cipher);
                aes.Padding = ConvertToImpl(Padding);

                using (var encryptor = aes.CreateEncryptor(key, iv))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(
                                    input,
                                    0,
                                    input.Length
                            );
                        }
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }
}
