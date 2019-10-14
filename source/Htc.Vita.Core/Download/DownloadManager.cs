using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Net;

namespace Htc.Vita.Core.Download
{
    public partial class DownloadManager
    {
        private HttpClient _httpClient;
        private readonly DownloadConfig _downloadConfig;
        private readonly SemaphoreSlim _hashCheckSemaphoreSlim;
        private readonly object _httpClientInstanceLock = new object();

        public DownloadManager(DownloadConfig downloadConfig)
        {
            _downloadConfig = downloadConfig;
            _hashCheckSemaphoreSlim = new SemaphoreSlim(_downloadConfig.HashCheckParallelCount);
        }

        public async Task<DownloadOperationResult> DownloadFileAsync(
                DownloadFileInfo fileInfo, 
                string destFolder, 
                List<string> hostList, 
                Action<long> progressReporter, 
                CancellationToken cancellationToken)
        {
            // WHEN DOWNLOAD IS PAUSED/CANCELED, REMAINING FILES STILL RUN THROUGH HERE, SO TRY TO SKIP IT ASAP.
            if (cancellationToken.IsCancellationRequested)
            {
                return DownloadStatus.Cancel;
            }

            // PREPARE DESTINATION FOLDER
            var destPath = Path.Combine(destFolder, fileInfo.RelPath);
            var retStatus = CreateDirectoryIfNecessary(destPath);
            if (!retStatus.Success)
            {
                Logger.GetInstance(typeof(DownloadManager)).Error($"Error creating directory: {destPath} Exception: {retStatus.Exception}.");
                return retStatus;
            }

            // PREPARE MULTIPLE HOSTS
            if (hostList == null || hostList.Count == 0)
            {
                try
                {
                    hostList = new List<string> {new Uri(fileInfo.Url).GetLeftPart(UriPartial.Authority)};
                }
                catch (Exception exc)
                {
                    Logger.GetInstance(typeof(DownloadManager)).Error($"Url is incorrect. url: {fileInfo.Url} Exception: {exc}");
                    return DownloadStatus.InternalError;
                }
            }
            var multipleHostHelper = new MultipleHostHelper(hostList, _downloadConfig.MaxRetryCountPerHost);
            var trial = 0;
            var fileUrl = string.Empty;
            long progressSize = 0;
            
            while (true)
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return DownloadStatus.Cancel;
                    }

                    if (retStatus?.Status == DownloadStatus.OutOfFreeSpaceError ||
                        (retStatus?.Status == DownloadStatus.ServerConnectionError && 
                         !NetworkInterface.IsInternetAvailable()))
                    {
                        var errorMsg = retStatus?.Status == DownloadStatus.OutOfFreeSpaceError ? "Disk full error." : "No internet error.";
                        Logger.GetInstance(typeof(DownloadManager)).Error($"{errorMsg} FileName: {fileInfo.RelPath} T`rial: {trial}");

                        return retStatus;                        
                    }

                    fileUrl = multipleHostHelper.GetNextUrl(fileInfo.Url);

                    if (string.IsNullOrEmpty(fileUrl))
                    {
                        Logger.GetInstance(typeof(DownloadManager)).Error(
                            $"No more host to try. FileName: {fileInfo.RelPath} Trial: {trial} Last error: {retStatus.Status} Last exception: {retStatus.Exception}");

                        return retStatus.Success ? DownloadStatus.InternalError : retStatus;
                    }

                    progressSize = 0;
                    trial++;

                    Logger.GetInstance(typeof(DownloadManager)).Info($"Trial #{trial} started. FileName: {fileInfo.RelPath} Size: {fileInfo.Size} FileUrl: {fileUrl}");

                    using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, fileUrl))
                    {
                        httpRequestMessage.Headers.Add("X-HTC-Expected-File-Size", fileInfo.Size.ToString());

                        using (var responseMessage = await HttpClientInstance.SendAsync(
                            httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                return DownloadStatus.Cancel;
                            }

                            if (!responseMessage.IsSuccessStatusCode) throw new HttpStatusErrorException(responseMessage.StatusCode,
                                $"Server response error. HttpStatusCode: {responseMessage.StatusCode} Url: {fileUrl}");

                            using (var urlStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                            {
                                using (var fileStream = new FileStream(destPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                {
                                    IProgress<long> progress = new SynchronousProgress<long>(value =>
                                    {
                                        progressSize += value;
                                        progressReporter(value);
                                    });
                                    StreamCopyTo(urlStream, fileStream, progress, cancellationToken,
                                        _downloadConfig.SleepPerBufferDownloadedInMilli,
                                        _downloadConfig.StreamBufferSize);
                                }
                            }
                        }
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return DownloadStatus.Cancel;
                    }

                    retStatus = await IsFileFullyDownloadedAsync(destPath, fileInfo.Size, fileInfo.Hash,
                        fileInfo.HashAlgorithm, cancellationToken).ConfigureAwait(false);

                    if (retStatus.Cancel)
                    {
                        return DownloadStatus.Cancel;
                    }

                    if (!retStatus.Success)
                    {
                        Logger.GetInstance(typeof(DownloadManager)).Error(
                            $"Incorrect hash error DestPath: {destPath} Size: {fileInfo.Size} FileUrl: {fileUrl} Trial: {trial} Exception: {retStatus.Exception}.");
                        continue;
                    }

                    Logger.GetInstance(typeof(DownloadManager)).Info($"File downloaded. FileName: {fileInfo.RelPath} Size: {fileInfo.Size} Trial: {trial}");
                    return DownloadStatus.Success;
                }
                catch (Exception exc)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return DownloadStatus.Cancel;
                    }

                    retStatus = DownloadErrorToOperationResult(exc, cancellationToken);
                    progressReporter(progressSize * -1);

                    Logger.GetInstance(typeof(DownloadManager)).Error(
                        $"Exception. FileName: {fileInfo.RelPath} Size: {fileInfo.Size} FileUrl: {fileUrl} Trial: {trial} Error: {retStatus.Status} Exception: {exc}.");
                }
            }
        }

        private HttpClient HttpClientInstance
        {
            get
            {
                lock (_httpClientInstanceLock)
                {
                    if (_httpClient != null) return _httpClient;

                    var handler = new HttpClientHandler
                    {
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                        Proxy = WebProxyFactory.GetInstance().GetWebProxy()
                    };

                    _httpClient = new HttpClient(handler)
                    {
                        Timeout = TimeSpan.FromMilliseconds(_downloadConfig.HttpClientTimeoutInMilli),
                    };

                    WebUserAgent.Name = _downloadConfig.UserAgent;
                    var userAgent = $"{WebUserAgent.GetModuleName()}/{WebUserAgent.GetModuleVersion()} ({WebUserAgent.GetModuleInstanceName()})";
                    Console.WriteLine(userAgent);
                    _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
                    _httpClient.DefaultRequestHeaders.ExpectContinue = false;

                    return _httpClient;
                }
            }
        }

        private static DownloadOperationResult CreateDirectoryIfNecessary(string fullPath)
        {
            try
            {
                if (string.IsNullOrEmpty(fullPath)) throw new Exception("Parameter *fullPath* is empty.");

                var folder = Path.GetDirectoryName(fullPath);

                if (folder == null) throw new Exception("Path.GetDirectoryName return null.");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                return DownloadStatus.Success;
            }
            catch (Exception exc)
            {
                var errorResult = DownloadErrorToOperationResult(exc, null);
                Logger.GetInstance(typeof(DownloadManager)).Error($"FullPath: {fullPath} Error: {errorResult.Status} Exception: {exc}");
                return errorResult;
            }
        }

        private static DownloadOperationResult DownloadErrorToOperationResult(Exception exc, CancellationToken? cancellationToken)
        {
            if (cancellationToken != null && ((CancellationToken)cancellationToken).IsCancellationRequested)
            {
                return DownloadStatus.Cancel;
            }

            var exception = exc;

            if (exc.InnerException != null)
            {
                exception = exc.InnerException;
            }

            if (exception is TaskCanceledException || exception is OperationCanceledException)
            {
                return new DownloadOperationResult(DownloadStatus.TimeoutError, exc);
            }

            var httpStatusErrorException = exception as HttpStatusErrorException;

            if (httpStatusErrorException != null)
            {
                Logger.GetInstance(typeof(DownloadManager)).Error(
                    $"Found a HttpStatusErrorException: {httpStatusErrorException.HttpStatusCode.ToString()} => return {DownloadStatus.ServerResponseError}");
                return new DownloadOperationResult(DownloadStatus.ServerResponseError, exc);
            }

            var webException = exception as WebException;

            if (webException != null)
            {
                var httpWebResponse = webException.Response as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    Logger.GetInstance(typeof(DownloadManager)).Error(
                        $"Found a HttpStatusCode: {httpWebResponse.StatusCode.ToString()} => return {DownloadStatus.ServerResponseError}");
                    return new DownloadOperationResult(DownloadStatus.ServerResponseError, exc);
                }

                Logger.GetInstance(typeof(DownloadManager)).Error(
                    $"Found a WebExceptionStatus: {webException.Status.ToString()} => return {DownloadStatus.ServerConnectionError}");
                return new DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);
            }

            var socketException = exception as SocketException;

            if (socketException != null)
            {
                Logger.GetInstance(typeof(DownloadManager)).Error(
                    $"Found a SocketErrorCode: {socketException.SocketErrorCode.ToString()} => return {DownloadStatus.ServerConnectionError}");
                return new DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);

            }

            if (exception is HttpRequestException)
            {
                return new DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);
            }

            if (exception is IOException)
            {
                if (IsDiskFullException(exception))
                {
                    return new DownloadOperationResult(DownloadStatus.OutOfFreeSpaceError, exc);
                }
                return new DownloadOperationResult(DownloadStatus.DiskIOException, exc);
            }

            return new DownloadOperationResult(DownloadStatus.InternalError, exc);
        }

        private static bool IsDiskFullException(Exception ex)
        {
            try
            {
                const int HR_ERROR_HANDLE_DISK_FULL = unchecked((int)0x80070027);
                const int HR_ERROR_DISK_FULL = unchecked((int)0x80070070);

                return ex.HResult == HR_ERROR_HANDLE_DISK_FULL
                       || ex.HResult == HR_ERROR_DISK_FULL;
            }
            catch (Exception) { }

            return false;
        }

        private async Task<DownloadOperationResult> IsFileFullyDownloadedAsync(string filePath,
            long expectedSize, string expectedHash, HashAlgorithm hashAlgorithm, CancellationToken cancellationToken)
        {
            await _hashCheckSemaphoreSlim.WaitAsync().ConfigureAwait(false);

            var inputMessage = $"FilePath: {filePath} ExpectedSize: {expectedSize} ExpectedHash: {expectedHash} Algorithm: {hashAlgorithm}";
            try
            {
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    if (fileInfo.Length == expectedSize)
                    {
                        switch (hashAlgorithm)
                        {
                            case HashAlgorithm.MD5:
                                if (expectedHash.Length == 32)
                                {
                                    if (expectedHash == await Md5.GetInstance()
                                            .GenerateInHexAsync(fileInfo, cancellationToken).ConfigureAwait(false))
                                    {
                                        return DownloadStatus.Success;
                                    }
                                }
                                else
                                {
                                    if (expectedHash == await Md5.GetInstance()
                                            .GenerateInBase64Async(fileInfo, cancellationToken)
                                            .ConfigureAwait(false))
                                    {
                                        return DownloadStatus.Success;
                                    }
                                }

                                break;
                            case HashAlgorithm.SHA256:
                                if (expectedHash.Length == 64)
                                {
                                    if (expectedHash == await Sha256.GetInstance()
                                            .GenerateInHexAsync(fileInfo, cancellationToken).ConfigureAwait(false))
                                    {
                                        return DownloadStatus.Success;
                                    }
                                }
                                else
                                {
                                    if (expectedHash == await Sha256.GetInstance()
                                            .GenerateInBase64Async(fileInfo, cancellationToken)
                                            .ConfigureAwait(false))
                                    {
                                        return DownloadStatus.Success;
                                    }
                                }

                                break;
                            default:
                                Logger.GetInstance(typeof(DownloadManager))
                                    .Error($"Unsupported hash algorithm {hashAlgorithm}");
                                break;
                        }

                        if (cancellationToken.IsCancellationRequested)
                        {
                            return DownloadStatus.Cancel;
                        }

                        Logger.GetInstance(typeof(DownloadManager)).Info($"{hashAlgorithm} incorrect: FilePath: {filePath}");
                    }
                    else
                    {
                        Logger.GetInstance(typeof(DownloadManager)).Error($"File size incorrect: {inputMessage} actual size: {fileInfo.Length}");
                    }
                }
            }
            catch (Exception exc)
            {
                var errorResult = new DownloadOperationResult(DownloadStatus.InternalError, exc);
                Logger.GetInstance(typeof(DownloadManager))
                    .Error($"Exception. Error: {errorResult.Status} Exception: {exc} {inputMessage}");
                return errorResult;
            }
            finally
            {
                _hashCheckSemaphoreSlim.Release();
            }

            return new DownloadOperationResult(DownloadStatus.IncorrectFileHashError);
        }

        private static void StreamCopyTo(Stream source, Stream destination, IProgress<long> progress, CancellationToken cancellationToken, int sleepIntervalMs, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            int bytesRead;
            long accumulatedBytesRead = 0;

            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                destination.Write(buffer, 0, bytesRead);
                cancellationToken.ThrowIfCancellationRequested();
                accumulatedBytesRead += bytesRead;

                if (accumulatedBytesRead >= bufferSize)
                {
                    progress?.Report(accumulatedBytesRead);
                    accumulatedBytesRead = 0;

                    if (sleepIntervalMs > 0) SpinWait.SpinUntil(() => false, sleepIntervalMs);
                }
            }

            if (accumulatedBytesRead > 0) progress?.Report(accumulatedBytesRead);
        }

        public enum HashAlgorithm
        {
            Unknown = 0,
            MD5 = 1,
            SHA256 = 2,
        }

        public enum DownloadStatus
        {
            Unknown = 0,
            Success = 1,
            Cancel = 2,
            InternalError = 3,
            IncorrectFileHashError = 4,
            OutOfFreeSpaceError = 5,
            ServerResponseError = 6,
            ServerConnectionError = 7,
            TimeoutError = 8,
            DiskIOException = 9,
        }
    }
}
