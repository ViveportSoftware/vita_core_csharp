namespace Htc.Vita.Core.Crypto
{
    public class DefaultAesFactory : AesFactory
    {
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
