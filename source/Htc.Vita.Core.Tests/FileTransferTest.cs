using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Log;
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
            const string jobName = "NewDownloadTest-0";
            var job = fileTransfer.RequestNewDownloadJob(jobName);
            Assert.NotNull(job);
            var jobId = job.GetId();
            Assert.False(string.IsNullOrWhiteSpace(jobId));
            Assert.Equal(jobName, job.GetDisplayName());
            Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
            Assert.Contains(jobId, fileTransfer.GetJobIdList());
            Assert.True(job.Cancel());
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }

        [Fact]
        public void Default_3_GetJob()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            const string jobName = "NewDownloadTest-1";
            var job = fileTransfer.RequestNewDownloadJob(jobName);
            Assert.NotNull(job);
            var jobId = job.GetId();
            Assert.False(string.IsNullOrWhiteSpace(jobId));
            Assert.Equal(jobName, job.GetDisplayName());
            Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
            var job2 = fileTransfer.GetJob(jobId);
            Assert.NotNull(job2);
            var jobId2 = job2.GetId();
            Assert.False(string.IsNullOrWhiteSpace(jobId2));
            Assert.Equal(jobId, jobId2);
            Assert.True(job.Cancel());
        }

        [Fact]
        public void Default_4_AddItem()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            var timestamp = Convert.ToTimestampInMilli(DateTime.UtcNow);
            const string jobName = "NewDownloadTest-2";
            var job = fileTransfer.RequestNewDownloadJob(jobName);
            Assert.NotNull(job);
            var jobId = job.GetId();
            Assert.False(string.IsNullOrWhiteSpace(jobId));
            Assert.Equal(jobName, job.GetDisplayName());
            Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
            Assert.Contains(jobId, fileTransfer.GetJobIdList());
            Assert.False(job.AddItem(new FileTransfer.FileTransferItem()));
            Assert.True(job.AddItem(new FileTransfer.FileTransferItem
            {
                    LocalPath = new FileInfo(Path.Combine(Path.GetTempPath(), $"vcredist.x64-{timestamp}.exe")),
                    RemotePath = new Uri("https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x64.exe")
            }));
            Assert.True(job.Cancel());
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }

        [Fact]
        public void Default_5_SetPriority()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            var timestamp = Convert.ToTimestampInMilli(DateTime.UtcNow);
            const string jobName = "NewDownloadTest-3";
            var job = fileTransfer.RequestNewDownloadJob(jobName);
            Assert.NotNull(job);
            var jobId = job.GetId();
            Assert.False(string.IsNullOrWhiteSpace(jobId));
            Assert.Equal(jobName, job.GetDisplayName());
            Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
            Assert.Contains(jobId, fileTransfer.GetJobIdList());
            Assert.True(job.AddItem(new FileTransfer.FileTransferItem
            {
                    LocalPath = new FileInfo(Path.Combine(Path.GetTempPath(), $"vcredist.x64-{timestamp}.exe")),
                    RemotePath = new Uri("https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x64.exe")
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
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }

        [Fact]
        public void Default_6_GetState()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            var timestamp = Convert.ToTimestampInMilli(DateTime.UtcNow);
            const string jobName = "NewDownloadTest-4";
            var job = fileTransfer.RequestNewDownloadJob(jobName);
            Assert.NotNull(job);
            var jobId = job.GetId();
            Assert.False(string.IsNullOrWhiteSpace(jobId));
            Assert.Equal(jobName, job.GetDisplayName());
            Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
            Assert.Contains(jobId, fileTransfer.GetJobIdList());
            Assert.True(job.AddItem(new FileTransfer.FileTransferItem
            {
                    LocalPath = new FileInfo(Path.Combine(Path.GetTempPath(), $"vcredist.x64-{timestamp}.exe")),
                    RemotePath = new Uri("https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x64.exe")
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
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }

        [Fact]
        public void Default_7_Resume()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
            var timestamp = Convert.ToTimestampInMilli(DateTime.UtcNow);
            const string jobName = "NewDownloadTest-5";
            var job = fileTransfer.RequestNewDownloadJob(jobName);
            Assert.NotNull(job);
            var jobId = job.GetId();
            Assert.False(string.IsNullOrWhiteSpace(jobId));
            Assert.Equal(jobName, job.GetDisplayName());
            Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
            Assert.Contains(jobId, fileTransfer.GetJobIdList());
            Assert.True(job.AddItem(new FileTransfer.FileTransferItem
            {
                    LocalPath = new FileInfo(Path.Combine(Path.GetTempPath(), $"vcredist.x64-{timestamp}.exe")),
                    RemotePath = new Uri("https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x64.exe")
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
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
        }

        [Fact]
        public void Default_8_Complete()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);

            fileTransfer.OnJobError -= OnFileTransferOnJobError;
            fileTransfer.OnJobError += OnFileTransferOnJobError;
            fileTransfer.OnJobTransferred -= OnFileTransferJobTransferred;
            fileTransfer.OnJobTransferred += OnFileTransferJobTransferred;

            var localPathSet = new HashSet<FileInfo>();
            var timestamp = Convert.ToTimestampInMilli(DateTime.UtcNow);
            const string jobName = "NewDownloadTest-6";
            var job = fileTransfer.RequestNewDownloadJob(jobName);
            Assert.NotNull(job);
            var jobId = job.GetId();
            Assert.False(string.IsNullOrWhiteSpace(jobId));
            Assert.Equal(jobName, job.GetDisplayName());
            Assert.True(fileTransfer.ListenJob(job));
            Assert.Equal(FileTransfer.FileTransferType.Download, job.GetTransferType());
            Assert.Contains(jobId, fileTransfer.GetJobIdList());
            Assert.True(job.AddItem(new FileTransfer.FileTransferItem
            {
                    LocalPath = new FileInfo(Path.Combine(Path.GetTempPath(), $"vcredist.x64-{timestamp}.exe")),
                    RemotePath = new Uri("https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x64.exe")
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
            while (true)
            {
                state = job.GetState();
                Logger.GetInstance(typeof(FileTransferTest)).Info($"transfer state: {state}");
                Assert.NotEqual(FileTransfer.FileTransferState.Error, state);
                if (state == FileTransfer.FileTransferState.Transferred)
                {
                    var itemList = job.GetItemList();
                    Assert.NotEmpty(itemList);
                    foreach (var item in itemList)
                    {
                        var localPath = item.LocalPath;
                        Assert.NotNull(localPath);
                        localPathSet.Add(localPath);
                    }
                    break;
                }
                if (state == FileTransfer.FileTransferState.Transferring)
                {
                    var progress = job.GetProgress();
                    Assert.NotNull(progress);
                    Logger.GetInstance(typeof(FileTransferTest)).Info($"transferred in bytes: {progress.TransferredBytes}/{progress.TotalBytes}");
                }
                SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(3));
            }
            Assert.True(job.Complete());
            state = job.GetState();
            Assert.Equal(FileTransfer.FileTransferState.Acknowledged, state);
            Assert.DoesNotContain(jobId, fileTransfer.GetJobIdList());
            foreach (var localPath in localPathSet)
            {
                Logger.GetInstance(typeof(FileTransferTest)).Info($"local path: {localPath}");
                Assert.True(localPath.Exists);
            }
        }

        private static void OnFileTransferJobTransferred(string jobId)
        {
            Logger.GetInstance(typeof(FileTransferTest)).Info($"job[{jobId}] is transferred");
        }

        private static void OnFileTransferOnJobError(string jobId)
        {
            var errorJob = FileTransfer.GetInstance().GetJob(jobId);
            if (errorJob == null)
            {
                Logger.GetInstance(typeof(FileTransferTest)).Warn($"Can not find error for job[{jobId}]");
                return;
            }

            Logger.GetInstance(typeof(FileTransferTest)).Error($"job[{jobId}] error: {errorJob.GetError()?.ErrorDescription}");
        }
    }
}
