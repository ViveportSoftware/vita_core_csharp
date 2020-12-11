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
    }
}
