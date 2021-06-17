using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Htc.Vita.Core.Net
{
    public partial class FileDownloader
    {
        /// <summary>
        /// Downloads the file asynchronously.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="size">The file size.</param>
        /// <param name="progressReporter">The progress reporter.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="hostList">The host list.</param>
        /// <returns>Task&lt;DownloadOperationResult&gt;.</returns>
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

        /// <summary>
        /// Called when downloading file asynchronously.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="size">The file size.</param>
        /// <param name="progressReporter">The progress reporter.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="hostList">The host list.</param>
        /// <returns>Task&lt;DownloadOperationResult&gt;.</returns>
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
