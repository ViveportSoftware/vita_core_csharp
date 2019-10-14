namespace Htc.Vita.Core.Download
{
    partial class DownloadManager
    {
        public class DownloadFileInfo
        {
            public string Url { get; set; }
            public string RelPath { get; set; }
            public long Size { get; set; }
            public string Hash { get; set; }
            public HashAlgorithm HashAlgorithm { get; set; }
        }
    }
}
