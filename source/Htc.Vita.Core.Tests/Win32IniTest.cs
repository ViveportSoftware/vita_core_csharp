using System.IO;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class Win32IniTest
    {
        [Fact]
        public static void Default_0_LoadFrom()
        {
            var fileInfo = new FileInfo("C:\\Windows\\system.ini");
            Assert.True(fileInfo.Exists);
            var win32Ini = Win32Ini.LoadFrom(fileInfo);
            Assert.NotNull(win32Ini);
        }

        [Fact]
        public static void Default_1_ReadString()
        {
            var fileInfo = new FileInfo("C:\\Windows\\system.ini");
            Assert.True(fileInfo.Exists);
            var win32Ini = Win32Ini.LoadFrom(fileInfo);
            Assert.NotNull(win32Ini);
            var data = win32Ini.ReadString("drivers", "timer");
            Assert.False(string.IsNullOrWhiteSpace(data));
            Logger.GetInstance(typeof(Win32IniTest)).Info("[driver] timer: " + data);
        }
    }
}
