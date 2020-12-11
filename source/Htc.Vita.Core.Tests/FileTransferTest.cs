using System;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Net;
using Xunit;
using Xunit.Abstractions;
using Convert = Htc.Vita.Core.Util.Convert;

namespace Htc.Vita.Core.Tests
{
    public class FileTransferTest
    {
        private readonly ITestOutputHelper _output;

        public FileTransferTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Default_0_GetInstance()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
        }

        [Fact]
        public void Default_1_GetJobIdList()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            var jobIdList = fileTransfer.GetJobIdList();
            Assert.NotNull(jobIdList);
            var index = 0;
            foreach (var jobId in jobIdList)
            {
                _output.WriteLine($"job[{index}] {jobId}");
                index++;
            }
        }

        [Fact]
        public static void Default_2_RequestNewDownloadJob()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            string jobId;
            const string jobName = "NewDownloadTest-0";
            using (var job = fileTransfer.RequestNewDownloadJob(jobName))
            {
                Assert.NotNull(job);
                jobId = job.GetId();
                Assert.False(string.IsNullOrWhiteSpace(jobId));
                Assert.Equal(jobName, job.GetDisplayName());
                Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
                Assert.Contains(jobId, fileTransfer.GetJobIdList());
                Assert.True(job.Cancel());
            }
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }

        [Fact]
        public void Default_3_GetJob()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            const string jobName = "NewDownloadTest-1";
            using (var job = fileTransfer.RequestNewDownloadJob(jobName))
            {
                Assert.NotNull(job);
                var jobId = job.GetId();
                Assert.False(string.IsNullOrWhiteSpace(jobId));
                Assert.Equal(jobName, job.GetDisplayName());
                Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
                using (var job2 = fileTransfer.GetJob(jobId))
                {
                    Assert.NotNull(job2);
                    var jobId2 = job2.GetId();
                    Assert.False(string.IsNullOrWhiteSpace(jobId2));
                    Assert.Equal(jobId, jobId2);
                }
                Assert.True(job.Cancel());
            }
        }

        [Fact]
        public void Default_4_AddItem()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            var timestamp = Convert.ToTimestampInMilli(DateTime.UtcNow);
            string jobId;
            const string jobName = "NewDownloadTest-2";
            using (var job = fileTransfer.RequestNewDownloadJob(jobName))
            {
                Assert.NotNull(job);
                jobId = job.GetId();
                Assert.False(string.IsNullOrWhiteSpace(jobId));
                Assert.Equal(jobName, job.GetDisplayName());
                Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
                Assert.Contains(jobId, fileTransfer.GetJobIdList());
                Assert.False(job.AddItem(new FileTransfer.FileTransferItem()));
                Assert.True(job.AddItem(new FileTransfer.FileTransferItem
                {
                        LocalPath = new FileInfo(Path.Combine(Path.GetTempPath(), $"VC_redist.x86-{timestamp}.exe")),
                        RemotePath = new Uri("https://download.visualstudio.microsoft.com/download/pr/12319034/ccd261eb0e095411af3b306273231b68/VC_redist.x86.exe")
                }));
                Assert.True(job.Cancel());
            }
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }

        [Fact]
        public void Default_5_SetPriority()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            var timestamp = Convert.ToTimestampInMilli(DateTime.UtcNow);
            string jobId;
            const string jobName = "NewDownloadTest-3";
            using (var job = fileTransfer.RequestNewDownloadJob(jobName))
            {
                Assert.NotNull(job);
                jobId = job.GetId();
                Assert.False(string.IsNullOrWhiteSpace(jobId));
                Assert.Equal(jobName, job.GetDisplayName());
                Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
                Assert.Contains(jobId, fileTransfer.GetJobIdList());
                Assert.True(job.AddItem(new FileTransfer.FileTransferItem
                {
                        LocalPath = new FileInfo(Path.Combine(Path.GetTempPath(), $"VC_redist.x86-{timestamp}.exe")),
                        RemotePath = new Uri("https://download.visualstudio.microsoft.com/download/pr/12319034/ccd261eb0e095411af3b306273231b68/VC_redist.x86.exe")
                }));
                var priority = job.GetPriority();
                Assert.NotEqual(FileTransfer.FileTransferPriority.Unknown, priority);
                if (priority != FileTransfer.FileTransferPriority.Foreground)
                {
                    Assert.True(job.SetPriority(FileTransfer.FileTransferPriority.Foreground));
                }
                priority = job.GetPriority();
                Assert.Equal(FileTransfer.FileTransferPriority.Foreground, priority);
                Assert.True(job.Cancel());
            }
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }

        [Fact]
        public void Default_6_GetState()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            var timestamp = Convert.ToTimestampInMilli(DateTime.UtcNow);
            string jobId;
            const string jobName = "NewDownloadTest-4";
            using (var job = fileTransfer.RequestNewDownloadJob(jobName))
            {
                Assert.NotNull(job);
                jobId = job.GetId();
                Assert.False(string.IsNullOrWhiteSpace(jobId));
                Assert.Equal(jobName, job.GetDisplayName());
                Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
                Assert.Contains(jobId, fileTransfer.GetJobIdList());
                Assert.True(job.AddItem(new FileTransfer.FileTransferItem
                {
                        LocalPath = new FileInfo(Path.Combine(Path.GetTempPath(), $"VC_redist.x86-{timestamp}.exe")),
                        RemotePath = new Uri("https://download.visualstudio.microsoft.com/download/pr/12319034/ccd261eb0e095411af3b306273231b68/VC_redist.x86.exe")
                }));
                var priority = job.GetPriority();
                Assert.NotEqual(FileTransfer.FileTransferPriority.Unknown, priority);
                if (priority != FileTransfer.FileTransferPriority.Foreground)
                {
                    Assert.True(job.SetPriority(FileTransfer.FileTransferPriority.Foreground));
                }
                priority = job.GetPriority();
                Assert.Equal(FileTransfer.FileTransferPriority.Foreground, priority);
                var state = job.GetState();
                Assert.Equal(FileTransfer.FileTransferState.Suspended, state);
                Assert.True(job.Cancel());
            }
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }

        [Fact]
        public void Default_7_Resume()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            var timestamp = Convert.ToTimestampInMilli(DateTime.UtcNow);
            string jobId;
            const string jobName = "NewDownloadTest-5";
            using (var job = fileTransfer.RequestNewDownloadJob(jobName))
            {
                Assert.NotNull(job);
                jobId = job.GetId();
                Assert.False(string.IsNullOrWhiteSpace(jobId));
                Assert.Equal(jobName, job.GetDisplayName());
                Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
                Assert.Contains(jobId, fileTransfer.GetJobIdList());
                Assert.True(job.AddItem(new FileTransfer.FileTransferItem
                {
                        LocalPath = new FileInfo(Path.Combine(Path.GetTempPath(), $"VC_redist.x86-{timestamp}.exe")),
                        RemotePath = new Uri("https://download.visualstudio.microsoft.com/download/pr/12319034/ccd261eb0e095411af3b306273231b68/VC_redist.x86.exe")
                }));
                var priority = job.GetPriority();
                Assert.NotEqual(FileTransfer.FileTransferPriority.Unknown, priority);
                if (priority != FileTransfer.FileTransferPriority.Foreground)
                {
                    Assert.True(job.SetPriority(FileTransfer.FileTransferPriority.Foreground));
                }
                priority = job.GetPriority();
                Assert.Equal(FileTransfer.FileTransferPriority.Foreground, priority);
                var state = job.GetState();
                Assert.Equal(FileTransfer.FileTransferState.Suspended, state);
                Assert.True(job.Resume());
                SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(1));
                state = job.GetState();
                Assert.NotEqual(FileTransfer.FileTransferState.Suspended, state);
                Assert.True(job.Suspend());
                SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(1));
                state = job.GetState();
                Assert.Equal(FileTransfer.FileTransferState.Suspended, state);
                Assert.True(job.Cancel());
            }
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }
    }
}
