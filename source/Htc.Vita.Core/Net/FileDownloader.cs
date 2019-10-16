using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public abstract partial class FileDownloader
    {
        private static Dictionary<string, FileDownloader> Instances { get; } = new Dictionary<string, FileDownloader>();
        private static Type _defaultType = typeof(HttpFileDownloader);
        private static readonly object InstanceLock = new object();

        public static Config DownloadConfig { get; set; } = new Config();

        public static void Register<T>() where T : FileDownloader
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(FileDownloader)).Info("Registered default file downloader type to " + _defaultType);
        }

        public static FileDownloader GetInstance()
        {
            FileDownloader instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileDownloader)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(FileDownloader)).Info("Initializing " + typeof(FileDownloader).FullName + "...");
                instance = new HttpFileDownloader();
            }
            return instance;
        }

        public DownloadOperationResult DownloadFile(string url, FileInfo fileInfo, long size, Action<long> progressReporter,
            CancellationToken cancellationToken, List<string> hostList = null)
        {
            return OnDownloadFile(url, fileInfo, size, progressReporter, cancellationToken, hostList);
        }

        private static FileDownloader DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get file downloader instance");
            }

            var key = type.FullName + "_";
            FileDownloader instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(FileDownloader)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (FileDownloader)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(FileDownloader)).Info("Initializing " + typeof(HttpFileDownloader).FullName + "...");
                instance = new HttpFileDownloader();
            }

            lock (InstanceLock)
            {
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
            }

            return instance;
        }
        
        protected abstract DownloadOperationResult OnDownloadFile(string url, FileInfo fileInfo, long size, Action<long> progressReporter,
            CancellationToken cancellationToken, List<string> hostList = null);

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
