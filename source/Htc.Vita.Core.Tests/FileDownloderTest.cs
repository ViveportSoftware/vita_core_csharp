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
            "https://download.visualstudio.microsoft.com/download/pr/12319034/ccd261eb0e095411af3b306273231b68/VC_redist.x86.exe";
        private static readonly long TestFileSize = 14611496;
        private static readonly string TestFileHash = "eb5f74215e4308d8f2b1d458e78f33050a779b9be19baaa2174de1be9be1b830";
        private static readonly string TestFileDestPath = Path.Combine(Path.GetTempPath(), "VC_redist.x86.exe");
        private static readonly FileVerifier.ChecksumType TestFileChecksumType = FileVerifier.ChecksumType.Sha256;

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
                    if (!FileVerifier.VerifyAsync(new FileInfo(TestFileDestPath), TestFileSize, TestFileHash, TestFileChecksumType,
                        CancellationToken.None).Result)
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
            catch (Exception) {}

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
                    if (!FileVerifier.VerifyAsync(new FileInfo(TestFileDestPath), TestFileSize, TestFileHash, TestFileChecksumType,
                        CancellationToken.None).Result)
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
