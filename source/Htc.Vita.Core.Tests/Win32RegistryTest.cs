using System;
using System.Diagnostics;
using System.Security;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class Win32RegistryTest
    {
        [Fact]
        public static void Default_0_GetStringValue32_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            Assert.NotNull(Win32Registry.GetStringValue32(Win32Registry.Hive.ClassesRoot, ".bat", null));
            Assert.NotNull(Win32Registry.GetStringValue32(Win32Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Win32Registry.GetStringValue32(Win32Registry.Hive.ClassesRoot, "textfile\\shell\\open", null));
            Assert.Null(Win32Registry.GetStringValue32(Win32Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue32_UnderHKLM()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            Assert.NotNull(Win32Registry.GetStringValue32(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", null));
            Assert.NotNull(Win32Registry.GetStringValue32(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Win32Registry.GetStringValue32(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue64_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            Assert.NotNull(Win32Registry.GetStringValue64(Win32Registry.Hive.ClassesRoot, ".bat", null));
            Assert.NotNull(Win32Registry.GetStringValue64(Win32Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Win32Registry.GetStringValue64(Win32Registry.Hive.ClassesRoot, "textfile\\shell\\open", null));
            Assert.Null(Win32Registry.GetStringValue64(Win32Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue64_UnderHKLM()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            Assert.NotNull(Win32Registry.GetStringValue64(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", null));
            Assert.NotNull(Win32Registry.GetStringValue64(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Win32Registry.GetStringValue64(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes", null));
            Assert.Null(Win32Registry.GetStringValue64(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            Assert.NotNull(Win32Registry.GetStringValue(Win32Registry.Hive.ClassesRoot, ".bat", null));
            Assert.NotNull(Win32Registry.GetStringValue(Win32Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Win32Registry.GetStringValue(Win32Registry.Hive.ClassesRoot, "textfile\\shell\\open", null));
            Assert.Null(Win32Registry.GetStringValue(Win32Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue_UnderHKLM()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            Assert.NotNull(Win32Registry.GetStringValue(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", null));
            Assert.NotNull(Win32Registry.GetStringValue(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Win32Registry.GetStringValue(Win32Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_1_SetStringValue_UnderHKCU()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var valueName = Guid.NewGuid().ToString();
            var valueData = valueName;
            Win32Registry.SetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Win32Registry.GetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Default_1_SetStringValue_UnderHKLM()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            if (!ProcessManager.IsElevatedProcess(Process.GetCurrentProcess()))
            {
                Logger.GetInstance(typeof(Win32RegistryTest)).Warn("This API should be invoked by elevated user process");
                return;
            }

            var valueName = Guid.NewGuid().ToString();
            var valueData = valueName;
            Win32Registry.SetStringValue(Win32Registry.Hive.LocalMachine, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Win32Registry.GetStringValue(Win32Registry.Hive.LocalMachine, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Default_2_DeleteValue32_UnderHKCU()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var valueName = "ffcc27a9-c3bb-463a-8716-3e2e5f2b85a1";
            var valueData = valueName;
            Win32Registry.SetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Win32Registry.GetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
            Assert.True(Win32Registry.DeleteValue32(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Default_2_DeleteValue64_UnderHKCU()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var valueName = "083c3747-6834-4ee7-b9af-e66eb9925c2a";
            var valueData = valueName;
            Win32Registry.SetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Win32Registry.GetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
            Assert.True(Win32Registry.DeleteValue64(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Default_2_DeleteValue_UnderHKCU()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var valueName = "2f937f96-aecd-477d-a204-d9ff67bdd8ba";
            var valueData = valueName;
            Win32Registry.SetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Win32Registry.GetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
            Assert.True(Win32Registry.DeleteValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Default_3_DeleteKey32_UnderHKCU()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var keyName = "0a9d53eb-36bb-4859-80d0-0816d47580af";
            var valueName = "valueName";
            var valueData = "valueData";
            Win32Registry.SetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName, valueData);
            Assert.Equal(valueData, Win32Registry.GetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName));
            Assert.True(Win32Registry.DeleteKey32(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", keyName));
        }

        [Fact]
        public static void Default_3_DeleteKey64_UnderHKCU()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var keyName = "20a3c3e3-9feb-4bd8-b451-1e357a6f9d98";
            var valueName = "valueName";
            var valueData = "valueData";
            Win32Registry.SetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName, valueData);
            Assert.Equal(valueData, Win32Registry.GetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName));
            Assert.True(Win32Registry.DeleteKey64(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", keyName));
        }

        [Fact]
        public static void Default_3_DeleteKey_UnderHKCU()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var keyName = "7a8dd5f9-9347-40b6-8511-3220af3cd429";
            var valueName = "valueName";
            var valueData = "valueData";
            Win32Registry.SetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName, valueData);
            Assert.Equal(valueData, Win32Registry.GetStringValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName));
            Assert.True(Win32Registry.DeleteKey(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", keyName));
        }

        [Fact]
        public static void Default_4_GetIntValue32_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            Assert.True(Win32Registry.GetDwordValue32(Win32Registry.Hive.ClassesRoot, "telnet", "EditFlags") != 0);
            Assert.True(Win32Registry.GetDwordValue32(Win32Registry.Hive.ClassesRoot, "telnet", "EditFlags1") == 0);
        }

        [Fact]
        public static void Default_4_GetIntValue64_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            Assert.True(Win32Registry.GetDwordValue64(Win32Registry.Hive.ClassesRoot, "telnet", "EditFlags") != 0);
            Assert.True(Win32Registry.GetDwordValue64(Win32Registry.Hive.ClassesRoot, "telnet", "EditFlags1") == 0);
        }

        [Fact]
        public static void Default_4_GetIntValue_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            Assert.True(Win32Registry.GetIntValue(Win32Registry.Hive.ClassesRoot, "telnet", "EditFlags") != 0);
            Assert.True(Win32Registry.GetIntValue(Win32Registry.Hive.ClassesRoot, "telnet", "EditFlags1") == 0);
        }

        [Fact]
        public static void Default_5_SetDwordValue_UnderHKCU()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var valueData = Guid.NewGuid().GetHashCode();
            var valueName = "" + valueData;
            Win32Registry.SetDwordValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Win32Registry.GetIntValue(Win32Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Key_0_OpenBaseKey()
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
        public static void Key_1_OpenSubKey_WithReadPermission()
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
                using (var subKey = baseKey.OpenSubKey("Software\\Microsoft", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Microsoft2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry32;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Microsoft", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Microsoft2", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.Null(subKey);
                }
            }
            registryView = Win32Registry.View.Registry64;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, registryView))
            {
                using (var subKey = baseKey.OpenSubKey("Software\\Microsoft", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                }
                using (var subKey = baseKey.OpenSubKey("Software\\Microsoft2", Win32Registry.KeyPermissionCheck.ReadSubTree))
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

        [Fact]
        public static void Key_1_OpenSubKey_WithWritePermission_Hkcr()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            if (ProcessManager.IsElevatedProcess(Process.GetCurrentProcess()))
            {
                Logger.GetInstance(typeof(Win32RegistryTest)).Warn("This API should be invoked by non-elevated user process");
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

        [Fact]
        public static void Key_1_OpenSubKey_WithWritePermission_Hkcu()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            if (ProcessManager.IsElevatedProcess(Process.GetCurrentProcess()))
            {
                Logger.GetInstance(typeof(Win32RegistryTest)).Warn("This API should be invoked by non-elevated user process");
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

        [Fact]
        public static void Key_1_OpenSubKey_WithWritePermission_Hklm()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            if (ProcessManager.IsElevatedProcess(Process.GetCurrentProcess()))
            {
                Logger.GetInstance(typeof(Win32RegistryTest)).Warn("This API should be invoked by non-elevated user process");
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
        public static void Key_2_GetValue_Hkcr()
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
        public static void Key_2_GetValue_Hklm()
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
        public static void Key_3_CreateSubKey()
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
        public static void Key_4_SetValue()
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
        public static void Key_5_DeleteValue()
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
        public static void Key_6_GetSubKeyNames()
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
        public static void Key_7_DeleteSubKeyTree()
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
                    /*
                    Assert.Throws<ArgumentException>(() =>
                    {
                            subKey.DeleteSubKeyTree("TestSubKey2", true);
                    });
                    */
                }
            }
        }

        [Fact]
        public static void Key_8_GetValueNames()
        {
            if (!Platform.IsWindows)
            {
                return;
            }

            var registryHive = Win32Registry.Hive.CurrentUser;
            using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive))
            {
                var testKeyName = Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}-TestKey", Win32Registry.KeyPermissionCheck.ReadWriteSubTree))
                {
                    Assert.NotNull(subKey);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName0", $"Test-{testKeyName}-TestValueData0", Win32Registry.ValueKind.String);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName1", $"Test-{testKeyName}-TestValueData1", Win32Registry.ValueKind.String);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName2", $"Test-{testKeyName}-TestValueData2", Win32Registry.ValueKind.String);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName3", $"Test-{testKeyName}-TestValueData3", Win32Registry.ValueKind.String);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName4", $"Test-{testKeyName}-TestValueData4", Win32Registry.ValueKind.String);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName5", $"Test-{testKeyName}-TestValueData5", Win32Registry.ValueKind.String);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName6", $"Test-{testKeyName}-TestValueData6", Win32Registry.ValueKind.String);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName7", $"Test-{testKeyName}-TestValueData7", Win32Registry.ValueKind.String);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName8", $"Test-{testKeyName}-TestValueData8", Win32Registry.ValueKind.String);
                    subKey.SetValue($"Test-{testKeyName}-TestValueName9", $"Test-{testKeyName}-TestValueData9", Win32Registry.ValueKind.String);
                }
                using (var subKey = baseKey.CreateSubKey($"SOFTWARE\\HTC\\Test\\Test-{testKeyName}-TestKey", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    Assert.NotNull(subKey);
                    var valueNames = subKey.GetValueNames();
                    Assert.True(valueNames.Length == 10);
                    foreach (var valueName in valueNames)
                    {
                        Assert.StartsWith($"Test-{testKeyName}-TestValueName", valueName);
                    }
                }
            }
        }
    }
}
