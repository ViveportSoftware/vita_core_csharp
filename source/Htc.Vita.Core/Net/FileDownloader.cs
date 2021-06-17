using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class FileDownloader.
    /// </summary>
    public abstract partial class FileDownloader
    {
        /// <summary>
        /// Gets or sets the download configuration.
        /// </summary>
        /// <value>The download configuration.</value>
        public static Config DownloadConfig { get; set; } = new Config();

        static FileDownloader()
        {
            TypeRegistry.RegisterDefault<FileDownloader, HttpFileDownloader>();
        }

        /// <summary>
        /// Registers this instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : FileDownloader, new()
        {
            TypeRegistry.Register<FileDownloader, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>FileDownloader.</returns>
        public static FileDownloader GetInstance()
        {
            return TypeRegistry.GetInstance<FileDownloader>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>FileDownloader.</returns>
        public static FileDownloader GetInstance<T>()
                where T : FileDownloader, new()
        {
            return TypeRegistry.GetInstance<FileDownloader, T>();
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="size">The file size.</param>
        /// <param name="progressReporter">The progress reporter.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="hostList">The host list.</param>
        /// <returns>DownloadOperationResult.</returns>
        public DownloadOperationResult DownloadFile(
                string url,
                FileInfo fileInfo,
                long size,
                Action<long> progressReporter,
                CancellationToken cancellationToken,
                List<string> hostList = null)
        {
            return OnDownloadFile(
                    url,
                    fileInfo,
                    size,
                    progressReporter,
                    cancellationToken,
                    hostList
            );
        }

        /// <summary>
        /// Called when downloading file.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="size">The file size.</param>
        /// <param name="progressReporter">The progress reporter.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <param name="hostList">The host list.</param>
        /// <returns>DownloadOperationResult.</returns>
        protected abstract DownloadOperationResult OnDownloadFile(
                string url,
                FileInfo fileInfo,
                long size,
                Action<long> progressReporter,
                CancellationToken cancellationToken,
                List<string> hostList = null
        );

        /// <summary>
        /// Enum DownloadStatus
        /// </summary>
        public enum DownloadStatus
        {
            /// <summary>
            /// The unknown
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// Success
            /// </summary>
            Success = 1,
            /// <summary>
            /// Cancelled
            /// </summary>
            Cancelled = 2,
            /// <summary>
            /// Internal error
            /// </summary>
            InternalError = 3,
            /// <summary>
            /// Out of free space error
            /// </summary>
            OutOfFreeSpaceError = 4,
            /// <summary>
            /// Server response error
            /// </summary>
            ServerResponseError = 5,
            /// <summary>
            /// Server connection error
            /// </summary>
            ServerConnectionError = 6,
            /// <summary>
            /// Timeout error
            /// </summary>
            TimeoutError = 7,
            /// <summary>
            /// Disk IO exception
            /// </summary>
            DiskIOException = 8,
        }
    }
}
