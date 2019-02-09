using Htc.Vita.Core.Config;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class Config
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
        }

        [Fact]
        public static void Default_1_Get()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("intKey");
            Assert.Equal("1", value);
        }

        [Fact]
        public static void Default_1_Get_WithDefault()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("intKey2", "2");
            Assert.Equal("2", value);
        }

        [Fact]
        public static void Default_2_GetBool()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("boolKey");
            Assert.Equal("true", value);
            var value2 = config.GetBool("boolKey");
            Assert.True(value2);
        }

        [Fact]
        public static void Default_2_GetBool_WithDefault()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("boolKey2");
            Assert.Null(value);
            var value2 = config.GetBool("boolKey2");
            Assert.False(value2);
            var value3 = config.GetBool("boolKey2", true);
            Assert.True(value3);
        }

        [Fact]
        public static void Default_3_GetDouble()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("doubleKey");
            Assert.Equal("3.3", value);
            var value2 = config.GetDouble("doubleKey");
            Assert.Equal(3.3D, value2);
        }

        [Fact]
        public static void Default_3_GetDouble_WithDefault()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("doubleKey2");
            Assert.Null(value);
            var value2 = config.GetDouble("doubleKey2");
            Assert.Equal(0.0D, value2);
            var value3 = config.GetDouble("doubleKey2", 13.3D);
            Assert.Equal(13.3D, value3);
        }

        [Fact]
        public static void Default_4_GetInt()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("intKey");
            Assert.Equal("1", value);
            var value2 = config.GetInt("intKey");
            Assert.Equal(1, value2);
        }

        [Fact]
        public static void Default_4_GetInt_WithDefault()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("intKey2");
            Assert.Null(value);
            var value2 = config.GetInt("intKey2");
            Assert.Equal(0, value2);
            var value3 = config.GetInt("intKey2", 10);
            Assert.Equal(10, value3);
        }

        [Fact]
        public static void Default_5_GetLong()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("longKey");
            Assert.Equal("100000000000", value);
            var value2 = config.GetLong("longKey");
            Assert.Equal(100000000000L, value2);
        }

        [Fact]
        public static void Default_5_GetLong_WithDefault()
        {
            var config = Core.Config.Config.GetInstance();
            Assert.NotNull(config);
            var value = config.Get("longKey2");
            Assert.Null(value);
            var value2 = config.GetLong("longKey2");
            Assert.Equal(0L, value2);
            var value3 = config.GetLong("longKey2", 200000000000L);
            Assert.Equal(200000000000L, value3);
        }

        [Fact]
        public static void AppSettings_0_GetInstance()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
        }

        [Fact]
        public static void AppSettings_1_Get()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("intKey");
            Assert.Equal("1", value);
        }

        [Fact]
        public static void AppSettings_1_Get_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("intKey2", "2");
            Assert.Equal("2", value);
        }

        [Fact]
        public static void AppSettings_2_GetBool()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("boolKey");
            Assert.Equal("true", value);
            var value2 = config.GetBool("boolKey");
            Assert.True(value2);
        }

        [Fact]
        public static void AppSettings_2_GetBool_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("boolKey2");
            Assert.Null(value);
            var value2 = config.GetBool("boolKey2");
            Assert.False(value2);
            var value3 = config.GetBool("boolKey2", true);
            Assert.True(value3);
        }

        [Fact]
        public static void AppSettings_3_GetDouble()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("doubleKey");
            Assert.Equal("3.3", value);
            var value2 = config.GetDouble("doubleKey");
            Assert.Equal(3.3D, value2);
        }

        [Fact]
        public static void AppSettings_3_GetDouble_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("doubleKey2");
            Assert.Null(value);
            var value2 = config.GetDouble("doubleKey2");
            Assert.Equal(0.0D, value2);
            var value3 = config.GetDouble("doubleKey2", 13.3D);
            Assert.Equal(13.3D, value3);
        }

        [Fact]
        public static void AppSettings_4_GetInt()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("intKey");
            Assert.Equal("1", value);
            var value2 = config.GetInt("intKey");
            Assert.Equal(1, value2);
        }

        [Fact]
        public static void AppSettings_4_GetInt_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("intKey2");
            Assert.Null(value);
            var value2 = config.GetInt("intKey2");
            Assert.Equal(0, value2);
            var value3 = config.GetInt("intKey2", 10);
            Assert.Equal(10, value3);
        }

        [Fact]
        public static void AppSettings_5_GetLong()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("longKey");
            Assert.Equal("100000000000", value);
            var value2 = config.GetLong("longKey");
            Assert.Equal(100000000000L, value2);
        }

        [Fact]
        public static void AppSettings_5_GetLong_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<AppSettingsConfig>();
            Assert.NotNull(config);
            var value = config.Get("longKey2");
            Assert.Null(value);
            var value2 = config.GetLong("longKey2");
            Assert.Equal(0L, value2);
            var value3 = config.GetLong("longKey2", 200000000000L);
            Assert.Equal(200000000000L, value3);
        }

        [Fact]
        public static void Registry_0_GetInstance()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
        }

        [Fact(Skip = "PredefinedGlobalDataNeeded")]
        public static void Registry_1_Get()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("intKey");
            Assert.Equal("1", value);
        }

        [Fact]
        public static void Registry_1_Get_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("intKey2", "2");
            Assert.Equal("2", value);
        }

        [Fact(Skip = "PredefinedGlobalDataNeeded")]
        public static void Registry_2_GetBool()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("boolKey");
            Assert.Equal("true", value);
            var value2 = config.GetBool("boolKey");
            Assert.True(value2);
        }

        [Fact]
        public static void Registry_2_GetBool_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("boolKey2");
            Assert.Null(value);
            var value2 = config.GetBool("boolKey2");
            Assert.False(value2);
            var value3 = config.GetBool("boolKey2", true);
            Assert.True(value3);
        }

        [Fact(Skip = "PredefinedGlobalDataNeeded")]
        public static void Registry_3_GetDouble()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("doubleKey");
            Assert.Equal("3.3", value);
            var value2 = config.GetDouble("doubleKey");
            Assert.Equal(3.3D, value2);
        }

        [Fact]
        public static void Registry_3_GetDouble_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("doubleKey2");
            Assert.Null(value);
            var value2 = config.GetDouble("doubleKey2");
            Assert.Equal(0.0D, value2);
            var value3 = config.GetDouble("doubleKey2", 13.3D);
            Assert.Equal(13.3D, value3);
        }

        [Fact(Skip = "PredefinedGlobalDataNeeded")]
        public static void Registry_4_GetInt()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("intKey");
            Assert.Equal("1", value);
            var value2 = config.GetInt("intKey");
            Assert.Equal(1, value2);
        }

        [Fact]
        public static void Registry_4_GetInt_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("intKey2");
            Assert.Null(value);
            var value2 = config.GetInt("intKey2");
            Assert.Equal(0, value2);
            var value3 = config.GetInt("intKey2", 10);
            Assert.Equal(10, value3);
        }

        [Fact(Skip = "PredefinedGlobalDataNeeded")]
        public static void Registry_5_GetLong()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("longKey");
            Assert.Equal("100000000000", value);
            var value2 = config.GetLong("longKey");
            Assert.Equal(100000000000L, value2);
        }

        [Fact]
        public static void Registry_5_GetLong_WithDefault()
        {
            var config = Core.Config.Config.GetInstance<RegistryConfig>();
            Assert.NotNull(config);
            var value = config.Get("longKey2");
            Assert.Null(value);
            var value2 = config.GetLong("longKey2");
            Assert.Equal(0L, value2);
            var value3 = config.GetLong("longKey2", 200000000000L);
            Assert.Equal(200000000000L, value3);
        }
    }
}
