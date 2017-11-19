using Htc.Vita.Core.Preference;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public static void PreferenceFactory_Default_0_GetInstance()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
        }

        [Fact]
        public static void PreferenceFactory_Default_1_GetInstance_WithType()
        {
            var preferenceFactory = PreferenceFactory.GetInstance<DefaultPreferenceFactory>();
            Assert.NotNull(preferenceFactory);
        }

        [Fact]
        public static void PreferenceFactory_Default_2_LoadPreferences()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            Assert.Equal("DefaultPreferences", preferences.ToString());
        }

        [Fact]
        public static void Preference_Default_00_PutBool()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", true);
            Assert.NotNull(preferences);
        }

        [Fact]
        public static void Preference_Default_01_PutDouble()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 1.1D);
            Assert.NotNull(preferences);
        }

        [Fact]
        public static void Preference_Default_02_PutFloat()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 2.2F);
            Assert.NotNull(preferences);
        }

        [Fact]
        public static void Preference_Default_03_PutInt()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 1);
            Assert.NotNull(preferences);
        }

        [Fact]
        public static void Preference_Default_04_PutLong()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 100000000000L);
            Assert.NotNull(preferences);
        }

        [Fact]
        public static void Preference_Default_05_PutString()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", "test");
            Assert.NotNull(preferences);
        }

        [Fact]
        public static void Preference_Default_06_ParseBool()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", true);
            Assert.NotNull(preferences);
            Assert.Equal(true, preferences.ParseBool("key"));
            preferences.Put("key2", "true");
            Assert.Equal(true, preferences.ParseBool("key2"));
        }

        [Fact]
        public static void Preference_Default_06_ParseBool_WithDefault()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", true);
            Assert.NotNull(preferences);
            Assert.Equal(true, preferences.ParseBool("key"));
            Assert.Equal(false, preferences.ParseBool("key2"));
            Assert.Equal(true, preferences.ParseBool("key3", true));
        }

        [Fact]
        public static void Preference_Default_07_ParseDouble()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 1.1D);
            Assert.NotNull(preferences);
            Assert.Equal(1.1D, preferences.ParseDouble("key"));
            preferences.Put("key2", "2.2");
            Assert.Equal(2.2D, preferences.ParseDouble("key2"));
        }

        [Fact]
        public static void Preference_Default_07_ParseDouble_WithDefault()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 1.1D);
            Assert.NotNull(preferences);
            Assert.Equal(1.1D, preferences.ParseDouble("key"));
            Assert.Equal(0.0D, preferences.ParseDouble("key2"));
            Assert.Equal(1.1D, preferences.ParseDouble("key3", 1.1D));
        }

        [Fact]
        public static void Preference_Default_08_ParseFloat()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 1.1F);
            Assert.NotNull(preferences);
            Assert.Equal(1.1F, preferences.ParseFloat("key"));
            preferences.Put("key2", "2.2");
            Assert.Equal(2.2F, preferences.ParseFloat("key2"));
        }

        [Fact]
        public static void Preference_Default_08_ParseFloat_WithDefault()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 1.1F);
            Assert.NotNull(preferences);
            Assert.Equal(1.1F, preferences.ParseFloat("key"));
            Assert.Equal(0.0F, preferences.ParseFloat("key2"));
            Assert.Equal(1.1F, preferences.ParseFloat("key3", 1.1F));
        }

        [Fact]
        public static void Preference_Default_09_ParseInt()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 1);
            Assert.NotNull(preferences);
            Assert.Equal(1, preferences.ParseInt("key"));
            preferences.Put("key2", "2");
            Assert.Equal(2, preferences.ParseInt("key2"));
        }

        [Fact]
        public static void Preference_Default_09_ParseInt_WithDefault()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 1);
            Assert.NotNull(preferences);
            Assert.Equal(1, preferences.ParseInt("key"));
            Assert.Equal(0, preferences.ParseInt("key2"));
            Assert.Equal(1, preferences.ParseInt("key3", 1));
        }

        [Fact]
        public static void Preference_Default_10_ParseLong()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 100000000000L);
            Assert.NotNull(preferences);
            Assert.Equal(100000000000L, preferences.ParseLong("key"));
            preferences.Put("key2", "200000000001");
            Assert.Equal(200000000001L, preferences.ParseLong("key2"));
        }

        [Fact]
        public static void Preference_Default_10_ParseLong_WithDefault()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", 100000000000L);
            Assert.NotNull(preferences);
            Assert.Equal(100000000000L, preferences.ParseLong("key"));
            Assert.Equal(0L, preferences.ParseLong("key2"));
            Assert.Equal(200000000001L, preferences.ParseLong("key3", 200000000001L));
        }

        [Fact]
        public static void Preference_Default_11_ParseString()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", "test");
            Assert.NotNull(preferences);
            Assert.Equal("test", preferences.ParseString("key"));
        }

        [Fact]
        public static void Preference_Default_11_ParseString_WithDefault()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key", "test");
            Assert.NotNull(preferences);
            Assert.Equal("test", preferences.ParseString("key"));
            Assert.Equal(null, preferences.ParseString("key2"));
            Assert.Equal("test2", preferences.ParseString("key3", "test2"));
        }

        [Fact]
        public static void Preference_Default_12_Commit()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key11", "test");
            Assert.NotNull(preferences);
            preferences.Put("key12", 1);
            preferences.Put("key13", 2.2D);
            preferences.Put("key14", true);
            preferences.Commit();
            Assert.NotNull(preferences);
            var preferences2 = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences2);
            Assert.Equal("test", preferences2.ParseString("key11"));
            Assert.Equal(1, preferences2.ParseInt("key12"));
            Assert.Equal(2.2D, preferences2.ParseDouble("key13"));
            Assert.Equal(true, preferences2.ParseBool("key14"));
        }

        [Fact]
        public static void Preference_Default_13_Clear()
        {
            var preferenceFactory = PreferenceFactory.GetInstance();
            Assert.NotNull(preferenceFactory);
            var preferences = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences);
            preferences.Put("key11", "test");
            Assert.NotNull(preferences);
            preferences.Put("key12", 1);
            preferences.Put("key13", 2.2D);
            preferences.Put("key14", true);
            preferences.Clear();
            preferences.Commit();
            Assert.NotNull(preferences);
            var preferences2 = preferenceFactory.LoadPreferences();
            Assert.NotNull(preferences2);
            Assert.Equal(null, preferences2.ParseString("key11"));
            Assert.Equal(0, preferences2.ParseInt("key12"));
            Assert.Equal(0.0D, preferences2.ParseDouble("key13"));
            Assert.Equal(false, preferences2.ParseBool("key14"));
        }
    }
}
