using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Htc.Vita.Core.Net
{
    partial class FileDownloader
    {
        public Task<DownloadOperationResult> DownloadFileAsync(
                string url,
                FileInfo fileInfo,
                long size,
                Action<long> progressReporter,
                CancellationToken cancellationToken,
                List<string> hostList = null)
        {
            return OnDownloadFileAsync(
                    url,
                    fileInfo,
                    size,
                    progressReporter,
                    cancellationToken,
                    hostList
            );
        }

        protected abstract Task<DownloadOperationResult> OnDownloadFileAsync(
                string url,
                FileInfo fileInfo,
                long size,
                Action<long> progressReporter,
                CancellationToken cancellationToken,
                List<string> hostList = null
        );
    }
}
