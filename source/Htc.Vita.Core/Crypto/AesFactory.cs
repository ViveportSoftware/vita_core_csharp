using System;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Crypto
{
    /// <summary>
    /// Class AesFactory.
    /// </summary>
    public abstract class AesFactory
    {
        static AesFactory()
        {
            TypeRegistry.RegisterDefault<AesFactory, DefaultAesFactory>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : AesFactory, new()
        {
            TypeRegistry.Register<AesFactory, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>AesFactory.</returns>
        public static AesFactory GetInstance()
        {
            return TypeRegistry.GetInstance<AesFactory>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>AesFactory.</returns>
        public static AesFactory GetInstance<T>()
                where T : AesFactory, new()
        {
            return TypeRegistry.GetInstance<AesFactory, T>();
        }

        /// <summary>
        /// Gets AES instance.
        /// </summary>
        /// <returns>Aes.</returns>
        public Aes Get()
        {
            return Get(
                    Aes.CipherMode.Cbc,
                    Aes.PaddingMode.Pkcs7
            );
        }

        /// <summary>
        /// Gets AES instance with the specified cipher mode and padding mode.
        /// </summary>
        /// <param name="cipherMode">The cipher mode.</param>
        /// <param name="paddingMode">The padding mode.</param>
        /// <returns>Aes.</returns>
        public Aes Get(
                Aes.CipherMode cipherMode,
                Aes.PaddingMode paddingMode)
        {
            Aes result = null;
            try
            {
                result = OnGet(
                        cipherMode,
                        paddingMode
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(AesFactory)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when getting AES instance.
        /// </summary>
        /// <param name="cipherMode">The cipher mode.</param>
        /// <param name="paddingMode">The padding mode.</param>
        /// <returns>Aes.</returns>
        protected abstract Aes OnGet(
                Aes.CipherMode cipherMode,
                Aes.PaddingMode paddingMode
        );
    }
}
