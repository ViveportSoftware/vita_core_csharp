using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    internal partial class HttpFileDownloader : FileDownloader
    {
        private HttpClient _httpClient;
        private readonly object _httpClientInstanceLock = new object();

        internal static DownloadOperationResult DownloadErrorToOperationResult(
                Exception exc,
                CancellationToken? cancellationToken)
        {
            if (cancellationToken != null && ((CancellationToken)cancellationToken).IsCancellationRequested)
            {
                return DownloadStatus.Cancelled;
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
                Logger.GetInstance(typeof(HttpFileDownloader)).Error($"Found a HttpStatusErrorException: {httpStatusErrorException.HttpStatusCode} => return {DownloadStatus.ServerResponseError}");
                return new DownloadOperationResult(DownloadStatus.ServerResponseError, exc);
            }

            var webException = exception as WebException;

            if (webException != null)
            {
                var httpWebResponse = webException.Response as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    Logger.GetInstance(typeof(HttpFileDownloader)).Error($"Found a HttpStatusCode: {httpWebResponse.StatusCode} => return {DownloadStatus.ServerResponseError}");
                    return new DownloadOperationResult(DownloadStatus.ServerResponseError, exc);
                }

                Logger.GetInstance(typeof(HttpFileDownloader)).Error($"Found a WebExceptionStatus: {webException.Status} => return {DownloadStatus.ServerConnectionError}");
                return new DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);
            }

            var socketException = exception as SocketException;

            if (socketException != null)
            {
                Logger.GetInstance(typeof(HttpFileDownloader)).Error($"Found a SocketErrorCode: {socketException.SocketErrorCode} => return {DownloadStatus.ServerConnectionError}");
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

        internal static bool IsDiskFullException(Exception ex)
        {
            try
            {
                return ex.HResult == unchecked((int)Windows.HResult.EWin32HandleDiskFull)
                       || ex.HResult == unchecked((int)Windows.HResult.EWin32DiskFull);
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        /// <inheritdoc />
        protected override DownloadOperationResult OnDownloadFile(
                string url,
                FileInfo fileInfo,
                long size,
                Action<long> progressReporter,
                CancellationToken cancellationToken,
                List<string> hostList = null)
        {
            var destPath = fileInfo.FullName;

            // WHEN DOWNLOAD IS PAUSED/CANCELED, REMAINING FILES STILL RUN THROUGH HERE, SO TRY TO SKIP IT ASAP.
            if (cancellationToken.IsCancellationRequested)
            {
                return DownloadStatus.Cancelled;
            }

            // PREPARE DESTINATION FOLDER
            try
            {
                var folder = fileInfo.DirectoryName;
                if (folder != null && !Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            catch (Exception exc)
            {
                Logger.GetInstance(typeof(HttpFileDownloader)).Error($"Unable to create directory: {destPath} ex: {exc}");
                return DownloadErrorToOperationResult(exc, cancellationToken);
            }

            // PREPARE MULTIPLE HOSTS
            if (hostList == null || hostList.Count == 0)
            {
                try
                {
                    hostList = new List<string> { new Uri(url).GetLeftPart(UriPartial.Authority) };
                }
                catch (Exception exc)
                {
                    Logger.GetInstance(typeof(HttpFileDownloader)).Error($"Url is incorrect. url: {url} Exception: {exc}");
                    return DownloadStatus.InternalError;
                }
            }

            var multipleHostHelper = new MultipleHostHelper(hostList, DownloadConfig.MaxRetryCountPerHost);
            var trial = 0;
            var fileUrl = string.Empty;
            long progressSize = 0;
            DownloadOperationResult retStatus = DownloadStatus.Unknown;

            while (true)
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return DownloadStatus.Cancelled;
                    }

                    if (retStatus?.Status == DownloadStatus.OutOfFreeSpaceError ||
                        (retStatus?.Status == DownloadStatus.ServerConnectionError &&
                         !NetworkInterface.IsInternetAvailable()))
                    {
                        var errorMsg = retStatus?.Status == DownloadStatus.OutOfFreeSpaceError ? "Disk full error." : "No internet error.";
                        Logger.GetInstance(typeof(HttpFileDownloader)).Error($"{errorMsg} File: {destPath} Trial: {trial}");

                        return retStatus;
                    }

                    fileUrl = multipleHostHelper.GetNextUrl(url);

                    if (string.IsNullOrEmpty(fileUrl))
                    {
                        Logger.GetInstance(typeof(HttpFileDownloader)).Error($"No more host to try. File: {destPath} Trial: {trial} Last error: {retStatus.Status} Last exception: {retStatus.Exception}");

                        return retStatus.Success ? DownloadStatus.InternalError : retStatus;
                    }

                    progressSize = 0;
                    trial++;

                    Logger.GetInstance(typeof(HttpFileDownloader)).Info($"Trial #{trial} started. File: {destPath} Size: {size} FileUrl: {fileUrl}");

                    using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, fileUrl))
                    {
                        using (var responseMessage = HttpClientInstance.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).Result)
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                return DownloadStatus.Cancelled;
                            }

                            if (!responseMessage.IsSuccessStatusCode) 
                            {
                                throw new HttpStatusErrorException(
                                        responseMessage.StatusCode,
                                        $"Server response error. HttpStatusCode: {responseMessage.StatusCode} Url: {fileUrl}"
                                );
                            }

                            using (var urlStream = responseMessage.Content.ReadAsStreamAsync().Result)
                            {
                                using (var fileStream = new FileStream(destPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                {
                                    IProgress<long> progress = new SynchronousProgress<long>(value =>
                                    {
                                            progressSize += value;
                                            progressReporter(value);
                                    });
                                    StreamCopyTo(
                                            urlStream,
                                            fileStream,
                                            progress,
                                            cancellationToken,
                                            DownloadConfig.SleepPerBufferDownloadedInMilli,
                                            DownloadConfig.StreamBufferSize
                                    );
                                }
                            }
                        }
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return DownloadStatus.Cancelled;
                    }

                    if (retStatus != null && retStatus.Cancel)
                    {
                        return DownloadStatus.Cancelled;
                    }

                    Logger.GetInstance(typeof(HttpFileDownloader)).Info($"File downloaded. File: {destPath} Size: {size} Trial: {trial}");
                    return DownloadStatus.Success;
                }
                catch (Exception exc)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return DownloadStatus.Cancelled;
                    }

                    retStatus = DownloadErrorToOperationResult(exc, cancellationToken);
                    progressReporter(progressSize * -1);

                    Logger.GetInstance(typeof(HttpFileDownloader)).Error($"Exception. FileName: {destPath} Size: {size} FileUrl: {fileUrl} Trial: {trial} Error: {retStatus.Status} Exception: {exc}.");
                }
            }
        }

        internal static void StreamCopyTo(
                Stream source,
                Stream destination,
                IProgress<long> progress,
                CancellationToken cancellationToken,
                int sleepIntervalMs,
                int bufferSize)
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
                        Timeout = TimeSpan.FromMilliseconds(DownloadConfig.ConnectionTimeoutInMilli),
                    };

                    WebUserAgent.Name = DownloadConfig.UserAgent;
                    var userAgent = $"{WebUserAgent.GetModuleName()}/{WebUserAgent.GetModuleVersion()} ({WebUserAgent.GetModuleInstanceName()})";
                    Console.WriteLine(userAgent);
                    _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
                    _httpClient.DefaultRequestHeaders.ExpectContinue = false;

                    return _httpClient;
                }
            }
        }
    }
}
