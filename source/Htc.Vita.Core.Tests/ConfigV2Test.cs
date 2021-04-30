using Htc.Vita.Core.Config;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class ConfigV2Test
    {
        [Fact]
        public static void Dummy_0_GetInstance()
        {
            var configV2 = ConfigV2.GetInstance();
            Assert.NotNull(configV2);
        }

        [Fact]
        public static void Dummy_1_HasKey()
        {
            var configV2 = ConfigV2.GetInstance();
            Assert.NotNull(configV2);
            Assert.False(configV2.HasKey("test"));
        }

        [Fact]
        public static void Dummy_2_Get()
        {
            var configV2 = ConfigV2.GetInstance();
            Assert.NotNull(configV2);
            Assert.Null(configV2.Get("test"));
        }

        [Fact]
        public static void Dummy_3_GetBool()
        {
            var configV2 = ConfigV2.GetInstance();
            Assert.NotNull(configV2);
            Assert.False(configV2.GetBool("test"));
        }

        [Fact]
        public static void Dummy_4_GetDouble()
        {
            var configV2 = ConfigV2.GetInstance();
            Assert.NotNull(configV2);
            Assert.Equal(0d, configV2.GetDouble("test"));
        }

        [Fact]
        public static void Dummy_5_GetFloat()
        {
            var configV2 = ConfigV2.GetInstance();
            Assert.NotNull(configV2);
            Assert.Equal(0f, configV2.GetFloat("test"));
        }

        [Fact]
        public static void Dummy_6_GetInt()
        {
            var configV2 = ConfigV2.GetInstance();
            Assert.NotNull(configV2);
            Assert.Equal(0, configV2.GetInt("test"));
        }

        [Fact]
        public static void Dummy_7_GetLong()
        {
            var configV2 = ConfigV2.GetInstance();
            Assert.NotNull(configV2);
            Assert.Equal(0L, configV2.GetLong("test"));
        }

        [Fact]
        public static void Dummy_8_AllKeys()
        {
            var configV2 = ConfigV2.GetInstance();
            Assert.NotNull(configV2);
            var allKeys = configV2.AllKeys();
            Assert.NotNull(allKeys);
            Assert.Equal(0, allKeys.Count);
        }
    }
}
