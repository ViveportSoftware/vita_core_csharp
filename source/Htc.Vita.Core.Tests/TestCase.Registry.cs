using System;
using Htc.Vita.Core.Runtime;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void Registry_0_GetStringValue32_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Registry.GetStringValue32(Registry.Hive.ClassesRoot, ".bat", null));
            Assert.NotNull(Registry.GetStringValue32(Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Registry.GetStringValue32(Registry.Hive.ClassesRoot, "textfile\\shell\\open", null));
            Assert.Null(Registry.GetStringValue32(Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public void Registry_0_GetStringValue32_UnderHKLM()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Registry.GetStringValue32(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", null));
            Assert.NotNull(Registry.GetStringValue32(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Registry.GetStringValue32(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public void Registry_0_GetStringValue64_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Registry.GetStringValue64(Registry.Hive.ClassesRoot, ".bat", null));
            Assert.NotNull(Registry.GetStringValue64(Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Registry.GetStringValue64(Registry.Hive.ClassesRoot, "textfile\\shell\\open", null));
            Assert.Null(Registry.GetStringValue64(Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public void Registry_0_GetStringValue64_UnderHKLM()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Registry.GetStringValue64(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", null));
            Assert.NotNull(Registry.GetStringValue64(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Registry.GetStringValue64(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID", null));
            Assert.Null(Registry.GetStringValue64(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public void Registry_0_GetStringValue_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Registry.GetStringValue(Registry.Hive.ClassesRoot, ".bat", null));
            Assert.NotNull(Registry.GetStringValue(Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Registry.GetStringValue(Registry.Hive.ClassesRoot, "textfile\\shell\\open", null));
            Assert.Null(Registry.GetStringValue(Registry.Hive.ClassesRoot, "CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public void Registry_0_GetStringValue_UnderHKLM()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            Assert.NotNull(Registry.GetStringValue(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}", null));
            Assert.NotNull(Registry.GetStringValue(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel"));
            Assert.Null(Registry.GetStringValue(Registry.Hive.LocalMachine, "SOFTWARE\\Classes\\CLSID\\{0000002F-0000-0000-C000-000000000046}\\InprocServer32", "ThreadingModel2"));
        }

        [Fact]
        public void Registry_1_SetStringValue_UnderHKCU()
        {
            var valueName = Guid.NewGuid().ToString();
            var valueData = valueName;
            Registry.SetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Registry.GetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact(Skip = "AdministratorPermissionNeeded")]
        public void Registry_1_SetStringValue_UnderHKLM()
        {
            var valueName = Guid.NewGuid().ToString();
            var valueData = valueName;
            Registry.SetStringValue(Registry.Hive.LocalMachine, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Registry.GetStringValue(Registry.Hive.LocalMachine, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public void Registry_2_DeleteValue32_UnderHKCU()
        {
            var valueName = "ffcc27a9-c3bb-463a-8716-3e2e5f2b85a1";
            var valueData = valueName;
            Registry.SetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Registry.GetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
            Assert.True(Registry.DeleteValue32(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public void Registry_2_DeleteValue64_UnderHKCU()
        {
            var valueName = "083c3747-6834-4ee7-b9af-e66eb9925c2a";
            var valueData = valueName;
            Registry.SetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Registry.GetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
            Assert.True(Registry.DeleteValue64(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public void Registry_2_DeleteValue_UnderHKCU()
        {
            var valueName = "2f937f96-aecd-477d-a204-d9ff67bdd8ba";
            var valueData = valueName;
            Registry.SetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Registry.GetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
            Assert.True(Registry.DeleteValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }

        [Fact]
        public void Registry_3_DeleteKey32_UnderHKCU()
        {
            var keyName = "0a9d53eb-36bb-4859-80d0-0816d47580af";
            var valueName = "valueName";
            var valueData = "valueData";
            Registry.SetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName, valueData);
            Assert.Equal(valueData, Registry.GetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName));
            Assert.True(Registry.DeleteKey32(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", keyName));
        }

        [Fact]
        public void Registry_3_DeleteKey64_UnderHKCU()
        {
            var keyName = "20a3c3e3-9feb-4bd8-b451-1e357a6f9d98";
            var valueName = "valueName";
            var valueData = "valueData";
            Registry.SetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName, valueData);
            Assert.Equal(valueData, Registry.GetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName));
            Assert.True(Registry.DeleteKey64(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", keyName));
        }

        [Fact]
        public void Registry_3_DeleteKey_UnderHKCU()
        {
            var keyName = "7a8dd5f9-9347-40b6-8511-3220af3cd429";
            var valueName = "valueName";
            var valueData = "valueData";
            Registry.SetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName, valueData);
            Assert.Equal(valueData, Registry.GetStringValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test\\" + keyName, valueName));
            Assert.True(Registry.DeleteKey(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", keyName));
        }

        [Fact]
        public void Registry_4_GetIntValue32_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            Assert.True(Registry.GetDwordValue32(Registry.Hive.ClassesRoot, "textfile", "EditFlags") != 0);
            Assert.True(Registry.GetDwordValue32(Registry.Hive.ClassesRoot, "textfile", "EditFlags1") == 0);
        }

        [Fact]
        public void Registry_4_GetIntValue64_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            Assert.True(Registry.GetDwordValue64(Registry.Hive.ClassesRoot, "textfile", "EditFlags") != 0);
            Assert.True(Registry.GetDwordValue64(Registry.Hive.ClassesRoot, "textfile", "EditFlags1") == 0);
        }

        [Fact]
        public void Registry_4_GetIntValue_UnderHKCR()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            Assert.True(Registry.GetIntValue(Registry.Hive.ClassesRoot, "textfile", "EditFlags") != 0);
            Assert.True(Registry.GetIntValue(Registry.Hive.ClassesRoot, "textfile", "EditFlags1") == 0);
        }

        [Fact]
        public void Registry_5_SetDwordValue_UnderHKCU()
        {
            var valueData = Guid.NewGuid().GetHashCode();
            var valueName = "" + valueData;
            Registry.SetDwordValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName, valueData);
            Assert.Equal(valueData, Registry.GetIntValue(Registry.Hive.CurrentUser, "SOFTWARE\\HTC\\Test", valueName));
        }
    }
}
