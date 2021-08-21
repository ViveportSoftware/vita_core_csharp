using System;
using System.IO;
using System.Threading;
using Htc.Vita.Core.IO;
using Htc.Vita.Core.Net;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Core.Tests
{
    public class FileDownloaderTest
    {
        private readonly ITestOutputHelper _output;

        private static readonly string TestFileUrl =
            "https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x86.exe";
        private static readonly long TestFileSize = 8993744;
        private static readonly string TestFileHash = "99dce3c841cc6028560830f7866c9ce2928c98cf3256892ef8e6cf755147b0d8";
        private static readonly string TestFileDestPath = Path.Combine(Path.GetTempPath(), "VC_redist.x86.exe");
        private static readonly FileVerifierV2.ChecksumType TestFileChecksumType = FileVerifierV2.ChecksumType.Sha256;

        public FileDownloaderTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void FileDownloader_Default_0_GetInstance()
        {
            var fileDownloader = FileDownloader.GetInstance();
            Assert.NotNull(fileDownloader);
        }

        [Fact]
        public void FileDownloader_Default_1_DownloadFile()
        {
            var fileDownloader = FileDownloader.GetInstance();
            FileDownloader.DownloadOperationResult downloadResult = FileDownloader.DownloadStatus.Unknown;
            for (var i = 0; i < 3; i++)
            {
                _output.WriteLine($"Start to download: {TestFileUrl}");
                long progress = 0;
                downloadResult = fileDownloader.DownloadFile(
                    TestFileUrl,
                    new FileInfo(TestFileDestPath),
                    TestFileSize,
                    incSize =>
                    {
                        progress += incSize;
                        _output.WriteLine($"Download progress: {progress}");
                    },
                    CancellationToken.None);

                if (downloadResult.Success)
                {
                    if (!FileVerifierV2.GetInstance().VerifyIntegrityAsync(new FileInfo(TestFileDestPath), TestFileHash, TestFileChecksumType).Result)
                    {
                        downloadResult = FileDownloader.DownloadStatus.InternalError;
                        continue;
                    }
                    break;
                }
                SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(1));
            }
            try
            {
                File.Delete(TestFileDestPath);
            }
            catch (Exception) { /* Skip */ }

            _output.WriteLine($"Download status: {downloadResult?.Status}");

            Assert.NotNull(downloadResult);
            Assert.Equal(FileDownloader.DownloadStatus.Success, downloadResult.Status);
        }

        [Fact]
        public void FileDownloader_Default_2_DownloadFileAsync()
        {
            var fileDownloader = FileDownloader.GetInstance();
            FileDownloader.DownloadOperationResult downloadResult = FileDownloader.DownloadStatus.Unknown;
            for (var i = 0; i < 3; i++)
            {
                _output.WriteLine($"Start to download: {TestFileUrl}");
                long progress = 0;
                downloadResult = fileDownloader.DownloadFileAsync(
                    TestFileUrl,
                    new FileInfo(TestFileDestPath),
                    TestFileSize,
                    incSize =>
                    {
                        progress += incSize;
                        _output.WriteLine($"Download progress: {progress}");
                    },
                    CancellationToken.None).Result;

                if (downloadResult.Success)
                {
                    if (!FileVerifierV2.GetInstance().VerifyIntegrityAsync(new FileInfo(TestFileDestPath), TestFileHash, TestFileChecksumType).Result)
                    {
                        downloadResult = FileDownloader.DownloadStatus.InternalError;
                        continue;
                    }
                    break;
                }
                SpinWait.SpinUntil(() => false, TimeSpan.FromSeconds(1));
            }
            try
            {
                File.Delete(TestFileDestPath);
            }
            catch (Exception) { }

            _output.WriteLine($"Download status: {downloadResult?.Status}");

            Assert.NotNull(downloadResult);
            Assert.Equal(FileDownloader.DownloadStatus.Success, downloadResult.Status);
        }
    }
}
