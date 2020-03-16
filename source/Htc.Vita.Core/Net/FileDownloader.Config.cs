using System.Reflection;

namespace Htc.Vita.Core.Net
{
    partial class FileDownloader
    {
        public class Config
        {
            public string UserAgent { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;
            public int ConnectionTimeoutInMilli { get; set; } = 21600000;
            public int StreamBufferSize { get; set; } = 786432;
            public int SleepPerBufferDownloadedInMilli { get; set; } = 10;
            public int MaxRetryCountPerHost { get; set; } = 2;
        }
    }
}
