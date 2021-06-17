using System;
using System.Reflection;

namespace Htc.Vita.Core.Net
{
    public partial class FileDownloader
    {
        public class Config
        {
            public string UserAgent { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;
            public int ConnectionTimeoutInMilli { get; set; } = 21600000;
            public int StreamBufferSize { get; set; } = 786432;
            public int SleepPerBufferDownloadedInMilli { get; set; } = 10;
            public int MaxRetryCountPerHost { get; set; } = 2;
        }

        public class SynchronousProgress<T> : IProgress<T>
        {
            private readonly Action<T> _callback;

            public SynchronousProgress(Action<T> callback)
            {
                _callback = callback;
            }

            void IProgress<T>.Report(T data) => _callback(data);
        }
    }
}
