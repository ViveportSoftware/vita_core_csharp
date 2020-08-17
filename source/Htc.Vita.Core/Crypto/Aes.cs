using System;
using System.IO;
using System.Security.Cryptography;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    /// <summary>
    /// Class Aes.
    /// </summary>
    public abstract class Aes
    {
        /// <summary>
        /// Gets the 128-bit IV size in bit.
        /// </summary>
        /// <value>The 128-bit IV size in bit.</value>
        public static int IvSize128BitInBit { get; } = 128;
        /// <summary>
        /// Gets the 128-bit IV size in byte.
        /// </summary>
        /// <value>The 128-bit IV size in byte.</value>
        public static int IvSize128BitInByte { get; } = IvSize128BitInBit / 8;
        /// <summary>
        /// Gets the 128-bit key size in bit.
        /// </summary>
        /// <value>The 128-bit key size in bit.</value>
        public static int KeySize128BitInBit { get; } = 128;
        /// <summary>
        /// Gets the 128-bit key size in byte.
        /// </summary>
        /// <value>The 128-bit key size in byte.</value>
        public static int KeySize128BitInByte { get; } = KeySize128BitInBit / 8;
        /// <summary>
        /// Gets the 192-bit key size in bit.
        /// </summary>
        /// <value>The 192-bit key size in bit.</value>
        public static int KeySize192BitInBit { get; } = 192;
        /// <summary>
        /// Gets the 192-bit key size in byte.
        /// </summary>
        /// <value>The 192-bit key size in byte.</value>
        public static int KeySize192BitInByte { get; } = KeySize192BitInBit / 8;
        /// <summary>
        /// Gets the 256-bit key size in bit.
        /// </summary>
        /// <value>The 256-bit key size in bit.</value>
        public static int KeySize256BitInBit { get; } = 256;
        /// <summary>
        /// Gets the 256-bit key size in byte.
        /// </summary>
        /// <value>The 256-bit key size in byte.</value>
        public static int KeySize256BitInByte { get; } = KeySize256BitInBit / 8;
        /// <summary>
        /// Gets the 128-bit salt size in bit.
        /// </summary>
        /// <value>The 128-bit salt size in bit.</value>
        public static int SaltSize128BitInBit { get; } = 128;
        /// <summary>
        /// Gets the 128-bit salt size in byte.
        /// </summary>
        /// <value>The 128-bit salt size in byte.</value>
        public static int SaltSize128BitInByte { get; } = SaltSize128BitInBit / 8;

        private CipherMode _cipherMode = CipherMode.Cbc;
        private PaddingMode _paddingMode = PaddingMode.Pkcs7;

        /// <summary>
        /// Gets or sets the cipher.
        /// </summary>
        /// <value>The cipher.</value>
        public CipherMode Cipher
        {
            get { return _cipherMode; }
            set
            {
                if (value == _cipherMode)
                {
                    return;
                }
                Logger.GetInstance(typeof(Aes)).Info($"set cipher mode to {value}");
                _cipherMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        /// <value>The padding.</value>
        public PaddingMode Padding
        {
            get { return _paddingMode; }
            set
            {
                if (value == _paddingMode)
                {
                    return;
                }
                Logger.GetInstance(typeof(Aes)).Info($"set padding mode to {value}");
                _paddingMode = value;
            }
        }

        /// <summary>
        /// Decrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="password">The password.</param>
        /// <returns>System.Byte[].</returns>
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
                Logger.GetInstance(typeof(Aes)).Error("input cipher text is malformed");
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

                using (var deriveBytes = new Rfc2898DeriveBytes(
                        password,
                        salt))
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
                Logger.GetInstance(typeof(Aes)).Fatal($"Decrypt input with password error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Decrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] Decrypt(
                byte[] input,
                byte[] key,
                byte[] iv)
        {
            if (input == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find input to decrypt");
                return null;
            }

            if (key == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find key to decrypt");
                return null;
            }

            if (iv == null)
            {
                Logger.GetInstance(typeof(Aes)).Warn("Can not find iv to decrypt");
                return null;
            }

            byte[] result = null;
            try
            {
                result = OnDecrypt(
                        input,
                        key,
                        iv
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Aes)).Fatal($"Decrypt input with key and iv error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Encrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="password">The password.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] Encrypt(
                byte[] input,
                string password)
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

            byte[] result = null;
            using (var deriveBytes = new Rfc2898DeriveBytes(
                    password,
                    SaltSize128BitInByte))
            {
                var salt = deriveBytes.Salt;
                var key = deriveBytes.GetBytes(KeySize256BitInByte);
                var iv = deriveBytes.GetBytes(IvSize128BitInByte);

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
                    Logger.GetInstance(typeof(Aes)).Fatal($"Encrypt input with password error: {e}");
                }
            }
            return result;
        }

        /// <summary>
        /// Encrypts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] Encrypt(
                byte[] input,
                byte[] key,
                byte[] iv)
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
                result = OnEncrypt(
                        input,
                        key,
                        iv
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Aes)).Fatal($"Encrypt input with key and iv error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Called when decrypting.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns>System.Byte[].</returns>
        protected abstract byte[] OnDecrypt(
                byte[] input,
                byte[] key,
                byte[] iv
        );
        /// <summary>
        /// Called when encrypting.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns>System.Byte[].</returns>
        protected abstract byte[] OnEncrypt(
                byte[] input,
                byte[] key,
                byte[] iv
        );

        /// <summary>
        /// Enum CipherMode
        /// </summary>
        public enum CipherMode
        {
            /// <summary>
            /// CBC
            /// </summary>
            Cbc
        }

        /// <summary>
        /// Enum PaddingMode
        /// </summary>
        public enum PaddingMode
        {
            /// <summary>
            /// PKCS7
            /// </summary>
            Pkcs7
        }
    }
}
