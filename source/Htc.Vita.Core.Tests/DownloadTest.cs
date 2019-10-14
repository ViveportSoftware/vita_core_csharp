using System;
using System.IO;
using System.Net;
using System.Threading;
using Htc.Vita.Core.Download;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class DownloadTest
    {
        private readonly ITestOutputHelper _output;

        public DownloadTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Download_Default_0_GetInstance()
        {
            var downloadManager = new DownloadManager(new DownloadManager.DownloadConfig());
            Assert.NotNull(downloadManager);
        }

        [Fact]
        public void Dns_Default_1_DownloadFile()
        {
            var fileInfo = new DownloadManager.DownloadFileInfo
            {
                Url = "https://htc-dev-viveportdesktop-download-test.s3-ap-southeast-1.amazonaws.com/download-test/2097152-8cb79ebe6c9962fe62d732aa905be396fd023540b5cbdd82a5e25a92bd4984b3.bin",
                RelPath = "2097152-8cb79ebe6c9962fe62d732aa905be396fd023540b5cbdd82a5e25a92bd4984b3.bin",
                Hash = "8cb79ebe6c9962fe62d732aa905be396fd023540b5cbdd82a5e25a92bd4984b3",
                HashAlgorithm = DownloadManager.HashAlgorithm.SHA256,
                Size = 2097152,
            };

            var downloadManager = new DownloadManager(new DownloadManager.DownloadConfig());
            DownloadManager.DownloadOperationResult downloadResult = DownloadManager.DownloadStatus.Unknown;
            for (var i = 0; i < 3; i++)
            {
                _output.WriteLine($"Start to download: {fileInfo.Url}");
                long progress = 0;
                downloadResult = downloadManager.DownloadFileAsync(
                        fileInfo, 
                        Path.GetTempPath(), 
                        null,
                        incSize =>
                        {
                            progress += incSize;
                            _output.WriteLine($"Download progress: {progress}");
                        }, 
                        CancellationToken.None).Result;
                if (downloadResult.Success) break;
                SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(1));
            }
            try
            {
                File.Delete(Path.Combine(Path.GetTempPath(), fileInfo.RelPath));
            }
            catch (Exception) {}

            _output.WriteLine($"Download status: {downloadResult?.Status}");

            Assert.NotNull(downloadResult);
            Assert.Equal(DownloadManager.DownloadStatus.Success, downloadResult.Status);
        }
    }
}
