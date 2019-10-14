using System.Reflection;

namespace Htc.Vita.Core.Download
{
    partial class DownloadManager
    {
        public class DownloadConfig
        {
            public string UserAgent { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;
            public int HttpClientTimeoutInMilli { get; set; } = 21600000;
            public int StreamBufferSize { get; set; } = 786432;
            public int SleepPerBufferDownloadedInMilli { get; set; } = 10;
            public int MaxRetryCountPerHost { get; set; } = 2;
            public int HashCheckParallelCount { get; set; } = 1;
        }
    }
}
