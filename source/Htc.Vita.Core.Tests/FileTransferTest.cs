using Htc.Vita.Core.Net;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class FileTransferTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var fileTransfer = FileTransfer.GetInstance();
            Assert.NotNull(fileTransfer);
        }
    }
}
