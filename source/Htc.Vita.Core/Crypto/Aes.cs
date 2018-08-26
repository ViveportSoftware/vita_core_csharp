using System;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    public abstract class Aes
    {
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

            byte[] result = null;
            try
            {
                result = OnDecrypt(input, password);
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

            byte[] result = null;
            try
            {
                result = OnEncrypt(input, password);
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

        protected abstract byte[] OnDecrypt(byte[] input, string password);
        protected abstract byte[] OnDecrypt(byte[] input, byte[] key, byte[] iv);
        protected abstract byte[] OnEncrypt(byte[] input, string password);
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
