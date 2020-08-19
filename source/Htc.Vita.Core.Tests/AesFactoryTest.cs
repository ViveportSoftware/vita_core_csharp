using System.Text;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Util;
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
            var outputInHex = Convert.ToHexString(outputInBytes);
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

        [Fact]
        public static void Aes_2_Decrypt_WithDataFromJava()
        {
            var aesFactory = AesFactory.GetInstance();
            Assert.NotNull(aesFactory);
            var aesDecryptor = aesFactory.Get();
            Assert.NotNull(aesDecryptor);
            const string encodedString = "ef02a816e71965c02f8a182cbf0f6c97427e9a23a3f4170db856b5e883ed502c";
            var encodedData = Convert.FromHexString(encodedString);
            Assert.NotNull(encodedData);
            const string password = "p@ssword";
            var decodedData = aesDecryptor.Decrypt(encodedData, password);
            Assert.NotNull(decodedData);
            var decodedString = Encoding.UTF8.GetString(decodedData);
            Assert.Equal("test data", decodedString);
        }
    }
}
