using System;
using System.IO;
using System.Security.Cryptography;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    public class DefaultAes : Aes
    {
        private const int KeySize128BitInBit = 128;
        private const int KeySize128BitInByte = KeySize128BitInBit / 8;
        private const int KeySize192BitInBit = 192;
        private const int KeySize192BitInByte = KeySize192BitInBit / 8;
        private const int KeySize256BitInBit = 256;
        private const int KeySize256BitInByte = KeySize256BitInBit / 8;
        private const int IvSize128BitInBit = 128;
        private const int IvSize128BitInByte = IvSize128BitInBit / 8;
        private const int SaltSize128BitInBit = 128;
        private const int SaltSize128BitInByte = SaltSize128BitInBit / 8;

        private CipherMode _cipherMode = CipherMode.Cbc;
        private PaddingMode _paddingMode = PaddingMode.Pkcs7;

        protected override byte[] OnDecrypt(byte[] input, string password)
        {
            var encryptedDataLength = input.Length - SaltSize128BitInByte;
            if (encryptedDataLength <= 0)
            {
                Logger.GetInstance().Error("input cipher text is malformed");
                return null;
            }

            var salt = new byte[SaltSize128BitInByte];
            var encryptedData = new byte[encryptedDataLength];
            using (var resultStream = new MemoryStream(input))
            {
                using (var binaryReader = new BinaryReader(resultStream))
                {
                    binaryReader.Read(
                            salt,
                            0,
                            SaltSize128BitInByte
                    );
                    binaryReader.Read(
                            encryptedData,
                            0,
                            encryptedDataLength
                    );
                }
            }

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt))
            {
                var key = deriveBytes.GetBytes(KeySize256BitInByte);
                var iv = deriveBytes.GetBytes(IvSize128BitInByte);
                return OnDecrypt(
                        encryptedData,
                        key,
                        iv
                );
            }
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
                    Logger.GetInstance().Info("can not create aes instance");
                    return null;
                }

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

        protected override byte[] OnEncrypt(byte[] input, string password)
        {
            var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize128BitInByte);
            var salt = deriveBytes.Salt;
            var key = deriveBytes.GetBytes(KeySize256BitInByte);
            var iv = deriveBytes.GetBytes(IvSize128BitInByte);

            var encryptedBytes = OnEncrypt(
                    input,
                    key,
                    iv
            );

            using (var memoryStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(salt);
                    binaryWriter.Write(encryptedBytes);
                }
                return memoryStream.ToArray();
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
                    Logger.GetInstance().Info("can not create aes instance");
                    return null;
                }

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

        protected override Aes OnSetCipherMode(CipherMode cipherMode)
        {
            if (_cipherMode == cipherMode)
            {
                return this;
            }

            Logger.GetInstance().Info("set cipher mode to " + cipherMode);
            _cipherMode = cipherMode;
            return this;
        }

        protected override Aes OnSetPaddingMode(PaddingMode paddingMode)
        {
            if (_paddingMode == paddingMode)
            {
                return this;
            }

            Logger.GetInstance().Info("set padding mode to " + paddingMode);
            _paddingMode = paddingMode;
            return this;
        }
    }
}
