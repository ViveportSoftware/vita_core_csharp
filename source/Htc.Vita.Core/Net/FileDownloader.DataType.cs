using System;
using System.Reflection;

namespace Htc.Vita.Core.Net
{
    public partial class FileDownloader
    {
        /// <summary>
        /// Class Config.
        /// </summary>
        public class Config
        {
            /// <summary>
            /// Gets or sets the user agent.
            /// </summary>
            /// <value>The user agent.</value>
            public string UserAgent { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;
            /// <summary>
            /// Gets or sets the connection timeout in millisecond.
            /// </summary>
            /// <value>The connection timeout in millisecond.</value>
            public int ConnectionTimeoutInMilli { get; set; } = 1000 * 60 * 60 * 6;
            /// <summary>
            /// Gets or sets the stream buffer size.
            /// </summary>
            /// <value>The size of the stream buffer.</value>
            public int StreamBufferSize { get; set; } = 1024 * 768;
            /// <summary>
            /// Gets or sets the sleep duration per buffer downloaded in millisecond.
            /// </summary>
            /// <value>The sleep duration per buffer downloaded in millisecond.</value>
            public int SleepPerBufferDownloadedInMilli { get; set; } = 10;
            /// <summary>
            /// Gets or sets the maximum retry count per host.
            /// </summary>
            /// <value>The maximum retry count per host.</value>
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
