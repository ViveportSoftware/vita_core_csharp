namespace Htc.Vita.Core.Net
{
    public abstract partial class FileTransfer
    {
        public class FileTransferProgress
        {
            public ulong TotalBytes { get; set; }
            public uint TotalFiles { get; set; }
            public ulong TransferredBytes { get; set; }
            public uint TransferredFiles { get; set; }
        }
    }
}
