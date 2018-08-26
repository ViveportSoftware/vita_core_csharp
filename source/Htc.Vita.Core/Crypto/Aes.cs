using System;
using System.IO;
using System.Security.Cryptography;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    public abstract class Aes
    {
        public const int IvSize128BitInBit = 128;
        public const int IvSize128BitInByte = IvSize128BitInBit / 8;
        public const int KeySize128BitInBit = 128;
        public const int KeySize128BitInByte = KeySize128BitInBit / 8;
        public const int KeySize192BitInBit = 192;
        public const int KeySize192BitInByte = KeySize192BitInBit / 8;
        public const int KeySize256BitInBit = 256;
        public const int KeySize256BitInByte = KeySize256BitInBit / 8;
        public const int SaltSize128BitInBit = 128;
        public const int SaltSize128BitInByte = SaltSize128BitInBit / 8;

        private CipherMode _cipherMode = CipherMode.Cbc;
        private PaddingMode _paddingMode = PaddingMode.Pkcs7;

        public CipherMode Cipher
        {
            get { return _cipherMode; }
            set
            {
                if (value == _cipherMode)
                {
                    return;
                }
                Logger.GetInstance().Info("set cipher mode to " + value);
                _cipherMode = value;
            }
        }

        public PaddingMode Padding
        {
            get { return _paddingMode; }
            set
            {
                if (value == _paddingMode)
                {
                    return;
                }
                Logger.GetInstance().Info("set padding mode to " + value);
                _paddingMode = value;
            }
        }

        public byte[] Decrypt(byte[] input, string password)
        {
            if (input == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find input to decrypt");
                return null;
            }

            if (password == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find password to decrypt");
                return null;
            }

            var encryptedDataLength = input.Length - SaltSize128BitInByte;
            if (encryptedDataLength <= 0)
            {
                Logger.GetInstance().Error("input cipher text is malformed");
                return null;
            }

            var salt = new byte[SaltSize128BitInByte];
            var encryptedData = new byte[encryptedDataLength];
            byte[] result = null;
            try
            {
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
                    result = Decrypt(
                            encryptedData,
                            key,
                            iv
                    );
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Aes)).Fatal("Decrypt input with password error: " + e);
            }
            return result;
        }

        public byte[] Decrypt(byte[] input, byte[] key, byte[] iv)
        {
            if (input == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find input to encrypt");
                return null;
            }

            if (key == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find key to encrypt");
                return null;
            }

            if (iv == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find iv to encrypt");
                return null;
            }

            byte[] result = null;
            try
            {
                result = OnDecrypt(input, key, iv);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Aes)).Fatal("Encrypt input with key and iv error: " + e);
            }
            return result;
        }

        public byte[] Encrypt(byte[] input, string password)
        {
            if (input == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find input to encrypt");
                return null;
            }

            if (password == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find password to encrypt");
                return null;
            }

            var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize128BitInByte);
            var salt = deriveBytes.Salt;
            var key = deriveBytes.GetBytes(KeySize256BitInByte);
            var iv = deriveBytes.GetBytes(IvSize128BitInByte);

            byte[] result = null;
            try
            {
                var encryptedBytes = Encrypt(
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
                    result = memoryStream.ToArray();
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Aes)).Fatal("Encrypt input with password error: " + e);
            }
            return result;
        }

        public byte[] Encrypt(byte[] input, byte[] key, byte[] iv)
        {
            if (input == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find input to encrypt");
                return null;
            }

            if (key == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find key to encrypt");
                return null;
            }

            if (iv == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find iv to encrypt");
                return null;
            }

            byte[] result = null;
            try
            {
                result = OnEncrypt(input, key, iv);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Aes)).Fatal("Encrypt input with key and iv error: " + e);
            }
            return result;
        }

        protected abstract byte[] OnDecrypt(byte[] input, byte[] key, byte[] iv);
        protected abstract byte[] OnEncrypt(byte[] input, byte[] key, byte[] iv);

        public enum CipherMode
        {
            Cbc
        }

        public enum PaddingMode
        {
            Pkcs7
        }
    }
}
