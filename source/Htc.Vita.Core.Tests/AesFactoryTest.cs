using System.Text;
using Htc.Vita.Core.Crypto;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class AesFactoryTest
    {
        private readonly ITestOutputHelper _output;

        public AesFactoryTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Default_0_GetInstance()
        {
            var aesFactory = AesFactory.GetInstance();
            Assert.NotNull(aesFactory);
        }

        [Fact]
        public static void Default_1_Get()
        {
            var aesFactory = AesFactory.GetInstance();
            Assert.NotNull(aesFactory);
            var aes = aesFactory.Get();
            Assert.NotNull(aes);
        }

        [Fact]
        public static void Aes_0_Encrypt_WithInput_WithPassword()
        {
            var aesFactory = AesFactory.GetInstance();
            Assert.NotNull(aesFactory);
            var aes = aesFactory.Get();
            Assert.NotNull(aes);
            const string plain = "data";
            const string password = "p@ssword";
            var inputInBytes = Encoding.UTF8.GetBytes(plain);
            var outputInBytes = aes.Encrypt(inputInBytes, password);
            Assert.NotNull(outputInBytes);
        }

        [Fact]
        public void Aes_0_Encrypt_WithEmptyInput_WithPassword()
        {
            var aesFactory = AesFactory.GetInstance();
            Assert.NotNull(aesFactory);
            var aes = aesFactory.Get();
            Assert.NotNull(aes);
            const string plain = "";
            const string password = "p@ssword";
            var inputInBytes = Encoding.UTF8.GetBytes(plain);
            var outputInBytes = aes.Encrypt(inputInBytes, password);
            Assert.NotNull(outputInBytes);
            var outputInHex = Util.Convert.ToHexString(outputInBytes);
            _output.WriteLine("outputInHex: " + outputInHex);
        }

        [Fact]
        public static void Aes_1_Decrypt_WithPassword()
        {
            var aesFactory = AesFactory.GetInstance();
            Assert.NotNull(aesFactory);
            var aesEncryptor = aesFactory.Get();
            Assert.NotNull(aesEncryptor);
            const string plain = "test data";
            const string password = "p@ssword";
            var plainInBytes = Encoding.UTF8.GetBytes(plain);
            var encryptedInBytes = aesEncryptor.Encrypt(plainInBytes, password);
            Assert.NotNull(encryptedInBytes);
            var aesDecryptor = aesFactory.Get();
            Assert.NotEqual(aesDecryptor, aesEncryptor);
            var decryptedInBytes = aesDecryptor.Decrypt(encryptedInBytes, password);
            var decrypted = Encoding.UTF8.GetString(decryptedInBytes);
            Assert.Equal(plain, decrypted);
        }
    }
}
