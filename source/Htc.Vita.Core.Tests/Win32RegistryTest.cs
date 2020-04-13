using System;
using System.Security;
using Htc.Vita.Core.Runtime;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class Win32RegistryTest
    {
        [Fact]
        public static void Default_0_OpenBaseKey()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.ClassesRoot;
            var registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.NotNull(baseKey);
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.NotNull(baseKey);
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.NotNull(baseKey);
            }

            registryHive = Win32Registry.Hive.CurrentUser;
            registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.NotNull(baseKey);
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.NotNull(baseKey);
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.NotNull(baseKey);
            }

            registryHive = Win32Registry.Hive.LocalMachine;
            registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.NotNull(baseKey);
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.NotNull(baseKey);
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.NotNull(baseKey);
            }
        }

        [Fact]
        public static void Default_1_OpenSubKey_WithReadPermission()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.ClassesRoot;
            var registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey(".bat", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey(".bat2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey(".bat", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey(".bat2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey(".bat", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey(".bat2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }

            registryHive = Win32Registry.Hive.CurrentUser;
            registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Clients", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Clients2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Clients", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Clients2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Clients", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Clients2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }

            registryHive = Win32Registry.Hive.LocalMachine;
            registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }
        }

        [Fact(Skip = "Only run as non-admin")]
        public static void Default_1_OpenSubKey_WithWritePermission_Hkcr()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.ClassesRoot;
            var registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.Throws<SecurityException>(() =>
                {
                        using (baseKey.OpenSubKey(".bat", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                        {
                        }
                });
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.Throws<SecurityException>(() =>
                {
                        using (baseKey.OpenSubKey(".bat", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                        {
                        }
                });
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.Throws<SecurityException>(() =>
                {
                        using (baseKey.OpenSubKey(".bat", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                        {
                        }
                });
            }
        }

        [Fact(Skip = "Only run as non-admin")]
        public static void Default_1_OpenSubKey_WithWritePermission_Hkcu()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.CurrentUser;
            var registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http2", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http2", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Classes\\http2", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.Null(subKey);
                }
            }
        }

        [Fact(Skip = "Only run as non-admin")]
        public static void Default_1_OpenSubKey_WithWritePermission_Hklm()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.LocalMachine;
            var registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.Throws<SecurityException>(() =>
                {
                        using (baseKey.OpenSubKey("SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                        {
                        }
                });
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.Throws<SecurityException>(() =>
                {
                        using (baseKey.OpenSubKey("SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                        {
                        }
                });
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                Assert.Throws<SecurityException>(() =>
                {
                        using (baseKey.OpenSubKey("SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                        {
                        }
                });
            }
        }

        [Fact]
        public static void Default_2_GetValue_Hkcr()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.ClassesRoot;
            var registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey(".bat", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    var value = subKey.GetValue(null);
                    Assert.NotNull(value);
                    var valueKind = subKey.GetValueKind(null);
                    Assert.Equal(Win32Registry.ValueKind.String, valueKind);
                    var realValue = (string) value;
                    Assert.NotEmpty(realValue);
                }

                using (var subKey = baseKey.OpenSubKey("ftp", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    var valueName = "EditFlags";
                    var value = subKey.GetValue(valueName);
                    Assert.NotNull(value);
                    var valueKind = subKey.GetValueKind(valueName);
                    Assert.Equal(Win32Registry.ValueKind.DWord, valueKind);
                    var realValue = (int)value;
                    Assert.NotEqual(0, realValue);
                }

                using (var subKey = baseKey.OpenSubKey("txtfile\\shell\\open\\command", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    var value = subKey.GetValue(null);
                    Assert.NotNull(value);
                    var valueKind = subKey.GetValueKind(null);
                    Assert.Equal(Win32Registry.ValueKind.ExpandString, valueKind);
                    var realValue = (string)value;
                    Assert.NotEmpty(realValue);
                    Assert.False(realValue.StartsWith("%"));
                }
            }
        }

        [Fact]
        public static void Default_2_GetValue_Hklm()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.LocalMachine;
            var registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Network", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    var valueName = "FilterClasses";
                    var value = subKey.GetValue(valueName);
                    Assert.NotNull(value);
                    var valueKind = subKey.GetValueKind(valueName);
                    Assert.Equal(Win32Registry.ValueKind.MultiString, valueKind);
                    var realValue = (string[])value;
                    Assert.NotEmpty(realValue);
                    Assert.True(realValue.Length > 0);
                }
                using (var subKey = baseKey.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ProductOptions", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    var valueName = "ProductPolicy";
                    var value = subKey.GetValue(valueName);
                    Assert.NotNull(value);
                    var valueKind = subKey.GetValueKind(valueName);
                    Assert.Equal(Win32Registry.ValueKind.Binary, valueKind);
                    var realValue = (byte[])value;
                    Assert.NotEmpty(realValue);
                    Assert.True(realValue.Length > 0);
                }
            }
            registryView = Win32Registry.View.Registry32;
            string realValueA;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    var valueName = "CommonFilesDir";
                    var value = subKey.GetValue(valueName);
                    Assert.NotNull(value);
                    var valueKind = subKey.GetValueKind(valueName);
                    Assert.Equal(Win32Registry.ValueKind.String, valueKind);
                    realValueA = (string)value;
                    Assert.NotEmpty(realValueA);
                    Assert.True(realValueA.Length > 0);
                }
            }
            registryView = Win32Registry.View.Registry64;
            string realValueB;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    var valueName = "CommonFilesDir";
                    var value = subKey.GetValue(valueName);
                    Assert.NotNull(value);
                    var valueKind = subKey.GetValueKind(valueName);
                    Assert.Equal(Win32Registry.ValueKind.String, valueKind);
                    realValueB = (string)value;
                    Assert.NotEmpty(realValueB);
                    Assert.True(realValueB.Length > 0);
                }
            }
            Assert.NotEqual(realValueA, realValueB);
        }

        [Fact]
        public static void Default_3_CreateSubKey()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.ClassesRoot;
            var registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.CreateSubKey(".bat", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    var value = subKey.GetValue(null);
                    Assert.NotNull(value);
                    var valueKind = subKey.GetValueKind(null);
                    Assert.Equal(Win32Registry.ValueKind.String, valueKind);
                    var realValue = (string)value;
                    Assert.NotEmpty(realValue);
                }
            }

            registryHive = Win32Registry.Hive.CurrentUser;
            registryView = Win32Registry.View.Default;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                var testKeyName = Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}.ReadWriteSubTree", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                }
            }
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                var testKeyName = Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}.ReadSubTree", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
            }
        }

        [Fact]
        public static void Default_4_SetValue()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.CurrentUser;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive))
            {
                var testKeyName = Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
                using (var subKey = baseKey.CreateSubKey("SOFTWARE\\HTC\\Test", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                    subKey.SetValue("Test-" + testKeyName + "-Binary", new byte[] { 0x00, 0x01, 0x02 }, Win32Registry.ValueKind.Binary);
                    subKey.SetValue("Test-" + testKeyName + "-DWord", -1, Win32Registry.ValueKind.DWord);
                    subKey.SetValue("Test-" + testKeyName + "-ExpandString", "%SystemRoot%", Win32Registry.ValueKind.ExpandString);
                    subKey.SetValue("Test-" + testKeyName + "-MultiString", new[] { "Test0", "Test1" }, Win32Registry.ValueKind.MultiString);
                    subKey.SetValue("Test-" + testKeyName + "-QWord", 345L, Win32Registry.ValueKind.QWord);
                    subKey.SetValue("Test-" + testKeyName + "-String", "string", Win32Registry.ValueKind.String);
                }
                using (var subKey = baseKey.CreateSubKey("SOFTWARE\\HTC\\Test", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                    Assert.Equal(Win32Registry.ValueKind.Binary, subKey.GetValueKind("Test-" + testKeyName + "-Binary"));
                    Assert.True(((byte[])subKey.GetValue("Test-" + testKeyName + "-Binary")).Length == 3);
                    Assert.Equal(Win32Registry.ValueKind.DWord, subKey.GetValueKind("Test-" + testKeyName + "-DWord"));
                    Assert.Equal(-1, (int)subKey.GetValue("Test-" + testKeyName + "-DWord"));
                    Assert.Equal(Win32Registry.ValueKind.ExpandString, subKey.GetValueKind("Test-" + testKeyName + "-ExpandString"));
                    Assert.False(((string)subKey.GetValue("Test-" + testKeyName + "-ExpandString")).StartsWith("%"));
                    Assert.Equal(Win32Registry.ValueKind.MultiString, subKey.GetValueKind("Test-" + testKeyName + "-MultiString"));
                    Assert.True(((string[])subKey.GetValue("Test-" + testKeyName + "-MultiString")).Length == 2);
                    Assert.Equal(Win32Registry.ValueKind.QWord, subKey.GetValueKind("Test-" + testKeyName + "-QWord"));
                    Assert.Equal(345L, (long)subKey.GetValue("Test-" + testKeyName + "-QWord"));
                    Assert.Equal(Win32Registry.ValueKind.String, subKey.GetValueKind("Test-" + testKeyName + "-String"));
                    Assert.Equal("string", (string)subKey.GetValue("Test-" + testKeyName + "-String"));
                }
            }
        }

        [Fact]
        public static void Default_5_DeleteValue()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.CurrentUser;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive))
            {
                var testKeyName = Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
                using (var subKey = baseKey.CreateSubKey("SOFTWARE\\HTC\\Test", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                    subKey.SetValue("Test-" + testKeyName + "-DWord", -1, Win32Registry.ValueKind.DWord);
                }
                using (var subKey = baseKey.CreateSubKey("SOFTWARE\\HTC\\Test", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                    Assert.Equal(Win32Registry.ValueKind.DWord, subKey.GetValueKind("Test-" + testKeyName + "-DWord"));
                    Assert.Equal(-1, (int)subKey.GetValue("Test-" + testKeyName + "-DWord"));
                    subKey.DeleteValue("Test-" + testKeyName + "-DWord", false);
                }
            }
        }

        [Fact]
        public static void Default_6_GetSubKeyNames()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.CurrentUser;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive))
            {
                var testKeyName = Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}-TestKey\\TestSubKey1", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}-TestKey\\TestSubKey2", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}-TestKey", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                    Assert.Equal(2, subKey.SubKeyCount);
                    var subKeyNames = subKey.GetSubKeyNames();
                    Assert.Equal(2, subKeyNames.Length);
                    foreach (var subKeyName in subKeyNames)
                    {
                        Assert.True("TestSubKey1".Equals(subKeyName) || "TestSubKey2".Equals(subKeyName));
                    }
                }
            }
        }

        [Fact]
        public static void Default_7_DeleteSubKeyTree()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.CurrentUser;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive))
            {
                var testKeyName = Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}-TestKey\\TestSubKey1", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}-TestKey\\TestSubKey1\\TestSubKey2", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}-TestKey", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                    subKey.DeleteSubKeyTree("TestSubKey1", false);
                    Assert.Throws<ArgumentException>(() =>
                    {
                            subKey.DeleteSubKeyTree("TestSubKey2", true);
                    });
                }
            }
        }
    }
}
