using System;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Crypto
{
    public abstract class Aes
    {
        public Aes SetCipherMode(CipherMode cipherMode)
        {
            return OnSetCipherMode(cipherMode);
        }

        public Aes SetPaddingMode(PaddingMode paddingMode)
        {
            return OnSetPaddingMode(paddingMode);
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
        protected abstract Aes OnSetCipherMode(CipherMode cipherMode);
        protected abstract Aes OnSetPaddingMode(PaddingMode paddingMode);

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
