namespace Htc.Vita.Core.IO
{
    public static partial class FileSystemManager
    {
        public class DiskSpaceInfo
        {
            public string Path { get; set; } = "";
            public long FreeOfBytes { get; set; } = -1;
            public long TotalOfBytes { get; set; } = -1;
            public long TotalFreeOfBytes { get; set; } = -1;
        }
    }
}
