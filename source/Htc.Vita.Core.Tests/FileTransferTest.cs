using Htc.Vita.Core.Net;
using Xunit;
using Xunit.Abstractions;

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
    }
}
