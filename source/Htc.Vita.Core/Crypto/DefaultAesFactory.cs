namespace Htc.Vita.Core.Crypto
{
    /// <summary>
    /// Class DefaultAesFactory.
    /// Implements the <see cref="AesFactory" />
    /// </summary>
    /// <seealso cref="AesFactory" />
    public class DefaultAesFactory : AesFactory
    {
        /// <inheritdoc />
        protected override Aes OnGet(Aes.CipherMode cipherMode, Aes.PaddingMode paddingMode)
        {
            return new DefaultAes
            {
                    Cipher = cipherMode,
                    Padding = paddingMode
            };
        }
    }
}
