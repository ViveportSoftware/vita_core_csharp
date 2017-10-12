using System;
using System.Diagnostics;
using System.IO;
using Htc.Vita.Core.Runtime;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void ProcessManager_Default_0_GetProcesses()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var processInfos = ProcessManager.GetProcesses();
            Assert.NotNull(processInfos);
            foreach (var processInfo in processInfos)
            {
                Assert.True(processInfo.Id >= 0);
                Assert.NotNull(processInfo.Name);
                Assert.NotNull(processInfo.User);
                Assert.NotNull(processInfo.Path);
            }
        }

        [Fact]
        public void ProcessManager_Default_1_GetProcessesByFirstActiveUser()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var processInfos = ProcessManager.GetProcessesByFirstActiveUser();
            foreach (var processInfo in processInfos)
            {
                Assert.True(processInfo.Id > 4);
                Assert.False(string.IsNullOrWhiteSpace(processInfo.Name));
                Assert.False(string.IsNullOrWhiteSpace(processInfo.User));
                Assert.NotNull(processInfo.Path);
            }
        }

        [Fact]
        public void ProcessManager_Default_2_KillProcessById()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var fileInfo = new FileInfo("C:\\Windows\\System32\\notepad.exe");
            Assert.True(fileInfo.Exists);
            var process = Process.Start(fileInfo.FullName);
            Assert.NotNull(process);
            Assert.True(process.Id > 4);
            Console.WriteLine("Start " + fileInfo.FullName + " successfully on PID: " + process.Id);
            Assert.True(ProcessManager.KillProcessById(process.Id));
            Console.WriteLine("Kill " + fileInfo.Name + " successfully on PID: " + process.Id);
        }
    }
}
