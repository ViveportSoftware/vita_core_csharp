using System.Diagnostics;
using System.IO;
using Htc.Vita.Core.Runtime;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class ProcessManagerTest
    {
        private readonly ITestOutputHelper _output;

        public ProcessManagerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Default_0_GetProcesses()
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
        public static void Default_1_GetProcessesByFirstActiveUser()
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
        public void Default_2_KillProcessById()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var fileInfo = new FileInfo("C:\\Windows\\System32\\notepad.exe");
            Assert.True(fileInfo.Exists);
            using (var process = Process.Start(fileInfo.FullName))
            {
                Assert.NotNull(process);
                Assert.True(process.Id > 4);
                _output.WriteLine("Start " + fileInfo.FullName + " successfully on PID: " + process.Id);
                Assert.True(ProcessManager.KillProcessById(process.Id));
                _output.WriteLine("Kill " + fileInfo.Name + " successfully on PID: " + process.Id);
            }
        }

        [Fact]
        public void Default_3_IsCurrentUserProcess()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var fileInfo = new FileInfo("C:\\Windows\\System32\\notepad.exe");
            Assert.True(fileInfo.Exists);
            using (var process = Process.Start(fileInfo.FullName))
            {
                Assert.NotNull(process);
                Assert.True(process.Id > 4);
                _output.WriteLine("Start " + fileInfo.FullName + " successfully on PID: " + process.Id);
                Assert.True(ProcessManager.IsCurrentUserProcess(process));
                Assert.True(ProcessManager.KillProcessById(process.Id));
                _output.WriteLine("Kill " + fileInfo.Name + " successfully on PID: " + process.Id);
            }
        }

        [Fact]
        public void Default_4_IsElevatedProcess()
        {
            if (!Platform.IsWindows)
            {
                return;
            }
            var fileInfo = new FileInfo("C:\\Windows\\System32\\notepad.exe");
            Assert.True(fileInfo.Exists);
            using (var process = Process.Start(fileInfo.FullName))
            {
                Assert.NotNull(process);
                Assert.True(process.Id > 4);
                _output.WriteLine("Start " + fileInfo.FullName + " successfully on PID: " + process.Id);
                _output.WriteLine("ProcessManager.IsElevatedProcess(process): " + ProcessManager.IsElevatedProcess(process));
                Assert.True(ProcessManager.KillProcessById(process.Id));
                _output.WriteLine("Kill " + fileInfo.Name + " successfully on PID: " + process.Id);
            }
        }
    }
}
