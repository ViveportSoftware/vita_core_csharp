using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    partial class HttpFileDownloader
    {
        protected override async Task<DownloadOperationResult> OnDownloadFileAsync(string url, FileInfo fileInfo, long size, Action<long> progressReporter,
            CancellationToken cancellationToken, List<string> hostList = null)
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
                        return DownloadStatus.Cancelled;
                    }

                    if (retStatus?.Status == DownloadStatus.OutOfFreeSpaceError ||
                        (retStatus?.Status == DownloadStatus.ServerConnectionError &&
                         !NetworkInterface.IsInternetAvailable()))
                    {
                        var errorMsg = retStatus?.Status == DownloadStatus.OutOfFreeSpaceError ? "Disk full error." : "No internet error.";
                        Logger.GetInstance(typeof(FileDownloader)).Error($"{errorMsg} File: {destPath} Trial: {trial}");

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
                        using (var responseMessage = await HttpClientInstance.SendAsync(
                            httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                return DownloadStatus.Cancelled;
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
                        return DownloadStatus.Cancelled;
                    }

                    if (retStatus != null && retStatus.Cancel)
                    {
                        return DownloadStatus.Cancelled;
                    }

                    Logger.GetInstance(typeof(FileDownloader)).Info($"File downloaded. File: {destPath} Size: {size} Trial: {trial}");
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

                    Logger.GetInstance(typeof(FileDownloader)).Error(
                        $"Exception. FileName: {destPath} Size: {size} FileUrl: {fileUrl} Trial: {trial} Error: {retStatus.Status} Exception: {exc}.");
                }
            }
        }
    }
}
