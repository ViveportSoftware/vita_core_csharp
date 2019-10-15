using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    partial class HttpFileDownloader : FileDownloader
    {
        private HttpClient _httpClient;
        private readonly object _httpClientInstanceLock = new object();

        protected override async Task<DownloadOperationResult> OnDownloadFileAsync(string url, string destPath, long size, Action<long> progressReporter,
            CancellationToken cancellationToken, List<string> hostList = null)
        {
            // WHEN DOWNLOAD IS PAUSED/CANCELED, REMAINING FILES STILL RUN THROUGH HERE, SO TRY TO SKIP IT ASAP.
            if (cancellationToken.IsCancellationRequested)
            {
                return DownloadStatus.Cancel;
            }

            // PREPARE DESTINATION FOLDER
            try
            {
                var folder = Path.GetDirectoryName(destPath);
                if (folder != null && !Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            catch (Exception exc)
            {
                Logger.GetInstance(typeof(FileDownloader)).Error($"Unable to create directory: {destPath} ex: {exc}");
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
                    Logger.GetInstance(typeof(FileDownloader)).Error($"Url is incorrect. url: {url} Exception: {exc}");
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
                        return DownloadStatus.Cancel;
                    }

                    if (retStatus?.Status == DownloadStatus.OutOfFreeSpaceError ||
                        (retStatus?.Status == DownloadStatus.ServerConnectionError &&
                         !NetworkInterface.IsInternetAvailable()))
                    {
                        var errorMsg = retStatus?.Status == DownloadStatus.OutOfFreeSpaceError ? "Disk full error." : "No internet error.";
                        Logger.GetInstance(typeof(FileDownloader)).Error($"{errorMsg} File: {destPath} T`rial: {trial}");

                        return retStatus;
                    }

                    fileUrl = multipleHostHelper.GetNextUrl(url);

                    if (string.IsNullOrEmpty(fileUrl))
                    {
                        Logger.GetInstance(typeof(FileDownloader)).Error(
                            $"No more host to try. File: {destPath} Trial: {trial} Last error: {retStatus.Status} Last exception: {retStatus.Exception}");

                        return retStatus.Success ? DownloadStatus.InternalError : retStatus;
                    }

                    progressSize = 0;
                    trial++;

                    Logger.GetInstance(typeof(FileDownloader)).Info($"Trial #{trial} started. File: {destPath} Size: {size} FileUrl: {fileUrl}");

                    using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, fileUrl))
                    {
                        httpRequestMessage.Headers.Add("X-HTC-Expected-File-Size", size.ToString());

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
                                        DownloadConfig.SleepPerBufferDownloadedInMilli, DownloadConfig.StreamBufferSize);
                                }
                            }
                        }
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return DownloadStatus.Cancel;
                    }

                    if (retStatus.Cancel)
                    {
                        return DownloadStatus.Cancel;
                    }

                    Logger.GetInstance(typeof(FileDownloader)).Info($"File downloaded. File: {destPath} Size: {size} Trial: {trial}");
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

                    Logger.GetInstance(typeof(FileDownloader)).Error(
                        $"Exception. FileName: {destPath} Size: {size} FileUrl: {fileUrl} Trial: {trial} Error: {retStatus.Status} Exception: {exc}.");
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
                return new FileDownloader.DownloadOperationResult(DownloadStatus.TimeoutError, exc);
            }

            var httpStatusErrorException = exception as HttpStatusErrorException;

            if (httpStatusErrorException != null)
            {
                Logger.GetInstance(typeof(FileDownloader)).Error(
                    $"Found a HttpStatusErrorException: {httpStatusErrorException.HttpStatusCode.ToString()} => return {DownloadStatus.ServerResponseError}");
                return new FileDownloader.DownloadOperationResult(DownloadStatus.ServerResponseError, exc);
            }

            var webException = exception as WebException;

            if (webException != null)
            {
                var httpWebResponse = webException.Response as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    Logger.GetInstance(typeof(FileDownloader)).Error(
                        $"Found a HttpStatusCode: {httpWebResponse.StatusCode.ToString()} => return {DownloadStatus.ServerResponseError}");
                    return new FileDownloader.DownloadOperationResult(DownloadStatus.ServerResponseError, exc);
                }

                Logger.GetInstance(typeof(FileDownloader)).Error(
                    $"Found a WebExceptionStatus: {webException.Status.ToString()} => return {DownloadStatus.ServerConnectionError}");
                return new FileDownloader.DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);
            }

            var socketException = exception as SocketException;

            if (socketException != null)
            {
                Logger.GetInstance(typeof(FileDownloader)).Error(
                    $"Found a SocketErrorCode: {socketException.SocketErrorCode.ToString()} => return {DownloadStatus.ServerConnectionError}");
                return new FileDownloader.DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);

            }

            if (exception is HttpRequestException)
            {
                return new FileDownloader.DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);
            }

            if (exception is IOException)
            {
                if (IsDiskFullException(exception))
                {
                    return new FileDownloader.DownloadOperationResult(DownloadStatus.OutOfFreeSpaceError, exc);
                }
                return new FileDownloader.DownloadOperationResult(DownloadStatus.DiskIOException, exc);
            }

            return new FileDownloader.DownloadOperationResult(DownloadStatus.InternalError, exc);
        }
    }
}
