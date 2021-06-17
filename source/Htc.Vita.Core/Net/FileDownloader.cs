using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    public abstract partial class FileDownloader
    {
        public static Config DownloadConfig { get; set; } = new Config();

        static FileDownloader()
        {
            TypeRegistry.RegisterDefault<FileDownloader, HttpFileDownloader>();
        }

        public static void Register<T>()
                where T : FileDownloader, new()
        {
            TypeRegistry.Register<FileDownloader, T>();
        }

        public static FileDownloader GetInstance()
        {
            return TypeRegistry.GetInstance<FileDownloader>();
        }

        public static FileDownloader GetInstance<T>()
                where T : FileDownloader, new()
        {
            return TypeRegistry.GetInstance<FileDownloader, T>();
        }

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

        protected abstract DownloadOperationResult OnDownloadFile(
                string url,
                FileInfo fileInfo,
                long size,
                Action<long> progressReporter,
                CancellationToken cancellationToken,
                List<string> hostList = null
        );

        public enum DownloadStatus
        {
            Unknown = 0,
            Success = 1,
            Cancelled = 2,
            InternalError = 3,
            OutOfFreeSpaceError = 4,
            ServerResponseError = 5,
            ServerConnectionError = 6,
            TimeoutError = 7,
            DiskIOException = 8,
        }
    }
}
