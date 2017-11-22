using System;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class Registry
    {
        [Fact]
        public static void Default_0_GetStringValue32_UnderHKCR()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Util.Registry.GetStringValue32(Util.Registry.Hive.ClassesRoot, ".bat", null));
            Assert.NotNull(Util.Registry.GetStringValue32(Util.Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Util.Registry.GetStringValue32(Util.Registry.Hive.ClassesRoot, "textfile\\shell\\open", null));
            Assert.Null(Util.Registry.GetStringValue32(Util.Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue32_UnderHKLM()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Util.Registry.GetStringValue32(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", null));
            Assert.NotNull(Util.Registry.GetStringValue32(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Util.Registry.GetStringValue32(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue64_UnderHKCR()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Util.Registry.GetStringValue64(Util.Registry.Hive.ClassesRoot, ".bat", null));
            Assert.NotNull(Util.Registry.GetStringValue64(Util.Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Util.Registry.GetStringValue64(Util.Registry.Hive.ClassesRoot, "textfile\\shell\\open", null));
            Assert.Null(Util.Registry.GetStringValue64(Util.Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue64_UnderHKLM()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Util.Registry.GetStringValue64(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", null));
            Assert.NotNull(Util.Registry.GetStringValue64(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Util.Registry.GetStringValue64(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes", null));
            Assert.Null(Util.Registry.GetStringValue64(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue_UnderHKCR()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Util.Registry.GetStringValue(Util.Registry.Hive.ClassesRoot, ".bat", null));
            Assert.NotNull(Util.Registry.GetStringValue(Util.Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Util.Registry.GetStringValue(Util.Registry.Hive.ClassesRoot, "textfile\\shell\\open", null));
            Assert.Null(Util.Registry.GetStringValue(Util.Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_0_GetStringValue_UnderHKLM()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Util.Registry.GetStringValue(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", null));
            Assert.NotNull(Util.Registry.GetStringValue(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Util.Registry.GetStringValue(Util.Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public static void Default_1_SetStringValue_UnderHKCU()
        {
            var valueName = Guid.NewGuid().ToString();
            var valueData = valueName;
            Util.Registry.SetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Util.Registry.GetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact(Skip = "AdministratorPermissionNeeded")]
        public static void Default_1_SetStringValue_UnderHKLM()
        {
            var valueName = Guid.NewGuid().ToString();
            var valueData = valueName;
            Util.Registry.SetStringValue(Util.Registry.Hive.LocalMachine, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Util.Registry.GetStringValue(Util.Registry.Hive.LocalMachine, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Default_2_DeleteValue32_UnderHKCU()
        {
            var valueName = "ffcc27a9-c3bb-463a-8716-3e2e5f2b85a1";
            var valueData = valueName;
            Util.Registry.SetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Util.Registry.GetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
            Assert.True(Util.Registry.DeleteValue32(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Default_2_DeleteValue64_UnderHKCU()
        {
            var valueName = "083c3747-6834-4ee7-b9af-e66eb9925c2a";
            var valueData = valueName;
            Util.Registry.SetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Util.Registry.GetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
            Assert.True(Util.Registry.DeleteValue64(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Default_2_DeleteValue_UnderHKCU()
        {
            var valueName = "2f937f96-aecd-477d-a204-d9ff67bdd8ba";
            var valueData = valueName;
            Util.Registry.SetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Util.Registry.GetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
            Assert.True(Util.Registry.DeleteValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public static void Default_3_DeleteKey32_UnderHKCU()
        {
            var keyName = "0a9d53eb-36bb-4859-80d0-0816d47580af";
            var valueName = "valueName";
            var valueData = "valueData";
            Util.Registry.SetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName, valueData);
            Assert.Equal(valueData, Util.Registry.GetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName));
            Assert.True(Util.Registry.DeleteKey32(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", keyName));
        }

        [Fact]
        public static void Default_3_DeleteKey64_UnderHKCU()
        {
            var keyName = "20a3c3e3-9feb-4bd8-b451-1e357a6f9d98";
            var valueName = "valueName";
            var valueData = "valueData";
            Util.Registry.SetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName, valueData);
            Assert.Equal(valueData, Util.Registry.GetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName));
            Assert.True(Util.Registry.DeleteKey64(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", keyName));
        }

        [Fact]
        public static void Default_3_DeleteKey_UnderHKCU()
        {
            var keyName = "7a8dd5f9-9347-40b6-8511-3220af3cd429";
            var valueName = "valueName";
            var valueData = "valueData";
            Util.Registry.SetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName, valueData);
            Assert.Equal(valueData, Util.Registry.GetStringValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName));
            Assert.True(Util.Registry.DeleteKey(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", keyName));
        }

        [Fact]
        public static void Default_4_GetIntValue32_UnderHKCR()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.True(Util.Registry.GetDwordValue32(Util.Registry.Hive.ClassesRoot, "telnet", "EditFlags") != 0);
            Assert.True(Util.Registry.GetDwordValue32(Util.Registry.Hive.ClassesRoot, "telnet", "EditFlags1") == 0);
        }

        [Fact]
        public static void Default_4_GetIntValue64_UnderHKCR()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.True(Util.Registry.GetDwordValue64(Util.Registry.Hive.ClassesRoot, "telnet", "EditFlags") != 0);
            Assert.True(Util.Registry.GetDwordValue64(Util.Registry.Hive.ClassesRoot, "telnet", "EditFlags1") == 0);
        }

        [Fact]
        public static void Default_4_GetIntValue_UnderHKCR()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            Assert.True(Util.Registry.GetIntValue(Util.Registry.Hive.ClassesRoot, "telnet", "EditFlags") != 0);
            Assert.True(Util.Registry.GetIntValue(Util.Registry.Hive.ClassesRoot, "telnet", "EditFlags1") == 0);
        }

        [Fact]
        public static void Default_5_SetDwordValue_UnderHKCU()
        {
            var valueData = Guid.NewGuid().GetHashCode();
            var valueName = "" + valueData;
            Util.Registry.SetDwordValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Util.Registry.GetIntValue(Util.Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }
    }
}
